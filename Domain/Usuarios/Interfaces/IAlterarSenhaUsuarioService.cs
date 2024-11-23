using Crosscutting.Dto.Usuario;

namespace Domain.Usuarios.Interfaces;

public interface IAlterarSenhaUsuarioService
{
    Task<AlterarSenhaUsuarioRespostaDto> AlterarSenhaAsync(AlterarSenhaUsuarioRequisicaoDto dtoUsuarioRequisicao);
}