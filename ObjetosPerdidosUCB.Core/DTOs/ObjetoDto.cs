namespace ObjetosPerdidosUCB.Core.DTOs;

/// <summary>
/// Respuesta de CU-03 / CU-05. No incluye DetalleVerificacion (dato reservado
/// a administradores para validar reclamos).
/// </summary>
public class ObjetoDto
{
    public uint Id { get; set; }
    public string Codigo { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string Imagen { get; set; } = null!;
    public string LugarEncontrado { get; set; } = null!;
    public string FechaEncontrada { get; set; } = null!;
    public string? FechaEntrega { get; set; }
    public int DiasGuardados { get; set; }
    public bool Estado { get; set; }
    public uint IdCategoria { get; set; }
    public string Categoria { get; set; } = null!;
    public uint IdLugarAlmacenamiento { get; set; }
    public string LugarAlmacenamiento { get; set; } = null!;
    public uint IdUsuarioRegistro { get; set; }
}
