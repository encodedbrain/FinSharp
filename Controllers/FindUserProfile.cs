using FinSharp.data;
using FinSharp.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinSharp.Controllers
{
    [ApiController]
    [Route("v1")]
    public class FindUserProfile : ControllerBase
    {
        [HttpGet]
        [Route("user/profile/")]
        public async Task<IActionResult> UserProfile(
            [FromServices] LocalDb Db,
            [FromQuery] string PrimaryKey
        )
        {
            if (string.IsNullOrEmpty(PrimaryKey))
            {
                return BadRequest("sorry , something missing");
            }
            else
            {
                try
                {
                    var ForLowerCase = PrimaryKey.ToLower();
                    var FormatForGuid = Guid.Parse(ForLowerCase);

                    using (var database = new LocalDb())
                    {
                        var users = await database.Profiles
                            .AsNoTracking()
                            .Where(user => user.Id == FormatForGuid)
                            .Select(user => new { user.Nickname, user.Photo })
                            .ToListAsync();
                        return Ok(users);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
