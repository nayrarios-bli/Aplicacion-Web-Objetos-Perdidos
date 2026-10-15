using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;

namespace ObjetosPerdidosUCB.Api.Data;

public partial class Objeto
{
    public uint Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public string LugarEncontrado { get; set; } = null!;

    public DateOnly FechaEncontrada { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool? Estado { get; set; }

    public uint IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
