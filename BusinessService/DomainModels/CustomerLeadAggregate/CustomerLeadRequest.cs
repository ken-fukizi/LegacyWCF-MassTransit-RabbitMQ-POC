using SharedKernel;


namespace BusinessService.DomainModels.CustomerLeadAggregate
{
    public class CustomerLeadRequest : AggregateRoot
    {
        private string cellNumber;
        private bool hasEverBeenCalled;

        public CustomerLeadRequest(string cellNumber, bool hasEverBeenCalled)
        {
            this.cellNumber = cellNumber;
            this.hasEverBeenCalled = hasEverBeenCalled;
        }

        public static class Factory
        {
            public static CustomerLeadRequest Create(string cellNumber, bool hasEverBeenCalled)
            {
                return new CustomerLeadRequest(cellNumber, hasEverBeenCalled);
            }
        }

        public string CellNumber
        {
            get { return cellNumber; } private set { cellNumber = value; }
        }
        public bool HasEverBeenCalled
        {
            get { return hasEverBeenCalled; } private set { hasEverBeenCalled = value; }
        }
    }
}