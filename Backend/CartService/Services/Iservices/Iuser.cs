namespace CartService;

public interface Iuser
{
     Task<ShopUserDto> GetUserById(Guid Id);
}
