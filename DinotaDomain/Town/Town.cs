using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.Town
{
    public class Town : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string Name { set; get; }
     
        public int DistrictUId { set; get; }
        public bool Active { set; get; }


    }
}
