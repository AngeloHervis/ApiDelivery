using Crosscutting.Constantes;
using Domain.Comida.Commands;
using Domain.Comida.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary>
/// API para gerenciar itens extras
/// </summary>
[ApiController]
[Route("api/itens")]
public class ItemExtraController : BaseController
{
    private readonly IMediator _mediator;

    public ItemExtraController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
            var itensExtras = await service.ListarTodosAsync(cancellationToken);

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
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }
        catch (Exception e)
        {
            return TratarExcecoes(e);
        }
    }
}