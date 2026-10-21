using AutoMapper;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Mapping
{
    public class DetalleobjetoProfile : Profile
    {
        public DetalleobjetoProfile()
        {
            CreateMap<Detalleobjeto, DetalleobjetoDto>()
                .ForMember(dest => dest.FechaEncontrada,
                opt => opt.ConvertUsing<DateOnlyToStringConverter, DateOnly>())
                .ForMember(dest => dest.FechaRecogida,
                opt => opt.ConvertUsing<NullableDateOnlyToStringConverter, DateOnly?>());
          

            CreateMap<DetalleobjetoDto, Detalleobjeto>()
                .ForMember(dest=> dest.FechaEncontrada,
                opt => opt.ConvertUsing<StringToDateOnlyConverter, string>())
                .ForMember(dest => dest.FechaRecogida,
                opt => opt.ConvertUsing<StringToNullableDateOnlyConverter, string?>());
        }
    }

    public class DateOnlyToStringConverter : IValueConverter<DateOnly, string>
    {
        public string Convert(DateOnly source, ResolutionContext context)
            => source.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
    }

    public class StringToDateOnlyConverter : IValueConverter<string, DateOnly>
    {
        private static readonly string[] Formats = { "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };

        public DateOnly Convert(string source, ResolutionContext context)
        {
            if (DateOnly.TryParseExact(source?.Trim(), Formats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;

            throw new FormatException($"Fecha inválida: '{source}'. Usa dd-MM-yyyy, dd/MM/yyyy o yyyy-MM-dd.");
        }
    }
    public class NullableDateOnlyToStringConverter : IValueConverter<DateOnly?, string?>
    {
        public string? Convert(DateOnly? source, ResolutionContext context)
            => source?.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
    }

    public class StringToNullableDateOnlyConverter : IValueConverter<string?, DateOnly?>
    {
        private static readonly string[] Formats = { "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };

        public DateOnly? Convert(string? source, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;

            if (DateOnly.TryParseExact(source.Trim(), Formats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;

            throw new FormatException($"Fecha inválida: '{source}'. Usa dd-MM-yyyy, dd/MM/yyyy o yyyy-MM-dd.");
        }
    }
}
