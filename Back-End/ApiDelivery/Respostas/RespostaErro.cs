namespace ApiDelivery.Respostas;

/// <summary>
/// </summary>
public class RespostaErro
{
    /// <summary>
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// </summary>
    public RespostaErro() { }

    /// <summary>
    /// </summary>
    public RespostaErro(string code, string message)
    {
        Code = code;
        Message = message;
    }
}