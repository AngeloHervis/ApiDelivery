using Domain.Comida.Models;
using Domain.Comidas.Interfaces;
using Domain.Comidas.Models;

namespace Domain.Comidas.Services;

public class ListagemProdutosService(IProdutoRepository produtoRepository) : IListagemProdutosService
{
    public async Task<List<Produto>> ConsultarProdutos(CancellationToken cancellationToken)
    {
        return await produtoRepository.ConsultarProdutosAsync(cancellationToken);
    }
}