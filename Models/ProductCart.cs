using System.ComponentModel.DataAnnotations;

namespace Models;

public class ProductCart
{
    [Key]
    public Guid Id {get;set;}
    
    
     public Guid CartId {get;set;}
    
     
    public Guid ProductId {get;set;}
     public string CouponCode {get;set;}="";
    public decimal Price {get;set;}
    public int Quantity {get;set;}
    public decimal Subtotal {get;set;}
     public decimal Discount {get;set;}=0;

}
