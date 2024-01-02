using Models;

namespace CartService;

public interface Icart
{
    Task<Cart> AddtoCart(AddCartDto newCart);
    Task<string>RemoveProduct(Guid ProductId);
    Task<string>DeleteCart(Guid Id);

    // Task<string> ApplyCoupon(Guid CartId,string CouponCode);


    
     Task saveChanges();
    
    Task<List<Cart>> GetAllCarts();
    Task<Cart> GetCartsByUserId(Guid UserId);
    Task<Cart> GetOneCart(Guid CartId);

     Task<ProductCart> GetCart(Guid CartId);
    Task<string> IncreaseQuantity(Guid ProductId, int Quantity);
    Task<string> DecreaseQuantity(Guid ProductId, int Quantity);

    
    


}
