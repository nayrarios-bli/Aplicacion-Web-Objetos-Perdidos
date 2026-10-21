namespace ObjetosPerdidosUCB.Core.Entities;

public partial class Categoria
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Estado { get; set; } = true;

    public virtual ICollection<Objeto> Objetos { get; set; } = new List<Objeto>();
}
