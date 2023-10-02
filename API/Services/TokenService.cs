using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;


namespace API.Services;
public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
    }

    // CREATE JWT TOKEN https://jwt.io/
    public string CreateToken(AppUser user)
    {
        // 1. Creating List of Claims --> Payload of JWT
        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Username)
        };
        // 2. SigningCredentials
        SigningCredentials creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        // 3. Describe token (Claims/Expirydate/SigningCredentials)
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };
        // 4. Tokenhandler
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        // 5. Create token with handler
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        // 6. return token
        return tokenHandler.WriteToken(token);
    }
}
