using Domain._Base.Models;

namespace Domain._Base.Interfaces;

public interface IErroDeDominioHandler
{
    void AdicionarErro(ErroDeDominio erro);
    List<ErroDeDominio> ObterErros();
    bool TemErros();
    void LimparErros();
    void LogarErros();
}