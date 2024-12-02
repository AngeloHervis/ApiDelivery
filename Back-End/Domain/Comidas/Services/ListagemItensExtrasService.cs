using Domain.Comida.Interfaces;
using Domain.Comida.Models;

namespace Domain.Comida.Services;

public class ListagemItensExtrasService : IListagemItensExtrasService
{
    public Task<List<ItemExtra>> ListarTodosAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}