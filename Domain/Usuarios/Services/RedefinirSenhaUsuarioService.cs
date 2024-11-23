using Crosscutting.Constantes;
using Crosscutting.Dto.Email;
using Crosscutting.Dto.Usuario;
using Crosscutting.Extensions;
using Crosscutting.Interfaces.Log;
using Crosscutting.Validadores;
using Domain._Base.Interfaces;
using Domain.Emails.Interfaces;
using Domain.Usuarios.Interfaces;

namespace Domain.Usuarios.Services;

public class RedefinirSenhaUsuarioService(
    ILoggerServicosDeDominio logger,
    IUsuarioRepository usuarioRepository,
    IEmailService emailService)
    : IRedefinirSenhaUsuarioService
{
    public async Task<RedefinirSenhaRespostaDto> RedefinirAsync(RedefinirSenhaRequisicaoDto dto, CancellationToken cancellationToken)
    {
        if (!RequisicaoValida(dto))
        {
            logger.LogErroSemException<RedefinirSenhaUsuarioService>(MensagensErro.EntradaInvalida);
            return new RedefinirSenhaRespostaDto(CodigosErro.EntradaInvalida, MensagensErro.EntradaInvalida, false);
        }
        
        var usuario = await usuarioRepository.ObterPorEmailAsync(dto.Email);
        if (usuario == null)
        {
            logger.LogErroSemException<RedefinirSenhaUsuarioService>(MensagensErro.EmailNaoEncontrado);
            return new RedefinirSenhaRespostaDto(CodigosErro.UsuarioNaoEncontrado, MensagensErro.EmailNaoEncontrado, false);
        }
        
        if (ValidadorSenha.SenhaInvalida(dto.NovaSenha))
        {
            logger.LogInformacao<RedefinirSenhaUsuarioService>(MensagensErro.SenhaInvalida);
            return new RedefinirSenhaRespostaDto(CodigosErro.SenhaInvalida, MensagensErro.SenhaInvalida, false);
        }
        
        if (dto.NovaSenha != dto.ConfirmaNovaSenha)
        {
            logger.LogInformacao<RedefinirSenhaUsuarioService>(MensagensErro.SenhasNaoBatem);
            return new RedefinirSenhaRespostaDto(CodigosErro.SenhasNaoBatem, MensagensErro.SenhasNaoBatem, false);
        }
        
        usuario.HashSenha = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);
        await usuarioRepository.AtualizarAsync(usuario);

        logger.LogInformacao<RedefinirSenhaUsuarioService>(MensagensLogInfo.SenhaRedefinidaComSucesso, dto.Email);
        
        var filtro = new FiltroEmailRedefinirSenhaDto();
        await emailService.EnviarEmailRedefinirSenhaAsync(filtro, usuario.Email);
        
        return new RedefinirSenhaRespostaDto(string.Empty, string.Empty, true);
    }

    private static bool RequisicaoValida(RedefinirSenhaRequisicaoDto dto)
        => dto.Email.IsNotNull() &&
           dto.NovaSenha.IsNotNull() &&
           dto.ConfirmaNovaSenha.IsNotNull();
}
