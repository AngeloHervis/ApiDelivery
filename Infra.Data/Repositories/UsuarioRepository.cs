using Domain._Base.Interfaces;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class UsuarioRepository(DeliveryDbContext context) : IUsuarioRepository
    {
        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return (await context.Usuarios.FirstOrDefaultAsync(u => u.Email == email))!;
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
            await context.SaveChangesAsync();
        }
    }
}