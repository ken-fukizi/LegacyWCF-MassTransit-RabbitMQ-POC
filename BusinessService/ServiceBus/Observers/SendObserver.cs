using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;

namespace BusinessService.ServiceBus.Observers
{
    public class SendObserver : ISendObserver
    {
        private readonly ILogger<SendObserver> _logger;
        public SendObserver(ILogger<SendObserver> logger)
        {
            _logger = logger;
        }
        public Task PostSend<T>(SendContext<T> context) where T : class
        {
            _logger.LogTrace($"POST SEND: {context.ConversationId} /// {context.MessageId} ");
            return Task.CompletedTask;
        }

        public Task PreSend<T>(SendContext<T> context) where T : class
        {
            _logger.LogTrace($"PRE SEND: {context.ConversationId} /// {context.MessageId} ");
            return Task.CompletedTask;
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
        {
            _logger.LogTrace($"SEND FAULT: {context.ConversationId} /// {context.MessageId} /// {exception.Message}");
            return Task.CompletedTask;
        }
    }

}