using System;
using System.Web.Mvc;
using AlfasiWeb.Properties;

namespace MIMS.Util
{
    public class ExceptionTranslator
    {
        public void Trnaslate(System.Data.DataException de, ModelStateDictionary modelState)
        {
            Exception rootException = de;
            while (rootException.InnerException != null)
            {
                rootException = rootException.InnerException;
            }

            if (rootException.Message.Contains("Cannot insert duplicate key"))
            {
                if (rootException.Message.Contains("UK_Group_Name"))
                {
                    modelState.AddModelError(string.Empty, Resources.ExceptionDuplicateGroupName);
                    return;
                }

                if (rootException.Message.Contains("UK_User_UserName"))
                {
                    modelState.AddModelError(string.Empty, Resources.ExceptionDuplicateUserName);
                    return;
                }

                if (rootException.Message.Contains("UK_Project_Name"))
                {
                    modelState.AddModelError(string.Empty, Resources.ExceptionDuplicateProjectName);
                    return;
                }
                
                modelState.AddModelError(string.Empty, Resources.ExceptionDuplicateKey);
                return;
            }

            modelState.AddModelError(string.Empty, rootException.Message);
        }

        public void Trnaslate(System.Net.WebException webException, ModelStateDictionary modelState)
        {
            Exception rootException = webException;
            while (rootException.InnerException != null)
            {
                rootException = rootException.InnerException;
            }

            modelState.AddModelError(string.Empty, rootException.Message);
        }
    }
}