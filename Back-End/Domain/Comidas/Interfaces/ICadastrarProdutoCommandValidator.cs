using Domain.Comidas.Commands;
using FluentValidation;

namespace Domain.Comidas.Interfaces;

public interface ICadastrarProdutoCommandValidator : IValidator<CadastrarProdutoCommand> { }