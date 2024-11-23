using Domain.Comida.Commands;
using FluentValidation;

namespace Domain.Comida.Interfaces;

public interface ICadastrarProdutoCommandValidator : IValidator<CadastrarProdutoCommand> { }