using Microsoft.AspNetCore.Mvc;
using MyShopMessageBus;

namespace AuthService;
[Route("api/[controller]")]
[ApiController]

public class UsersController:ControllerBase
{
    private readonly IShopUser _UserServices;
    private readonly ResponseDto _ResponseDto;
    private readonly IConfiguration _IConfiguration;
    public UsersController(IShopUser ShopUser, IConfiguration configuration)
    {
        _UserServices=ShopUser;
        _IConfiguration=configuration;
        _ResponseDto=new ResponseDto();
        
    }
    [HttpPost("Register")]
    public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUser){
        var response = await _UserServices.SignUpUser(registerUser);

        if(string.IsNullOrWhiteSpace(response)){
            _ResponseDto.Result="Registration successful";

            //Add message to queue
            var message = new ShopUserMessageDto(){
                Fullname=registerUser.Fullname,
                Email=registerUser.Email

            };
            var MessageBody=new MessageBus();
            await MessageBody.PublishMessage(message,_IConfiguration.GetValue<string>("ServiceBus:register")!);
            

            return Created("", _ResponseDto);
        }
        _ResponseDto.Error=response;
        _ResponseDto.Success=false;
        return BadRequest(_ResponseDto);
    }
    [HttpPost("Login")]
    public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequest){
        var response= await _UserServices.SignInUser(loginRequest);
        //Check details exist
        if(response.User!=null){
            _ResponseDto.Result=response;
            return Ok(_ResponseDto);
        }
        _ResponseDto.Error="Invalid credentials";
        _ResponseDto.Success=false;
        return BadRequest(_ResponseDto);


    }
    [HttpPost("AssignRole")]
    public async Task<ActionResult<ResponseDto>> AssignRole(AssignRoleDto assignRole){
        var response= await _UserServices.AssignUserRole(assignRole.Email, assignRole.Role);
        if(response){
            _ResponseDto.Result=response;
            return Ok(_ResponseDto);
        }
        _ResponseDto.Error="Cannot assign role";
        _ResponseDto.Result=response!;
        _ResponseDto.Success=false;
        return BadRequest(_ResponseDto);

    }

}
