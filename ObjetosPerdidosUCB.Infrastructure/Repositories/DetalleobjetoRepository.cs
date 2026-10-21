using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class DetalleobjetoRepository : IDetalleobjetoRepository
    {
        private readonly ObjetoPerdidosContext _objetoPerdidosContext;
        public DetalleobjetoRepository(ObjetoPerdidosContext objetosPerdidosContext)
        {
            _objetoPerdidosContext = objetosPerdidosContext;
        }
        public async Task<IEnumerable<Detalleobjeto>> GetAllDetalleobjetosAsync()
        {
            var detalleobjetos = await _objetoPerdidosContext.Detalleobjetos.ToListAsync();
            return detalleobjetos;
        }
        public async Task<Detalleobjeto> GetDetalleobjetoByIdAsync(int id)
        {
            var detalleObjeto = await _objetoPerdidosContext.Detalleobjetos.FirstOrDefaultAsync(y => y.Id == id);
            return detalleObjeto;
        }
        public async Task InsertDetalleobjeto(Detalleobjeto detalleobjeto)
        {
            _objetoPerdidosContext.Detalleobjetos.Add(detalleobjeto);
            await _objetoPerdidosContext.SaveChangesAsync();
        }
        public async Task UpdateDetalleobjeto(Detalleobjeto detalleobjeto)
        {
            _objetoPerdidosContext.Detalleobjetos.Update(detalleobjeto);
            await _objetoPerdidosContext.SaveChangesAsync();
        }
        public async Task DeleteDetalleobjeto(Detalleobjeto detalleobjeto)
        {
            _objetoPerdidosContext.Detalleobjetos.Remove(detalleobjeto);
            await _objetoPerdidosContext.SaveChangesAsync();
        }
    }
}
