namespace ObjetosPerdidosUCB.Core.Entities;

public partial class Detalleobjeto
{
    public uint IdObjeto { get; set; }

    public string LugarEncontrado { get; set; } = null!;

    public DateOnly FechaEncontrada { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public string? DetalleVerificacion { get; set; }

    public uint IdLugarAlmacenamiento { get; set; }

    public uint IdUsuarioRegistro { get; set; }

    public virtual Objeto IdObjetoNavigation { get; set; } = null!;

    public virtual LugarAlmacenamiento IdLugarAlmacenamientoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioRegistroNavigation { get; set; } = null!;
}
