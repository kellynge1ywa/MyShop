using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CartService;
[Route("api/[controller]")]
[ApiController, Authorize]
public class CartsController : ControllerBase
{
    private readonly IMapper _Imapper;
    private readonly Icart _Icart;
    private readonly IProduct _Iproduct;
    private readonly Icoupon _Icoupon;
    private readonly ResponseDto _response;
    public CartsController(IMapper mapper, Icart icart, IProduct iproduct, Icoupon icoupon)
    {
        _Imapper = mapper;
        _Icart = icart;
        _Iproduct = iproduct;
        _response = new ResponseDto();
    }

    [HttpPost]
    public async Task<ActionResult<Cart>> AddToCart(AddCartDto newcart)
    {
        var UserId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        newcart.UserId = new Guid(UserId);

        var product = await _Iproduct.GetOneProduct(newcart.ProductId);
        Console.WriteLine($"Product name {product.ProductName}");
        if (string.IsNullOrWhiteSpace(product.Id.ToString()))
        {
            _response.Error = "Product not found";
            return NotFound(_response);    
        } else{
         if(product.Quantity > 1){
                var response = await _Icart.AddtoCart(newcart);
                _response.Result = response;
                return Ok(_response);

            }else {
                _response.Error="Product not available at the moment";
                return BadRequest(_response);
            } 
        }   
        
    }
    [HttpGet("AllCarts")]
    public async Task<ActionResult<ResponseDto>> GetAllCarts()
    {
        var allCarts = await _Icart.GetAllCarts();
        _response.Result = allCarts;
        return Ok(_response);
    }
    [HttpGet("Cart/{Id}")]
    public async Task<ActionResult<Cart>> GetOneCart(Guid Id)
    {
        var cart = await _Icart.GetOneCart(Id);
        _response.Result = cart;
        return Ok(_response);
    }

    [HttpPost("Decrease/{Id}")]
    public async Task<ActionResult<ResponseDto>> DecreaseQuantity(Guid Id, int Quantity)
    {
        var value = await _Icart.DecreaseQuantity(Id, Quantity);
        _response.Result = value;
        return Ok(_response);
    }

    [HttpPost("Increase/{Id}")]
    public async Task<ActionResult<ResponseDto>> IncreaseQuantity(Guid Id, int Quantity)
    {
        var value = await _Icart.IncreaseQuantity(Id, Quantity);
        _response.Result = value;
        return Ok(_response);
    }
    [HttpPost("RemoveProduct/{Id}")]
    public async Task<ActionResult<ResponseDto>> RemoveProduct(Guid Id)
    {
        var item = await _Icart.RemoveProduct(Id);
        _response.Result = item;
        return Ok(_response);
    }
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ResponseDto>> DeleteCart(Guid Id)
    {
        var item = await _Icart.DeleteCart(Id);
        _response.Result = item;
        return Ok(_response);

    }









}



