namespace CartService;

public interface Icoupon
{
    Task<CouponDto> GetCouponByCode(string CouponCode);

}
