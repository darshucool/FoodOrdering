using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.IngredientInfo;

namespace Dinota.DataAccces.IngredientInfo
{
    public class IngredientInfoRepo : CrudRepository<Domain.IngredientInfo.IngredientInfo>, IIngredientInfoRepo
    {
        public IngredientInfoRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
