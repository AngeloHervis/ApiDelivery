using Crosscutting.Constantes;

namespace Crosscutting.Dto.Usuario;

public class AlterarSenhaUsuarioRespostaDto
{
    public string Codigo { get; set; }
    public string Mensagem { get; set; }

    public static AlterarSenhaUsuarioRespostaDto ComSucesso()
        => new();

    public static AlterarSenhaUsuarioRespostaDto ComFalhaSenhasDiferentes()
        => new()
        {
            Codigo = CodigosErro.SenhasNaoBatem,
            Mensagem = MensagensErro.SenhasNaoBatem
        };

    public static AlterarSenhaUsuarioRespostaDto ComFalhaNovaSenhaInvalida()
        => new()
        {
            Codigo = CodigosErro.EntradaInvalida,
            Mensagem = MensagensErro.EntradaInvalida
        };
    

    public static AlterarSenhaUsuarioRespostaDto ComFalhaSenhaAntigaErrada()
        => new()
        {
            Codigo = CodigosErro.SenhaErrada,
            Mensagem = MensagensErro.SenhaErrada
        };
}