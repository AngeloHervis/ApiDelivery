using Domain.Comidas.Interfaces;
using Domain.Comidas.Models;

namespace Domain.Comidas.Services;

public class ListagemIngredientesService(IIngredienteRepository ingredienteRepository) : IListagemIngredientesService
{
    public async Task<List<Ingrediente>> ConsultarIngredientes(CancellationToken cancellationToken)
    {
        return await ingredienteRepository.ConsultarIngredientesAsync(cancellationToken);
    }
}