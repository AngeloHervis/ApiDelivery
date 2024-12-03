using Domain.Comida.Models;
using Domain.Comidas.Interfaces;
using Domain.Comidas.Models;
using Infra.Data.Repository._Base;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class ItemExtraRepository(DeliveryDbContext context) : RepositoryBase<ItemExtra>(context), IItemExtraRepository
{
    private readonly DeliveryDbContext _context = context;
    
    public async Task<List<ItemExtra>> ConsultarItensExtrasAsync(CancellationToken cancellationToken)
    => await _context.ItensExtras
        .AsQueryable()
        .ToListAsync(cancellationToken);
            
}