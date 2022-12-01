using System.Data.Entity.ModelConfiguration;
using Dinota.Domain;

namespace Dinota.DataAccces
{
    public class TxnEntityMap<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : TxnHeaderBase
    {
        public TxnEntityMap()
        {
            HasKey(txn => new { txn.Uid, txn.SiteUid });

         
        }
    }
}
