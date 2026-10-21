using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllCategoriasAsync(bool soloActivas);
        Task<Categoria?> GetCategoriaByIdAsync(uint id);
    }
}
