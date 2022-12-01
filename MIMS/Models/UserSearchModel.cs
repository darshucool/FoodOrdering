using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIMS.Models
{
    public class UserSearchModel : ListModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}