using AutoMapper;
using Contracts;
using Domain.Entities;

namespace Services.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

             CreateMap<Employee, EmployeeDTO>()
            .ForMember(
                dest => dest.Puesto,
                opt => opt.MapFrom(src => src.Position.Nombre)
            )
            .ForMember(
                dest => dest.IdPuesto,
                opt => opt.MapFrom(src => src.Position.IdPuesto)
            ).ReverseMap();

            CreateMap<PositionDTO, Position>().ReverseMap(); //reverse so the both direction

            CreateMap<EmployeeForCreationDTO, Employee>().ReverseMap(); //reverse so the both direction

            CreateMap<EmployeeForUpdateDTO, Employee>().ReverseMap(); //reverse so the both direction

            CreateMap<PositionForPersitenceDto, Position>().ReverseMap(); //reverse so the both direction

            CreateMap<PositionDTO, Position>().ReverseMap(); //reverse so the both direction

        }
    }
}
