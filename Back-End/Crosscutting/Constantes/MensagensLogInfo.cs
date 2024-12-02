namespace Crosscutting.Constantes;

public static class MensagensLogInfo
{
    // Autenticação
    public const string GerarTokenParaUsuarioGerouComSucesso = "O token para o usuário {0} foi gerado com sucesso.";
    public const string LoginBemSucedido = "O login foi bem-sucedido para o usuário {0}";
    
    // Usuário
    public const string AlterarSenhaUsuarioFormatoSenhaInvalido = "A alteração de senha do usuário externo de ID {0} falhou devido à nova senha estar em um formato inválido.";
    public const string AlterarSenhaUsuarioSenhasNaoBatem = "A alteração de senha do usuário externo de ID {0} falhou devido às senhas não serem iguais.";
    public const string AlterarSenhaUsuarioBemSucedido = "A alteração de senha do usuário externo de ID {0} foi realizada com sucesso.";
    public const string SenhaRedefinidaComSucesso = "A senha do usuário externo de ID {0} foi redefinida com sucesso.";
}