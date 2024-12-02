using Crosscutting.Constantes;
using Crosscutting.Extensions;
using Domain.Comida.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class ProdutoIfoodMapping : IEntityTypeConfiguration<ProdutoIfood>
{
    public void Configure(EntityTypeBuilder<ProdutoIfood> builder)
    {
        builder.ToTable(nameof(ProdutoIfood).ToSnakeCase());

        builder.Property(p => p.Id)
            .HasColumnName(nameof(ProdutoIfood.Id).ToSnakeCase())
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(p => p.Nome)
            .HasColumnName(nameof(ProdutoIfood.Nome).ToSnakeCase())
            .HasMaxLength(Caracteres.DuzentosECinquenta)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnName(nameof(ProdutoIfood.Descricao).ToSnakeCase())
            .HasMaxLength(Caracteres.Mil);

        builder.Property(p => p.UnidadeMedida)
            .HasColumnName(nameof(ProdutoIfood.UnidadeMedida).ToSnakeCase())
            .HasConversion<int>()
            .IsRequired();

        builder.Property(p => p.ValorPago)
            .HasColumnName(nameof(ProdutoIfood.ValorPago).ToSnakeCase())
            .HasColumnType(Caracteres.PrecisaoDecimal)
            .IsRequired();

        builder.Property(p => p.Marca)
            .HasColumnName(nameof(ProdutoIfood.Marca).ToSnakeCase())
            .HasMaxLength(Caracteres.Cem);

        builder.Property(p => p.Ativo)
            .HasColumnName(nameof(ProdutoIfood.Ativo).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.DataCadastro)
            .HasColumnName(nameof(ProdutoIfood.DataCadastro).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.TaxaPlano)
            .HasColumnName(nameof(ProdutoIfood.TaxaPlano).ToSnakeCase())
            .HasColumnType(Caracteres.PrecisaoDecimal)
            .IsRequired();

        builder.Property(p => p.TaxaTransacao)
            .HasColumnName(nameof(ProdutoIfood.TaxaTransacao).ToSnakeCase())
            .HasColumnType(Caracteres.PrecisaoDecimal)
            .IsRequired();

        builder.Property(p => p.TaxaRepasse)
            .HasColumnName(nameof(ProdutoIfood.TaxaRepasse).ToSnakeCase())
            .HasColumnType(Caracteres.PrecisaoDecimal)
            .IsRequired();
    }
}