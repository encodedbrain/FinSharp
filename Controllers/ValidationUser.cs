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
            [FromBody] string password,
            string email,
            string phone,
            string cpf
        )
        {
            UserData NewUserData = new UserData();

            var Email = NewUserData.VaLidateEmail(email);
            var Password = NewUserData.ValidatePassword(password);
            var Phone = NewUserData.ValidatePhone(phone);
            var Cpf = NewUserData.ValidateCpf(cpf);
            if (Cpf != true)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                return Ok("data validated successfully");
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
            [FromBody] string nickname,
            string age
        )
        {
            UserProfile NewUserProfile = new UserProfile();

            NewUserProfile.ValidateNickname(nickname);
            NewUserProfile.ValidateAge(age);


            
            return Ok();
        }
    }
}
