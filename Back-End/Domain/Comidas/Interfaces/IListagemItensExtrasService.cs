using Domain.Comida.Models;
using Domain.Comidas.Models;

namespace Domain.Comidas.Interfaces;

public interface IListagemItensExtrasService
{
    Task<List<ItemExtra>> ConsultarItensExtras(CancellationToken cancellationToken);
}