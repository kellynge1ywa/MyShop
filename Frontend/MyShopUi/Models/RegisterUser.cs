using System.ComponentModel.DataAnnotations;

namespace MyShopUi.Models;

public class RegisterUser
{
     [Required]
    public string Name {get;set;}="";
    [Required]
    [EmailAddress]
    public string Email {get;set;}="";
    [Required]
    public string Password {get;set;}="";

}
