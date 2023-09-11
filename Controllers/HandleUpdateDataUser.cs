using LinkShortener.data;
using LinkShortener.model;
using LinkShortener.schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("v1")]
    public class Users : ControllerBase
    {
        [HttpPatch]
        [Route("user/profile/account/update")]
        [Authorize]
        public async Task<IActionResult> Update(
            [FromServices] LocalDb db,
            [FromBody] UserUpdate user,
            [FromQuery] string option
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

            if (filter != null)
            {
                if (
                    option.ToLower() == "name"
                    && newUser.ValidateName(user.Name)
                    && !string.IsNullOrEmpty(user.Name)
                )
                {
                    filter.Name = user.NewName;
                }
                else if (option.ToLower() == "password")
                {
                    filter.Password = newUser.EncryptingPassword(user.NewPassword);
                }
                else
                {
                    if (newUser.VaLidateEmail(user.NewEmail))
                        filter.Email = user.NewEmail;
                    else
                        return BadRequest("invalid email format");
                }
                await db.SaveChangesAsync();

                filter.Cpf = "";
                filter.Password = "";
                return Ok(filter);
            }
            return BadRequest("something wrong");
        }
    }
}
