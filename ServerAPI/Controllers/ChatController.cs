using Microsoft.AspNetCore.Mvc;


namespace ServerAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        NewChatSedContext _newChat;
        public ChatController(NewChatSedContext newChat)
        {
            _newChat = newChat;
        }

        [HttpGet ("Login")]
        public void Get(int uuser)
        {
           var autoriz = _newChat.UserData.Where(p => p.Id == uuser).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            // Обработка POST-запроса
            // Принять сообщение
            return Ok("Сообщение получено");
        }
    }
}