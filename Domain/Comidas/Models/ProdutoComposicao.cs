using Crosscutting.Dto.Comida;
using Crosscutting.Enums;

namespace Domain.Comida.Models;

public class ProdutoComposicao
{
    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public Guid ItemId { get; set; }
    public Ingrediente Ingrediente { get; set; }
    public ItemExtra ItemExtra { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public decimal Quantidade { get; set; }
    public TipoItem TipoItem { get; set; }
    
    public ProdutoComposicao() { }
    public ProdutoComposicao(ComposicaoDto request)
    {
        ProdutoId = request.ProdutoId;
        ItemId = request.ItemId;
        UnidadeMedida = request.UnidadeMedida;
        Quantidade = request.Quantidade;
        TipoItem = request.TipoItem;
    }
}