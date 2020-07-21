using System.Linq;
using AutoMapper;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.PalestrantesEventos, opt => {
                    opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Palestrante).ToList());
                });
            CreateMap<EventoDto, Evento>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<LoteDto, Lote>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<RedeSocialDto, RedeSocial>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>()
                .ForMember(dest => dest.Evento, opt => {
                    opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Evento).ToList());
                }).ReverseMap();
            CreateMap<PalestranteEvento, PalestranteEventoDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            
        }                

    }
}