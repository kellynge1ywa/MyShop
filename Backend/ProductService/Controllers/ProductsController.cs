﻿using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace ProductService;
[Route("api/[controller]")]
[ApiController]
public class ProductsController:ControllerBase
{
    private readonly IMapper _IMapper;
    private readonly Iproduct _Iproduct;
    private readonly IproductImage _IproductImage;
    private readonly ResponseDto _ResponseDto;
    public ProductsController(IMapper mapper, Iproduct Iproduct, IproductImage iproductImage)
    {
        _IMapper=mapper;
        _Iproduct=Iproduct;
        _IproductImage=iproductImage;
        _ResponseDto=new ResponseDto();
    }
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ResponseDto>> GetAllProducts(){
        var allProducts= await _Iproduct.GetAllProducts();
        _ResponseDto.Result=allProducts;
        
        return Ok(_ResponseDto);

    }
    [HttpGet("{Id}")]
    [Authorize]
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
    [Authorize(Roles="Admin")]
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
    [Authorize(Roles="Admin")]
    public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid Id, AddProductDto updatedProduct){
        var oneProduct= await _Iproduct.GetOneProduct(Id);
        if(oneProduct==null){
            _ResponseDto.Error="Product not found";
            return NotFound(_ResponseDto);
        }
        _IMapper.Map(updatedProduct,oneProduct);
        var response= await _Iproduct.UpdateProduct(Id,updatedProduct);
        _ResponseDto.Result=response;
        return Ok(_ResponseDto);

    }
    [HttpDelete("{Id}")]
    [Authorize(Roles="Admin")]
    public async Task<ActionResult<ResponseDto>> DeleteProduct(Guid Id){
        var product= await _Iproduct.GetOneProduct(Id);
        if(product==null){
            _ResponseDto.Error="Product not found";
            return NotFound(_ResponseDto);
        }
        var deleted= await _Iproduct.DeleteProduct(product);
        _ResponseDto.Result=deleted;
        return Ok(_ResponseDto);


    }

}
