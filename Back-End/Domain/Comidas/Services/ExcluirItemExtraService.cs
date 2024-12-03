using Crosscutting.Exception;
using Domain._Base.Models;
using Domain.Comidas.Interfaces;

namespace Domain.Comidas.Services;

public class ExcluirItemExtraService(IItemExtraRepository itemExtraRepository)
{
    public async Task<CommandResult<Guid>> ExcluirItemExtra(Guid id, CancellationToken cancellationToken)
    {
        var itemExtra = await itemExtraRepository.ObterPorIdAsync(id, cancellationToken);

        if (itemExtra == null)
        {
            throw new NotFoundException("Item extra não encontrado");
        }

        itemExtraRepository.Remover(itemExtra);

        return CommandResult<Guid>.Success(id);
    }
}