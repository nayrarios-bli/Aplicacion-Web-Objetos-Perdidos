using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Interfaces
{
    public interface IReclamoService
    {
        Task<IEnumerable<Reclamo>> GetAllReclamosAsync();
        Task<bool> ExistsPendienteAsync(uint idObjeto, uint idUsuario);
        Task<Reclamo?> GetReclamoByIdAsync(uint id);
        Task InsertReclamo(Reclamo objeto);
        Task UpdateReclamo(Reclamo objeto, uint idUsuarioAdmin);
        Task DeleteReclamo(uint objeto);
    }
}