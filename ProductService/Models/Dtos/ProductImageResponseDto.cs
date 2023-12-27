namespace ProductService;

public class ProductImageResponseDto
{
     public Guid ProductId {get;set;}
    public string ProductName {get;set;}="";
    public string Description {get;set;}="";
    public decimal Price {get;set;}
    public int Quantity {get;set;}
    public DateTime DateAdded {get;set;}
    public List<AddProductImageDto> Images {get;set;}=new List<AddProductImageDto>();

}
