using Domain.Comida.Commands;
using Domain.Comidas.Commands;
using FluentValidation;

namespace Domain.Comida.Interfaces;

public interface ICadastrarIngredienteCommandValidator : IValidator<CadastrarIngredienteCommand> { }