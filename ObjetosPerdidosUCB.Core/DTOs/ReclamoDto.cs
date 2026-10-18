
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.DTOs
{
    public class ReclamoDto
    {
        public uint Id { get; set; }

        public string FechaEmitida { get; set; }

        public string? FechaRevisada { get; set; }

        public string Estado { get; set; } = null!;

        public uint IdUsuario { get; set; }

        public uint IdObjeto { get; set; }
    }
}
