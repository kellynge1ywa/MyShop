namespace CartService;

public class Cart
{
    public Guid CartId {get;set;}
    public Guid UserId {get;set;}
    public decimal SubTotal {get;set;}
    public string CouponCode {get;set;}="";
    public decimal Discount {get;set;}
    public decimal Total {get;set;}

}
