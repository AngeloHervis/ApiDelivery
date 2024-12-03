using Crosscutting.Constantes;
using Domain.Comidas.Commands;
using Domain.Comidas.Interfaces;
using Domain.Comidas.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary>
/// API para gerenciar produtos
/// </summary>
[ApiController]
[Route("api/produtos")]
public class ProdutoController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Retorna lista de produtos
    /// </summary>
    /// <response code="200">Lista obtida com sucesso</response>
    /// <response code="404">Nenhum produto encontrado</response>
    [HttpGet]
    public async Task<IActionResult> ListarTodosProdutos(
        [FromServices] IListagemProdutosService service,
        CancellationToken cancellationToken)
    {
        try
        {
            var produtos = await service.ConsultarProdutos(cancellationToken);

            return Ok(produtos);
        }
        catch (Exception e)
        {
            return TratarExcecoes(e);
        }
    }
    
    /// <summary>
    /// Cadastro de um novo produto
    /// </summary>
    /// <response code="201">Produto cadastrado com sucesso</response>
    /// <response code="400">Erro de validação</response>
    /// <response code="503">Falha de conexão com a API</response>
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpPost("cadastrar")]
    public async Task<IActionResult> CadastrarProduto(CadastrarProdutoCommand request,
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
    /// Excluir um produto
    /// </summary>
    /// <response code="201">Produto excluido com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    /// <response code="503">Falha de conexão com a API</response>
    [Consumes(TiposRequisicaoERetorno.JsonText)]
    [HttpDelete("excluir")]
    public async Task<IActionResult> ExcluirProduto(ExcluirProdutoService service,
        [FromQuery] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await service.ExcluirProduto(id, cancellationToken);
        return result.IsError
            ? StatusCode(result.StatusCode, result.Errors)
            : Ok(result.Data);
    }
}