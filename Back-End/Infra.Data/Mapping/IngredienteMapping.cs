using Crosscutting.Constantes;
using Crosscutting.Extensions;
using Domain.Comida.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class IngredienteMapping : IEntityTypeConfiguration<Ingrediente>
{
    public void Configure(EntityTypeBuilder<Ingrediente> builder)
    {
        builder.ToTable(nameof(Ingrediente).ToSnakeCase());

        builder.Property(p => p.Id)
            .HasColumnName(nameof(Ingrediente.Id).ToSnakeCase())
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(p => p.Nome)
            .HasColumnName(nameof(Ingrediente.Nome).ToSnakeCase())
            .HasMaxLength(Caracteres.DuzentosECinquenta)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnName(nameof(Ingrediente.Descricao).ToSnakeCase())
            .HasMaxLength(Caracteres.Mil)
            .IsRequired();

        builder.Property(p => p.UnidadeMedida)
            .HasColumnName(nameof(Ingrediente.UnidadeMedida).ToSnakeCase());

        builder.Property(p => p.ValorPago)
            .HasColumnName(nameof(Ingrediente.ValorPago).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.Marca)
            .HasColumnName(nameof(Ingrediente.Marca).ToSnakeCase())
            .HasMaxLength(Caracteres.Cem)
            .IsRequired();

        builder.Property(p => p.QuantidadeEstoque)
            .HasColumnName(nameof(Ingrediente.QuantidadeEstoque).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.Ativo)
            .HasColumnName(nameof(Ingrediente.Ativo).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.DataCadastro)
            .HasColumnName(nameof(Ingrediente.DataCadastro).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.TipoItem)
            .HasColumnName(nameof(ProdutoComposicao.TipoItem).ToSnakeCase())
            .HasConversion<int>()
            .IsRequired();
    }
}