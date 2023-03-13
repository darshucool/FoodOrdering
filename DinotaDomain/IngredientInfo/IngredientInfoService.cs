using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.IngredientInfo
{
    public class IngredientInfoService : EntityCrudService<IngredientInfo, IIngredientInfoRepo>
    {
        public IngredientInfoService(IIngredientInfoRepo repository)
            : base(repository)
        {
        }

    
    }
}
