
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;


namespace AuthService;

public class JwtService : Ijwt
{
    private readonly JwtOptions _JwtOptions;
    public JwtService(IOptions<JwtOptions> options)
    {
        _JwtOptions=options.Value;
    }
    public string GenerateToken(ShopUser shopUser, IEnumerable<string> Roles)
    {
        var mykey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.SecretKey));

        var cred= new SigningCredentials(mykey, SecurityAlgorithms.HmacSha256);

        //payload
        List<Claim> claims=new List<Claim>();
        claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,shopUser.Id.ToString()));
        //Adding a list of roles
        claims.AddRange(Roles.Select(k=>new Claim(ClaimTypes.Role, k)));
        
         //token
        var tokendescriptor=new SecurityTokenDescriptor(){
            Issuer=_JwtOptions.Issuer,
            Audience=_JwtOptions.Audience,
            Expires= DateTime.UtcNow.AddHours(4),
            Subject= new ClaimsIdentity(claims),
            SigningCredentials=cred
        };
        var token= new JwtSecurityTokenHandler().CreateToken(tokendescriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
