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

       

        public async Task<Usuario?> GetUsuarioByCorreoAsync(string correo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
        }

       
    }
}
