using Dinota.DataAccces.Context;

namespace MIMS.Util
{
    public class DomainContextCreater
    {
        private static DomainContext _domainContext;

        private DomainContextCreater()
        {

        }

        public static DomainContext GetDomainContext
        {
            get
            {
                if (_domainContext == null)
                {
                    _domainContext = new DomainContext("Alfasi", "System");
                }

                return _domainContext;
            }
        }
    }
}