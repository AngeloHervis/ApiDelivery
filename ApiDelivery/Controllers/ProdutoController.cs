using Crosscutting.Constantes;
using Domain.Comida.Commands;
using Domain.Comida.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiDelivery.Controllers;

/// <summary>
/// API para gerenciar produtos
/// </summary>
[ApiController]
[Route("api/produtos")]
public class ProdutoController : BaseController
{
    private readonly IMediator _mediator;

    public ProdutoController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
            var produtos = await service.ListarTodosAsync(cancellationToken);

            return Ok(produtos);
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
    public async Task<IActionResult> CadastrarProduto(CadastrarProdutoCommand request,
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
    // URL: https://localhost:5001/api/produtos/cadastrar
    // JSON para testar
    // {
    //     "nome": "Coca-Cola",
    //     "descricao": "Refrigerante de cola",
    //     "unidadeMedida": 1,
    //     "valorPago": 2.5,
    //     "valorVenda": 5.0,
    //     "marca": "Coca-Cola",
    //     "quantidade": 100,
    //     "ativo": true,
    //     "custoVariavel": 1.0,
    //     "impostos": 0.5,
    //     "taxaCartao": 0.5,
    //     "composicao": [
    //         {
    //             
    // }
}