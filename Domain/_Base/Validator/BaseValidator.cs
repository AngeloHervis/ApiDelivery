using Crosscutting.Constantes;
using FluentValidation;
using FluentValidation.Results;

namespace Domain._Base.Validator;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
    {
        if (context.InstanceToValidate is not null) return true;
        
        result.Errors.Add(new ValidationFailure(string.Empty, CodigosErro.EntradaInvalida));

        return false;
    }
}