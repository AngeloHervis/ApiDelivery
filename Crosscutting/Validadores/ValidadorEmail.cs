using System.Text.RegularExpressions;

namespace Crosscutting.Validadores;

public static class ValidadorEmail
{
    public static bool EmailEhValido(this string email)
    {
        // Adicionado timeout para evitar DoS - SonarQube
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        TimeSpan timeout = TimeSpan.FromSeconds(1);
        return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, pattern, RegexOptions.None, timeout);
    }
}