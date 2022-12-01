using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.UserType
{
    public class UserType : Entity
    {
        
        public int UId { get; set; }
      
        [Required]
        [Display(Name = "Name")]
        public string Name { set; get; }
        public int DivisionId { set; get; }
        public bool Active { set; get; }
        public virtual Division.Division Division { get; set; }

    }
}
