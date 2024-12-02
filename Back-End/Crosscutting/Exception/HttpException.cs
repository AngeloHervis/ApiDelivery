using System.Net;

namespace Crosscutting.Exception;

public class HttpException : System.Exception
{
    public string Codigo { get; }
    public string Mensagem { get; }
    public List<string> Detalhes { get; }
    public HttpStatusCode HttpStatus { get; }

    public HttpException(string mensagem) : base(mensagem)
    {
        Mensagem = mensagem;
    }
    
    public HttpException(string codigo, string mensagem, HttpStatusCode httpStatusCode) 
        : base(mensagem)
    {
        Codigo = codigo;
        Mensagem = mensagem;
        HttpStatus = httpStatusCode;
    }

    public HttpException(string codigo, string mensagem, List<string> detalhes, HttpStatusCode httpStatusCode) 
        : base(mensagem)
    {
        Codigo = codigo;
        Mensagem = mensagem;
        Detalhes = detalhes;
        HttpStatus = httpStatusCode;
    }
}