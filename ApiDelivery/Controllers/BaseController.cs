using ApiDelivery.Respostas;
using Crosscutting.Constantes;
using Crosscutting.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary></summary>
[Authorize("bearer")]
public abstract class BaseController : ControllerBase
{
    /// <summary></summary>
    
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult TratarExcecoes(Exception excecao)
    {
        switch (excecao)
        {
            case ValidacaoException validacaoException:
            {
                var retorno = new RespostaListaErros
                {
                    Code = validacaoException.Message,
                    Erros = validacaoException.Erros.Select(e => new RespostaErro(e.Code, e.Message)).ToList()
                };
                return UnprocessableEntity(retorno);
            }
            case HttpRequestException erro:
                return StatusCode(503, FalhaDeConexaoComApi(erro.Message));
            default:
                return StatusCode(500, Desconhecido());
        }
    }
    
    /// <summary></summary>
    protected static RespostaErro FalhaDeConexaoComApi(string mensagem)
        => new()
        {
            Code = CodigosErro.FalhaDeConexaoComApi,
            Message = mensagem
        };
    
    /// <summary></summary>
    protected static RespostaErro Desconhecido()
        => new()
        {
            Code = CodigosErro.ErroDesconhecido,
            Message = MensagensErro.ErroDesconhecido
        };
}