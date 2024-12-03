using Crosscutting.Constantes;
using Domain._Base.Validator;
using Domain.Comidas.Commands;
using Domain.Comidas.Interfaces;
using FluentValidation;

namespace Domain.Comidas.Validators;

public class EditarIngredienteCommandValidator : BaseValidator<EditarIngredienteCommand>,
    IEditarIngredienteCommandValidator
{
    public EditarIngredienteCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.Nome)))
            .MaximumLength(Caracteres.Cem)
            .WithErrorCode(CodigosErro.TamanhoExcedido)
            .WithMessage(c => string.Format(MensagensErroValidacao.TamanhoExcedido, nameof(c.Nome), Caracteres.Cem));

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.Descricao)))
            .MaximumLength(Caracteres.DuzentosECinquenta)
            .WithErrorCode(CodigosErro.TamanhoExcedido)
            .WithMessage(c => string.Format(MensagensErroValidacao.TamanhoExcedido, nameof(c.Descricao), Caracteres.DuzentosECinquenta));

        RuleFor(x => x.Marca)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.Marca)))
            .MaximumLength(Caracteres.Cem)
            .WithErrorCode(CodigosErro.TamanhoExcedido)
            .WithMessage(c => string.Format(MensagensErroValidacao.TamanhoExcedido, nameof(c.Marca), Caracteres.Cem));

        RuleFor(x => x.UnidadeMedida)
            .NotNull()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.UnidadeMedida)));

        RuleFor(x => x.ValorPago)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.ValorPago)))
            .GreaterThan(0)
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(c => string.Format(MensagensErroValidacao.ValorMaiorQueZero, nameof(c.ValorPago)));

        RuleFor(x => x.Quantidade)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.Quantidade)))
            .GreaterThan(0)
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(c => string.Format(MensagensErroValidacao.ValorMaiorQueZero, nameof(c.Quantidade)));

        RuleFor(x => x.Ativo)
            .NotNull()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.Ativo)));
    }
}