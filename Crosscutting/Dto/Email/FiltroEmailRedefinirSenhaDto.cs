namespace Crosscutting.Dto.Email;

public class FiltroEmailRedefinirSenhaDto
{
    public static string Assunto => "Redefinição de Senha";
    public string TemplateRedefinicao { get; set; } = "Olá, {0}! Clique no link para redefinir sua senha.";
    public string LinkRedefinicao { get; set; }

    public string ObterMensagemFormatada(string nomeUsuario)
    {
        return string.Format(TemplateRedefinicao, nomeUsuario) + $"\n{LinkRedefinicao}";
    }
}
