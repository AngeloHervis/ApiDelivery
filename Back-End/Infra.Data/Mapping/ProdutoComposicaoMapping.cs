using Crosscutting.Constantes;
using Crosscutting.Extensions;
using Domain.Comida.Models;
using Domain.Comidas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class ProdutoComposicaoMapping : IEntityTypeConfiguration<ProdutoComposicao>
{
    public void Configure(EntityTypeBuilder<ProdutoComposicao> builder)
    {
        builder.ToTable(nameof(ProdutoComposicao).ToSnakeCase());
        
        builder.Property(pc => pc.Id)
            .HasColumnName(nameof(ProdutoComposicao.Id).ToSnakeCase())
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.HasKey(pc => pc.Id);

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
            .HasForeignKey(pc => pc.IngredienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pc => pc.ItemExtra)
            .WithMany()
            .HasForeignKey(pc => pc.ItemExtraId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}