using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class LugarAlmacenamientoRepository : ILugarAlmacenamientoRepository
    {
        private readonly ObjetosPerdidosContext _context;

        public LugarAlmacenamientoRepository(ObjetosPerdidosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LugarAlmacenamiento>> GetAllLugaresAsync(bool soloActivos)
        {
            var query = _context.LugaresAlmacenamiento.AsNoTracking().AsQueryable();
            if (soloActivos)
                query = query.Where(l => l.Estado);

            return await query.OrderBy(l => l.Nombre).ToListAsync();
        }

     
    }
}
