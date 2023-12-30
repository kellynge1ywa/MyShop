using Models;

namespace ProductService;

public interface Iproduct
{
    Task<string> AddProduct(Product newProduct);
    Task<string>UpdateProduct();
    Task<string> DeleteProduct(Product product);
    Task<Product> GetOneProduct(Guid Id);
    Task<List<ProductImageResponseDto>>GetAllProducts();

}
