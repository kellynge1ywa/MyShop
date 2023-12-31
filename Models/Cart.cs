using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Cart
    {
        [Key]
    public Guid Id {get;set;}
    
    [ForeignKey("UserId")]
    public Guid UserId {get;set;}
   
   
    public DateTime OrderDate {get;set;}
    public string Status {get;set;}="Pending";
    
    public List<ProductCart> Products{get;set;}=new List<ProductCart>();
        
    }
}