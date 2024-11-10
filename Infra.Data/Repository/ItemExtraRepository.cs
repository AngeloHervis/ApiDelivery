﻿using Crosscutting.Paginacao;
using Domain.Comida.Interfaces;
using Domain.Comida.Models;
using Infra.Data.Repository._Base;

namespace Infra.Data.Repository;

public class ItemExtraRepository : RepositoryBase<ItemExtra>, IItemExtraRepository
{
    public ItemExtraRepository(DeliveryDbContext context) : base(context)
    {
    }

    public Task<RespostaPaginacao<ItemExtra>> ConsultarItemExtraAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}