using System.Linq.Expressions;
using Crosscutting.Paginacao;
using Domain._Base.Interfaces;
using Domain._Base.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository._Base;

public abstract class RepositoryBase<TEntidade> : IRepositoryBase<TEntidade>
    where TEntidade : Entidade
{
    private readonly DbSet<TEntidade> _dbSet;
    private readonly DeliveryDbContext _context;

    protected RepositoryBase(DeliveryDbContext context)
    {
        _dbSet = context.Set<TEntidade>();
        _context = context;
    }
    
    public async Task<bool> ExisteEntidadePorIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbSet.AsQueryable().AnyAsync(e => e.Id.Equals(id), cancellationToken: cancellationToken);

    public async Task<IEnumerable<TEntidade>> BuscarAsync(Expression<Func<TEntidade, bool>> predicate, CancellationToken cancellationToken) =>
        await _dbSet.Where(predicate).ToListAsync(cancellationToken: cancellationToken);

    public async Task<TEntidade> ObterPorIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbSet.FindAsync(id);
    
    public async Task AdicionarAsync(TEntidade obj) => await _dbSet.AddAsync(obj);
    
    public async Task AdicionarESalvarAsync(TEntidade obj)
    {
        await AdicionarAsync(obj);
        await _context.SaveChangesAsync();
    }

    public void Adicionar(TEntidade obj) => _dbSet.Add(obj);

    public async Task AdicionarMuitosAsync(IEnumerable<TEntidade> entidades) => await _dbSet.AddRangeAsync(entidades);

    public void Remover(TEntidade obj) => _dbSet.Remove(obj);
        
    public void RemoverMuitos(IEnumerable<TEntidade> entidades) => _dbSet.RemoveRange(entidades);

    public void Salvar() => _context.SaveChanges();
    public async Task SalvarAsync() => await _context.SaveChangesAsync();

    protected async Task<RespostaPaginacao<T>> PaginarAsync<T>(IQueryable<T> queryable, FiltroPaginacao filtro,
        CancellationToken cancellationToken)
    {
        var totalRegistros = await queryable.CountAsync(cancellationToken);

        if (filtro.PaginaAtual <= 0) filtro.PaginaAtual = 1;
        if (filtro.TamanhoPagina <= 0) filtro.TamanhoPagina = 1;

        var resultado = await queryable
            .Skip((filtro.PaginaAtual - 1) * filtro.TamanhoPagina)
            .Take(filtro.TamanhoPagina)
            .ToListAsync(cancellationToken);

        return new RespostaPaginacao<T>(resultado, filtro.PaginaAtual, filtro.TamanhoPagina, totalRegistros);
    }
}