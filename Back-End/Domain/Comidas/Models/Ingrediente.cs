using Crosscutting.Enums;
using Domain._Base.Models;
using Domain.Comida.Commands;
using Domain.Comidas.Commands;

namespace Domain.Comida.Models;

public class Ingrediente : Entidade
{
    public TipoItem TipoItem { get; set; } = TipoItem.Ingrediente;
    public string Marca { get; set; }
    public int QuantidadeEstoque { get; set; }
    
    public Ingrediente() { }
    
    public Ingrediente(CadastrarIngredienteCommand request)
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