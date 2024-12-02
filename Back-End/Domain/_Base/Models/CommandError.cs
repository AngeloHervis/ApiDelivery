namespace Domain._Base.Models;

public class CommandError(string code, string message)
{
    public string Code { get; set; } = code;
    public string Message { get; set; } = message;
}