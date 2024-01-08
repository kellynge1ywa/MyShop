using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CouponService;
[Route("api/[controller]")]
[ApiController, Authorize]
public class CouponsController : ControllerBase
{
    private readonly IMapper _IMapper;
    private readonly Icoupon _IcouponServices;
    private readonly ResponseDto _ResponseDto;

    public CouponsController(IMapper mapper, Icoupon icoupon)
    {
        _IMapper = mapper;
        _IcouponServices = icoupon;
        _ResponseDto = new ResponseDto();

    }

    [HttpPost]
     [Authorize(Roles="Admin")]
    public async Task<ActionResult<ResponseDto>> AddCoupon(AddCouponDto newCoupon)
    {
        try
        {
            var coupon = _IMapper.Map<Coupon>(newCoupon);
            var response = await _IcouponServices.AddCoupon(coupon);
            _ResponseDto.Result = response;
            return Created("", _ResponseDto);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }

    }
    [HttpGet]
    public async Task<ActionResult<ResponseDto>> GetCoupons()
    {
        var allCoupons = await _IcouponServices.GetAllCoupons();
        _ResponseDto.Result = allCoupons;
        return Ok(_ResponseDto);

    }
    [HttpGet("{Id}")]
    public async Task<ActionResult<ResponseDto>> GetCouponById(Guid Id)
    {
        var oneCoupon = await _IcouponServices.GetCoupon(Id);
        if (oneCoupon == null)
        {
            _ResponseDto.Error = "Coupon not found!";
            return NotFound(_ResponseDto);
        }
        _ResponseDto.Result = oneCoupon;
        return Ok(_ResponseDto);
    }
    [HttpGet("code/{code}")]
    public async Task<ActionResult<ResponseDto>> GetCouponByCode(string code)
    {
        var oneCoupon = await _IcouponServices.GetOneCoupon(code.ToLower());
        if (oneCoupon == null)
        {
            _ResponseDto.Error = "Coupon not found!";
            return NotFound(_ResponseDto);
        }
        _ResponseDto.Result = oneCoupon;
        return Ok(_ResponseDto);
    }
    [HttpPut("{Id}")]
     [Authorize(Roles="Admin")]
    public async Task<ActionResult<ResponseDto>> UpdateCoupon(Guid Id, AddCouponDto UpdatedCoupon){
        var coupon= await _IcouponServices.GetCoupon(Id);
        if(coupon==null){
            _ResponseDto.Error="Coupon not found!!";
            return NotFound(_ResponseDto);
        }
        _IMapper.Map(UpdatedCoupon, coupon);
        var response= await _IcouponServices.UpdateCoupon();
        _ResponseDto.Result=response;
        return Ok(_ResponseDto);

    }
    [HttpDelete("{Id}")]
     [Authorize(Roles="Admin")]
    public async Task<ActionResult<ResponseDto>> DeleteCoupon(Guid Id){

        var coupon= await _IcouponServices.GetCoupon(Id);
        if(coupon==null){
            _ResponseDto.Error="Coupon not found!!";
            return NotFound(_ResponseDto);
        }
        var deleted=await _IcouponServices.DeleteCoupon(coupon);
        _ResponseDto.Result=deleted;
        return Ok(_ResponseDto);

    }

}
