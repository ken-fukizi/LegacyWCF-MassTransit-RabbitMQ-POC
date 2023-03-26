using SharedKernel.Exceptions;
using SharedKernel.Service;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BusinessService.ServiceModels
{
    [DataContract]
    public class SaveCustomerLeadResponse : IResponse
    {
        private readonly IList<DomainException> _domainExceptions;
        public SaveCustomerLeadResponse(Guid commandId = default)
        {
            if (commandId == default)
                CommandId = Guid.NewGuid();
            else CommandId = commandId;
        }
        [DataMember]
        public Guid CommandId { get; set; }

        public void AddException(DomainException ex)
        {
            _domainExceptions.Add(ex);
        }
    }
}