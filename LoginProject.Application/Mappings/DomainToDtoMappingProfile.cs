using AutoMapper;
using LoginProject.Application.Dtos.Request;
using LoginProject.Domain.Entities;

namespace LoginProject.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<ApplicationUser, UserRegisterDto>();
            CreateMap<UserRegisterDto, ApplicationUser>()
                .ForMember(r => r.UserName, u => u.MapFrom(x => x.Email))
                .ForMember(r => r.NormalizedUserName, u => u.MapFrom(x => x.Email.ToUpper()))
                .ForMember(r => r.Gender, u => u.MapFrom(x => (int)x.Gender))
                .ForMember(r => r.PhoneNumber, u => u.MapFrom(x => x.Phone));
        }
    }
}
