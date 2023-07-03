using FinSharp.data;
using FinSharp.model;
using FinSharp.schemas;
using FinSharp.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSharp.Controllers;

[ApiController]
[Route("v1")]
public class Login : ControllerBase
{
    [HttpPost]
    [Route("loggin/account")]
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
}