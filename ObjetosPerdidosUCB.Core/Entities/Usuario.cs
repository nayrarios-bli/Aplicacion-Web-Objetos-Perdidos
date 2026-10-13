using System;
using System.Collections.Generic;

namespace ObjetosPerdidosUCB.Api.Data;

public partial class Usuario
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<Objeto> Objetos { get; set; } = new List<Objeto>();

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
