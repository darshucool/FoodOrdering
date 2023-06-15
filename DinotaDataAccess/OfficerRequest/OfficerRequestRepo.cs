using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.OfficerRequest;

namespace Dinota.DataAccces.OfficerRequest
{
    public class OfficerRequestRepo : CrudRepository<Domain.OfficerRequest.OfficerRequest>, IOfficerRequestRepo
    {
        public OfficerRequestRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
