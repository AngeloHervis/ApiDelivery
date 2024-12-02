using System.Linq.Expressions;

namespace Domain._Base.Interfaces;

public interface IRepositoryBase<TItem>
{
    Task<IEnumerable<TItem>> BuscarAsync(Expression<Func<TItem, bool>> predicate, CancellationToken cancellationToken);
    Task<TItem?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AdicionarAsync(TItem obj);
    Task AdicionarESalvarAsync(TItem obj);
    void Adicionar(TItem obj);
    Task AdicionarMuitosAsync(IEnumerable<TItem> entidades);
    void Remover(TItem obj);
    void RemoverMuitos(IEnumerable<TItem> entidades); 
    void Salvar();
    Task SalvarAsync();
}