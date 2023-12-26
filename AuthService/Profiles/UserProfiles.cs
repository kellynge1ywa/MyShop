using AutoMapper;

namespace AuthService;

public class UserProfiles:Profile
{
    public UserProfiles()
    {
        CreateMap<RegisterUserDto, ShopUser>().ForMember(dest=>dest.UserName, src=>src.MapFrom(e=>e.Email));
        CreateMap<ShopUserDto, ShopUser>().ReverseMap();
    }

}
