namespace ObjetosPerdidosUCB.Core.DTOs;

public class LugarAlmacenamientoDto
{
    public uint Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public bool Estado { get; set; }
}
