namespace ObjetosPerdidosUCB.Core.DTOs;

public class CrearUsuarioDto
{
    public string Nombre { get; set; } = null!;
    public string CorreoElectronico { get; set; } = null!;
    public string Contrasena { get; set; } = null!;
    public string Rol { get; set; } = null!;
}
