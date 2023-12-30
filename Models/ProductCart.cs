using System.ComponentModel.DataAnnotations;

namespace Models;

public class ProductCart
{
    [Key]
    public Guid Id {get;set;}
    // [ForeignKey("CartId")]
     public Guid CartId {get;set;}
    //[ForeignKey("ProductId")]
     
    public Guid ProductId {get;set;}
    public decimal Price {get;set;}
    public decimal Discount {get;set;}
    public int Quantity {get;set;}

}
