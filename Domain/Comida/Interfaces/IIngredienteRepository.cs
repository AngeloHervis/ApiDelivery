using Crosscutting.Paginacao;
using Domain._Base.Interfaces;
using Domain.Comida.Models;

namespace Domain.Comida.Interfaces;

public interface IIngredienteRepository : IRepositoryBase<Ingrediente>
{
    Task<RespostaPaginacao<Ingrediente>> ConsultarIngredientesAsync(CancellationToken cancellationToken);
}