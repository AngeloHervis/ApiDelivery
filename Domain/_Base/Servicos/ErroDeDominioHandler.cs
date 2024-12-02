using Crosscutting.Constantes;
using Crosscutting.Interfaces.Log;
using Domain._Base.Interfaces;
using Domain._Base.Models;

namespace Domain._Base.Servicos;

public class ErroDeDominioHandler : IErroDeDominioHandler
{
    private readonly ILoggerServicosDeDominio _logger;
    
    public ErroDeDominioHandler(ILoggerServicosDeDominio logger)
    {
        _logger = logger;
    }
    
    private List<ErroDeDominio> Erros { get; set; } = new();

    public void AdicionarErro(ErroDeDominio erro)
    {
        Erros.Add(erro);
    }
    
    public List<ErroDeDominio> ObterErros() => Erros.ToList();

    public bool TemErros() => Erros.Count != 0;

    public void LimparErros()
    {
        Erros = new List<ErroDeDominio>();
    }

    public void LogarErros()
    {
        foreach (var erro in Erros)
        {
            _logger.LogErroSemException<ErroDeDominioHandler>(string.Format(MensagensLogErro.ErroDeDominio, erro.Codigo,
                erro.Mensagem));
        }
    }
}