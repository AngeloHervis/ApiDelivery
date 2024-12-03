using Crosscutting.Dto.Comida;
using Crosscutting.Enums;
using Domain.Comida.Models;

namespace Domain.Comidas.Models;

public class ProdutoComposicao
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public Guid? ItemExtraId { get; set; }
    public ItemExtra ItemExtra { get; set; }
    public Guid? IngredienteId { get; set; }
    public Ingrediente Ingrediente { get; set; }

    public UnidadeMedida UnidadeMedida { get; set; }
    public decimal Quantidade { get; set; }
    public TipoItem TipoItem { get; set; }
    
    public ProdutoComposicao() { }
    public ProdutoComposicao(ComposicaoDto request)
    {
        ProdutoId = request.ProdutoId;
        ItemExtraId = request.ItemExtraId;
        IngredienteId = request.IngredienteId;
        UnidadeMedida = request.UnidadeMedida;
        Quantidade = request.Quantidade;
        TipoItem = request.TipoItem;
    }
}