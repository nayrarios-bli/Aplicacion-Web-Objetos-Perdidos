using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.DTOs
{
    public class ObjetoDto 
    {
        public uint Id { get; set; }

        public string Codigo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Imagen { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public bool? Estado { get; set; }

        public uint IdUsuario { get; set; }

        public int IddetalleObjeto { get; set; }

    }
}
