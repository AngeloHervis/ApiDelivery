using System.Text.Json.Serialization;
using Crosscutting.Enums;
using Crosscutting.Exception;
using Domain._Base.Models;
using Domain.Comidas.Interfaces;
using MediatR;

namespace Domain.Comidas.Commands;

public class EditarIngredienteCommand : IRequest<CommandResult<Guid>>
{
    [JsonIgnore] public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public decimal ValorPago { get; set; }
    public int Quantidade { get; set; }
    public bool Ativo { get; set; }
    public string Marca { get; set; }
}

public class EditarIngredienteCommandHandler(
    IIngredienteRepository ingredienteRepository,
    IEditarIngredienteCommandValidator validator
) : IRequestHandler<EditarIngredienteCommand,
        CommandResult<Guid>>
{
    public async Task<CommandResult<Guid>> Handle(EditarIngredienteCommand request, CancellationToken cancellationToken)
    {
        var ingrediente = await ingredienteRepository.ObterPorIdAsync(request.Id, cancellationToken);

        if (ingrediente == null)
        {
            throw new NotFoundException("Ingrediente não encontrado");
        }

        ingrediente.Nome = request.Nome;
        ingrediente.Descricao = request.Descricao;
        ingrediente.UnidadeMedida = request.UnidadeMedida;
        ingrediente.ValorPago = request.ValorPago;
        ingrediente.Quantidade = request.Quantidade;
        ingrediente.Ativo = request.Ativo;
        ingrediente.Marca = request.Marca;
        
        ingrediente.Atualizar(request);
        await ingredienteRepository.SalvarAsync();

        return CommandResult<Guid>.Success(ingrediente.Id);
    }
}