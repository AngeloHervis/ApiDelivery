using Domain.Comida.Models;

namespace Domain.Comida.Interfaces;

public interface IListagemItensExtrasService
{
    Task<List<ItemExtra>> ListarTodosAsync(CancellationToken cancellationToken);
}