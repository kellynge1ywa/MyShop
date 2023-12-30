
using Microsoft.EntityFrameworkCore;
using Models;

namespace CouponService;

public class CouponsService : Icoupon
{
    private readonly AppDbContext _AppDbContext;
    public CouponsService(AppDbContext appDbContext)
    {
        _AppDbContext=appDbContext;
        
    }
    public async Task<string> AddCoupon(Coupon coupon)
    {
        _AppDbContext.Coupons?.AddAsync(coupon);
        await _AppDbContext.SaveChangesAsync();
        return "Coupon added successfully!!!";


    }

    public async Task<string> DeleteCoupon(Coupon coupon)
    {
        _AppDbContext.Coupons.Remove(coupon);
        await _AppDbContext.SaveChangesAsync();
        return "Coupon deleted successfully";
    }

    public async Task<List<Coupon>> GetAllCoupons()
    {
        try{
            return await _AppDbContext.Coupons.ToListAsync();
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
            return new List<Coupon>();
        }
    }

    public async  Task<Coupon> GetCoupon(Guid Id)
    {
        try{
            var singleCoupon= await _AppDbContext.Coupons.Where(k=>k.CouponId==Id).FirstOrDefaultAsync();
            if(singleCoupon==null){
                return new Coupon();
            }
            return singleCoupon;
        } catch(Exception ex){
            Console.WriteLine(ex.Message);
            return new Coupon();
        }
    }

    public async Task<Coupon> GetOneCoupon(string code)
    {
        try{
            var oneCoupon= await _AppDbContext.Coupons.Where(k=>k.CouponCode==code).FirstOrDefaultAsync();
            if(oneCoupon==null){
                return new Coupon();
            }
            return oneCoupon;
        } catch(Exception ex){
            Console.WriteLine(ex.Message);
            return new Coupon();
        }
    }

    public async Task<string> UpdateCoupon()
    {
        try{
            await _AppDbContext.SaveChangesAsync();
            return "Coupon updated successfully!!!";
        }
        catch(Exception ex){
            return ex.Message;
        }
    }
}
