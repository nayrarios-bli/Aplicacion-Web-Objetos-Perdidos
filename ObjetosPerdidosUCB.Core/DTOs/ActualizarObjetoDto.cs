using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.DTOs
{
    public class ActualizarObjetoDto
    {
        public uint Id { get; set; }

        public string Codigo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string Imagen { get; set; } = null!;

        public string LugarEncontrado { get; set; } = null!;

        public string FechaEncontrada { get; set; } = null!;

        public string? FechaEntrega { get; set; }

        public string? DetalleVerificacion { get; set; }

        public bool Estado { get; set; }

        public uint IdCategoria { get; set; }

        public uint IdLugarAlmacenamiento { get; set; }

        public uint IdUsuarioRegistro { get; set; }
    }
}
