using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.BOCTransaction
{
    public class BOCTransactionService : EntityCrudService<BOCTransaction, IBOCTransactionRepo>
    {
        public BOCTransactionService(IBOCTransactionRepo repository)
            : base(repository)
        {
        }

    
    }
}
