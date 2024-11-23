using ApiDelivery.Respostas;
using Crosscutting.Constantes;
using Crosscutting.Dto.Autenticacao;
using Domain.Autenticacao.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary>
/// API para autenticação de usuários 
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AutenticacaoController : ControllerBase
{
    private readonly ILoginUsuarioService _loginService;

    /// <summary>
    /// </summary>
    public AutenticacaoController(ILoginUsuarioService loginService)
    {
        _loginService = loginService;
    }

    /// <summary>
    /// Realiza o login do usuário
    /// </summary>
    /// <response code="200">Autenticação efetuada com sucesso</response>
    /// <response code="401">Falha na autenticação</response>
    /// <response code="422">E-mail inválido</response>
    /// <response code="503">Conexão com IFS falhou</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [Produces(TiposRequisicaoERetorno.JsonText)]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUsuarioInterno([FromBody] LoginDto dto)
    {
        var resposta = await _loginService.LoginAsync(dto);

        if (!string.IsNullOrEmpty(resposta.Token))
        {
            return Ok(new
            {
                resposta.Token
            });
        }

        var respostaErro = new RespostaErro
        {
            Code = resposta.Codigo,
            Message = resposta.Mensagem
        };

        return resposta.Codigo switch
        {
            CodigosErroLogin.FalhaEmailDoUsuarioInvalido => UnprocessableEntity(respostaErro),
            CodigosErroLogin.FalhaAutenticacao => Unauthorized(respostaErro),
            _ => StatusCode(500,
                new RespostaErro { Code = CodigosErro.ErroDesconhecido, Message = MensagensErro.ErroDesconhecido })
        };
    }
    
    /// <summary>
    /// Realiza o refresh da autenticação do colaborador (usuário interno)
    /// </summary>
    /// <response code="200">Refresh efetuado com sucesso</response>
    /// <response code="401">Falha na autenticação do refresh token</response>
    /// <response code="503">Conexão com IFS falhou</response>
    [AllowAnonymous]
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [Produces(TiposRequisicaoERetorno.JsonText)]
    [HttpPost("refresh-token-interno")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
    {
        var resposta = await _loginService.RefreshTokenAsync(dto);
        
        if (!string.IsNullOrEmpty(resposta.Token))
        {
            return Ok(new
            {
                resposta.Token
            });
        }

        var respostaErro = new RespostaErro
        {
            Code = resposta.Codigo,
            Message = resposta.Mensagem
        };

        return resposta.Codigo switch
        {
            CodigosErroLogin.FalhaRefreshToken => Unauthorized(respostaErro),
            CodigosErroLogin.FalhaAutenticacaoDoRefreshToken => Unauthorized(respostaErro),
            _ => StatusCode(500,
                new RespostaErro { Code = CodigosErro.ErroDesconhecido, Message = MensagensErro.ErroDesconhecido })
        };
    }
}