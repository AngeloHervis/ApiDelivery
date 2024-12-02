using Crosscutting.Paginacao;
using Domain._Base.Interfaces;
using Domain.Comida.Models;

namespace Domain.Comida.Interfaces;

public interface IItemExtraRepository : IRepositoryBase<ItemExtra>
{
    Task<RespostaPaginacao<ItemExtra>> ConsultarItemExtraAsync(CancellationToken cancellationToken);
}