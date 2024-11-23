using System.Text.RegularExpressions;
using Crosscutting.Constantes;

namespace Crosscutting.Validadores;

public static class ValidadorSenha
{
    public static bool SenhaInvalida(string senha)
    {
        return string.IsNullOrEmpty(senha) ||
               !Regex.Match(
                   senha,
                   PadroesRegex.Senha,
                   RegexOptions.None,
                   ValoresPadrao.RegexTimeout
               ).Success;
    }
}