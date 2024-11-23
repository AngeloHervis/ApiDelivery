using Domain._Base.Interfaces;
using Domain._Base.Models;
using Domain.Comida.Models;
using Domain.Usuarios;
using Infra.Data.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class DeliveryDbContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly IEventStore _eventStore;

    private DeliveryDbContext(DbContextOptions<DeliveryDbContext> options,
        IMediator mediator,
        IEventStore eventStore
    ) : base(options)
    {
        _mediator = mediator;
        _eventStore = eventStore;
    }
    
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
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
        await PublicarEventosDeDominio();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task PublicarEventosDeDominio()
    {
        var domainEntities = ChangeTracker
            .Entries<Entidade>()
            .Where(x => x.Entity.ObterEvents().Count != 0)
            .ToList();
    
        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.ObterEvents())
            .ToList();
    
        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEvents());
    
        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                _eventStore.Save(domainEvent);
                await _mediator.Publish(domainEvent);
            });
    
        await Task.WhenAll(tasks);
    }
}