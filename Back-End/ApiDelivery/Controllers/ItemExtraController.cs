using Crosscutting.Constantes;
using Domain.Comida.Commands;
using Domain.Comidas.Interfaces;
using Domain.Comidas.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary>
/// API para gerenciar itens extras
/// </summary>
[ApiController]
[Route("api/itensExtras")]
public class ItemExtraController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Retorna lista de itens extras
    /// </summary>
    /// <response code="200">Lista obtida com sucesso</response>
    /// <response code="404">Nenhum item extra encontrado</response>
    [HttpGet]
    public async Task<IActionResult> ListarTodosItensExtras(
        [FromServices] IListagemItensExtrasService service,
        CancellationToken cancellationToken)
    {
        try
        {
            var itensExtras = await service.ConsultarItensExtras(cancellationToken);

            return Ok(itensExtras);
        }
        catch (Exception e)
        {
            return TratarExcecoes(e);
        }
    }
    
    /// <summary>
    /// Cadastro de um novo item extra
    /// </summary>
    /// <response code="201">Item extra cadastrado com sucesso</response>
    /// <response code="400">Erro de validação</response>
    /// <response code="503">Falha de conexão com a API</response>
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpPost("cadastrar")]
    public async Task<IActionResult> CadastrarItemExtra(CadastrarItemExtraCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(request, cancellationToken);
            return Ok();
        }
        catch (Exception e)
        {
            return TratarExcecoes(e);
        }
    }
    
    /// <summary>
    /// Excluir um Item Extra
    /// </summary>
    /// <response code="201">Item Extra excluido com sucesso</response>
    /// <response code="404">Item Extra não encontrado</response>
    /// <response code="503">Falha de conexão com a API</response>
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpDelete("excluir")]
    public async Task<IActionResult> ExcluirItemExtra(ExcluirItemExtraService service,
        [FromQuery] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await service.ExcluirItemExtra(id, cancellationToken);
        return result.IsError
            ? StatusCode(result.StatusCode, result.Errors)
            : Ok(result.Data);
    }
}