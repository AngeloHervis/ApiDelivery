using Crosscutting.Constantes;
using Domain.Comida.Interfaces;
using Domain.Comidas.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary>
/// API para gerenciar ingredientes
/// </summary>
[ApiController]
[Route("api/ingredientes")]
public class IngredienteController : BaseController
{
    private readonly IMediator _mediator;

    public IngredienteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retorna lista de ingredientes
    /// </summary>
    /// <response code="200">Lista obtida com sucesso</response>
    /// <response code="404">Nenhum ingrediente encontrado</response>
    [HttpGet]
    public async Task<IActionResult> ListarTodosIngredientes(
        [FromServices] IListagemIngredientesService service,
        CancellationToken cancellationToken)
    {
        try
        {
            var ingredientes = await service.ListarTodosAsync(cancellationToken);

            return Ok(ingredientes);
        }
        catch (Exception e)
        {
            return TratarExcecoes(e);
        }
    }
    
    /// <summary>
    /// Cadastro de um novo ingrediente
    /// </summary>
    /// <response code="201">Ingrediente cadastrado com sucesso</response>
    /// <response code="400">Erro de validação</response>
    /// <response code="503">Falha de conexão com a API</response>
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpPost("cadastrar")]
    [ProducesResponseType(typeof(CadastrarIngredienteCommand), StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarIngrediente(CadastrarIngredienteCommand request,
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