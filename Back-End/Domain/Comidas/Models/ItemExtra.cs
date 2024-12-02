using Crosscutting.Enums;
using Domain._Base.Models;
using Domain.Comida.Commands;

namespace Domain.Comida.Models;

public class ItemExtra : Entidade
{
    public TipoItem TipoItem { get; set; } = TipoItem.ItemExtra;
    public string Marca { get; set; }
    public int QuantidadeEstoque { get; set; }
    
    public ItemExtra() { }
    
    public ItemExtra(CadastrarItemExtraCommand request)
    {
        Nome = request.Nome;
        Descricao = request.Descricao;
        UnidadeMedida = request.UnidadeMedida;
        ValorPago = request.ValorPago;
        QuantidadeEstoque = request.QuantidadeEstoque;
        Ativo = request.Ativo;
        Marca = request.Marca;
    }
}