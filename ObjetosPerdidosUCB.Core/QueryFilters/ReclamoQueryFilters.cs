using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.QueryFilters
{
    public class ReclamoQueryFilters
    {
        public DateTime? FechaEmitida { get; set; }
        public string? Estado { get; set; }
        public string? objeto { get; set; }
        public string? usuario { get; set; }
    }
}
