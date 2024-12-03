using Domain._Base.Interfaces;
using Domain.Comida.Models;
using Domain.Comidas.Models;

namespace Domain.Comidas.Interfaces;

public interface IItemExtraRepository : IRepositoryBase<ItemExtra>
{
    Task<List<ItemExtra>> ConsultarItensExtrasAsync(CancellationToken cancellationToken);
}