using Dinota.Core.Data;

namespace Dinota.Security.FunctionalArea
{
    public class FunctionalAreaService : EntityService<FunctionalArea, IFunctionalAreaRepo>
    {
        public FunctionalAreaService(IFunctionalAreaRepo repository)
            : base(repository)
        {
        }

    }
}
