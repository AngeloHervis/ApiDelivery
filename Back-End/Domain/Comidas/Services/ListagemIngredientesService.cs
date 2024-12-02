using Domain.Comida.Interfaces;
using Domain.Comida.Models;

namespace Domain.Comidas.Services;

public class ListagemIngredientesService : IListagemIngredientesService
{
    public Task<List<Ingrediente>> ListarTodosAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}