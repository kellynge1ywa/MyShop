using Models;

namespace OrderService;

public interface ICart
{
    Task<Cart> GetCartById(Guid CartId);
}
