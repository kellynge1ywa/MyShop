using Models;

namespace CartService;

public class CartResponseDto
{
    public Guid Id {get;set;}
    
    
    public Guid UserId {get;set;}
   
   
    public DateTime OrderDate {get;set;}
    public string Status {get;set;}="Pending";
    
    public List<ProductCart> Products{get;set;}=new List<ProductCart>();

}
