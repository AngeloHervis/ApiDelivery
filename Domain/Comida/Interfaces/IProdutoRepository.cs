using Crosscutting.Paginacao;
using Domain._Base.Interfaces;
using Domain.Comida.Models;

namespace Domain.Comida.Interfaces;

public interface IProdutoRepository : IRepositoryBase<Produto>
{
    Task<RespostaPaginacao<Produto>> ConsultarProdutosIfoodAsync(CancellationToken cancellationToken);
}