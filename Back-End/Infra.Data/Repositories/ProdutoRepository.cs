using Domain.Comida.Models;
using Domain.Comidas.Interfaces;
using Domain.Comidas.Models;
using Infra.Data.Repository._Base;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class ProdutoRepository(DeliveryDbContext context) : RepositoryBase<Produto>(context), IProdutoRepository
{
    private readonly DeliveryDbContext _context = context;
    public async Task<List<Produto>> ConsultarProdutosAsync(CancellationToken cancellationToken)
    => await _context.Produtos
        .AsQueryable()
        .ToListAsync(cancellationToken);
}