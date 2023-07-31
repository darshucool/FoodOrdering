using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.AlertNotify
{
    public class AlertNotify : Entity
    {
        
        public int UId { get; set; }
        public string AlertText { get; set; }
        public int Status { get; set; }
        public int AlertType { get; set; }
        public int SLAFLocationUId { get; set; } 
        public int UserId { get; set; }
        public bool Active { set; get; }


    }
}
