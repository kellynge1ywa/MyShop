namespace CartService;

public interface Iproduct
{
    Task<ProductDto> GetOneProduct(Guid productId);

}
