using System.ComponentModel.DataAnnotations;

namespace MyShop.Models.Auth;

public class LoginRequestDto
{
     [Required]
     [EmailAddress]
    public string Email {get;set;}="";
     [Required]
    public string Password {get;set;}="";

}
