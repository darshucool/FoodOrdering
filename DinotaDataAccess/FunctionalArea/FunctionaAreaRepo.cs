using Dinota.DataAccces.Context;
using Dinota.DataAccces.Context;
using Dinota.Security.FunctionalArea;

namespace Dinota.DataAccces.FunctionalArea
{
    public class FunctionaAreaRepo : Repository.Repository<Security.FunctionalArea.FunctionalArea>, IFunctionalAreaRepo
    {
        public FunctionaAreaRepo(SecurityContext securityContext)
            : base(securityContext)
        {
        }
    }
}
