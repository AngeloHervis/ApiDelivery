using Crosscutting.Constantes;
using Crosscutting.Dto.Usuario;
using Crosscutting.Extensions;
using Crosscutting.Interfaces;
using Crosscutting.Interfaces.Log;
using Crosscutting.Validadores;
using Domain._Base.Interfaces;
using Domain.Usuarios.Interfaces;

namespace Domain.Usuarios.Services;

public class AlterarSenhaUsuarioService : IAlterarSenhaUsuarioService
{
    private readonly IUsuarioLogadoService _usuarioLogadoService;
    private readonly ILoggerServicosDeDominio _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public AlterarSenhaUsuarioService(IUsuarioLogadoService usuarioLogadoService, ILoggerServicosDeDominio logger, IUsuarioRepository usuarioRepository)
    {
        _usuarioLogadoService = usuarioLogadoService;
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<AlterarSenhaUsuarioRespostaDto> AlterarSenhaAsync(AlterarSenhaUsuarioRequisicaoDto dtoUsuarioRequisicao)
    {
        var dadosUsuario = _usuarioLogadoService.ObterInformacoesDoUsuario();

        if (SenhasNaoBatem(dtoUsuarioRequisicao))
        {
            _logger.LogInformacao<AlterarSenhaUsuarioService>(MensagensLogInfo.AlterarSenhaUsuarioSenhasNaoBatem);
            return AlterarSenhaUsuarioRespostaDto.ComFalhaSenhasDiferentes();
        }

        if (ValidadorSenha.SenhaInvalida(dtoUsuarioRequisicao.NovaSenha))
        {
            _logger.LogInformacao<AlterarSenhaUsuarioService>(
                string.Format(MensagensLogInfo.AlterarSenhaUsuarioFormatoSenhaInvalido, dadosUsuario.Id.OcultarCpfCnpj()));
            return AlterarSenhaUsuarioRespostaDto.ComFalhaNovaSenhaInvalida();
        }
        
        var usuario = await _usuarioRepository.ObterPorEmailAsync(dadosUsuario.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dtoUsuarioRequisicao.SenhaAtual, usuario.HashSenha))
        {
            _logger.LogAviso<AlterarSenhaUsuarioService>(string.Format(
                MensagensLogAviso.AlterarSenhaUsuarioSenhaAntigaIncorreta, dadosUsuario.Id.OcultarCpfCnpj()));
            return AlterarSenhaUsuarioRespostaDto.ComFalhaSenhaAntigaErrada();
        }
        
        usuario.HashSenha = BCrypt.Net.BCrypt.HashPassword(dtoUsuarioRequisicao.NovaSenha);
        await _usuarioRepository.AtualizarAsync(usuario);

        _logger.LogInformacao<AlterarSenhaUsuarioService>(string.Format(MensagensLogInfo.AlterarSenhaUsuarioBemSucedido, dadosUsuario.Id.OcultarCpfCnpj()));
        return AlterarSenhaUsuarioRespostaDto.ComSucesso();
    }
    
    private static bool SenhasNaoBatem(AlterarSenhaUsuarioRequisicaoDto dto)
    {
        return dto.NovaSenha != dto.ConfirmacaoNovaSenha;
    }
}
