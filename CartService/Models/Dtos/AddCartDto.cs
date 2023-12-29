using System.ComponentModel.DataAnnotations;

namespace CartService;

public class AddCartDto
{
    
    
   
    public Guid ProductId {get;set;}
    public int Quantity{get;set;}=1;

}
