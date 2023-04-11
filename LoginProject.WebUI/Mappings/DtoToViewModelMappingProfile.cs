using AutoMapper;
using LoginProject.Application.Dtos.Request;
using LoginProject.Application.Extensions;
using LoginProject.WebUI.ViewModels;

namespace LoginProject.WebUI.Mappings
{
    public class DtoToViewModelMappingProfile : Profile
    {
        public DtoToViewModelMappingProfile()
        {
            CreateMap<RegisterViewModel, UserRegisterDto>()
                .ForMember(r => r.Age, u => u.MapFrom(x => x.Birth.GetAge()));
        }
    }
}
