using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using MIMS.Controllers;
using SiteMap = MIMS.Security.SiteMap.SiteMap;

namespace MIMS.Helpers
{
    public class MenuBuilder
    {
        public static void Setup()
        {
            var menuBuilder = new MenuBuilder();

            var controllerCache = new Dictionary<Type, MethodInfo[]>();

            var topMenu = new SiteMap(controllerCache);
            menuBuilder.TopMenu(topMenu);
            topMenu.Build();
            Configuration.Instance.TopMenu = topMenu;
        }

        public void TopMenu(SiteMap siteMap)
        {

            siteMap.CreateSection("Settings")
                   .Node<GroupsController>("User Groups", "Index")
                   .Node<UsersController>("Manage Users", "Index");
                    
         
            //       .Node<AccountController>("Yet to be purchased status", "report")
            //       .Node<AccountController>("Procurement completed status", "report")
            //       .Node<AccountController>("Yet to be fabricated status", "report")
            //       .Node<AccountController>("Fabrication completed status", "report")
            //       .Node<AccountController>("Yet to be shipped status", "report")
            //       .Node<AccountController>("Shipping and transport completed status", "report")
            //       .Node<AccountController>("Erection Report", "report")
            //       .Node<AccountController>("Erection completed status", "report")
            //       .Node<AccountController>("Procurement in progress", "report")
            //       .Node<AccountController>("Fabrication in progress", "report")
            //       .Node<AccountController>("Transport and shipping in progress", "report")
            //       .Node<AccountController>("Monthly Production", "report")
            //       .Node<AccountController>("Manufacturing status", "report")
            //       .Node<AccountController>("Comparison manufacturing report", "report");
        }

    }
}