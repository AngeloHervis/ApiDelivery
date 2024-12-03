using Domain.Comida.Models;
using Domain.Comidas.Interfaces;
using Domain.Comidas.Models;

namespace Domain.Comidas.Services;

public class ListagemItensExtrasService(IItemExtraRepository itemExtraRepository) : IListagemItensExtrasService
{
    public async Task<List<ItemExtra>> ConsultarItensExtras(CancellationToken cancellationToken)
    {
        return await itemExtraRepository.ConsultarItensExtrasAsync(cancellationToken);
    }
}