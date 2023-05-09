using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.BOCTransaction;

namespace Dinota.DataAccces.BOCTransaction
{
    public class BOCTransactionRepo : CrudRepository<Domain.BOCTransaction.BOCTransaction>, IBOCTransactionRepo
    {
        public BOCTransactionRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
