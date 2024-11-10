using Crosscutting.Dto.Usuario;
using Crosscutting.Enums;
using Crosscutting.UsuarioLogado;

namespace Crosscutting.Interfaces;

public interface IUsuarioLogadoService
{
    string ObterUsuarioId();
    string ObterEmailUsuario();
    TipoUsuario ObterTipoUsuario();
    string ObterNomeUsuario();
    InformacoesDoUsuarioDto ObterInformacoesDoUsuario();
}