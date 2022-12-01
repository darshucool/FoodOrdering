using System;
using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;

namespace Dinota.Domain
{
    public abstract class TxnHeaderBase : Entity
    {
        public int SiteUid { get; set; }

        public int Uid { get; set; }

        [Required]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int  OutletSiteUid{ get; set; }

        [Required]
        public int OutletUid { get; set; }

        [Required]
        public int CallSiteUid { get; set; }

        [Required]
        public int CallUid { get; set; }

        [Required]
        public int AgentUid { get; set; }

        [Display(Name = "Line Count")]
        public short LineCount { get; set; }
    }
}
