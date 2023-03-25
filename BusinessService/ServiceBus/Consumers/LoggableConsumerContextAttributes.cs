using MassTransit;

namespace BusinessService.ServiceBus.Consumers
{
    public class LoggableConsumerContextAttributes<T> where T : class
    {
        public LoggableConsumerContextAttributes(ConsumeContext<T> consumeContext)
        {
            this.ConsumeContext = consumeContext;
            //RetryCount = consumeContext.GetRetryAttempt();
        }
        public ConsumeContext<T> ConsumeContext { get; private set; }
        public string MessageId { get { return this.ConsumeContext.MessageId.ToString(); } }
        public string ConversationId { get { return this.ConsumeContext.ConversationId.ToString(); } }
        public string CorrelationId { get { return this.ConsumeContext.CorrelationId.ToString(); } }
        public string RequestId { get { return this.ConsumeContext.RequestId.ToString(); } }
        public string SourceAddress { get { return this.ConsumeContext.SourceAddress.ToString(); } }
        public string DestinationAddress { get { return this.ConsumeContext.DestinationAddress.ToString(); } }
        public string ResponseAddress { get { return this.ConsumeContext.ResponseAddress.ToString(); } }
        public string FaultAddress { get { return this.ConsumeContext.FaultAddress.ToString(); } }
        
        public string ExpirationTime { get { return this.ConsumeContext.ExpirationTime.ToString(); } }
        public string Message { get { return this.ConsumeContext.Message.ToString(); } }  
        //public int RetryCount { get; private set; }
        public int RetryCount { get { return this.ConsumeContext.GetRetryAttempt(); } }
    }
}