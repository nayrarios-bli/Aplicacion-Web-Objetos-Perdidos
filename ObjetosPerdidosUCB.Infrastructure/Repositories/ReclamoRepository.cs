using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Infrastructure.Data;
using ObjetosPerdidosUCB.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ObjetosPerdidosUCB.Api.Data;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class ReclamoRepository: IReclamoRepository
    {
        private readonly ObjetosPerdidosContext _objetosPerdidosContext;
        public ReclamoRepository(ObjetosPerdidosContext objetosPerdidosContext)
        {
            _objetosPerdidosContext = objetosPerdidosContext;
        }
        public async Task DeleteReclamo(Reclamo reclamo)
        {
            _objetosPerdidosContext.Reclamos.Remove(reclamo);
            await _objetosPerdidosContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reclamo>> GetAllReclamosAsync()
        {
            var reclamos = await _objetosPerdidosContext.Reclamos.ToListAsync();
            return reclamos;
        }

        public async Task<Reclamo> GetReclamoByIdAsync(int id)
        {
            var reclamo = await _objetosPerdidosContext.Reclamos.FirstOrDefaultAsync(x => x.Id == id);
            return reclamo;
        }

        public async Task InsertReclamo(Reclamo reclamo)
        {
            _objetosPerdidosContext.Reclamos.Add(reclamo);
            await _objetosPerdidosContext.SaveChangesAsync();
        }

        public async Task UpdateReclamo(Reclamo reclamo)
        {
            _objetosPerdidosContext.Reclamos.Update(reclamo);
            await _objetosPerdidosContext.SaveChangesAsync();

        }
    }
}
