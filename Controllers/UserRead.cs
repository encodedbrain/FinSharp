using FinSharp.data;
using FinSharp.model;
using FinSharp.schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSharp.Controllers
{
    [ApiController]
    [Route("v1")]

    public class UserRead : ControllerBase
    {
        [HttpGet]
        [Route("user/profile")]
        [Authorize]
        public async Task<IActionResult> Get([FromServices] LocalDb db, [FromBody] LoginSchema user)
        {
            var newUser = new User();
            var filter = db.Users.Where(u => u.Name == user.Name && u.Password == newUser.EncryptingPassword(user.Password)).FirstOrDefault();


            if (filter != null)
            {
                filter.Cpf = "";
                filter.Password = "";

                return Ok(filter);

            }
            else
            {
                return BadRequest("access denied");
            }
        }
    }
}