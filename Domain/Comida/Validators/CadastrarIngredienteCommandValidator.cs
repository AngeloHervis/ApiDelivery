using Crosscutting.Constantes;
using Domain._Base.Validator;
using Domain.Comida.Commands;
using Domain.Comida.Interfaces;
using FluentValidation;

namespace Domain.Comida.Validators;

public class CadastrarIngredienteCommandValidator : BaseValidator<CadastrarIngredienteCommand>,
    ICadastrarIngredienteCommandValidator
{
        public CadastrarIngredienteCommandValidator()
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
            .WithMessage(c => string.Format(MensagensErroValidacao.TamanhoExcedido, nameof(c.Descricao),
                Caracteres.DuzentosECinquenta));
        
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
        
        RuleFor(x => x.QuantidadeEstoque)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.QuantidadeEstoque)))
            .GreaterThan(0)
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(c => string.Format(MensagensErroValidacao.ValorMaiorQueZero, nameof(c.QuantidadeEstoque)));
        
        RuleFor(x => x.Ativo)
            .NotNull()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.Ativo)));
    }
}