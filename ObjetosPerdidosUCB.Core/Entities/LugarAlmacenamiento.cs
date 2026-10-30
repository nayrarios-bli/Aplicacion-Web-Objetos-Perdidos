namespace ObjetosPerdidosUCB.Core.Entities;

public partial class LugarAlmacenamiento : BaseEntity
{


    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    /// <summary>true = activo, false = inactivo.</summary>
    public bool Estado { get; set; } = true;

    public virtual ICollection<Detalleobjeto> Detalleobjetos { get; set; } = new List<Detalleobjeto>();
}
