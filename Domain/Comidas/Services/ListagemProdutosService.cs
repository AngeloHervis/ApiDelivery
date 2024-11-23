using Domain.Comida.Interfaces;
using Domain.Comida.Models;

namespace Domain.Comida.Services;

public class ListagemProdutosService(IProdutoRepository produtoRepository) : IListagemProdutosService
{
    
    public Task<List<Produto>> ListarTodosAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}