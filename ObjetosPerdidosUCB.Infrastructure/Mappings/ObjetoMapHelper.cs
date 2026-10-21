using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Utilities;

namespace ObjetosPerdidosUCB.Infrastructure.Mappings
{
    public static class ObjetoMapHelper
    {
        public static string LugarEncontrado(Detalleobjeto? d) =>
            d?.LugarEncontrado ?? string.Empty;

        public static string FechaEncontrada(Detalleobjeto? d) =>
            d is null ? string.Empty : FechaParser.Format(d.FechaEncontrada);

        public static string? FechaEntrega(Detalleobjeto? d) =>
            d is null || d.FechaEntrega is null ? null : FechaParser.Format(d.FechaEntrega.Value);

        public static uint IdLugarAlmacenamiento(Detalleobjeto? d) =>
            d?.IdLugarAlmacenamiento ?? 0;

        public static string LugarAlmacenamiento(Detalleobjeto? d) =>
            d?.IdLugarAlmacenamientoNavigation?.Nombre ?? string.Empty;

        public static uint IdUsuarioRegistro(Detalleobjeto? d) =>
            d?.IdUsuarioRegistro ?? 0;

        public static int DiasGuardados(Detalleobjeto? d)
        {
            if (d is null) return 0;
            var fin = d.FechaEntrega ?? DateOnly.FromDateTime(DateTime.Today);
            return Math.Max(0, fin.DayNumber - d.FechaEncontrada.DayNumber);
        }
    }
}
