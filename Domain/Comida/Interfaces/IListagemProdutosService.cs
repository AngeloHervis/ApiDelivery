using Domain.Comida.Models;

namespace Domain.Comida.Interfaces;

public interface IListagemProdutosService
{
    Task<List<Produto>> ListarTodosAsync(CancellationToken cancellationToken);
}