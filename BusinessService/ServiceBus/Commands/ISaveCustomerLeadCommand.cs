﻿using BusinessService.DomainModels.CustomerLeadAggregate;
using BusinessService.ServiceModels;
using System;


namespace BusinessService.ServiceBus.Commands
{
    public interface ISaveCustomerLeadCommand
    {
        Guid CommandId { get; set; }
        DateTime? CreatedDate { get; set; }
        SaveCustomerLeadRequest SaveCustomerLeadRequest { get; set; }
        CustomerLeadRequest CustomerLeadRequest { get; set; }
    }
}
