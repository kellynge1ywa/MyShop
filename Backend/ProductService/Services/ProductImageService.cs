

using Microsoft.EntityFrameworkCore;
using Models;

namespace ProductService;

public class ProductImageService : IproductImage
{
    private readonly ShopDbContext _appDbContext;
    public ProductImageService(ShopDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<string> AddProductImage(Guid Id, ProductImages productImages)
    {
        var product = await _appDbContext.Products.Where(k=>k.Id==Id).FirstOrDefaultAsync();
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

    public async Task<List<ProductImages>> GetAllProductImage(Guid Id)
    {
        return await _appDbContext.ProductImages.Where(k=>k.ProductId==Id).ToListAsync();
        
    }
}
