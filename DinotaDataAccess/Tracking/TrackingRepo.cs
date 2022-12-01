using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Security.Tracking;

namespace Dinota.DataAccces.Tracking
{
    public class TrackingRepo : CrudRepository<Security.Tracking.Tracking>, ITrackingRepo
    {
        public TrackingRepo(DomainContext securityContext)
            : base(securityContext)
        {
        }
    }
}
