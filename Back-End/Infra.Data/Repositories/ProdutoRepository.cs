using Crosscutting.Paginacao;
using Domain.Comida.Interfaces;
using Domain.Comida.Models;
using Infra.Data.Repository._Base;

namespace Infra.Data.Repositories;

public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
{
    private readonly DeliveryDbContext _context;
    
    public ProdutoRepository(DeliveryDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<RespostaPaginacao<Produto>> ConsultarProdutosIfoodAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}