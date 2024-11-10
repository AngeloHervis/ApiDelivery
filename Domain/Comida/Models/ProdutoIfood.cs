using Crosscutting.Enums;

namespace Domain.Comida.Models;

public class ProdutoIfood
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public decimal ValorPago { get; set; }
    public string Marca { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public decimal TaxaPlano { get; set; }
    public decimal TaxaTransacao { get; set; }
    public decimal TaxaRepasse { get; set; }
}