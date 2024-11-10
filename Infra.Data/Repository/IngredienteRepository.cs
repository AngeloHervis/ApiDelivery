using Crosscutting.Paginacao;
using Domain.Comida.Interfaces;
using Domain.Comida.Models;
using Infra.Data.Repository._Base;

namespace Infra.Data.Repository;

public class IngredienteRepository : RepositoryBase<Ingrediente>, IIngredienteRepository
{
    public IngredienteRepository(DeliveryDbContext context) : base(context)
    {
    }

    public Task<RespostaPaginacao<Ingrediente>> ConsultarIngredientesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}