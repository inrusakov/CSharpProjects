using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using HSEApiTraining;
using Telegram.Bot;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TelegrammAspMvcDotNetCoreBot.Controllers
{
    [Route("api/message/update")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        private TelegramBotClient botClient;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET api/values
        [HttpGet]
        public Update Get()
        {
            Update update = new Update();
            update.Message = new Message();
            update.Message.Chat = new Chat();
            update.Message.From = new User();
            //update.Message.From.Id = 440239804;
            ///update.Message.Chat.Id = 440239804;
            update.Message.Text = "/start";
            _messageService.Execute(update);
            return update;
        }

        // POST api/values
        [HttpPost]
        public async Task<OkResult> Post([FromBody] Update update)
        {
            try
            {
                if (await _messageService.Execute(update) == 1)
                    return Ok();
            }
            catch (Exception)
            {
            }
            return Ok();
        }
    }
}
