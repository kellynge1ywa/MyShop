using MyShop.Models.Auth;

namespace MyShop.Services.Auth;

public interface IuserAuth
{
     Task<ResponseDto> Register(RegisterUserDto registerUserDto);

    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

}
