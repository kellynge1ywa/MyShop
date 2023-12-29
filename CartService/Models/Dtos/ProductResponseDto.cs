using System.ComponentModel.DataAnnotations;

namespace CartService;

public class ProductResponseDto
{
    [Key]
    public Guid ProductId {get;set;}
    public string ProductName {get;set;}="";
    public string Description {get;set;}="";
    public decimal Price {get;set;}

}
