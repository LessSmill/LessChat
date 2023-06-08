using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ServerAPI.Controllers;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly NewChatSedContext _context;

        public AuthController(NewChatSedContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login([FromForm] string Login, [FromForm] string Password)
        {
            var authorizedUser = _context.UserData.FirstOrDefault(u => u.Login == Login && u.Password == Password);

            if (authorizedUser == null)
            {
                return NotFound("Неверное имя пользователя или пароль");
            }

            return Ok(authorizedUser);
        }
    }
}