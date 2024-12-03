using Crosscutting.Enums;
using Domain._Base.Models;
using Domain.Comidas.Commands;

namespace Domain.Comidas.Models;

public class Ingrediente : Entidade
{
    public TipoItem TipoItem { get; set; } = TipoItem.Ingrediente;
    public string Marca { get; set; }
    public int Quantidade { get; set; }
    
    public Ingrediente() { }
    
    public Ingrediente(CadastrarIngredienteCommand request)
    {
        Nome = request.Nome;
        Descricao = request.Descricao;
        UnidadeMedida = request.UnidadeMedida;
        ValorPago = request.ValorPago;
        Quantidade = request.Quantidade;
        Ativo = request.Ativo;
        Marca = request.Marca;
    }
    
    public void Atualizar(EditarIngredienteCommand request)
    {
        Nome = request.Nome;
        Descricao = request.Descricao;
        UnidadeMedida = request.UnidadeMedida;
        ValorPago = request.ValorPago;
        Quantidade = request.Quantidade;
        Ativo = request.Ativo;
        Marca = request.Marca;
    }
}