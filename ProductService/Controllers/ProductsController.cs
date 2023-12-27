using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProductService;
[Route("api/[controller]")]
[ApiController]
public class ProductsController:ControllerBase
{
    private readonly IMapper _IMapper;
    private readonly Iproduct _Iproduct;
    private readonly ResponseDto _ResponseDto;
    public ProductsController(IMapper mapper, Iproduct Iproduct)
    {
        _IMapper=mapper;
        _Iproduct=Iproduct;
        _ResponseDto=new ResponseDto();
    }
    [HttpGet]
    public async Task<ActionResult<ResponseDto>> GetAllProducts(){
        var allProducts= await _Iproduct.GetAllProducts();
        _ResponseDto.Result=allProducts;
        return Ok(_ResponseDto);

    }
    [HttpGet("{Id}")]
    public async Task<ActionResult<ResponseDto>> GetOneProduct(Guid Id){
        var oneProduct= await _Iproduct.GetOneProduct(Id);
        if(oneProduct==null){
            _ResponseDto.Error="Product not found";
            return NotFound(_ResponseDto);
        }
        _ResponseDto.Result=oneProduct;
        return Ok(_ResponseDto);
    }
    [HttpPost]
    public async Task<ActionResult<ResponseDto>> AddNewProduct(AddProductDto addProduct){
        try{
            var product=_IMapper.Map<Product>(addProduct);
        var response= await _Iproduct.AddProduct(product);
        _ResponseDto.Result=response;
        return Created("",_ResponseDto);

        } catch(Exception ex){
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
        
    }
    [HttpPut("{Id}")]
    public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid Id, AddProductDto updatedProduct){
        var oneProduct= await _Iproduct.GetOneProduct(Id);
        if(oneProduct==null){
            _ResponseDto.Error="Product not found";
            return NotFound(_ResponseDto);
        }
        _IMapper.Map(updatedProduct,oneProduct);
        var response= await _Iproduct.UpdateProduct();
        _ResponseDto.Result=response;
        return Ok(_ResponseDto);

    }
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ResponseDto>> DeleteProduct(Guid Id){
        var oneProduct= await _Iproduct.GetOneProduct(Id);
        if(oneProduct==null){
            _ResponseDto.Error="Product not found";
            return NotFound(_ResponseDto);
        }
        var deleted= await _Iproduct.DeleteProduct(oneProduct);
        _ResponseDto.Result=deleted;
        return Ok(_ResponseDto);


    }

}
