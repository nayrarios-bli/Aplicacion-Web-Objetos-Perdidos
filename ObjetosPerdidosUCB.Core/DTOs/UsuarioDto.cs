namespace ObjetosPerdidosUCB.Core.DTOs;

/// <summary>Nunca incluye la contraseña.</summary>
public class UsuarioDto
{
    public uint Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string CorreoElectronico { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public bool Estado { get; set; }
}
