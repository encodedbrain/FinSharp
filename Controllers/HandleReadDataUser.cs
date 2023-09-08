using LinkShortener.data;
using LinkShortener.model;
using LinkShortener.schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public Task<ActionResult<dynamic>> Get([FromServices] LocalDb db, [FromBody] LoginSchema user)
        {
            var newUser = new User();
            var filter = db.Users.Where(u => u.Name == user.Name && u.Password == newUser.EncryptingPassword(user.Password)).FirstOrDefault();


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