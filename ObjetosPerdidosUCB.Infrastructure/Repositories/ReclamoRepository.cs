using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class ReclamoRepository : IReclamoRepository
    {
        private readonly ObjetosPerdidosContext _context;

        public ReclamoRepository(ObjetosPerdidosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reclamo>> GetAllReclamosAsync()
        {
            return await _context.Reclamos
                .AsNoTracking()
                .OrderByDescending(r => r.FechaEmitida)
                .ToListAsync();
        }

       

        public async Task<bool> ExistsPendienteAsync(uint idObjeto, uint idUsuario)
        {
            return await _context.Reclamos.AnyAsync(r =>
                r.IdObjeto == idObjeto &&
                r.IdUsuario == idUsuario &&
                r.Estado == "pendiente");
        }

      
    }
}
