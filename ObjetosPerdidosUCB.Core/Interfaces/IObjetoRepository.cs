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
        Task<Objeto?> GetObjetoByIdAsync(uint id);
        Task<bool> ExistsCodigoAsync(string codigo);
        Task InsertObjeto(Objeto objeto);
        Task UpdateObjeto(Objeto objeto);
        Task DeleteObjeto(uint id);

    }
}
