using facturacionBack.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private List<UserDto> _users = new List<UserDto>()
        {
            new UserDto
            {
                Username = "camila",
                Password = "camila123.",
                Fullname = "camila contreras"
            },
              new UserDto
            {
                Username = "andres",
                Password = "andres123.",
                Fullname = "andres zapata"

            },
             new UserDto
            {
                Username = "carlos",
                Password = "carlos123.",
                Fullname = "carlos pardo"

            },
        };

        [HttpPost]
        public  ActionResult<UserDto> PostLogin(LoginRequestDto item)
        {
            var user = _users.Where(x => x.Username.ToUpper() == item.Username.ToUpper() && x.Password == item.Password).FirstOrDefault();
            if (user == null) return BadRequest();
            return user;
        }
    }
}
