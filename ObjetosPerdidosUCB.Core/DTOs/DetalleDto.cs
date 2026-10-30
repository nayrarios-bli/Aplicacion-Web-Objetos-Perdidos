using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.DTOs
{
    public partial class DetalleDto
    {
        public uint IdObjeto { get; set; }

        public string LugarEncontrado { get; set; } = null!;

        public DateOnly FechaEncontrada { get; set; }

        public DateOnly? FechaEntrega { get; set; }

        public string? DetalleVerificacion { get; set; }

        public uint IdLugarAlmacenamiento { get; set; }

        public uint IdUsuarioRegistro { get; set; }
    }
   }
