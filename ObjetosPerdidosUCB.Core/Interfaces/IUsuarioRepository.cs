using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetUsuarioByCorreoAsync(string correo);
        
    }
}
