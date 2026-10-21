using AutoMapper;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Mappings
{
    public class LugarAlmacenamientoProfile : Profile
    {
        public LugarAlmacenamientoProfile()
        {
            CreateMap<LugarAlmacenamiento, LugarAlmacenamientoDto>();
        }
    }
}
