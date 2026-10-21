using System.Globalization;

namespace ObjetosPerdidosUCB.Core.Utilities;

/// <summary>
/// Conversión centralizada de fechas string &lt;-&gt; DateOnly/DateTime.
/// La usan los validadores (Services) y los mapeos (Infrastructure).
/// </summary>
public static class FechaParser
{
    private static readonly string[] FormatosFecha =
    {
        "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd"
    };

    public static bool TryParseDateOnly(string? texto, out DateOnly fecha) =>
        DateOnly.TryParseExact(texto?.Trim(), FormatosFecha,
            CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);

    public static DateOnly ParseDateOnly(string texto) =>
        TryParseDateOnly(texto, out var f)
            ? f
            : throw new FormatException($"Fecha inválida: '{texto}'. Use dd-MM-yyyy o yyyy-MM-dd.");

    public static string Format(DateOnly fecha) =>
        fecha.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

    public static string Format(DateTime fecha) =>
        fecha.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
}
