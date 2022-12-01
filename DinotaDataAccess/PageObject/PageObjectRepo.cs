using System.Collections.Generic;
using System.Linq;
using Dinota.Core.Specification;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.PageObject;

namespace Dinota.DataAccces.PageObject
{
    public class PageObjectRepo : CrudRepository<Domain.PageObject.PageObject>, IPageObjectRepo
    {
        public PageObjectRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }

       
    }
}
