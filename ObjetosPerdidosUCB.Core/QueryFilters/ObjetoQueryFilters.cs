using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.QueryFilters
{
    public class ObjetoQueryFilters
    {
        public string? texto { get; set; }

        public string? categoria { get; set; }

        public string? lugarAlmacenamiento { get; set; }
        public string? lugarEncontrado { get; set; }
        public bool? soloDisponibles { get; set; }
    }
}
