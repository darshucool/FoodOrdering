using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.Core.Data;

namespace Dinota.Security.Login
{
    public class LoginService : EntityCrudService<Login, ILoginRepo>
    {
        public LoginService(ILoginRepo repository): base(repository)
        {
        }
    }
}
