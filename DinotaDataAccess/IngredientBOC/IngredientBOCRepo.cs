using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.IngredientBOC;

namespace Dinota.DataAccces.IngredientBOC
{
    public class IngredientBOCRepo : CrudRepository<Domain.IngredientBOC.IngredientBOC>, IIngredientBOCRepo
    {
        public IngredientBOCRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
