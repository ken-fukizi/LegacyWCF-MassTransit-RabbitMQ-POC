using SharedKernel.Exceptions;
using SharedKernel.Service;
using System.Collections.Generic;

namespace BusinessService.ServiceModels
{
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

        public IEnumerable<DomainException> Exceptions => _domainExceptions; 
    }
}