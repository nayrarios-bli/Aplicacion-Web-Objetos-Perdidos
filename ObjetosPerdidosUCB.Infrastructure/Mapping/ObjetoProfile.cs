using AutoMapper;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Mapping
{
    public class ObjetoProfile : Profile
    {
        public ObjetoProfile()
        {
            CreateMap<Objeto, ObjetoDto>()
                 .ForMember(dest => dest.Imagen,
                 opt => opt.MapFrom(src => src.Image));
            CreateMap<ObjetoDto, Objeto>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.Imagen));
        }
    }
}
