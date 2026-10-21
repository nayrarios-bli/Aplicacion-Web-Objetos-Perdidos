using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface ILugarAlmacenamientoRepository
    {
        Task<IEnumerable<LugarAlmacenamiento>> GetAllLugaresAsync(bool soloActivos);
        Task<LugarAlmacenamiento?> GetLugarByIdAsync(uint id);
    }
}
