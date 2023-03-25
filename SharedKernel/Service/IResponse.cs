

using SharedKernel.Exceptions;

namespace SharedKernel.Service
{
    public interface IResponse
    {
        void AddException(DomainException ex);
    }
}
