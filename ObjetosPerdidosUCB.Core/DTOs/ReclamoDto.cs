using ObjetosPerdidosUCB.Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.DTOs
{
    public class ReclamoDto
    {
        public uint Id { get; set; }

        public DateTime? FechaRecogida { get; set; }

        public uint IdUsuario { get; set; }

        public uint IdObjeto { get; set; }
    }
}
