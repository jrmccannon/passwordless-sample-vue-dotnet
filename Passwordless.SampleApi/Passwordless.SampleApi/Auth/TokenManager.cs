using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Passwordless.SampleApi.Models;

namespace Passwordless.SampleApi.Auth;

public interface ITokenManager
{
    string GenerateToken(User userId);
}

public class TokenManager : ITokenManager
{
    private readonly IConfiguration configuration;

    public TokenManager(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Auth:Secret"]));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(tokenHandler.CreateToken(descriptor));
    }
}