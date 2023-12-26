using System.ComponentModel.DataAnnotations;

namespace CouponService;

public class AddCouponDto
{
     [Required]
     public string CouponCode {get;set;}="";
     [Required]
     [Range(10000,100000)]
    public int CouponAmount {get;set;}
    [Required]
    [Range(50000,int.MaxValue)]
    public int CouponMinAmount {get;set;}
    [Required]
    public DateTime CreatedDate {get;set;}

}
