using Domain.Usuarios;

namespace Domain._Base.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> ObterPorEmailAsync(string email);
    Task AdicionarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
}