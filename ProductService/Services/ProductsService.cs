
using Microsoft.EntityFrameworkCore;
using Models;

namespace ProductService;

public class ProductsService : Iproduct
{
    private readonly AppDbContext _AppDbContext;
    public ProductsService(AppDbContext appDbContext)
    {
        _AppDbContext = appDbContext;
    }
    public async Task<string> AddProduct(Product newProduct)
    {
        try
        {
            _AppDbContext.Products.Add(newProduct);
            await _AppDbContext.SaveChangesAsync();
            return "Product Added successfully!!";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    public async Task<string> DeleteProduct(Product product)
    {
        try
        {
            _AppDbContext.Products.Remove(product);
            await _AppDbContext.SaveChangesAsync();
            return "Product deleted successfully";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<List<ProductImageResponseDto>> GetAllProducts()
    {
        return await _AppDbContext.Products.Select(K => new ProductImageResponseDto()
        {
            ProductId = K.Id,
            ProductName = K.ProductName,
            Description = K.Description,
            Price = K.Price,
            Quantity = K.Quantity,
            DateAdded = K.DateAdded,
            Images = K.ProductImages.Select(I => new ImageDto()
            {

                Image = I.Image

            }).ToList()

        }).ToListAsync();

    }

    public async Task<Product> GetOneProduct(Guid Id)
    {
        try
        {
            var oneProduct = await _AppDbContext.Products.Select(K => new Product()
            {
                Id = K.Id,
                ProductName = K.ProductName,
                Description = K.Description,
                Price = K.Price,
                Quantity = K.Quantity,
                DateAdded = K.DateAdded,
                ProductImages= K.ProductImages.Select(I => new ProductImages()
                {
                    ImageId=I.ImageId,
                    Image = I.Image,
                    ProductId=I.ProductId,
                    

                }).ToList()

            }).Where(k => k.Id == Id).FirstOrDefaultAsync();
            if (oneProduct == null)
            {
                return new Product();
            }
            return oneProduct;


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Product();
        }


    }

    public async Task<string> UpdateProduct()
    {
        try
        {
            await _AppDbContext.SaveChangesAsync();
            return "Product updated successfully!!";

        }
        catch (Exception ex)
        {
            return ex.Message;
        }


    }
}
