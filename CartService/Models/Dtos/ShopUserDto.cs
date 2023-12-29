using System.ComponentModel.DataAnnotations;

namespace CartService;

public class ShopUserDto
{
    [Key]
    public Guid UserId {get;set;}
    public string Fullname {get;set;}="";
    
    public string Email {get;set;}="";
    
    public string Phone {get;set;}="";
    public string Residence {get;set;}="";
    
}
