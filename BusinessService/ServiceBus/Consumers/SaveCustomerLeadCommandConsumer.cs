using BusinessService.ServiceBus.Commands;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace BusinessService.ServiceBus.Consumers
{
    public class SaveCustomerLeadCommandConsumer : IConsumer<ISaveCustomerLeadCommand>
    {
        private readonly ILogger<SaveCustomerLeadCommandConsumer> _logger;
        public SaveCustomerLeadCommandConsumer(ILogger<SaveCustomerLeadCommandConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ISaveCustomerLeadCommand> context)
        {
            try
            {
                var dateCreated = context.Message.CreatedDate;
                var customerLeadRequest = context.Message.CustomerLeadRequest;

                var saveCustomerLeadRequest = context.Message.SaveCustomerLeadRequest;

                //You can save the data above to a DB through a repository, or Send/Publish etc method to some endpoint or a Bus
                //-- in other words, do whatever you want with the data
                
                await context.Publish(customerLeadRequest);

                //await context.Send<>
                //await context.Publish<>
                //await context.Respond<>
                //await context.Forward<>
                //await context.RetryLater<>
                //await context.NotifyFaulted<>
                //await context.NotifyConsumed<>
                //await context.NotifyCanceled<>
                

                _logger.LogInformation
                    ("SaveCustomerLeadCommandConsumer: Consume: MessageId: {MessageId}, ConversationId: {ConversationId}, CorrelationId: {CorrelationId}, RequestId: {RequestId}, SourceAddress: {SourceAddress}, DestinationAddress: {DestinationAddress}, ResponseAddress: {ResponseAddress}, FaultAddress: {FaultAddress}, ExpirationTime: {ExpirationTime}, Message: {Message}, RetryCount: {RetryCount}, DateCreated: {DateCreated}, CustomerLeadRequest: {CustomerLeadRequest}, SaveCustomerLeadRequest: {SaveCustomerLeadRequest}", context.MessageId, context.ConversationId, context.CorrelationId, context.RequestId, context.SourceAddress, context.DestinationAddress, context.ResponseAddress, context.FaultAddress, context.ExpirationTime, context.Message, context.GetRetryAttempt(), dateCreated, customerLeadRequest, saveCustomerLeadRequest);               
            }
            catch (Exception ex)
            {

                var exc = new ConsumeException<ISaveCustomerLeadCommand>(innerException: ex, message: "SaveCustomerLeadCommandConsumer: Consume: Exception", appServiceIdentifier: "SaveCustomerLeadCommandConsumer", consumeContext: context);

                _logger.LogError(exc, exc.Message);
            }
          
        }
    }
}