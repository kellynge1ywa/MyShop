namespace Models;

public class Coupon
{
     public Guid CouponId {get;set;}
    public string CouponCode {get;set;}="";
    public int CouponAmount {get;set;}
    public int CouponMinAmount {get;set;}
    public DateTime CreatedDate {get;set;}

}
