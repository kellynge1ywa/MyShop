using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartService;

public class Cart
{
    [Key]
    public Guid CartId {get;set;}
    [ForeignKey("UserId")]
    public Guid UserId {get;set;}
   
    public string CouponCode {get;set;}="";
   
    public DateTime OrderDate {get;set;}
    
    public List<ProductResponseDto> Products{get;set;}=new List<ProductResponseDto>();
    

}
