using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;
using Dinota.Core.Extensions;

namespace Dinota.Domain
{
    public abstract class SyncEntity : Entity
    {
        public int Uid { get; set; }

        private string _id;

        [StringLength(32)]
        public string ID
        {
            get
            {
                return _id;
            }

            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    _id = value.Trim();
                }
            }
        }

        /// <summary>
        /// Returns whether the entity data is pushed to EMC
        /// </summary>
        public bool IsSynced { get; set; }
    }
}
