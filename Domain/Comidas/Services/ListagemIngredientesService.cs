using Domain.Comida.Interfaces;
using Domain.Comida.Models;

namespace Domain.Comida.Services;

public class ListagemIngredientesService : IListagemIngredientesService
{
    public Task<List<Ingrediente>> ListarTodosAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}