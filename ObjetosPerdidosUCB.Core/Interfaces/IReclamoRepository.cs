using ObjetosPerdidosUCB.Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public class IReclamoRepository
    {
        Task<IEnumerable<Reclamo>> GetAllReclamosAsync();
        Task<Reclamo> GetReclamoByIdAsync(int id);
        Task InsertReclamo(Reclamo reclamo);
        Task UpdateReclamo(Reclamo reclamo);
        Task DeleteReclamo(Reclamo reclamo);
    }
}
