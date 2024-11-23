using Domain._Base.Events;

namespace Infra.Data.Repository.EventSourcing;

public interface IStoredEventRepository
{
    Task<IList<StoredEvent>> ObterTodos(Guid aggregateId);
    void Salvar(StoredEvent @event);
    Task SalvarMuitosAsync(IEnumerable<StoredEvent> eventos);
}