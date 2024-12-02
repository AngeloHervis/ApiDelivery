using System.Text.RegularExpressions;
using Crosscutting.Constantes;

namespace Crosscutting.Extensions;

public static class CnpjCpfExtensions
{
    private const int TamanhoPermitidoCpf = 11;
    private static readonly int[] MultiplicadorCpf1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] MultiplicadorCpf2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

    public static bool EhCpfValido(this string cpf)
    {
        var cpfSemMascara = cpf.Trim().Replace(".", "").Replace("-", "");
        return EhValido(cpfSemMascara, MultiplicadorCpf1, MultiplicadorCpf2, TamanhoPermitidoCpf);
    }

    private static bool EhValido(string cpf, IReadOnlyList<int> multiplicador1, IReadOnlyList<int> multiplicador2,
        int tamanhoPermitido)
    {
        if (!FormatoEhValido(cpf, tamanhoPermitido))
            return false;

        var tamanhoSemDigitoVerificador = tamanhoPermitido - 2;
        var tempCpf = cpf[..tamanhoSemDigitoVerificador];

        var digito = CalcularDigitoVerificador(multiplicador1, tamanhoSemDigitoVerificador, tempCpf);
        tempCpf += digito;

        digito = CalcularDigitoVerificador(multiplicador2, tamanhoSemDigitoVerificador + 1, tempCpf, digito);

        return cpf.EndsWith(digito);
    }

    private static bool FormatoEhValido(string cpf, int tamanhoPermitido)
    {
        if (cpf.Length != tamanhoPermitido)
            return false;

        return cpf.Distinct().Count() != 1 &&
               Regex.IsMatch(cpf, PadroesRegex.FormatoPermitidoCnpjCpf, RegexOptions.None, ValoresPadrao.RegexTimeout);
    }

    private static string CalcularDigitoVerificador(IReadOnlyList<int> multiplicador, int limiteParaPercorrerNoCalculo,
        string tempCpf, string digito = default)
    {
        var soma = 0;
        for (var i = 0; i < limiteParaPercorrerNoCalculo; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        return digito + resto;
    }
}