using System.Text.Json.Serialization;
using Crosscutting.Enums;

namespace Domain._Base.Events;

public abstract class Event
{
    [JsonIgnore]
    public Guid? AggregateId { get; }
    [JsonIgnore]
    public TipoStoredEvent Tipo { get; }

    protected Event(Guid? aggregateId = default, TipoStoredEvent tipo = TipoStoredEvent.Dominio)
    {
        AggregateId = aggregateId;
        Tipo = tipo;
    }
}