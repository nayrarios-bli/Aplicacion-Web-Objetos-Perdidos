using AutoMapper;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDto>();

            
            CreateMap<UsuarioDto, Usuario>()
                .ForMember(d => d.Contrasena, o => o.Ignore())
                .ForMember(d => d.DetallesRegistrados, o => o.Ignore())
                .ForMember(d => d.Reclamos, o => o.Ignore())
                .ForMember(d => d.ReclamosRevisados, o => o.Ignore());

            
            CreateMap<CrearUsuarioDto, Usuario>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Contrasena, o => o.Ignore())
                .ForMember(d => d.Estado, o => o.MapFrom(_ => true))
                .ForMember(d => d.DetallesRegistrados, o => o.Ignore())
                .ForMember(d => d.Reclamos, o => o.Ignore())
                .ForMember(d => d.ReclamosRevisados, o => o.Ignore());
        }
    }
}
