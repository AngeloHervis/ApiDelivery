using Crosscutting.Exception;
using Domain._Base.Models;
using Domain.Comidas.Interfaces;

namespace Domain.Comidas.Services;

public class ExcluirProdutoService(IProdutoRepository produtoRepository)
{
    public async Task<CommandResult<Guid>> ExcluirProduto(Guid id, CancellationToken cancellationToken)
    {
        var produto = await produtoRepository.ObterPorIdAsync(id, cancellationToken);

        if (produto == null)
        {
            throw new NotFoundException("Produto não encontrado");
        }

        produtoRepository.Remover(produto);

        return CommandResult<Guid>.Success(id);
    }
}