namespace ObjetosPerdidosUCB.Core.DTOs;

/// <summary>Entrada de CU-07. Fecha, estado y revisión los define el servidor.</summary>
public class CrearReclamoDto
{
    public uint IdObjeto { get; set; }
    public uint IdUsuario { get; set; }
    public string Justificacion { get; set; } = null!;
}
