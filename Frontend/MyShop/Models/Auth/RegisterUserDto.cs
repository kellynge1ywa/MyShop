using System.ComponentModel.DataAnnotations;

namespace MyShop.Models.Auth;

public class RegisterUserDto
{
    [Required]
    public string Fullname {get;set;}="";
    [Required]
    [EmailAddress]
    public string Email {get;set;}="";
    [Required]
    public string Phone{get;set;}="";
    
    [Required]
    public DateTime DOB {get;set;}
    [Required]
    public string Residence {get;set;}="";
    [Required]
    public string Password {get;set;}="";
   
    public string? Role {get;set;}="User";

}
