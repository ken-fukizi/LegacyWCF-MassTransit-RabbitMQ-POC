

namespace BusinessService
{
    public interface ITester
    {
        string Test(int testValue);
    }

    public class Tester : ITester
    {
        public string Test(int testValue)
        {
            return string.Format("You entered: {0} , from the Tester class", testValue);
        }
    }
}
