using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Crosscutting.Constantes;
using Crosscutting.Helpers;

namespace Crosscutting.Extensions;

public static class StringExtension
{
    public static string ToSnakeCase(this string text, bool isAcronym = false)
    {
        ArgumentNullException.ThrowIfNull(text);

        if (text.Length < 2)
        {
            return text.ToUpper();
        }

        if (isAcronym)
        {
            return text.ToLower();
        }

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));

        foreach (var c in text[1..])
        {
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
                continue;
            }

            sb.Append(c);
        }

        return sb.ToString();
    }

    public static string ToUpperSnakeCase(this string text, bool isAcronym = false)
        => text.ToSnakeCase().ToUpper();

    /// <summary>
    /// Obtém o UserId a partir do e-mail do funcionário da Plaenge.
    /// O UserID é igual à parte do e-mail antes do @, em CAPS
    /// Por exemplo, db1.dev@plaenge.com.br tem o UserID = DB1.DEV
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static string ExtractUserIdFromEmail(this string email)
        => email.Substring(0, email.IndexOf('@')).ToUpper();

    /// <summary>
    /// Verifica se a string é nula ou vazia.
    /// Se não for, retorna a própria string.
    /// Se for, retorna o defaultValue
    /// </summary>
    /// <param name="value">A string original</param>
    /// <param name="defaultValue">Por padrão, é null</param>
    /// <returns>A própria string se ela não for nula ou vazia, defaultValue se ela for</returns>
    public static string ValueOrDefault(this string value, string defaultValue = null)
        => string.IsNullOrEmpty(value) ? defaultValue : value;

    /// <summary>
    /// Remove todos os caracteres classificados como espaço em branco.
    /// </summary>
    public static string RemoverEspacosEmBranco(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        return new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }

    /// <summary>
    /// Remove espaços em branco em volta, remove acentuação, coloca tudo em maiúscula
    /// </summary>
    public static string Normalizar(this string value)
        => value?.Trim().TransformarEmAscii().ToUpper();

    public static bool ContemSomenteNumeros(this string input)
    {
        return input == null ||
               Regex.IsMatch(input, PadroesRegex.SomenteNumeros, RegexOptions.None, ValoresPadrao.RegexTimeout);
    }

    /// <summary>
    /// Limpa strings numéricas, removendo caracteres que não sejam dígitos.
    /// </summary>
    /// <returns></returns>
    public static string RemoverNaoNumericos(this string input)
    {
        return input == null
            ? null
            : Regex.Replace(input, PadroesRegex.DiferenteDeNumeros, string.Empty, RegexOptions.None,
                ValoresPadrao.RegexTimeout);
    }

    /// <summary>
    /// Substitui os caracteres de uma senha por asteriscos, para fins de logging.
    /// </summary>
    public static string OcultarSenha(this string senha)
    {
        return string.IsNullOrWhiteSpace(senha)
            ? senha
            : new string('*', senha.Length);
    }

    /// <summary>
    /// Oculta parte do CPF ou CNPJ. Entrada e saída sem máscara.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <example>
    /// 12345678911 -> 123*****911
    /// 12345678000199 -> 12******000199
    /// </example>
    public static string OcultarCpfCnpj(this string value)
    {
        if (string.IsNullOrEmpty(value) || !value.ContemSomenteNumeros())
        {
            return value;
        }

        return value.Length switch
        {
            11 => value[..3] + new string('*', 5) + value[8..11],
            14 => value[..2] + new string('*', 6) + value[8..14],
            _ => value
        };
    }

    public static string AdicionarMascaraCpfCnpj(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        return value.Length switch
        {
            11 => value.Insert(3, ".").Insert(7, ".").Insert(11, "-"),
            14 => value.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-"),
            _ => value
        };
    }

    /// <summary>
    /// Adiciona uma extensão ao nome do arquivo, se ele já não tiver a extensão correta.
    /// </summary>
    /// <param name="nome">O nome do arquivo.</param>
    /// <param name="extensao">A extensão a ser adicionada.</param>
    /// <returns>O nome do arquivo com a extensão correta.</returns>
    public static string AdicionarExtensao(this string nome, string extensao)
    {
        var nomeArquivo = string.IsNullOrWhiteSpace(nome)
            ? extensao
            : nome;

        var extensaoArquivo = $".{extensao.ToLower()}";

        return nomeArquivo.EndsWith($"{extensaoArquivo}", StringComparison.OrdinalIgnoreCase)
            ? nomeArquivo
            : $"{nomeArquivo}{extensaoArquivo}";
    }

    /// <summary>
    /// Retorna a extensão do arquivo, em letras maiúsculas, sem o ponto.
    /// </summary>
    /// <param name="input">String original</param>
    /// <returns>Extensão em forma IFS</returns>
    public static string ObterExtensaoFormatoIfs(this string input)
    {
        var extensao = Path.GetExtension(input);

        return string.IsNullOrWhiteSpace(extensao)
            ? extensao
            : extensao.ToUpper().Replace(".", "");
    }

    /// <summary>
    /// Remove caracteres inválidos de um nome de arquivo.
    /// </summary>
    public static string LimparNomeDeArquivo(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var caracteresInvalidos = Path.GetInvalidFileNameChars();

        return new string(input.ToCharArray()
            .Where(c => !caracteresInvalidos.Contains(c))
            .ToArray());
    }

    /// <summary>
    /// Converte a string para Title Case.
    /// </summary>
    /// <param name="input">Input string.</param>
    /// <returns>Output string.</returns>
    public static string ToTitleCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        var textInfo = CultureInfo.CurrentCulture.TextInfo;

        return textInfo.ToTitleCase(input.ToLower());
    }

    public static bool IsNotNull(this string referenciaDocumento)
        => !string.IsNullOrEmpty(referenciaDocumento);
}