using Crosscutting.Constantes;
using Crosscutting.Dto._Base;
using Crosscutting.Enums;
using Crosscutting.Exception;
using Crosscutting.Interfaces.Log;
using Domain.Comida.Interfaces;
using Domain.Comida.Models;
using Domain.Comidas.Interfaces;
using Domain.Comidas.Models;
using MediatR;

namespace Domain.Comidas.Commands;

public class CadastrarIngredienteCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public decimal ValorPago { get; set; }
    public int Quantidade { get; set; }
    public bool Ativo { get; set; }
    public string Marca { get; set; }

    public class CadastrarIngredienteCommandHandler(
        ILoggerServicosDeDominio logger,
        IIngredienteRepository repository,
        ICadastrarIngredienteCommandValidator validator)
        : IRequestHandler<CadastrarIngredienteCommand, Guid>
    {
        public async Task<Guid> Handle(CadastrarIngredienteCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogAvisoComDados<CadastrarIngredienteCommandHandler>(
                    MensagensLogAviso.RequestBloqueadoPorValidador, request);
                var erros = validationResult.Errors.Select(e => new ErroValidacaoDto(e.ErrorCode, e.ErrorMessage))
                    .ToList();
                throw new ValidacaoException(CodigosErro.ErroDeValidacao, erros);
            }

            try
            {
                var ingrediente = new Ingrediente(request);
                await repository.AdicionarESalvarAsync(ingrediente);
                return ingrediente.Id;
            }
            catch (Exception e)
            {
                logger.LogErroComDados<CadastrarIngredienteCommandHandler>(e, e.Message, request);
                throw;
            }
        }
    }
}