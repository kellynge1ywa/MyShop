namespace ProductService;

public class Product
{
    public Guid ProductId {get;set;}
    public string ProductName {get;set;}="";
    public string Description {get;set;}="";
    public decimal Price {get;set;}
    public int Quantity {get;set;}
    public DateTime DateAdded {get;set;}
    public List<ProductImages> ProductImages {get;set;} =new List<ProductImages>();

}
