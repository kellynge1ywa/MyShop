﻿using System.ComponentModel.DataAnnotations;

namespace MyShop;

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
