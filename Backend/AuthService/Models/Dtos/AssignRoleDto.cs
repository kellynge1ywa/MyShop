using System.ComponentModel.DataAnnotations;

namespace AuthService;

public class AssignRoleDto
{
    [Required]
    public string Fullname {get;set;}="";
    [Required]
    [EmailAddress]
    public string Email {get;set;}="";
    [Required]
    public string Phone {get;set;}="";
    [Required]
    public string Password {get;set;}="";
   
    [Required]
    public string Role {get;set;}="";

}
