using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IReclamoRepository
    {
        Task<IEnumerable<Reclamo>> GetAllReclamosAsync();
        Task<Reclamo?> GetReclamoByIdAsync(uint id);
        Task<bool> ExistsPendienteAsync(uint idObjeto, uint idUsuario);
        Task InsertReclamo(Reclamo reclamo);
    }
}


