using MyShopUi.Models.Auth;

namespace MyShopUi.Services.Auth;

public interface IuserAuth
{
     Task<ResponseDto> Register(RegisterUserDto registerUserDto);

    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

}
