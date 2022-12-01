using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.Core.Data;
using Dinota.Domain.Filter;
using Dinota.Core.Specification;

namespace Dinota.Domain.PageObject
{
    public class PageObjectService : EntityCrudService<PageObject, IPageObjectRepo>
    {
        public PageObjectService(IPageObjectRepo repository)
            : base(repository)
        {
        }

    
    }
}
