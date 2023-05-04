using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.IngredientBOC
{
    public class IngredientBOCService : EntityCrudService<IngredientBOC, IIngredientBOCRepo>
    {
        public IngredientBOCService(IIngredientBOCRepo repository)
            : base(repository)
        {
        }

    
    }
}
