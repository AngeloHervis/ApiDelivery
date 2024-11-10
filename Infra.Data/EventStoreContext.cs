using Domain._Base.Events;
using Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class EventStoreContext : DbContext
{
    public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options) { }

    public DbSet<StoredEvent> StoredEvents { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StoredEventMapping());

        base.OnModelCreating(modelBuilder);
    }
}