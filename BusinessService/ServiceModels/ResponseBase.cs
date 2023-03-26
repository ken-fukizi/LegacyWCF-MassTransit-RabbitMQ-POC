using SharedKernel.Exceptions;
using SharedKernel.Service;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BusinessService.ServiceModels
{
    [DataContract]
    public class ResponseBase : IResponse
    {
        private readonly IList<DomainException> _domainExceptions;
        public ResponseBase()
        {
            _domainExceptions = new List<DomainException>();
        }

        public void AddException(DomainException ex)
        {
            _domainExceptions.Add(ex);
        }

        //[DataMember]
        public IEnumerable<DomainException> Exceptions => _domainExceptions; 
    }
}