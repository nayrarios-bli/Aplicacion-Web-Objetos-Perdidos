using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ObjetosPerdidosContext _context;

        public UsuarioRepository(ObjetosPerdidosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(uint id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetUsuarioByCorreoAsync(string correo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
        }

        public async Task InsertUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
