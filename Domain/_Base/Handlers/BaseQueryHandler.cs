using Crosscutting.Constantes;
using Crosscutting.Interfaces.Log;
using Domain._Base.Events;
using FluentValidation;
using MediatR;

namespace Domain._Base.Handlers;

public abstract class BaseQueryHandler<TRequest, TResponse, THandler>(
    ILoggerServicosDeDominio logger,
    IPublisher publisher = null,
    IValidator<TRequest> validator = null)
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where THandler : IRequestHandler<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var requestValido = await ValidarRequest(request, cancellationToken);

        if (!requestValido) return default;

        return await HandleValido(request, cancellationToken);
    }

    protected abstract Task<TResponse> HandleValido(TRequest request, CancellationToken cancellationToken);

    private async Task<bool> ValidarRequest(TRequest request, CancellationToken cancellationToken)
    {
        if (validator == null) return true;

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid) return true;

        await publisher.Publish(new ErroDeDominioEvent(validationResult), cancellationToken);
        logger.LogAvisoComDados<THandler>(MensagensLogAviso.RequestBloqueadoPorValidador, request);
        return false;
    }
}