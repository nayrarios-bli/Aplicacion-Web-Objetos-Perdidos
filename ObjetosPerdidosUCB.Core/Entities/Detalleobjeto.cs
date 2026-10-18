using System;
using System.Collections.Generic;

namespace ObjetosPerdidosUCB.Core.Entities;

public partial class Detalleobjeto
{
    public int Id { get; set; }

    public DateOnly FechaEncontrada { get; set; }

    public DateOnly? FechaRecogida { get; set; }

    public string LugarEncontrado { get; set; } = null!;

    public string LugarAlmacenado { get; set; } = null!;

    public sbyte DiasGuardados { get; set; }

    public uint Idobjeto { get; set; }

    public virtual Objeto IdobjetoNavigation { get; set; } = null!;

    public virtual ICollection<Objeto> Objetos { get; set; } = new List<Objeto>();
}
