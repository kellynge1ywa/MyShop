namespace AuthService;

public interface Ijwt
{
    string GenerateToken(ShopUser shopUser, IEnumerable<string> Roles);

}
