using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.News
{
    public class NewsService : EntityCrudService<News, INewsRepo>
    {
        public NewsService(INewsRepo repository)
            : base(repository)
        {
        }

    
    }
}
