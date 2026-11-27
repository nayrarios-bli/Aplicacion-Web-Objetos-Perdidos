using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IUsuarioRepository: IBaseRepository<Usuario>
    {
        Task<Usuario?> GetUsuarioByCorreoAsync(string correo);
        Task<Usuario> GetLoginByCredentials(UserLogin login);
    }
}
