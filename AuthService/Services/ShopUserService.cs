
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService;

public class ShopUserService : IShopUser
{
    private readonly ShopDbContext _ShopDbContext;
    private readonly IMapper _IMapper;
    private readonly UserManager<ShopUser> _UserManager;
    private readonly RoleManager<IdentityRole> _RoleManager;
    private readonly Ijwt _JwtService;
    public ShopUserService(ShopDbContext shopDbContext, IMapper IMapper, UserManager<ShopUser> UserManager, RoleManager<IdentityRole> RoleManager, Ijwt ijwt)
    {
        _ShopDbContext = shopDbContext;
        _IMapper = IMapper;
        _UserManager = UserManager;
        _RoleManager = RoleManager;
        _JwtService=ijwt;

    }
    public async Task<bool> AssignUserRole(string Email, string RoleName)
    {
        try{
            //Find if there is a user with provided email
            var response= await _ShopDbContext.ShopUsers.Where(k=>k.Email!.ToLower()==Email.ToLower()).FirstOrDefaultAsync();

            //Check if user exists
            if(response==null){
                return  false;
            } else{
                //Check if role exists
                if(!_RoleManager.RoleExistsAsync(RoleName).GetAwaiter().GetResult()){
                     //Create a role if role does not exist
                    await _RoleManager.CreateAsync(new IdentityRole(RoleName));
                
                   

                }
                 //If role exists
                await _UserManager.AddToRoleAsync(response,RoleName);
                return true;
               
            }
        } 
        catch(Exception ex){
            Console.WriteLine(ex.Message);
            return false;

        }
    }

    public async Task<LoginResponseDto> SignInUser(LoginRequestDto LoginUser)
    {
        try
        {
            //Check if user exists
            var user = await _ShopDbContext.ShopUsers.Where(k => k.UserName!.ToLower() == LoginUser.Email.ToLower()).FirstOrDefaultAsync();

            //Check if hashed password is equal to plain password
            var IsPasswordValid = _UserManager.CheckPasswordAsync(user!, LoginUser.Password).GetAwaiter().GetResult();
            //check if either user in null or password is wrong
            if (user == null || !IsPasswordValid)
            {
                //If either user is null or password is wrong
                return new LoginResponseDto();
            }
            //Map user to type of shopUserDto
            var LoggedUser = _IMapper.Map<ShopUserDto>(user);
            //Get roles
            var roles= await _UserManager.GetRolesAsync(user);

            //Get token
            var token=  _JwtService.GenerateToken(user,roles);
            var LoggedInuser = new LoginResponseDto()
            {
                User = LoggedUser,
                Token = token

            };
            return LoggedInuser;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new LoginResponseDto();

        }



    }

    public async Task<string> SignUpUser(RegisterUserDto NewUser)
    {
        try
        {
            var newuser = _IMapper.Map<ShopUser>(NewUser);
            //Create new user
            var result = await _UserManager.CreateAsync(newuser, NewUser.Password);
            //Check if user was created
            if (result.Succeeded)
            {
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault()!.Description;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
