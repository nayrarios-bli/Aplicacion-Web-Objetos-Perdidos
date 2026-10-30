namespace ObjetosPerdidosUCB.Core.Entities;

public partial class Usuario : BaseEntity
{

    public string Nombre { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public bool Estado { get; set; } = true;

    public virtual ICollection<Detalleobjeto> DetallesRegistrados { get; set; } = new List<Detalleobjeto>();

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();

    public virtual ICollection<Reclamo> ReclamosRevisados { get; set; } = new List<Reclamo>();
}
