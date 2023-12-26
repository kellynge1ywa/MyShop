namespace CouponService;

public interface Icoupon
{
    Task<string> DeleteCoupon(Coupon coupon);
    Task<string> UpdateCoupon();
    Task<Coupon> GetCoupon(Guid Id);
    Task<List<Coupon>> GetAllCoupons();
    Task<string> AddCoupon(Coupon coupon);

}
