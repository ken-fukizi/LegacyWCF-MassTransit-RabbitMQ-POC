using MassTransit;
using MassTransit.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessService.ServiceBus
{
    public class ApplicationBus
    {
        private readonly ILogger<ApplicationBus> _logger;
        private readonly IBus _bus;

        public ApplicationBus(ILogger<ApplicationBus> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }
    }
}