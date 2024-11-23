using System.Text.Json;
using Crosscutting.Interfaces;
using Domain._Base.Events;
using Domain._Base.Interfaces;
using Infra.Data.Repository.EventSourcing;

namespace Infra.Data.EventSourcing;

public class EventStore : IEventStore
{
    private readonly IStoredEventRepository _repository;
    private readonly IUsuarioLogadoService _usuarioLogadoService;

    public EventStore(
        IStoredEventRepository repository,
        IUsuarioLogadoService usuarioLogadoService)
    {
        _repository = repository;
        _usuarioLogadoService = usuarioLogadoService;
    }

    public void Save<T>(T @event) where T : Event
    {
        var dadosSerializados = JsonSerializer.Serialize(
            @event, @event.GetType(),
            options: new JsonSerializerOptions { WriteIndented = true });

        var usuario = _usuarioLogadoService.ObterUsuarioId();

        var storedEvent = new StoredEvent
        (
            @event,
            dadosSerializados,
            usuario
        );

        _repository.Salvar(storedEvent);
    }

    public async Task SalvarMuitosAsync<T>(IEnumerable<T> eventos) where T : Event
    {
        var usuario = _usuarioLogadoService.ObterUsuarioId();
        var options = new JsonSerializerOptions { WriteIndented = true };

        await _repository.SalvarMuitosAsync(eventos
            .Select(@event => new StoredEvent(
                @event,
                JsonSerializer.Serialize(@event, @event.GetType(), options: options),
                usuario
            )));
    }
}