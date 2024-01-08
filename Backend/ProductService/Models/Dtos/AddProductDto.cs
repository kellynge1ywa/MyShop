using System.ComponentModel.DataAnnotations;

namespace ProductService;

public class AddProductDto
{
    [Required]
    public string ProductName {get;set;}="";
     [Required]
    public string Description {get;set;}="";
     [Required]
     
    public decimal Price {get;set;}
     [Required]
    public int Quantity {get;set;}
     [Required]
    public DateTime DateAdded {get;set;}

}
