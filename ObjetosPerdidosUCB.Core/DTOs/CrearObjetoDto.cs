namespace ObjetosPerdidosUCB.Core.DTOs;

/// <summary>Entrada de CU-02: registrar un objeto encontrado.</summary>
public class CrearObjetoDto
{
    public string Codigo { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string Imagen { get; set; } = null!;
    public string? DetalleVerificacion { get; set; }
    public string LugarEncontrado { get; set; } = null!;
    public string FechaEncontrada { get; set; } = null!;
    public uint IdCategoria { get; set; }
    public uint IdLugarAlmacenamiento { get; set; }
    public uint IdUsuarioRegistro { get; set; }
}
