using AutoMapper;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Utilities;

namespace ObjetosPerdidosUCB.Infrastructure.Mappings
{
    public class ObjetoProfile : Profile
    {
        public ObjetoProfile()
        {
            // Lectura: Objeto (con Detalle, categoría y lugar cargados) -> ObjetoDto plano
            CreateMap<Objeto, ObjetoDto>()
                .ForMember(d => d.Categoria,
                    o => o.MapFrom(s => s.IdCategoriaNavigation.Nombre))
                .ForMember(d => d.LugarEncontrado,
                    o => o.MapFrom(s => ObjetoMapHelper.LugarEncontrado(s.Detalle)))
                .ForMember(d => d.FechaEncontrada,
                    o => o.MapFrom(s => ObjetoMapHelper.FechaEncontrada(s.Detalle)))
                .ForMember(d => d.FechaEntrega,
                    o => o.MapFrom(s => ObjetoMapHelper.FechaEntrega(s.Detalle)))
                .ForMember(d => d.DiasGuardados,
                    o => o.MapFrom(s => ObjetoMapHelper.DiasGuardados(s.Detalle)))
                .ForMember(d => d.IdLugarAlmacenamiento,
                    o => o.MapFrom(s => ObjetoMapHelper.IdLugarAlmacenamiento(s.Detalle)))
                .ForMember(d => d.LugarAlmacenamiento,
                    o => o.MapFrom(s => ObjetoMapHelper.LugarAlmacenamiento(s.Detalle)))
                .ForMember(d => d.IdUsuarioRegistro,
                    o => o.MapFrom(s => ObjetoMapHelper.IdUsuarioRegistro(s.Detalle)));

            // CU-02: CrearObjetoDto -> Objeto con su Detalle (se guardan juntos).
            CreateMap<CrearObjetoDto, Objeto>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Estado, o => o.MapFrom(_ => true))
                .ForMember(d => d.IdCategoriaNavigation, o => o.Ignore())
                .ForMember(d => d.Reclamos, o => o.Ignore())
                .ForMember(d => d.Detalle, o => o.MapFrom(s => new Detalleobjeto
                {
                    LugarEncontrado = s.LugarEncontrado,
                    FechaEncontrada = FechaParser.ParseDateOnly(s.FechaEncontrada),
                    DetalleVerificacion = s.DetalleVerificacion,
                    IdLugarAlmacenamiento = s.IdLugarAlmacenamiento,
                    IdUsuarioRegistro = s.IdUsuarioRegistro
                }));
        }
    }
}
