using FluentValidation.Results;
using MediatR;

namespace Domain._Base.Events;

public class ErroDeDominioEvent : Event, INotification
{
    public ValidationResult ValidationResult { get; }

    public ErroDeDominioEvent(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }
    
    public ErroDeDominioEvent(string name, string errorMensage, string errorCode = "")
    {
        var validationResult = new ValidationResult
        {
            Errors = [new ValidationFailure(name, errorMensage)
            {
                ErrorCode = errorCode
            }]
        };
        
        ValidationResult = validationResult;
    }
}