using Domain._Base.Events;

namespace Domain._Base.Interfaces;

public interface IEventStore
{
    void Save<T>(T @event) where T : Event;
    Task SalvarMuitosAsync<T>(IEnumerable<T> eventos) where T : Event;
}