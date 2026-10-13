using System;
using System.Collections.Generic;

namespace ObjetosPerdidosUCB.Api.Data;

public partial class Reclamo
{
    public uint Id { get; set; }

    public DateTime? FechaRecogida { get; set; }

    public uint IdUsuario { get; set; }

    public uint IdObjeto { get; set; }

    public virtual Objeto IdObjetoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
