using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartService;
[Route("api/[controller]")]
[ApiController]
public class CartsController:ControllerBase
{
    private readonly IMapper _Imapper;
    private readonly Icart _Icart;
    private readonly Iproduct _Iproduct;
    private readonly Icoupon _Icoupon;
    private readonly ResponseDto _response;
    public CartsController(IMapper mapper,Icart icart,Iproduct iproduct,Icoupon icoupon)
    {
        _Imapper=mapper;
        _Icart=icart;
        _Iproduct=iproduct;
        _response=new ResponseDto();
    }

    [HttpGet]
    public async Task<ActionResult<List<Cart>>> GetAllCarts(){
        var Carts= await _Icart.GetAllCarts();
        _response.Result=Carts;
        return Ok(_response);
    }

   // [HttpGet("{Id}")]
    // public async Task<ActionResult<ResponseDto>> GetOneCart(Guid Id){
    //     var Cart=await _Icart.GetOneCart(Id);
    //     if(Cart==null){
    //         _response.Error="Cart not found";
    //         return NotFound(_response);
    //     }
    //     _response.Result=Cart;
    //     return Ok(_response);
    //}

   [HttpPost]
    [Authorize]
    public async Task<ActionResult<ResponseDto>> AddCart( AddCartDto addCart){
        
        var UserId=User.Claims.FirstOrDefault(claim=>claim.Type==ClaimTypes.NameIdentifier)?.Value;
        if(UserId==null){
            _response.Error="Invalid UserdId";
            return BadRequest(_response);
        }
        var NewCart= _Imapper.Map<Cart>(addCart);

       
        NewCart.UserId=new Guid(UserId);
        
        
      
        




        var AddedCart= await _Icart.AddtoCart(NewCart);
        _response.Result=AddedCart;
        return Created("",_response);
    }
}



