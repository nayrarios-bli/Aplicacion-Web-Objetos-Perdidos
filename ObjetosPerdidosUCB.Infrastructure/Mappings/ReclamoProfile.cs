using AutoMapper;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Mappings
{
    public class ReclamoProfile : Profile
    {
        public ReclamoProfile()
        {
            CreateMap<Reclamo, ReclamoDto>()
                .ForMember(d => d.FechaEmitida,
                    o => o.ConvertUsing<DateTimeToStringConverter, DateTime>())
                .ForMember(d => d.FechaRevisada,
                    o => o.ConvertUsing<NullableDateTimeToStringConverter, DateTime?>());

            // CU-07: fecha, estado y revisión los fija el servidor.
            CreateMap<CrearReclamoDto, Reclamo>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.FechaEmitida, o => o.MapFrom(_ => DateTime.Now))
                .ForMember(d => d.Estado, o => o.MapFrom(_ => "pendiente"))
                .ForMember(d => d.FechaRevisada, o => o.Ignore())
                .ForMember(d => d.Observacion, o => o.Ignore())
                .ForMember(d => d.IdRevisor, o => o.Ignore())
                .ForMember(d => d.IdObjetoNavigation, o => o.Ignore())
                .ForMember(d => d.IdUsuarioNavigation, o => o.Ignore())
                .ForMember(d => d.IdRevisorNavigation, o => o.Ignore());
        }
    }
}
