namespace ObjetosPerdidosUCB.Core.DTOs;

public class ReclamoDto
{
    public uint Id { get; set; }
    public string Justificacion { get; set; } = null!;
    public string FechaEmitida { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string? FechaRevisada { get; set; }
    public string? Observacion { get; set; }
    public uint IdObjeto { get; set; }
    public uint IdUsuario { get; set; }
    public uint? IdRevisor { get; set; }
}
