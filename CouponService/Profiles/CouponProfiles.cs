using AutoMapper;

namespace CouponService;

public class CouponProfiles:Profile
{
    public CouponProfiles()
    {
        CreateMap<AddCouponDto, Coupon>().ReverseMap();
        
    }

}
