using System.Linq;
using AutoMapper;
using ProAgil.API.Dtos;
using ProAgil.Domain;

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
            CreateMap<EventoDto, Evento>();
            CreateMap<Lote, LoteDto>();
            CreateMap<LoteDto, Lote>();
            CreateMap<RedeSocial, RedeSocialDto>();
            CreateMap<RedeSocialDto, RedeSocial>();
            CreateMap<Palestrante, PalestranteDto>()
                .ForMember(dest => dest.Evento, opt => {
                    opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Evento).ToList());
                });
            CreateMap<PalestranteEvento, PalestranteEventoDto>();       

        }                

    }
}