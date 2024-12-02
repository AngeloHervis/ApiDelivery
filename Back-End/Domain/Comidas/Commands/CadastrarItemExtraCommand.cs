using Crosscutting.Constantes;
using Crosscutting.Dto._Base;
using Crosscutting.Enums;
using Crosscutting.Exception;
using Crosscutting.Interfaces.Log;
using Domain.Comida.Interfaces;
using Domain.Comida.Models;
using MediatR;

namespace Domain.Comida.Commands;

public class CadastrarItemExtraCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public decimal ValorPago { get; set; }
    public int QuantidadeEstoque { get; set; }
    public bool Ativo { get; set; }
    public string Marca { get; set; }
    
    public class CadastrarItemExtraCommandHandler(
        ILoggerServicosDeDominio logger,
        IItemExtraRepository repository, 
        ICadastrarItemExtraCommandValidator validator)
    : IRequestHandler<CadastrarItemExtraCommand, Guid>
    {
        public async Task<Guid> Handle(CadastrarItemExtraCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogAvisoComDados<CadastrarItemExtraCommandHandler>(MensagensLogAviso.RequestBloqueadoPorValidador, request);
                var erros = validationResult.Errors.Select(e => new ErroValidacaoDto(e.ErrorCode, e.ErrorMessage)).ToList();
                throw new ValidacaoException(CodigosErro.ErroDeValidacao, erros);
            }

            try
            {
                var itemExtra = new ItemExtra(request);
                await repository.AdicionarESalvarAsync(itemExtra);
                return itemExtra.Id;
            }
            catch (Exception e)
            {
                logger.LogErroComDados<CadastrarItemExtraCommandHandler>(e, e.Message, request);
                throw;
            }
        }
    }
}