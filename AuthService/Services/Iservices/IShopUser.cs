namespace AuthService;

public interface IShopUser
{
    Task<string> SignUpUser(RegisterUserDto NewUser);
    Task<LoginResponseDto> SignInUser(LoginRequestDto LoginUser);
    Task<bool> AssignUserRole(string Email, string RoleName);

}
