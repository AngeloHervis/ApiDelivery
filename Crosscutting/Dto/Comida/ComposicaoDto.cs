using Crosscutting.Enums;

namespace Crosscutting.Dto.Comida;

public class ComposicaoDto
{
    public Guid ItemId { get; set; }
    public Guid ProdutoId { get; set; }
    public decimal Quantidade { get; set; }
    public TipoItem TipoItem { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
}