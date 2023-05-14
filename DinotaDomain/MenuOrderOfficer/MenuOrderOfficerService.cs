using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuOrderOfficer
{
    public class MenuOrderOfficerService : EntityCrudService<MenuOrderOfficer, IMenuOrderOfficerRepo>
    {
        public MenuOrderOfficerService(IMenuOrderOfficerRepo repository)
            : base(repository)
        {
        }

    
    }
}
