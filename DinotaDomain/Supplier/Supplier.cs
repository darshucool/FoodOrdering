﻿using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.Supplier
{
    public class Supplier : Entity
    {
        
        public int UId { get; set; }

          [Required]
        [Display(Name = "Name")]
        
        public string Name { set; get; }
        public string Address { set; get; }
        public string ContactNo { set; get; }
        
     public int LocationUId { get; set; }
        public bool Active { set; get; }
       
    
    }
}
