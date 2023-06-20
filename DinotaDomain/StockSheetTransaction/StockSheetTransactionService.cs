using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.StockSheetTransaction
{
    public class StockSheetTransactionService : EntityCrudService<StockSheetTransaction, IStockSheetTransactionRepo>
    {
        public StockSheetTransactionService(IStockSheetTransactionRepo repository)
            : base(repository)
        {
        }

    
    }
}
