namespace Crosscutting.Constantes;

public static class PadroesRegex
{
    public const string Email = @"^(?i)[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$";
    public const string SomenteNumeros = "^[0-9]*$";
    public const string DiferenteDeNumeros = @"[^\d]";
    public const string Senha = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()\-_=+\\[\]{};:'""<>,.?\\/\\|]).{13,}$";
    public const string FormatoPermitidoCnpjCpf = @"(^\d{3}\d{3}\d{3}\d{2}$)|(^\d{2}\d{3}\d{3}\d{4}\d{2}$)";
}