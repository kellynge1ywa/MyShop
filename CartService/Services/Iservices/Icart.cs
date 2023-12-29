namespace CartService;

public interface Icart
{
    Task<Cart> AddtoCart(Cart newCart);
    Task<string>RemoveProduct(Guid ProductId);
    
    Task<List<Cart>> GetAllCarts();
    Task<Cart> GetOneCart(Guid CartId);
    Task<string> IncreaseQuantity(Guid ProductId, int Quantity);
    Task<string> DecreaseQuantity(Guid ProductId, int Quantity);
    


}
