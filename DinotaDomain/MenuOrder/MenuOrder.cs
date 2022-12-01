using Dinota.Core.Data;
using Dinota.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuOrder
{
    public class MenuOrder : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public bool? IsVeg { set; get; }
        public bool? IsDiet { set; get; }
        public bool? IsEgg { set; get; }
        public bool? WithVeg { set; get; }
        public decimal Qty { set; get; }
        public int OptionType1 { set; get; }
        public int OptionType2 { set; get; } 
        public int UserId { set; get; }
        public int SLAFLocationUId { get; set; }
        public int Status { set; get; }
        public int MenuItemUId { set; get; }
        public bool Active { set; get; }
        public int? BiteOpt { get; set; }
       
        public bool? IsPlain { get; set; }
        public int? MilkShakeType { get; set; }
        public string Remark { get; set; } 
        
        public string Location { get; set; }
        public string LocationRef { get; set; }
        public string Time { get; set; }  
        
        public DateTime OrderDate { get; set; }
        public virtual MenuItem.MenuItem MenuItem { get; set; }
        public virtual UserBase UserBase { get; set; }

        public virtual MenuOption.MenuOption MenuOption { get; set; }
        public virtual MenuMultiOption.MenuMultiOption MenuMultiOption { get; set; }
    }
}
