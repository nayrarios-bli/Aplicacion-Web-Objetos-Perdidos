using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class ObjetoRepository : BaseRepository<Objeto>, IObjetoRepository
    {
        ObjetosPerdidosContext _context;
        public ObjetoRepository(ObjetosPerdidosContext context): base(context)
        {
            _context = context;
        }
        public async Task<Objeto?> GetObjetoByIdAsync(uint id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<bool> ExistsCodigoAsync(string codigo, uint? excludeId = null)
        {
            return await _context.Objetos.AnyAsync(o => 
            o.Codigo == codigo && (excludeId == null || o.Id != excludeId));
        }

        // Trackeado y solo con Detalle: así EF detecta únicamente lo que cambió
        public async Task<Objeto?> GetObjetoParaActualizarAsync(uint id)
        {
            return await _context.Objetos
                .Include(o => o.Detalle)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateObjetoAsync(Objeto objeto)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Objeto>> GetObjetosByNombreAsync(string texto)
        {
            return await _entities.Where(x => x.Nombre.ToLower() == texto.ToLower()).ToListAsync();
        }
    }
}
