using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.DTOs
{
    public class DetalleobjetoDto
    {
        public int Id { get; set; }

        public string FechaEncontrada { get; set; } = null!;

        public string? FechaRecogida { get; set; } = null;

        public string LugarEncontrado { get; set; } = null!;

        public string LugarAlmacenado { get; set; } = null!;

        public sbyte DiasGuardados { get; set; }
    }
}
