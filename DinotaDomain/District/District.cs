using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.District
{
    public class District : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string Name { set; get; }
     
        public int Status { set; get; }
        public string Discode { set; get; }
        public bool Active { set; get; }


    }
}
