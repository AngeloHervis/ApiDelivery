using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Crosscutting.Enums;

namespace Domain._Base.Models;

public abstract class Entidade
{
    protected Entidade() => Id = Guid.NewGuid();
    
    public Guid Id { get; }
    
    [Column("nome", TypeName = "varchar(250)"), Required]
    public string Nome { get; set; }
    
    [Column("descricao", TypeName = "varchar(250)"), Required]
    public string Descricao { get; set; }
    
    [Column("unidade_medida", TypeName = "varchar(250)"), Required]
    public UnidadeMedida UnidadeMedida { get; set; }
    
    [Column("valor_pago", TypeName = "decimal(10, 2)")]
    public decimal ValorPago { get; set; }
    
    [Column("ativo", TypeName = "bit")]
    public bool Ativo { get; set; }
    
    [Column("data_cadastro", TypeName = "datetime(6)"), Required]
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
    public override bool Equals(object obj)
    {
        var compareTo = obj as Entidade;

        if (ReferenceEquals(this, compareTo)) return true;

        return compareTo is not null && Id.Equals(compareTo.Id);
    }
    
    public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();
}