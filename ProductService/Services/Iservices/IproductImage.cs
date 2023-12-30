using Models;

namespace ProductService;

public interface IproductImage
{
    Task<string> AddProductImage(Guid Id, ProductImages productImages);
    Task<List<ProductImages>> GetAllProductImage(Guid Id);

}
