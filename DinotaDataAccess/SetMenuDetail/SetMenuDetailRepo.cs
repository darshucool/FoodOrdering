using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.SetMenuDetail;

namespace Dinota.DataAccces.SetMenuDetail
{
    public class SetMenuDetailRepo : CrudRepository<Domain.SetMenuDetail.SetMenuDetail>, ISetMenuDetailRepo
    {
        public SetMenuDetailRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
