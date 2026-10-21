using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<Usuario?> GetUsuarioByIdAsync(uint id);
        Task<Usuario?> GetUsuarioByCorreoAsync(string correo);
        Task InsertUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
    }
}
