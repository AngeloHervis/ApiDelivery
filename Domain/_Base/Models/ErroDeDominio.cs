namespace Domain._Base.Models;

public class ErroDeDominio
{
    public string Codigo { get; set; }
    public string Mensagem { get; set; }

    public ErroDeDominio(string codigo, string mensagem)
    {
        Codigo = codigo;
        Mensagem = mensagem;
    }
}