using System.Security.Claims;
using Api.Entities;
using Api.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string CreateToken(AppUser user)
    {

        var tokenKey = configuration["TokenKey"] ?? throw new Exception("TokenKey is null");

        if (tokenKey.Length < 64)
            throw new Exception("TokenKey must be at least 64 characters long");

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHaller = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var token = tokenHaller.CreateToken(tokenDescriptor);

        return tokenHaller.WriteToken(token);
    }
}
