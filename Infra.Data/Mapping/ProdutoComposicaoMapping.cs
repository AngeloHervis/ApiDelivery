using Crosscutting.Constantes;
using Crosscutting.Extensions;
using Domain.Comida.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class ProdutoComposicaoMapping : IEntityTypeConfiguration<ProdutoComposicao>
{
    public void Configure(EntityTypeBuilder<ProdutoComposicao> builder)
    {
        builder.ToTable(nameof(ProdutoComposicao).ToSnakeCase());

        builder.HasKey(pc => new { pc.ProdutoId, pc.ItemId });

        builder.Property(pc => pc.Quantidade)
            .HasColumnName(nameof(ProdutoComposicao.Quantidade).ToSnakeCase())
            .HasColumnType(Caracteres.PrecisaoDecimal)
            .IsRequired();

        builder.Property(pc => pc.TipoItem)
            .HasColumnName(nameof(ProdutoComposicao.TipoItem).ToSnakeCase())
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(pc => pc.UnidadeMedida)
            .HasColumnName(nameof(ProdutoComposicao.UnidadeMedida).ToSnakeCase())
            .HasConversion<int>()
            .IsRequired();
        
        builder.HasOne(pc => pc.Produto)
            .WithMany(p => p.Composicao)
            .HasForeignKey(pc => pc.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(pc => pc.Ingrediente)
            .WithMany()
            .HasForeignKey(pc => pc.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}