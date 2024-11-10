using Crosscutting.Constantes;
using Crosscutting.Dto._Base;
using Crosscutting.Dto.Comida;
using Crosscutting.Enums;
using Crosscutting.Exception;
using Crosscutting.Interfaces.Log;
using Domain.Comida.Interfaces;
using Domain.Comida.Models;
using MediatR;

namespace Domain.Comida.Commands;

public class CadastrarProdutoCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public decimal ValorPago { get; set; }
    public decimal ValorVenda { get; set; }
    public int QuantidadePorcao { get; set; }
    public bool Ativo { get; set; }
    public decimal CustoVariavel { get; set; }
    public decimal Impostos { get; set; }
    public decimal TaxaCartao { get; set; }
    public List<ComposicaoDto> Composicao { get; set; } = [];

    public class CadastrarProdutoCommandHandler(
        ILoggerServicosDeDominio logger,
        IProdutoRepository repository,
        ICadastrarProdutoCommandValidator validator)
        : IRequestHandler<CadastrarProdutoCommand, Guid>
    {
        public async Task<Guid> Handle(CadastrarProdutoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) 
            {
                logger.LogAvisoComDados<CadastrarProdutoCommandHandler>(MensagensLogAviso.RequestBloqueadoPorValidador, request);
                var erros = validationResult.Errors.Select(e => new ErroValidacaoDto(e.ErrorCode, e.ErrorMessage)).ToList();
                throw new ValidacaoException(CodigosErro.ErroDeValidacao, erros);
            }

            try
            {
                var produto = new Produto(request);
                await repository.AdicionarESalvarAsync(produto);
                return produto.Id;
            }
            catch (Exception e)
            {
                logger.LogErroComDados<CadastrarProdutoCommandHandler>(e, e.Message, request);
                throw;
            }
        }
    }
}