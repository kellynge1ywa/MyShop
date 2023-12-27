using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductService;

public class ProductImages
{
    [Key]
    public Guid ImageId {get;set;}
    public string Image {get;set;}="";
    [ForeignKey("ProductId")]
     public Guid ProductId {get;set;}
    public Product ImageProduct {get;set;}=default!;
    

}
