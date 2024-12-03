using Crosscutting.Exception;
using Domain._Base.Models;
using Domain.Comidas.Interfaces;

namespace Domain.Comidas.Services;

public class ExcluirIngredienteService(IIngredienteRepository ingredienteRepository)
{
    public async Task<CommandResult<Guid>> ExcluirIngrediente(Guid id, CancellationToken cancellationToken)
    {
        var ingrediente = await ingredienteRepository.ObterPorIdAsync(id, cancellationToken);

        if (ingrediente == null)
        {
            throw new NotFoundException("Ingrediente não encontrado");
        }

        ingredienteRepository.Remover(ingrediente);

        return CommandResult<Guid>.Success(id);
    }
}