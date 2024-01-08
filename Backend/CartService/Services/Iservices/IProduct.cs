using Models;

namespace CartService;

public interface IProduct
{
    Task<Product> GetOneProduct(Guid productId, string Token);

}
