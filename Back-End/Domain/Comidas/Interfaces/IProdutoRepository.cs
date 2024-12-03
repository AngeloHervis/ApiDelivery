using Domain._Base.Interfaces;
using Domain.Comida.Models;
using Domain.Comidas.Models;

namespace Domain.Comidas.Interfaces;

public interface IProdutoRepository : IRepositoryBase<Produto>
{
    Task<List<Produto>> ConsultarProdutosAsync(CancellationToken cancellationToken);
}