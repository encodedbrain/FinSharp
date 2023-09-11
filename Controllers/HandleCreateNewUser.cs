using LinkShortener.data;
using LinkShortener.model;
using LinkShortener.schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserCreate : ControllerBase
    {
        [HttpPost]
        [Route("user/account/create")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Create(
            [FromServices] LocalDb db,
            [FromBody] UserSchema user
        )
        {
            var enconded = new User().EncryptingPassword(user.Password);

            var methods = new User();

            var isCpf = methods.ValidateCpf(user.Cpf);
            var isEmail = methods.VaLidateEmail(user.Email);
            var isName = methods.ValidateName(user.Name);
            var isAge = methods.ValidateAge(user.Age);

            var cpf = methods.ReturnCpfFormated(user.Cpf);

            var findCpf = db.Users.Where(x => x.Cpf == cpf).FirstOrDefault();

            try
            {
                if (!isCpf || !isEmail || !isName || !isAge || findCpf?.Cpf == cpf)
                {
                    return BadRequest("something wrong");
                }

                var newUser = new User(
                    name: user.Name,
                    age: user.Age,
                    email: user.Email,
                    password: enconded,
                    cpf: cpf
                );
                await db.Users.AddAsync(newUser);
                await db.SaveChangesAsync();

                newUser.Password = "";
                newUser.Cpf = "";

                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
