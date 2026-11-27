using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;
using ObjetosPerdidosUCB.Infrastructure.Queries;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class ObjetoRepository : BaseRepository<Objeto>, IObjetoRepository
    {
        private readonly ObjetosPerdidosContext _context;
        private readonly IDapperContext _dapper;
        public ObjetoRepository(ObjetosPerdidosContext context, IDapperContext dapper): base(context)
        {
            _context = context;
            _dapper= dapper;
        }
        public async Task<ObjetoDto?> GetObjetoByIdAsync(uint id)
        {
            return await _dapper.QueryFirstOrDefaultAsync<ObjetoDto>
                (ObjetoQuery.DetallePorId, new { Id = id }); 
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

        public async Task<IEnumerable<Objeto>> GetObjetosParaFiltrarAsync()
        {
            return await _context.Objetos
                .AsNoTracking()
                .Include(o => o.IdCategoriaNavigation)
                .Include(o => o.Detalle)
                    .ThenInclude(d => d!.IdLugarAlmacenamientoNavigation)
                .ToListAsync();
        }
    }
}
