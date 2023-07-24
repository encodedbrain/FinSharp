using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FinSharp.data;
using FinSharp.model;
using FinSharp.schemas;
using FinSharp.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FinSharp.Controllers;

[ApiController]
[Route("v1")]
public class Login : ControllerBase
{
    [HttpPost]
    [Route("login/account")]
    [AllowAnonymous]
    public Task<ActionResult<dynamic>> UserLogin(
        [FromServices] LocalDb db,
        [FromBody] LoginSchema user)
    {
        var u = new User();
        var filter = db.Users.Where(
            x => user.Password != null &&
                 x.Name == user.Name &&
                 x.Password == u.EncryptingPassword(user.Password)).FirstOrDefault();


        if (filter == null)
        {
            return Task.FromResult<ActionResult<dynamic>>(NotFound("access denied"));
        }
        else
        {
            var generateToken = TokenServices.GenerateToken(filter);

            filter.Password = "";
            filter.Cpf = "";

            return Task.FromResult<ActionResult<dynamic>>(new
            {
                filter,
                token = generateToken
            });
        }
    }

    [HttpPost]
    [Route("login/account/v2")]
    [AllowAnonymous]
    public Task<ActionResult<dynamic>> LoginWithEmail([FromServices] LocalDb db, [FromBody] LoginWithEmailSchema user)
    {
        var u = new User();
        var filter = db.Users.Where(
            x => user.Password != null &&
                 x.Email == user.Email &&
                 x.Password == u.EncryptingPassword(user.Password)).FirstOrDefault();


        if (filter == null)
        {
            return Task.FromResult<ActionResult<dynamic>>(NotFound("access denied"));
        }
        else
        {
            var generateToken = TokenServices.GenerateToken(filter);

            filter.Password = "";
            filter.Cpf = "";

            return Task.FromResult<ActionResult<dynamic>>(new
            {
                filter,
                token = generateToken
            });
        }
    }

    [HttpPost]
    [Route("login/auth")]
    [AllowAnonymous]
    public bool DecodeToken([FromHeader] string? token)
    {
        var builder = WebApplication.CreateBuilder();
        var mySecret = builder.Configuration.GetConnectionString("Secret");
        if (mySecret != null)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false, // Because there is no expiration in the generated token
                    ValidateAudience = false, // Because there is no audiance in the generated token
                    ValidateIssuer = false, // Because there is no issuer in the generated token
                    ValidIssuer = "Sample",
                    ValidAudience = "Sample",
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
        }

        return true;
    }
}