using Models;

namespace CouponService;

public interface Icoupon
{
    Task<string> DeleteCoupon(Coupon coupon);
    Task<string> UpdateCoupon();
    Task<Coupon> GetCoupon(Guid Id);
    Task<Coupon> GetOneCoupon(string code);
    Task<List<Coupon>> GetAllCoupons();
    Task<string> AddCoupon(Coupon coupon);

}
