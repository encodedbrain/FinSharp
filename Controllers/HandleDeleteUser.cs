using LinkShortener.data;
using LinkShortener.model;
using LinkShortener.schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserDelete : ControllerBase
    {
        [HttpDelete]
        [Route("user/account/delete")]
        [Authorize]
        public async Task<IActionResult> Delete(
            [FromServices] LocalDb db,
            [FromBody] LoginSchema user
        )
        {
            var newUser = new User();

            var filter = db.Users
                .Where(
                    u =>
                        u.Name == user.Name
                        && u.Password == newUser.EncryptingPassword(user.Password)
                )
                .FirstOrDefault();

            if (filter == null)
                return BadRequest("access denied");

            var removeUser = db.Users.Remove(filter);
            await db.SaveChangesAsync();

            filter.Cpf = "";
            filter.Password = "";

            return Ok(filter);
        }
    }
}
