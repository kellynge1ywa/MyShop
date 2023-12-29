using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartService;

public class ProductCart
{
    [Key]
    public Guid Id {get;set;}
    [ForeignKey("CartId")]
     public Guid CartId {get;set;}
     [ForeignKey("CartId")]
    public Guid ProductId {get;set;}
    public ProductDto product {get;set;}
    public int Quantity {get;set;}

}
