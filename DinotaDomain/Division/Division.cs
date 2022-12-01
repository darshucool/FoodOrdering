using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.Division
{
    public class Division : Entity
    {
        
        public int UId { get; set; }

          [Required]
        [Display(Name = "Name")]
        public string Name { set; get; }
        public bool Active { set; get; }


    }
}
