using System.ComponentModel.DataAnnotations;

namespace CartService;

public class CouponDto
{
    [Key]
    public Guid CouponId {get;set;}
    public string CouponCode {get;set;}="";
    public int CouponAmount {get;set;}
    public int CouponMinAmount {get;set;}

}
