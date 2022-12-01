using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.News
{
    public class News : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Heading")]
        public string NewsHeader { set; get; }
     public int SLAFLocationUId { set; get; }
        public string Description { set; get; }
        public bool Active { set; get; }


    }
}
