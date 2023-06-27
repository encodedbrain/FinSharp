using FinSharp.data;
using FinSharp.model;
using Microsoft.AspNetCore.Mvc;

namespace FinSharp.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ValidationUser : ControllerBase
    {
        [HttpPost]
        [Route("user/profile/validations/data")]
        public IActionResult ValidationData(
            [FromServices] LocalDb Db,
            [FromBody]
            CPF data
        )
        {
            UserData NewUserData = new UserData();

            var Email = NewUserData.VaLidateEmail(data.Email);
            var Password = NewUserData.ValidatePassword(data.Password);
            var Phone = NewUserData.ValidatePhone(data.Phone);
            var Cpf = NewUserData.ValidateCpf(data.Cpf);

            try
            {
                if (Cpf != true)
                {
                    return BadRequest();
                }

                return Ok(true);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("user/profile/validations/profile")]
        public IActionResult ValidationProfile(
            [FromServices] LocalDb Db,
            [FromBody] Profile profile
        )
        {
            UserProfile NewUserProfile = new UserProfile();

            NewUserProfile.ValidateNickname(profile.Nickname);
            NewUserProfile.ValidateAge(profile.Age);
            NewUserProfile.ValidatePhoto(profile.Photo);

            return Ok();
        }
    }
}
