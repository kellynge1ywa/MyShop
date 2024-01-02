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
        try
        {
            var token=HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            // var UserId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            
            // newcart.UserId = new Guid(UserId);

            var product = await _Iproduct.GetOneProduct(newcart.ProductId, token);
            Console.WriteLine($"Product name {product.ProductName}");
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                _response.Error = "Product not found";
                return NotFound(_response);
            }
            if(product.Quantity < 1){
                _response.Error = "Product not available at the moment";
                return BadRequest(_response);

            }
           
            // newcart= new AddCartDto(){
            //     ProductId=product.Id,
            //     UserId=new Guid(UserId),
            //     Price=product.Price,
            //     Quantity=newcart.Quantity,
                
            // };
        
            
             var response = await _Icart.AddtoCart(newcart);
             _response.Result = response;
             return Ok(_response);
           

        } catch(Exception ex){
            _response.Error=ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, _response);
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
      [HttpGet("ProductCart/{Id}")]
    public async Task<ActionResult<Cart>> GetOneProductCart(Guid Id)
    {
        var cart = await _Icart.GetCart(Id);
        _response.Result = cart;
        return Ok(_response);
    }

    [HttpGet("UserCart/{UserId}")]
    public async Task<ActionResult<Cart>> GetCartByUserId(Guid UserId)
    {
        var cart = await _Icart.GetCartsByUserId(UserId);
        _response.Result = cart;
        return Ok(_response);
    }

    [HttpGet("coupon/{code}")]
    public async Task<ActionResult<Cart>> GetCartByUserId(string code)
    {
        var token=HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

        var cart = await _Icoupon.GetCouponByCode(code,token);
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

    [HttpPut("Coupon/{CouponCode}")]
   
    public async Task<ActionResult<ResponseDto>> ApplyCoupon( string CouponCode, Guid Id){
        try{
            var token=HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var UserId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            var userCart= await _Icart.GetCartsByUserId(new Guid(UserId));
            
           
            if(userCart==null){
                _response.Error = "Cart not found";
                return NotFound(_response);
            }
             var cartitem= await _Icart.GetCart(Id);
             if(cartitem==null){
                _response.Error = "CartItem not found";
                return NotFound(_response);

             }

            var coupon= await _Icoupon.GetCouponByCode(CouponCode,token);
            if(coupon==null){
                _response.Error = "Coupon not found";
                return NotFound(_response);
            }
            if(coupon.CouponMinAmount <= cartitem.Subtotal){
                userCart= new Cart(){
                    Id=userCart.Id,
                    UserId= new Guid(UserId),
                    OrderDate=userCart.OrderDate,
                    Status=userCart.Status,
                    Products=userCart.Products.Select(cartitem=> new ProductCart(){
                        Id=cartitem.Id,
                        CartId=cartitem.CartId,
                        ProductId=cartitem.ProductId,
                        CouponCode=coupon.CouponCode,
                        Price=cartitem.Price,
                        Quantity=cartitem.Quantity,
                        Subtotal=cartitem.Subtotal,
                        Discount=coupon.CouponAmount
                    }).ToList(),
                   
                      
                };
                 await _Icart.saveChanges();
                // cartitem.CouponCode=coupon.CouponCode;
                // cartitem.Discount=coupon.CouponAmount;
                // await _Icart.saveChanges();
            } 
            _response.Error="You have not attained the minimum amount to get coupon";
            return BadRequest(_response);
        } catch (Exception ex){
            _response.Error=ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, _response);

        }

    }









}



