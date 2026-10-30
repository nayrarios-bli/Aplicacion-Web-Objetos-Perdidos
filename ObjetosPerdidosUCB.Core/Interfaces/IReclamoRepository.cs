using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IReclamoRepository
    {
        Task<IEnumerable<Reclamo>> GetAllReclamosAsync();

        Task<bool> ExistsPendienteAsync(uint idObjeto, uint idUsuario);
     
    }
}


