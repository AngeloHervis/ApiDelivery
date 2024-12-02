namespace Crosscutting.Constantes;

public static class MensagensErroValidacao
{
    public const string CampoObrigatorio = "O campo {0} é obrigatório.";
    public const string TamanhoExcedido = "O campo {0} deve ter no máximo {1} caracteres.";
    public const string ValorMaiorQueZero = "O campo {0} deve ser maior que zero.";
    public const string ValorNaoNegativo = "O campo {0} não pode ser negativo.";
    public const string ValorVendaMaiorQueValorPago = "O campo {0} deve ser maior que o campo {1}.";
    public const string ComposicaoVazia = "A composição deve conter pelo menos um item.";
    public const string QuantidadeMaiorQueZeroParaCadaItem = "Cada item da composição deve ter uma quantidade maior que zero.";
    public const string ComposicaoDeveConterIngrediente = "A composição deve incluir pelo menos um ingrediente.";
    public const string ItemDuplicadoNaComposicao = "A composição não pode conter itens duplicados.";
}