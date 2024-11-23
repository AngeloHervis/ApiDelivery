using System.Text.RegularExpressions;
using Crosscutting.Constantes;
using Crosscutting.Dto.Autenticacao;
using Crosscutting.Exception;
using Crosscutting.Interfaces.Log;
using Domain._Base.Interfaces;
using Domain.Autenticacao.Interfaces;

namespace Domain.Autenticacao.Services;

public class LoginUsuarioService : ILoginUsuarioService
{
    private readonly ITokenService _tokenService;
    private readonly ILoggerServicosDeDominio _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginUsuarioService(
        ITokenService tokenService, 
        ILoggerServicosDeDominio logger,
        IUsuarioRepository usuarioRepository)
    {
        _tokenService = tokenService;
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<LoginRespostaDto> LoginAsync(LoginDto loginDto)
    {
        if (EmailInvalido(loginDto.Email))
        {
            _logger.LogErroSemException<LoginUsuarioService>(MensagensLogErro.EmailInvalido);
            return LoginRespostaDto.ComFalhaEmailDoUsuarioInvalido();
        }

        if (!SenhaValida(loginDto.Senha))
        {
            _logger.LogErroSemException<LoginUsuarioService>(MensagensLogErro.SenhaInvalida);
            return LoginRespostaDto.ComFalhaNoLoginSenhaInvalida();
        }
        
        var usuario = await _usuarioRepository.ObterPorEmailAsync(loginDto.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.HashSenha))
        {
            _logger.LogErro<LoginUsuarioService>(new AutenticacaoException(), MensagensLogErro.CredenciaisInvalidas, loginDto.Email);
            return LoginRespostaDto.ComFalhaNaAutenticacao();
        }

        var token = _tokenService.GerarTokenParaUsuario(loginDto.Email);
        var refreshToken = _tokenService.GerarRefreshToken();
        
        _tokenService.SaveRefreshToken(loginDto.Email, refreshToken);

        _logger.LogInformacao<LoginUsuarioService>(MensagensLogInfo.LoginBemSucedido, loginDto.Email);
        return LoginRespostaDto.ComSucesso(token);
    }

    public async Task<LoginRespostaDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var refreshTokens = _tokenService.ObterRefreshToken(refreshTokenDto.Email);
        if (!refreshTokens.Contains(refreshTokenDto.RefreshToken))
        {
            _logger.LogErro<LoginUsuarioService>(new AutenticacaoException(), MensagensErro.FalhaRefreshToken, refreshTokenDto.Email);
            return LoginRespostaDto.ComFalhaNoRefreshToken();
        }

        var novoToken = _tokenService.GerarTokenParaUsuario(refreshTokenDto.Email);
        var novoRefreshToken = _tokenService.GerarRefreshToken();

        _tokenService.DeleteRefreshToken(refreshTokenDto.Email, refreshTokenDto.RefreshToken);
        _tokenService.SaveRefreshToken(refreshTokenDto.Email, novoRefreshToken);

        _logger.LogInformacao<LoginUsuarioService>(MensagensLogInfo.GerarTokenParaUsuarioGerouComSucesso, refreshTokenDto.Email);
        return LoginRespostaDto.ComSucesso(novoToken);
    }

    private static bool EmailInvalido(string email)
        => !Regex.IsMatch(email, PadroesRegex.Email);

    private static bool SenhaValida(string senha)
        => Regex.IsMatch(senha, PadroesRegex.Senha);
}
