using AutoMapper;
using LoginProject.Api.Models;
using LoginProject.Application.Dtos.Request;
using LoginProject.Application.Extensions;

namespace LoginProject.Api.Mappings
{
    public class DtoToModelMappingProfile : Profile
    {
        public DtoToModelMappingProfile()
        {
            CreateMap<RegisterModel, UserRegisterDto>()
               .ForMember(r => r.Age, u => u.MapFrom(x => x.Birth.GetAge()));
        }
    }
}
