using System;

namespace BusinessService
{    
    public class Service : IService
    {
        ITester _tester;
        public Service(ITester tester)
        {

            _tester = tester;

        }
        public string GetData(int value)
        {
            return _tester.Test(value);            
        }              

    }
}
