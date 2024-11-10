using Crosscutting.DataHora;

namespace Domain._Base.Events;

public class StoredEvent : Event
{
    protected StoredEvent() { }

    public StoredEvent(Event @event, string dados, string usuario) : base(@event.AggregateId, @event.Tipo)
    {
        Id = Guid.NewGuid();
        Nome = @event.GetType().Name;
        Data = PadroesDataHora.Agora;
        Usuario = usuario;
        Dados = dados;
    }
    
    public Guid Id { get; }
    public string Dados { get; }
    public DateTime Data { get; }
    public string Nome { get; }
    public string Usuario { get; }
}