using AutoMapper;
using TestApiSep.DTOs;
using TestApiSep.Models;

namespace TestApiSep.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistroInsertDto, Participante>();
            CreateMap<Participante, RegistroDto>();
        }
    }
}
