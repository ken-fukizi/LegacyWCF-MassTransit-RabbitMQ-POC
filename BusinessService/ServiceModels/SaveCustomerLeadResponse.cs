using SharedKernel.Service;
using System;

namespace BusinessService.ServiceModels
{
    public class SaveCustomerLeadResponse : ResponseBase, IResponse
    {
        public SaveCustomerLeadResponse(Guid commandId = default)
        {
            if (commandId == default)
                CommandId = Guid.NewGuid();
            else CommandId = commandId;
        }
        public Guid CommandId { get; set; }
    }
}