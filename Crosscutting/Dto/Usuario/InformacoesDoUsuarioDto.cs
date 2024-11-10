using Crosscutting.Enums;

namespace Crosscutting.Dto.Usuario;

public class InformacoesDoUsuarioDto
{
    public string Id { get; init; }
    public string Nome { get; init; }
    public string Email { get; init; }
    public TipoUsuario TipoUsuario { get; init; }
}