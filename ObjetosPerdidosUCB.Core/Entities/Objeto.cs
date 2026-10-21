using System;
using System.Collections.Generic;

namespace ObjetosPerdidosUCB.Core.Entities;

public partial class Objeto
{
    public uint Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool? Estado { get; set; }

    public uint IdUsuario { get; set; }

    public int IddetalleObjeto { get; set; }

    public virtual ICollection<Detalleobjeto> Detalleobjetos { get; set; } = new List<Detalleobjeto>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Detalleobjeto IddetalleObjetoNavigation { get; set; } = null!;

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
