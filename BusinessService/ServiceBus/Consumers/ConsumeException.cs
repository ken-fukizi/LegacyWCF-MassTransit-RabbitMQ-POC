using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessService.ServiceBus.Consumers
{
    public class ConsumeException<T> : Exception where T : class
    {
        public ConsumeException(Exception innerException, string message, string appServiceIdentifier, ConsumeContext<T> consumeContext) 
            : base (message: message, innerException: innerException) 
        { 
        }

        public string AppServiceIdentifier { get; private set; }
        public LoggableConsumerContextAttributes<T> ConsumerContextAttributes { get; private set; }
    }
}