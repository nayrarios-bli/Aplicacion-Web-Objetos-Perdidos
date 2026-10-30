namespace ObjetosPerdidosUCB.Core.Entities;

public partial class Reclamo : BaseEntity
{
   

    public string Justificacion { get; set; } = null!;

    public DateTime FechaEmitida { get; set; }

    public string Estado { get; set; } = "pendiente";

    public DateTime? FechaRevisada { get; set; }

    public string? Observacion { get; set; }

    public uint IdObjeto { get; set; }

    public uint IdUsuario { get; set; }

    public uint? IdRevisor { get; set; }

    public virtual Objeto IdObjetoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Usuario? IdRevisorNavigation { get; set; }
}
