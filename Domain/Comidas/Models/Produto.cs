using Domain._Base.Models;
using Domain.Comida.Commands;

namespace Domain.Comida.Models;

public class Produto : Entidade
{
    public ICollection<ProdutoComposicao> Composicao { get; set; }
    public decimal ValorVenda { get; set; }
    public decimal CustoVariavel { get; set; }
    public decimal Impostos { get; set; }
    public decimal TaxaCartao { get; set; }
    public int QuantidadePorcao { get; set; }
    
    public Produto() { }

    public Produto(CadastrarProdutoCommand request)
    {
        Nome = request.Nome;
        Descricao = request.Descricao;
        UnidadeMedida = request.UnidadeMedida;
        ValorPago = request.ValorPago;
        Ativo = request.Ativo;
        Composicao = request.Composicao.Select(c => new ProdutoComposicao(c)).ToList();
        ValorVenda = request.ValorVenda;
        CustoVariavel = request.CustoVariavel;
        Impostos = request.Impostos;
        TaxaCartao = request.TaxaCartao;
        QuantidadePorcao = request.QuantidadePorcao;
    }

    public decimal CalcularCustoTotal()
    {
        var composicao = Composicao.Sum(c => c.Quantidade * c.Ingrediente!.ValorPago + c.Quantidade * c.ItemExtra!.ValorPago);
        return composicao + CustoVariavel + Impostos + TaxaCartao;
    }
    
    public decimal CalcularPrecoVenda(string margemLucro)
    {
        var custoTotal = CalcularCustoTotal();
        var margem = decimal.Parse(margemLucro);
        return custoTotal + (custoTotal * margem / 100);
    }
}