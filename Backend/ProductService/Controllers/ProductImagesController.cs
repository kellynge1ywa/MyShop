using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ProductService;
[Route("api/[controller]")]
[ApiController]
public class ProductImagesController:ControllerBase
{
    private readonly IMapper _IMapper;
    private readonly Iproduct _Iproduct;
    private readonly IproductImage _IproductImage;
    private readonly ResponseDto _ResponseDto;
    public ProductImagesController(IMapper Imapper,Iproduct Iproduct, IproductImage IproductImage)
    {
        _IMapper=Imapper;
        _Iproduct=Iproduct;
        _IproductImage=IproductImage;
        _ResponseDto=new ResponseDto();
        
    }
    [HttpPost("{Id}")]
    [Authorize(Roles="Admin")]
    public async Task<ActionResult<ResponseDto>> AddImages(Guid Id, AddProductImageDto productImage){
        var product= await _Iproduct.GetOneProduct(Id);
        if(product==null){
            _ResponseDto.Error="Product not found";
            return NotFound(_ResponseDto);
        }
        var Image= _IMapper.Map<ProductImages>(productImage);
        var response = await _IproductImage.AddProductImage(Id,Image);
        _ResponseDto.Result=response;
        return Created("",_ResponseDto);
    }

}
