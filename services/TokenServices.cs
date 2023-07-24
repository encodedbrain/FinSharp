using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinSharp.model;
using Microsoft.IdentityModel.Tokens;

namespace FinSharp.services;

public class TokenServices
{
    public static object GenerateToken(User users)
    {
        var builder = WebApplication.CreateBuilder();

        var hash = builder.Configuration.GetConnectionString("secret");

        if (hash != null)
        {
            var key = Encoding.ASCII.GetBytes(hash);

            if (users.Name != null)
            {
                var config = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, users.Name),
                        new Claim(ClaimTypes.Role, users.Role.ToString() ?? throw new InvalidOperationException())
                    }),

                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials =
                        new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(config);
                var tokenString = tokenHandler.WriteToken(token);


                return new
                {
                    token = tokenString
                };
            }
        }

        return null!;
    }
}