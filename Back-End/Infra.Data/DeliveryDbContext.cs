using Domain._Base.Interfaces;
using Domain._Base.Models;
using Domain.Comida.Models;
using Domain.Comidas.Models;
using Infra.Data.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class DeliveryDbContext : DbContext
{
    private readonly IMediator _mediator;

    private DeliveryDbContext(DbContextOptions<DeliveryDbContext> options,
        IMediator mediator
    ) : base(options)
    {
        _mediator = mediator;
    }
    
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }
    
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }
    public DbSet<ItemExtra> ItensExtras { get; set; }
    public DbSet<ProdutoComposicao> ProdutosComposicoes { get; set; }
    public DbSet<ProdutoIfood> ProdutosIfood { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new ProdutoMapping())
            .ApplyConfiguration(new IngredienteMapping())
            .ApplyConfiguration(new ItemExtraMapping())
            .ApplyConfiguration(new ProdutoComposicaoMapping())
            .ApplyConfiguration(new ProdutoIfoodMapping());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}