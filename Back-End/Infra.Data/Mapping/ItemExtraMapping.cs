using Crosscutting.Constantes;
using Crosscutting.Extensions;
using Domain.Comida.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class ItemExtraMapping : IEntityTypeConfiguration<ItemExtra>
{
    public void Configure(EntityTypeBuilder<ItemExtra> builder)
    {
        builder.ToTable(nameof(ItemExtra).ToSnakeCase());

        builder.Property(p => p.Id)
            .HasColumnName(nameof(ItemExtra.Id).ToSnakeCase())
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(p => p.Nome)
            .HasColumnName(nameof(ItemExtra.Nome).ToSnakeCase())
            .HasMaxLength(Caracteres.DuzentosECinquenta)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnName(nameof(ItemExtra.Descricao).ToSnakeCase())
            .HasMaxLength(Caracteres.Mil)
            .IsRequired();

        builder.Property(p => p.UnidadeMedida)
            .HasColumnName(nameof(ItemExtra.UnidadeMedida).ToSnakeCase())
            .HasConversion<int>();

        builder.Property(p => p.ValorPago)
            .HasColumnName(nameof(ItemExtra.ValorPago).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.Marca)
            .HasColumnName(nameof(ItemExtra.Marca).ToSnakeCase())
            .HasMaxLength(Caracteres.Cem)
            .IsRequired();

        builder.Property(p => p.QuantidadeEstoque)
            .HasColumnName(nameof(ItemExtra.QuantidadeEstoque).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.Ativo)
            .HasColumnName(nameof(ItemExtra.Ativo).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.DataCadastro)
            .HasColumnName(nameof(ItemExtra.DataCadastro).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.TipoItem)
            .HasColumnName(nameof(ProdutoComposicao.TipoItem).ToSnakeCase())
            .HasConversion<int>()
            .IsRequired();
    }
}