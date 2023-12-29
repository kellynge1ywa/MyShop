
using Microsoft.EntityFrameworkCore;

namespace CartService;

public class CartServices :Icart
{
    private readonly AppDbContext _dbcontext;
    public CartServices(AppDbContext dbContext)
    {
        _dbcontext = dbContext;

    }

    public async Task<Cart> AddtoCart(Cart newCart)
    {
        
        {
            var CartItem = await _dbcontext.Carts.FirstOrDefaultAsync();
            if (CartItem == null)
            {
                _dbcontext.Carts.Add(newCart);
                await _dbcontext.SaveChangesAsync();
                return CartItem;

            } else{
                Console.WriteLine($"Cart with exists  ");
                return new Cart();
            }
        }
    }

    public Task<string> DecreaseQuantity(Guid ProductId, int Quantity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Cart>> GetAllCarts()
    {
        return await _dbcontext.Carts.Include(j=>j.Products).ToListAsync();
    }

    public Task<Cart> GetOneCart(Guid CartId)
    {
        throw new NotImplementedException();
    }

    public Task<string> IncreaseQuantity(Guid ProductId, int Quantity)
    {
        throw new NotImplementedException();
    }

    public Task<string> RemoveProduct(Guid ProductId)
    {
        throw new NotImplementedException();
    }
}

    

  

   

   

   

    // public async Task<Cart> AddtoCart(Cart newCart)
    // {
    //     try
    //     {
    //         var CartItem = await _dbcontext.Carts.FirstOrDefaultAsync();
    //         if (CartItem == null)
    //         {
    //             _dbcontext.Carts.Add(newCart);
    //             await _dbcontext.SaveChangesAsync();
    //             return CartItem;

    //         } else{
    //             Console.WriteLine($"Cart with exists  ");
    //             return new Cart();
    //         }


    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine(ex.Message);
    //        return new Cart();
    //     }
    // }

    // public async Task<string> DecreaseQuantity(Guid ProductId, int Quantity)
    // {
    //     try
    //     {
    //         var CartItem = await _dbcontext.Carts.FirstOrDefaultAsync();
    //         if (CartItem != null)
    //         {
    // if (CartItem.Quantity >= Quantity)
    // {
    //     CartItem.Quantity -= Quantity;
    //     await _dbcontext.SaveChangesAsync();
    //     return "Product quantity decreased successfully";
    //         // }
    //         else
    //         {
    //             return "Cart item not found";
    //         }

    //     }
    //     else
    //     {
    //         return "Cart item quantity is less than the requested quantity  deduction figure";
    //     }



    // }
    //     catch (Exception ex)
    //     {
    //         return ex.Message;
    //     }
    // }

    // public async Task<List<Cart>> GetAllCarts()
    // {
    //     return await _dbcontext.Carts.Include(c=>c.Products).ToListAsync();
    // }

    // public async Task<Cart> GetOneCart(Guid CartId)
    // {
    //     var cart=await _dbcontext.Carts.Include(k=>k.Products).FirstOrDefaultAsync();
    //     if(cart==null){
    //         return new Cart();
    //     }
    //     return cart;
    // }

    // public async Task<string> IncreaseQuantity(Guid ProductId, int Quantity)
    // {
    //     try
    //     {
    //         var CartItem = await _dbcontext.Carts.FirstOrDefaultAsync();
    //         if (CartItem != null)
    //         {
    //             CartItem.Quantity += Quantity;
    //             await _dbcontext.SaveChangesAsync();
    //             return "Product quantity increased successfully";
    //         }
    //         else
    //         {
    //             return "Cart item not found";
    //         }


    //     }
    //     catch (Exception ex)
    //     {
    //         return ex.Message;
    //     }
    // }

    // public async Task<string> RemoveProduct(Guid ProductId)
    // {
    //     try
    //     {
    //         var product = await _dbcontext.Carts.FirstOrDefaultAsync();
    //         _dbcontext.Remove(product);
    //         await _dbcontext.SaveChangesAsync();
    //         return "Product deleted successfully";



    //     }
    //         catch (Exception ex)
    //         {
    //             return ex.Message;
    //         }
    //     }

