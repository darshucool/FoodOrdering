
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuMultiOption;
using Dinota.Domain.MenuOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlfasiWeb.Models
{
    public class MenuModel
    {
        public MenuCategory oMenuCategory { get; set; }
        public List<MenuItemModel> MenuItemModel { get; set; }
    }
    public class MenuItemModel {
        public MenuItem MenuItem { get; set; }
    }
    public class MenuClientOrderModel { 
        public MenuItem MenuItem { get; set; }

        public int LocationId { get; set; }
        public List<MenuOption> MenuOptionList { get; set; }
        public List<MenuMultiOption> MenuMultiOptionList { get; set; }
    }
}