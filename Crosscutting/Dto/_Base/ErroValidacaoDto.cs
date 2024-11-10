namespace Crosscutting.Dto._Base;

public class ErroValidacaoDto(string code, string message)
{
    public string Code { get; set; } = code;
    public string Message { get; set; } = message;
}