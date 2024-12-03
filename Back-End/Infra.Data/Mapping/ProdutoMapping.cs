using Crosscutting.Constantes;
using Crosscutting.Extensions;
using Domain._Base.Models;
using Domain.Comida.Models;
using Domain.Comidas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable(nameof(Produto).ToSnakeCase());
        

        builder.Property(p => p.Id)
            .HasColumnName(nameof(Produto.Id).ToSnakeCase())
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(p => p.Nome)
            .HasColumnName(nameof(Produto.Nome).ToSnakeCase())
            .HasMaxLength(Caracteres.DuzentosECinquenta)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnName(nameof(Produto.Descricao).ToSnakeCase())
            .HasMaxLength(Caracteres.Mil)
            .IsRequired();

        builder.Property(p => p.UnidadeMedida)
            .HasColumnName(nameof(Produto.UnidadeMedida).ToSnakeCase());

        builder.Property(p => p.ValorPago)
            .HasColumnName(nameof(Produto.ValorPago).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.ValorVenda)
            .HasColumnName(nameof(Produto.ValorVenda).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.Ativo)
            .HasColumnName(nameof(Produto.Ativo).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.DataCadastro)
            .HasColumnName(nameof(Produto.DataCadastro).ToSnakeCase())
            .IsRequired();

        builder.Property(p => p.CustoVariavel)
            .HasColumnName(nameof(Produto.CustoVariavel).ToSnakeCase())
            .HasColumnType(Caracteres.PrecisaoDecimal)
            .IsRequired();

        builder.Property(p => p.Impostos)
            .HasColumnName(nameof(Produto.Impostos).ToSnakeCase())
            .HasColumnType(Caracteres.PrecisaoDecimal)
            .IsRequired();

        builder.Property(p => p.TaxaCartao)
            .HasColumnName(nameof(Produto.TaxaCartao).ToSnakeCase())
            .HasColumnType(Caracteres.PrecisaoDecimal)
            .IsRequired();
        
        builder.HasMany(p => p.Composicao)
            .WithOne(pc => pc.Produto)
            .HasForeignKey(pc => pc.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}