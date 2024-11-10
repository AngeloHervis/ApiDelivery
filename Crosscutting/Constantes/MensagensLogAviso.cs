namespace Crosscutting.Constantes;

public static class MensagensLogAviso
{
    public const string NaoEncontrado = "O {0} de ID {1} não foi encontrado.";
    public const string NaoEncontradoSemId = "O {0} não foi encontrado.";
    public const string InconsistenciaEmBanco = "Dados inconsistentes encontrados na tabela {0}. IDs: {1}. Inconsistencia: {2}";
    
    public const string FalhaEmExecucaoDeQuery = "Falha durante a execução da query. Handler: {0}.";
    public const string ConsultaExclusivaUsuarioExterno = "A consulta de {0} falhou pois o usuário logado é interno.";
    public const string RequestBloqueadoPorValidador = "A requisição foi bloqueada pelo validador.";
    public const string NaoHaDadosFiltroEspecificado = "Não foram encontrados dados para o filtro especificado";
}