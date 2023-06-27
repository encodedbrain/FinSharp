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
        public async Task<IActionResult> ValidationData(
            [FromServices] LocalDb Db,
            [FromBody]
            CPF data
        )
        {
            UserData NewUserData = new UserData();

            var IsCpf = NewUserData.ValidateCpf(data.Cpf);
            var IsEmail = NewUserData.VaLidateEmail(data.Email);
            var IsPassword = NewUserData.ValidatePassword(data.Password);

            var Phone = NewUserData.ValidatePhone(data.Phone);
            var CpfFormated = NewUserData.ReturnCpfFormated(IsCpf, data.Cpf);
            var ReturnEmail = NewUserData.ReturnEmail(IsEmail, data.Email);
            var EncodedPwd = NewUserData.EncryptingPassword(IsPassword, data.Password);

            try
            {
                if (IsCpf != true || IsEmail != true)
                {

                    return BadRequest("to something wrong!");
                }

                var result = new UserData(ReturnEmail, "Password", Phone, CpfFormated);


                await Db.UserDatas.AddAsync(result);

                await Db.SaveChangesAsync();

                return Ok(result);

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
