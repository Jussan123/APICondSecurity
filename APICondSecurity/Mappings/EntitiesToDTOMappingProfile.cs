using APICondSecurity.DTOs;
using APICondSecurity.Models;
using AutoMapper;

namespace APICondSecurity.Mappings
{
    public class EntitiesToDTOMappingProfile : Profile
    {
        public EntitiesToDTOMappingProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>().ReverseMap();
            CreateMap<Cameras, CamerasDTO>().ReverseMap();
        }
    }
}
