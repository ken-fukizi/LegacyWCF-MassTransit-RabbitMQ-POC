using BusinessService.ServiceBus;
using BusinessService.ServiceModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BusinessService
{    
    public class Service : IService
    {
        private readonly ITester _tester;
        private readonly ILogger<Service> _logger;
        private readonly ApplicationBus _applicationBus;
        public Service(ITester tester, ApplicationBus applicationBus)
        {

            _tester = tester;
            //_logger = logger;
            _applicationBus = applicationBus;

        }
        public string GetData(int value)
        {
            return _tester.Test(value);            
        }

        public SaveCustomerLeadResponse SaveCustomerLead(SaveCustomerLeadRequest customerLeadRequest)
        {
            //_logger.LogInformation($"Incoming customerLeadRequest {JsonConvert.SerializeObject(customerLeadRequest)} ");
            return _applicationBus.SendSaveCustomerLeadRequest(customerLeadRequest, new System.Threading.CancellationToken()).Result;
        }
    }
}
