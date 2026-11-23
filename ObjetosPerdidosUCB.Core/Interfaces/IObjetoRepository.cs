using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IObjetoRepository : IBaseRepository<Objeto>
    {
        Task<bool> ExistsCodigoAsync(string codigo, uint? excludeID = null);
        Task<Objeto?> GetObjetoByIdAsync(uint id);
        Task<IEnumerable<Objeto>> GetObjetosByNombreAsync(string texto);
        Task<Objeto?> GetObjetoParaActualizarAsync(uint id);
        Task UpdateObjetoAsync(Objeto objeto);
    }
}
