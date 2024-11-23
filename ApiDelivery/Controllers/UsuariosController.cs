using ApiDelivery.Respostas;
using Crosscutting.Constantes;
using Crosscutting.Dto.Usuario;
using Domain.Usuarios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary>
/// API de usuários
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : BaseController
{
    /// <summary></summary>

    /// <summary>
    /// Alteração de senha do usuário
    /// </summary>
    /// <remarks>
    /// A nova senha deve satisfazer cinco condições:
    ///
    /// • Mínimo de 13 caracteres
    ///
    /// • Pelo menos 1 número
    ///
    /// • Pelo menos 1 letra ASCII maiúscula
    ///
    /// • Pelo menos 1 letra ASCII minúscula
    ///
    /// • Pelo menos 1 caractere especial
    /// </remarks>
    /// <response code="200">Senha alterada com sucesso</response>
    /// <response code="401">Senha antiga errada</response>
    /// <response code="422">Erro de validação</response>
    /// <response code="503">Conexão com IFS falhou</response>
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpPost("alteracao-senha")]
    public async Task<IActionResult> AlteraSenha(
        [FromBody] AlterarSenhaUsuarioRequisicaoDto dtoUsuarioRequisicao,
        [FromServices] IAlterarSenhaUsuarioService service)
    {
        var resposta = await service.AlterarSenhaAsync(dtoUsuarioRequisicao);

        if (resposta.Codigo == null)
        {
            return Ok();
        }

        var respostaErro = new RespostaErro
        {
            Code = resposta.Codigo,
            Message = resposta.Mensagem
        };

        return resposta.Codigo switch
        {
            CodigosErro.EntradaInvalida or CodigosErro.SenhasNaoBatem => UnprocessableEntity(respostaErro),
            CodigosErro.FalhaDeConexaoComApi => StatusCode(503, respostaErro),
            CodigosErro.SenhaErrada => Unauthorized(respostaErro),
            _ => StatusCode(500, Desconhecido())
        };
    }
    
    /// <summary>
    /// Redefinição de senha do usuário
    /// </summary>
    /// <remarks>
    /// A nova senha deve satisfazer cinco condições:
    ///
    /// • Mínimo de 13 caracteres
    ///
    /// • Pelo menos 1 número
    ///
    /// • Pelo menos 1 letra ASCII maiúscula
    ///
    /// • Pelo menos 1 letra ASCII minúscula
    ///
    /// • Pelo menos 1 caractere especial
    ///
    /// Se utilizar um usuário de testes coletivo, lembre-se de devolver a senha original depois!
    /// </remarks>
    /// <response code="200">Senha alterada com sucesso</response>
    /// <response code="401">Senha antiga errada</response>
    /// <response code="422">Erro de validação</response>
    /// <response code="503">Conexão com IFS falhou</response>
    [AllowAnonymous]
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpPost("redefinir-senha")]
    public async Task<IActionResult> RedefinirSenha(
        [FromBody] RedefinirSenhaRequisicaoDto dto,
        [FromServices] IRedefinirSenhaUsuarioService service,
        CancellationToken cancellationToken)
    {
        var resposta = await service.RedefinirAsync(dto, cancellationToken);

        var respostaErro = new RespostaErro
        {
            Code = resposta.Codigo,
            Message = resposta.Mensagem
        };
        
        if (resposta.Sucesso)
        {
            return Ok();
        }

        return resposta.Codigo switch
        {
            CodigosErro.EntradaInvalida or CodigosErro.SenhasNaoBatem => UnprocessableEntity(respostaErro),
            CodigosErro.FalhaDeConexaoComApi => StatusCode(503, respostaErro),
            CodigosErro.SenhaErrada => Unauthorized(respostaErro),
            _ => StatusCode(500, Desconhecido())
        };
    }
}