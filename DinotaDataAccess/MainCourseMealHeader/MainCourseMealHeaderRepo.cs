using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MainCourseMealHeader;

namespace Dinota.DataAccces.MainCourseMealHeader
{
    public class MainCourseMealHeaderRepo : CrudRepository<Domain.MainCourseMealHeader.MainCourseMealHeader>, IMainCourseMealHeaderRepo
    {
        public MainCourseMealHeaderRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
