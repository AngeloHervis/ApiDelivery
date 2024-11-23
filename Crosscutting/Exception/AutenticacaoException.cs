using System.Security;
using Crosscutting.Constantes;

namespace Crosscutting.Exception;

public class AutenticacaoException : SecurityException
{
    public AutenticacaoException() : base(MensagensErro.CredenciaisInvalidas) 
    { }
    
    public AutenticacaoException(string message) : base(message) 
    { }
    
    public AutenticacaoException(string message, System.Exception innerException) : base(message, innerException) 
    { }
}