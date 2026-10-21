using System;
using System.Collections.Generic;

namespace ObjetosPerdidosUCB.Core.Entities;

public partial class Reclamo
{
    public uint Id { get; set; }

    public DateTime FechaEmitida { get; set; }

    public DateTime? FechaRevisada { get; set; }

    public string Estado { get; set; } = null!;

    public uint IdUsuario { get; set; }

    public uint IdObjeto { get; set; }

    public virtual Objeto IdObjetoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
