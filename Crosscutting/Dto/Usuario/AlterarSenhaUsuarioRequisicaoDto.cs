namespace Crosscutting.Dto.Usuario;

public class AlterarSenhaUsuarioRequisicaoDto
{
    public string SenhaAtual { get; set; }
    public string NovaSenha { get; set; }
    public string ConfirmacaoNovaSenha { get; set; }
}