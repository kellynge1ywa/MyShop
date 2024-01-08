using System.ComponentModel.DataAnnotations;

namespace CartService;

public class AddCartDto
{
    
    
   
    public Guid ProductId {get;set;}
    // public Guid UserId {get;set;}
    //    public decimal Price {get;set;}
    public int Quantity{get;set;}=1;

}
