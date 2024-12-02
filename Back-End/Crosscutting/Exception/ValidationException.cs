using Crosscutting.Dto._Base;

namespace Crosscutting.Exception;

public class ValidacaoException(string message, List<ErroValidacaoDto> erros) : System.Exception(message)
{
    public List<ErroValidacaoDto> Erros { get; init; } = erros;
}