using System.ComponentModel.DataAnnotations;

namespace Crosscutting.Paginacao;

public abstract class FiltroPaginacao
{
    [Required]
    public int PaginaAtual { get; set; } = 1;
    [Required]
    public int TamanhoPagina { get; set; } = 12;
}