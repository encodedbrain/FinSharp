using LinkShortener.data;
using LinkShortener.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserRead : ControllerBase
    {
        [HttpGet]
        [Route("user/profile")]
        [Authorize]
        public Task<ActionResult<dynamic>> Get(
            [FromServices] LocalDb db,
            [FromQuery] string Name,
            string Password
        )
        {
            var newUser = new User();
            var filter = db.Users
                .Where(u => u.Name == Name && u.Password == newUser.EncryptingPassword(Password))
                .FirstOrDefault();

            if (filter != null)
            {
                filter.Cpf = "";
                filter.Password = "";

                return Task.FromResult<ActionResult<dynamic>>(Ok(filter));
            }
            else
            {
                return Task.FromResult<ActionResult<dynamic>>(BadRequest("access denied"));
            }
        }
    }
}
