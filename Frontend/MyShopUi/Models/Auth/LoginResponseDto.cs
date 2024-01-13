namespace MyShopUi.Models.Auth;

public class LoginResponseDto
{
    public string Token {get;set;}="";
    public ShopUserDto User {get;set;}=default!;

}
