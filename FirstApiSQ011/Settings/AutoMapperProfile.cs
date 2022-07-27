using AutoMapper;
using FirstApiSQ011.DTOs;
using FirstApiSQ011.Models;

namespace FirstApiSQ011.Settings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserToAddDto, User>()
                .ForMember(Dest => Dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<User, UserDetailToReturnDto>();
        }
    }
}
