using Domain.Comidas.Interfaces;
using Domain.Comidas.Models;
using Infra.Data.Repository._Base;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class IngredienteRepository(DeliveryDbContext context) : RepositoryBase<Ingrediente>(context), IIngredienteRepository
{
    private readonly DeliveryDbContext _context = context;
    
    public async Task<List<Ingrediente>> ConsultarIngredientesAsync(CancellationToken cancellationToken)
    => await _context.Ingredientes
        .AsQueryable()
        .ToListAsync(cancellationToken);
}