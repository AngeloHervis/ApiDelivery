using Domain.Comida.Models;
using Domain.Comidas.Models;

namespace Domain.Comidas.Interfaces;

public interface IListagemProdutosService
{
    Task<List<Produto>> ConsultarProdutos(CancellationToken cancellationToken);
}