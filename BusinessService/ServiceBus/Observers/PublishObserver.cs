using MassTransit;
using MassTransit.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BusinessService.ServiceBus.Observers
{
    public class PublishObserver : IPublishObserver
    {
        private readonly ILogger<PublishObserver> _logger;
        public PublishObserver(ILogger<PublishObserver> logger)
        {
            _logger = logger;
        }
        public Task PostPublish<T>(PublishContext<T> context) where T : class
        {
            _logger.LogTrace($"POST PUBLISH: {context.ConversationId} /// {context.MessageId} ");
            return Task.CompletedTask;
        }

        public Task PrePublish<T>(PublishContext<T> context) where T : class
        {
            _logger.LogTrace($"PRE PUBLISH: {context.ConversationId} /// {context.MessageId} ");
            return Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
        {
            _logger.LogTrace($"PUBLISH FAULT: {context.ConversationId} /// {context.MessageId} /// {exception.Message}");
            return Task.CompletedTask;
        }
    }
}