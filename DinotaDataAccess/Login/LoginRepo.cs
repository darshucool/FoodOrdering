using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Security.Login;

namespace Dinota.DataAccces.Login
{
    public class LoginRepo : CrudRepository<Security.Login.Login>, ILoginRepo
    {
        public LoginRepo(SecurityContext securityContext)
            : base(securityContext)
        {
        }

    }
}
