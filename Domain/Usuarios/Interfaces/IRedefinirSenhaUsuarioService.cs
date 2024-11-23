using Crosscutting.Dto.Email;
using Crosscutting.Dto.Usuario;

namespace Domain.Usuarios.Interfaces;

public interface IRedefinirSenhaUsuarioService
{
    Task<RedefinirSenhaRespostaDto> RedefinirAsync(RedefinirSenhaRequisicaoDto dto, CancellationToken cancellationToken);
}