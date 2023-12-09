using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MainCourseMealHeader
{
    public class MainCourseMealHeaderService : EntityCrudService<MainCourseMealHeader, IMainCourseMealHeaderRepo>
    {
        public MainCourseMealHeaderService(IMainCourseMealHeaderRepo repository)
            : base(repository)
        {
        }

    
    }
}
