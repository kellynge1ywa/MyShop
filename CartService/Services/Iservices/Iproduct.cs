namespace CartService;

public interface IProduct
{
    Task<ProductDto> GetOneProduct(Guid productId);

}
