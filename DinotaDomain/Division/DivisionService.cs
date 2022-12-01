using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.Division
{
    public class DivisionService : EntityCrudService<Division, IDivisionRepo>
    {
        public DivisionService(IDivisionRepo repository)
            : base(repository)
        {
        }

    
    }
}
