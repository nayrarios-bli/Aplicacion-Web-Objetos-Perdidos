namespace ObjetosPerdidosUCB.Core.Entities;

public partial class Objeto
{
    public uint Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public bool Estado { get; set; } = true;

    public uint IdCategoria { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Detalleobjeto? Detalle { get; set; }

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
