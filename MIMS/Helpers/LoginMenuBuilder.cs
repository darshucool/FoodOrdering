using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using MIMS.Controllers;
using SiteMap = MIMS.Security.SiteMap.SiteMap;
using MIMS;

namespace MIMS.Helpers
{
    public class LoginMenuBuilder
    {

        public static void Setup()
        {
            var menuBuilder = new LoginMenuBuilder();

            var controllerCache = new Dictionary<Type, MethodInfo[]>();

            var loginMenu = new SiteMap(controllerCache);
            menuBuilder.LoginMenu(loginMenu);
            loginMenu.Build();
            Configuration.Instance.LoginMenu = loginMenu;
        }

        public void LoginMenu(SiteMap siteMap)
        {
            siteMap.CreateSection(HttpContext.Current.User.Identity.Name)
                .Node<AccountController>("Change Password", "ChangePassword")
                .Node<AccountController>("Log off", "LogOff");
            siteMap.CreateSection<HomeController>("Home", "Index");
            
            
        }
        
    }
}