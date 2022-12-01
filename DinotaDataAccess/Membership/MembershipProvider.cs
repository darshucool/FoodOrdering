using Dinota.DataAccces.Context;
using Dinota.DataAccces.Tracking;
using Dinota.DataAccces.User;
using Dinota.Domain;
using Dinota.Domain.User;
using Dinota.Security.Membership;
using Dinota.Security.Tracking;

namespace Dinota.DataAccces.Membership
{
    public class MembershipProvider : MembershipProviderBase
    {
        protected override IDomainContext GetDomainContext()
        {
            return new DomainContext("Alfasi", "System");
        }

        protected override UserBaseService GetUserService(IDomainContext domainContext)
        {
            return new UserBaseService(new UserBaseRepo((DomainContext) domainContext));
        }

        protected override TrackingService GetTrackingService(IDomainContext domainContext)
        {
            return new TrackingService(new TrackingRepo((DomainContext) domainContext));
        }
    }
}
