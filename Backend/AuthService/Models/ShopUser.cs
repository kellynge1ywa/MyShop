using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AuthService;

public class ShopUser:IdentityUser
{
    
    public string Fullname {get;set;}="";
    public DateTime DOB {get;set;}
    public string Residence {get;set;}="";
    public DateTime RegisteredDate {get;set;}

}
