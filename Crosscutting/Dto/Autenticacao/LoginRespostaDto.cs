using Crosscutting.Constantes;

namespace Crosscutting.Dto.Autenticacao;

public class LoginRespostaDto
{
    public string Codigo { get; private init; }
    public string Mensagem { get; private init; }
    public string Token { get; private init; }

    public static LoginRespostaDto ComSucesso(string token)
        => new()
        {
            Token = token
        };

    public static LoginRespostaDto ComFalhaEmailDoUsuarioInvalido()
        => new()
        {
            Codigo = CodigosErroLogin.FalhaEmailDoUsuarioInvalido,
            Mensagem = MensagensErro.FalhaEmailDoUsuarioInvalido
        };

    public static LoginRespostaDto ComFalhaNaAutenticacao()
        => new()
        {
            Codigo = CodigosErroLogin.FalhaAutenticacao,
            Mensagem = MensagensErro.FalhaAutenticacao
        };

    public static LoginRespostaDto ComFalhaNaAutenticacaoDoRefreshToken()
        => new()
        {
            Codigo = CodigosErroLogin.FalhaAutenticacaoDoRefreshToken,
            Mensagem = MensagensErro.FalhaAutenticacaoDoRefreshToken
        };

    public static LoginRespostaDto ComFalhaNoRefreshToken()
        => new()
        {
            Codigo = CodigosErroLogin.FalhaRefreshToken,
            Mensagem = MensagensErro.FalhaRefreshToken
        };

    public static LoginRespostaDto ComFalhaNoLoginSenhaInvalida()
        => new()
        {
            Codigo = CodigosErroLogin.FalhaLoginSenhaInvalida,
            Mensagem = MensagensErro.FalhaLoginSenhaInvalida
        };
}