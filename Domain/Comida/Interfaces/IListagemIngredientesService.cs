using Domain.Comida.Models;

namespace Domain.Comida.Interfaces;

public interface IListagemIngredientesService
{
    Task<List<Ingrediente>> ListarTodosAsync(CancellationToken cancellationToken);
}