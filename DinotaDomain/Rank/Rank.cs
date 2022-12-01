using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.Rank
{
    public class Rank : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string Name { set; get; }
     
        public int Seniority { set; get; }
        public bool Active { set; get; }


    }
}
