using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IReclamoRepository
    {
        Task<IEnumerable<Reclamo>> GetAllReclamosAsync();
        Task<Reclamo> GetReclamoByIdAsync(int id);
        Task InsertReclamo(Reclamo reclamo);
        Task UpdateReclamo(Reclamo reclamo);
        Task DeleteReclamo(Reclamo reclamo);
    }
}
