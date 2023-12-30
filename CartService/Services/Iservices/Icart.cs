using Models;

namespace CartService;

public interface Icart
{
    Task<Cart> AddtoCart(AddCartDto newCart);
    Task<string>RemoveProduct(Guid ProductId);
    Task<string>DeleteCart(Guid Id);
    
    Task<List<Cart>> GetAllCarts();
    Task<Cart> GetCartsByUserId(Guid UserId);
    Task<Cart> GetOneCart(Guid CartId);
    Task<string> IncreaseQuantity(Guid ProductId, int Quantity);
    Task<string> DecreaseQuantity(Guid ProductId, int Quantity);
    


}
