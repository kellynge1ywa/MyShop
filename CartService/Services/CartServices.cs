
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CartService;

public class CartServices : Icart
{
    private readonly AppDbContext _dbcontext;

    public CartServices(AppDbContext dbContext)
    {
        _dbcontext = dbContext;


    }

    public async Task<Cart> AddtoCart(AddCartDto newCart)
    {
        var cart = await _dbcontext.Carts.Where(k => k.UserId == newCart.UserId && k.Status == "Pending").FirstOrDefaultAsync();
        if (cart == null)
        {
            cart = new Cart
            {
                OrderDate = DateTime.Now,
                Status = "Pending",
                UserId = newCart.UserId,
            };
            _dbcontext.Carts.Add(cart);
            await _dbcontext.SaveChangesAsync();
        }
        var existingProduct = await _dbcontext.ProductCarts.Where(p => p.ProductId == newCart.ProductId && cart.Id == p.CartId).FirstOrDefaultAsync();
        if (existingProduct == null)
        {
            existingProduct = new ProductCart
            {
                Id = new Guid(),
                CartId = cart.Id,
                ProductId = newCart.ProductId,
                Quantity = newCart.Quantity,
                
                
                
            };
            _dbcontext.ProductCarts.Add(existingProduct);
        }
        else
        {
            existingProduct.Quantity += newCart.Quantity;
        }

        await _dbcontext.SaveChangesAsync();
        return cart;



        //  _dbcontext.Carts.Add(newCart)
        // await _dbcontext.SaveChangesAsync();
        // return Cart;
    }

    public async Task<string> DecreaseQuantity(Guid ProductId, int Quantity)
    {
        var cartitem = await _dbcontext.ProductCarts.Where(k => k.ProductId == ProductId).FirstOrDefaultAsync();
        if (cartitem != null)
        {
            if (cartitem.Quantity >= Quantity)
            {
                cartitem.Quantity -= Quantity;
                if(cartitem.Quantity < 1){
                    await RemoveProduct(cartitem.ProductId);
                    return "Product deleted succesfully from the cart";
                }
                await _dbcontext.SaveChangesAsync();
                return "Quantity reduced!!";
            }
            else
            {
                return $"Product quantity {cartitem.Quantity} is less than the requested quantity value {Quantity}";
            }

        }
        else
        {
            return "Product not found";
        }
    }

    public async Task<string> DeleteCart(Guid Id)
    {
         var oneCart= await _dbcontext.Carts.Where(c=>c.Id==Id).FirstOrDefaultAsync();
        if(oneCart == null){
            return "Cart not found";
        }else{
            _dbcontext.Carts.Remove(oneCart);
            await _dbcontext.SaveChangesAsync();
            return "Cart Deleted successfully";
        }
    }

    public async Task<List<Cart>> GetAllCarts()
    {
        return await _dbcontext.Carts.Include(k => k.Products).ToListAsync();
    }

    public async Task<Cart> GetCartsByUserId(Guid UserId)
    {
        return await _dbcontext.Carts.Include(k => k.Products).FirstOrDefaultAsync(c => c.UserId == UserId);
    }

    public async Task<Cart> GetOneCart(Guid CartId)
    {
        var oneCart= await _dbcontext.Carts.Where(c=>c.Id==CartId).FirstOrDefaultAsync();
        if(oneCart == null){
            return new Cart();
        }else{
            return oneCart;
        }
    }

    public async Task<string> IncreaseQuantity(Guid ProductId, int Quantity)
    {
        var cartitem = await _dbcontext.ProductCarts.Where(k => k.ProductId == ProductId).FirstOrDefaultAsync();
        if (cartitem != null)
        {
            if (Quantity > 1)
            {
                cartitem.Quantity += Quantity;
                await _dbcontext.SaveChangesAsync();
                return "Quantity increased!!";
            }
            else
            {
                return $"Value must be more than one";
            }

        }
        else
        {
            return "Product not found";
        }
    }

    public async Task<string> RemoveProduct(Guid ProductId)
    {
        try
        {
            var cartitem = await _dbcontext.ProductCarts.Where(k => k.ProductId == ProductId).FirstOrDefaultAsync();
            if (cartitem == null)
            {
                return "Product not found";
            }
            _dbcontext.ProductCarts.Remove(cartitem);
            await _dbcontext.SaveChangesAsync();
            return "Product removed successfully";

        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
}

























