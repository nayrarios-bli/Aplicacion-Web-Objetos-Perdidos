using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface ILugarAlmacenamientoRepository: IBaseRepository<LugarAlmacenamiento>
    {
        Task<IEnumerable<LugarAlmacenamiento>> GetAllLugaresAsync(bool soloActivos);
      
    }
}
