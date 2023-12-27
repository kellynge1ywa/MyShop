using System.ComponentModel.DataAnnotations;

namespace ProductService;

public class AddProductImageDto
{
   
    [Required]
     public string Image {get;set;}="";

}
