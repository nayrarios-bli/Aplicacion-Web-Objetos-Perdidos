using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IObjetoRepository
    {
        Task<IEnumerable<Objeto>> SearchObjetosAsync(
            string? texto,
            uint? idCategoria,
            uint? idLugarAlmacenamiento,
            string? lugarEncontrado,
            bool? soloDisponibles);
        
        Task<bool> ExistsCodigoAsync(string codigo, uint? excludeID = null);
        Task<Objeto?> GetObjetoByIdAsync(uint id);
        Task<Objeto?> GetObjetoParaActualizarAsync(uint id);
        Task UpdateObjetoAsync(Objeto objeto);
    }
}
