namespace Crosscutting.Paginacao;

public class RespostaPaginacao<T>(List<T> dados, int paginaAtual, int tamanhoPagina, int totalRegistros)
{
    public List<T> Dados { get; private set; } = dados;

    public int TotalRegistros { get; } = totalRegistros;
    public int PaginaAtual { get; private set; } = paginaAtual;
    public int TamanhoPagina { get; } = tamanhoPagina;
    public int TotalPaginas => (int)Math.Ceiling(TotalRegistros / (double)TamanhoPagina);
}