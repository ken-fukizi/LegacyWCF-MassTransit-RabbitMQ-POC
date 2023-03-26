using BusinessService.DomainModels.CustomerLeadAggregate;
using BusinessService.ServiceBus.Commands;
using BusinessService.ServiceModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessService.ServiceBus
{
    public class ApplicationBus
    {
        private readonly ILogger<ApplicationBus> _logger;
        private readonly IBus _bus;

        public ApplicationBus(IBus bus)
        {
            //_logger = logger;
            _bus = bus;
        }

        public async Task<SaveCustomerLeadResponse> SendSaveCustomerLeadRequest(SaveCustomerLeadRequest SaveCustomerLeadRequest, CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"Starting {nameof(ApplicationBus)}.{nameof(SendSaveCustomerLeadRequest)}");
            var saveCustomerLeadResponse = new SaveCustomerLeadResponse();
            //Get futher information for the lead in a repository by ID, etc and append to the request
            // This is a deliberate example for composing a domain object
            //-- for now I'll hardcode below:
            var cellNumber = "0780557777";
            bool hasEverBeenCalled = false;
            
            var customerLeadRequest = CustomerLeadRequest.Factory.Create(
                    cellNumber: cellNumber,
                    hasEverBeenCalled: hasEverBeenCalled
                );
            try
            {
                await _bus.Publish<ISaveCustomerLeadCommand>
                    (
                        new 
                        { 
                            CommandId = saveCustomerLeadResponse.CommandId, SaveCustomerLeadRequest = SaveCustomerLeadRequest, CustomerLeadRequest =  customerLeadRequest
                        }, cancellationToken
                    );
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Faliure to send {nameof(SaveCustomerLeadRequest)} to {nameof(ISaveCustomerLeadCommand)} . Ref; {string.Join(",", saveCustomerLeadResponse.CommandId)}");
                saveCustomerLeadResponse.AddException(new SharedKernel.Exceptions.DomainException($"Faliure to send {nameof(SaveCustomerLeadRequest)} to {nameof(ISaveCustomerLeadCommand)} . Ref; {string.Join(",", saveCustomerLeadResponse.CommandId)}", ex));
            }
            return saveCustomerLeadResponse;
        }
    }
}