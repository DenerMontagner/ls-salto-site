using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LSalto.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LSalto.API.Services;

public class JwtTokenService(IConfiguration config) : IJwtTokenService
{
    public string GenerateToken(int id, string nome, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("sub", id.ToString()),
            new Claim("name", nome),
            new Claim("role", role)
        };

        var expiresInHours = double.Parse(config["Jwt:ExpiresInHours"] ?? "12");

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expiresInHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
