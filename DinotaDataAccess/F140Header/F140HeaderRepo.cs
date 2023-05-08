using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.F140Header;

namespace Dinota.DataAccces.F140Header
{
    public class F140HeaderRepo : CrudRepository<Domain.F140Header.F140Header>, IF140HeaderRepo
    {
        public F140HeaderRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
