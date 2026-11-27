using ObjetosPerdidosUCB.Core.Entities;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
    Task<Usuario?> GetUsuarioByIdAsync(uint id);
    Task<Usuario?> GetUsuarioByCorreoAsync(string correo);
    Task InsertUsuario(Usuario objeto);
    Task UpdateUsuario(Usuario objeto);
    Task DeleteUsuario(uint objeto);
    Task ValidarAdministradorAsync(uint idUsuario);
}