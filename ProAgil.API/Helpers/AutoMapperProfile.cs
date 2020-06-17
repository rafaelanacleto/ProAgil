using AutoMapper;
using ProAgil.API.Dtos;
using ProAgil.Domain;

namespace ProAgil.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDto>();
        }
        

    }
}