﻿

using Microsoft.EntityFrameworkCore;

namespace ProductService;

public class ProductImageService : IproductImage
{
    private readonly AppDbContext _appDbContext;
    public ProductImageService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<string> AddProductImage(Guid Id, ProductImages productImages)
    {
        var product = await _appDbContext.Products.Where(k=>k.ProductId==Id).FirstOrDefaultAsync();
        if (product!=null)
        {
            product.ProductImages.Add(productImages);
            await _appDbContext.SaveChangesAsync();
            return "Image added";

        }
        else
        {
            return "Product not found";
        }


    }
}