using Crosscutting.Constantes;
using Crosscutting.Interfaces.Log;
using Domain._Base.Events;
using FluentValidation;
using MediatR;

namespace Domain._Base.Handlers;

public abstract class BaseCommandHandler<TRequest, THandler>(
    ILoggerServicosDeDominio logger,
    IPublisher publisher,
    IValidator<TRequest> validator)
    : IRequestHandler<TRequest>
    where TRequest : IRequest
    where THandler : IRequestHandler<TRequest>
{
    public async Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) 
        {
            await publisher.Publish(new ErroDeDominioEvent(validationResult), cancellationToken);
            logger.LogAvisoComDados<THandler>(MensagensLogAviso.RequestBloqueadoPorValidador, request);
            return;
        }

        await HandleValido(request, cancellationToken);
    }

    protected abstract Task HandleValido(TRequest request, CancellationToken cancellationToken);
}