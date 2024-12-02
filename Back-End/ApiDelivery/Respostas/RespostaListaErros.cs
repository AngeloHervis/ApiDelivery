namespace ApiDelivery.Respostas;

/// <summary>
/// </summary>
public class RespostaListaErros
{
    /// <summary>
    /// </summary>
    public string Code { get; init; }
    /// <summary>
    /// </summary>
    public List<RespostaErro> Erros { get; init; }
}