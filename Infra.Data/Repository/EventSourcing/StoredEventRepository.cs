// using Domain._Base.Events;
// using Microsoft.EntityFrameworkCore;
//
// namespace Infra.Data.Repository.EventSourcing;
//
// public class StoredEventRepository : IStoredEventRepository
// {
//     private readonly EventStoreContext _context;
//
//     public StoredEventRepository(EventStoreContext context)
//     {
//         _context = context;
//     }
//
//     public async Task<IList<StoredEvent>> ObterTodos(Guid aggregateId)
//     {
//         return await (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToListAsync();
//     }
//
//     public void Salvar(StoredEvent @event)
//     {
//         _context.StoredEvents.Add(@event);
//         _context.SaveChanges();
//     }
//
//     public async Task SalvarMuitosAsync(IEnumerable<StoredEvent> eventos)
//     {
//         await _context.StoredEvents.AddRangeAsync(eventos);
//         await _context.SaveChangesAsync();
//     }
// }