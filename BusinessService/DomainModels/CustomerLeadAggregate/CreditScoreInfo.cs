

namespace BusinessService.DomainModels.CustomerLeadAggregate
{
    public class CreditScoreInfo //: ValueObject -- just as an example ... not implemented
    {
        public static class Factory
        {
            public static CreditScoreInfo Create()
            {
                return new CreditScoreInfo();
            }
        }
    }
}