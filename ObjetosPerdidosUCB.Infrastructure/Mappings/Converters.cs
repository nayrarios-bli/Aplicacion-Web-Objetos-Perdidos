using AutoMapper;
using ObjetosPerdidosUCB.Core.Utilities;

namespace ObjetosPerdidosUCB.Infrastructure.Mappings
{
    public class DateTimeToStringConverter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime source, ResolutionContext context) =>
            FechaParser.Format(source);
    }

    public class NullableDateTimeToStringConverter : IValueConverter<DateTime?, string?>
    {
        public string? Convert(DateTime? source, ResolutionContext context) =>
            source.HasValue ? FechaParser.Format(source.Value) : null;
    }
}
