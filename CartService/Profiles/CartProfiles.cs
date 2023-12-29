using AutoMapper;

namespace CartService;

public class CartProfiles:Profile
{
    public CartProfiles()
    {
        CreateMap<AddCartDto,Cart>().ReverseMap();
    }

}
