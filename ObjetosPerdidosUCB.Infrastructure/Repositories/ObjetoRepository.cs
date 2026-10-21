using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class ObjetoRepository : IObjetoRepository
    {
        private readonly ObjetosPerdidosContext _context;

        public ObjetoRepository(ObjetosPerdidosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Objeto>> GetAllObjetosAsync()
        {
            var objetos = await _context.Objetos.ToListAsync();
            return objetos;
        }
        public async Task<IEnumerable<Objeto>> SearchObjetosAsync(
            string? texto,
            uint? idCategoria,
            uint? idLugarAlmacenamiento,
            string? lugarEncontrado,
            bool? soloDisponibles)
        {
            var query = _context.Objetos
                .AsNoTracking()
                .Include(o => o.IdCategoriaNavigation)
                .Include(o => o.Detalle)
                    .ThenInclude(d => d!.IdLugarAlmacenamientoNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                var t = texto.Trim();
                query = query.Where(o =>
                    o.Nombre.Contains(t) ||
                    o.Descripcion.Contains(t) ||
                    o.Codigo.Contains(t));
            }

            if (idCategoria.HasValue)
                query = query.Where(o => o.IdCategoria == idCategoria.Value);

            // CU-06: filtrar por lugar de almacenamiento
            if (idLugarAlmacenamiento.HasValue)
                query = query.Where(o => o.Detalle != null &&
                                         o.Detalle.IdLugarAlmacenamiento == idLugarAlmacenamiento.Value);

            if (!string.IsNullOrWhiteSpace(lugarEncontrado))
            {
                var le = lugarEncontrado.Trim();
                query = query.Where(o => o.Detalle != null && o.Detalle.LugarEncontrado.Contains(le));
            }

            if (soloDisponibles == true)
                query = query.Where(o => o.Estado);

            return await query
                .OrderByDescending(o => o.Id)
                .ToListAsync();
        }

        public async Task<Objeto?> GetObjetoByIdAsync(uint id)
        {
            return await _context.Objetos
                .Include(o => o.IdCategoriaNavigation)
                .Include(o => o.Detalle)
                    .ThenInclude(d => d!.IdLugarAlmacenamientoNavigation)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> ExistsCodigoAsync(string codigo)
        {
            return await _context.Objetos.AnyAsync(o => o.Codigo == codigo);
        }

        public async Task InsertObjeto(Objeto objeto)
        {
            _context.Objetos.Add(objeto);
            await _context.SaveChangesAsync();
        }

        
    }
}
