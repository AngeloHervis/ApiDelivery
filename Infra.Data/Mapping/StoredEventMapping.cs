using Crosscutting.Constantes;
using Crosscutting.Extensions;
using Domain._Base.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class StoredEventMapping : IEntityTypeConfiguration<StoredEvent>
{
    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName(nameof(StoredEvent.Id))
            .ValueGeneratedNever();

        builder.Property(x => x.AggregateId)
            .HasColumnName(nameof(StoredEvent.AggregateId).ToSnakeCase());

        builder.Property(x => x.Tipo)
            .HasColumnName(nameof(StoredEvent.Tipo).ToSnakeCase());

        builder.Property(x => x.Data)
            .HasColumnName(nameof(StoredEvent.Data).ToSnakeCase());

        builder.Property(x => x.Nome)
            .HasColumnName(nameof(StoredEvent.Nome).ToSnakeCase())
            .HasMaxLength(Caracteres.DuzentosECinquenta);

        builder.Property(x => x.Usuario)
            .HasColumnName(nameof(StoredEvent.Usuario).ToSnakeCase())
            .HasMaxLength(Caracteres.DuzentosECinquenta);

        builder.Property(x => x.Dados)
            .HasColumnName(nameof(StoredEvent.Dados).ToSnakeCase());

        builder.ToTable(nameof(StoredEvent).ToSnakeCase());
    }
}