using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HSEApiTraining
{
    public interface IMessageService
    {
        Task<int> Execute(Update update);
    }
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
            => _messageRepository = messageRepository;

        public async Task<int> Execute(Update update)
            => await _messageRepository.Execute(update);

    }
}
