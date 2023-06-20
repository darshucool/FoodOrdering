using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.StockSheetTransaction;

namespace Dinota.DataAccces.StockSheetTransaction
{
    public class StockSheetTransactionRepo : CrudRepository<Domain.StockSheetTransaction.StockSheetTransaction>, IStockSheetTransactionRepo
    {
        public StockSheetTransactionRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
