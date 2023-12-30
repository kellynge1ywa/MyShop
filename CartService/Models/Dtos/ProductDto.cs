using System.ComponentModel.DataAnnotations;

namespace CartService;

public class ProductDto
{ 
    [Key]
     public Guid Id {get;set;}
    public string ProductName {get;set;}="";
    public string Description {get;set;}="";
    public decimal Price {get;set;}
    public int Quantity {get;set;}
    
    

}
