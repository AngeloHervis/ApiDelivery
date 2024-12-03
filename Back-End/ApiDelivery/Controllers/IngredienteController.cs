using Crosscutting.Constantes;
using Domain.Comidas.Commands;
using Domain.Comidas.Interfaces;
using Domain.Comidas.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary>
/// API para gerenciar ingredientes
/// </summary>
[ApiController]
[Route("api/ingredientes")]
public class IngredienteController(IMediator mediator) : BaseController
{
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
            var ingredientes = await service.ConsultarIngredientes(cancellationToken);

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
            await mediator.Send(request, cancellationToken);
            return Ok();
        }
        catch (Exception e)
        {
            return TratarExcecoes(e);
        }
    }
    
    /// <summary>
    /// Edição de um novo ingrediente
    /// </summary>
    /// <response code="201">Ingrediente editado com sucesso</response>
    /// <response code="400">Erro de validação</response>
    /// <response code="503">Falha de conexão com a API</response>
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpPost("editar")]
    [ProducesResponseType(typeof(EditarIngredienteCommand), StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarIngrediente(EditarIngredienteCommand request,
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
    /// Excluir um ingrediente
    /// </summary>
    /// <response code="201">Ingrediente excluido com sucesso</response>
    /// <response code="404">Ingrediente não encontrado</response>
    /// <response code="503">Falha de conexão com a API</response>
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpDelete("excluir")]
    public async Task<IActionResult> ExcluirIngrediente(ExcluirIngredienteService service,
        [FromQuery] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await service.ExcluirIngrediente(id, cancellationToken);
        return result.IsError
            ? StatusCode(result.StatusCode, result.Errors)
            : Ok(result.Data);
    }
}