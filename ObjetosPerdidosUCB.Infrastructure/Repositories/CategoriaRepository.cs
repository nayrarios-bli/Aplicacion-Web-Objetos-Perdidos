using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ObjetosPerdidosContext _context;

        public CategoriaRepository(ObjetosPerdidosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync(bool soloActivas)
        {
            var query = _context.Categorias.AsNoTracking().AsQueryable();
            if (soloActivas)
                query = query.Where(c => c.Estado);

            return await query.OrderBy(c => c.Nombre).ToListAsync();
        }

        public async Task<Categoria?> GetCategoriaByIdAsync(uint id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
