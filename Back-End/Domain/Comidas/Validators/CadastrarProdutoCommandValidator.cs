using Crosscutting.Constantes;
using Crosscutting.Enums;
using Domain._Base.Validator;
using Domain.Comidas.Commands;
using Domain.Comidas.Interfaces;
using FluentValidation;

namespace Domain.Comidas.Validators;

public class CadastrarProdutoCommandValidator : BaseValidator<CadastrarProdutoCommand>,
    ICadastrarProdutoCommandValidator
{
    public CadastrarProdutoCommandValidator()
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

        RuleFor(x => x.ValorVenda)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.ValorVenda)))
            .GreaterThan(0)
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(c => string.Format(MensagensErroValidacao.ValorMaiorQueZero, nameof(c.ValorVenda)))
            .GreaterThan(x => x.ValorPago)
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(c => string.Format(MensagensErroValidacao.ValorVendaMaiorQueValorPago, nameof(c.ValorVenda),
                nameof(c.ValorPago)));
        
        RuleFor(x => x.Ativo)
            .NotNull()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.Ativo)));

        RuleFor(x => x.CustoVariavel)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.CustoVariavel)))
            .GreaterThanOrEqualTo(0)
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(c => string.Format(MensagensErroValidacao.ValorNaoNegativo, nameof(c.CustoVariavel)));

        RuleFor(x => x.Impostos)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.Impostos)))
            .GreaterThanOrEqualTo(0)
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(c => string.Format(MensagensErroValidacao.ValorNaoNegativo, nameof(c.Impostos)));

        RuleFor(x => x.TaxaCartao)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(c => string.Format(MensagensErroValidacao.CampoObrigatorio, nameof(c.TaxaCartao)))
            .GreaterThanOrEqualTo(0)
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(c => string.Format(MensagensErroValidacao.ValorNaoNegativo, nameof(c.TaxaCartao)));

        RuleFor(x => x.Composicao)
            .NotEmpty()
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(MensagensErroValidacao.ComposicaoVazia)
            .Must(c => c.TrueForAll(i => i.Quantidade > 0))
            .WithErrorCode(CodigosErro.ValorInvalido)
            .WithMessage(MensagensErroValidacao.QuantidadeMaiorQueZeroParaCadaItem)
            .Must(c => c.Exists(i => i.TipoItem == TipoItem.Ingrediente))
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(MensagensErroValidacao.ComposicaoDeveConterIngrediente)
            .Must(c => c.Exists(i => i.TipoItem == TipoItem.ItemExtra))
            .WithErrorCode(CodigosErro.CampoObrigatorio)
            .WithMessage(MensagensErroValidacao.ComposicaoDeveConterItemExtra)
            .Must(c => c.GroupBy(i => i.IngredienteId).All(g => g.Count() == 1))
            .WithErrorCode(CodigosErro.Duplicidade)
            .WithMessage(MensagensErroValidacao.ItemDuplicadoNaComposicao);
    }
}