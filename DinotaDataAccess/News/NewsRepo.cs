using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.News;

namespace Dinota.DataAccces.News
{
    public class NewsRepo : CrudRepository<Domain.News.News>, INewsRepo
    {
        public NewsRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
