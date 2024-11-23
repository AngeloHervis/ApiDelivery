namespace Crosscutting.Dto.Usuario;

public class RedefinirSenhaRespostaDto
{
    public string Codigo { get; set; }
    public string Mensagem { get; set; }
    public bool Sucesso { get; set; }

    public RedefinirSenhaRespostaDto() { }
    
    public RedefinirSenhaRespostaDto(string codigo, string mensagem, bool sucesso)
    {
        Codigo = codigo;
        Mensagem = mensagem;
        Sucesso = sucesso;
    }
}