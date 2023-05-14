using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuOrderOfficer;

namespace Dinota.DataAccces.MenuOrderOfficer
{
    public class MenuOrderOfficerRepo : CrudRepository<Domain.MenuOrderOfficer.MenuOrderOfficer>, IMenuOrderOfficerRepo
    {
        public MenuOrderOfficerRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
