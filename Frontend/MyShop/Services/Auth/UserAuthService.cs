using System.Text;
using MyShop.Models.Auth;
using Newtonsoft.Json;

namespace MyShop.Services.Auth;

public class UserAuthService : IuserAuth
{
    private readonly HttpClient _HttpClient;
    private readonly string  UserURL="https://localhost:7010";
    public UserAuthService(HttpClient httpClient)
    {
        _HttpClient=httpClient;
    }
    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var request= JsonConvert.SerializeObject(loginRequestDto);
        var bodyContent=new StringContent(request,Encoding.UTF8,"application/json");
        //communicate with backend
        var response=await _HttpClient.PostAsync($"{UserURL}/api/Users/Login",bodyContent);
        var content= await response.Content.ReadAsStringAsync();
        var results=JsonConvert.DeserializeObject<ResponseDto>(content);
        if(results.Success){
            return JsonConvert.DeserializeObject<LoginResponseDto>(results.Result.ToString());
        }

        return new LoginResponseDto();
    }

    public async Task<ResponseDto> Register(RegisterUserDto registerUserDto)
    {
        var request= JsonConvert.SerializeObject(registerUserDto);
        var bodyContent=new StringContent(request, Encoding.UTF8,"application/json");
        //communicate with backend

        var response= await _HttpClient.PostAsync($"{UserURL}/api/Users/Register",bodyContent);
        var content= await response.Content.ReadAsStringAsync();
        var results =JsonConvert.DeserializeObject<ResponseDto>(content);
        if(results.Success){
            return results;
        }
        return new ResponseDto();
    }
}
