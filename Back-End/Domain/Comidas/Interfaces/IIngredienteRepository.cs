using Domain._Base.Interfaces;
using Domain.Comida.Models;
using Domain.Comidas.Models;

namespace Domain.Comidas.Interfaces;

public interface IIngredienteRepository : IRepositoryBase<Ingrediente>
{
    Task<List<Ingrediente>> ConsultarIngredientesAsync(CancellationToken cancellationToken);
}