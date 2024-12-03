using Domain.Comidas.Models;

namespace Domain.Comidas.Interfaces;

public interface IListagemIngredientesService
{
    Task<List<Ingrediente>> ConsultarIngredientes(CancellationToken cancellationToken);
}