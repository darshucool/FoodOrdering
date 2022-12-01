using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Routing;
using MIMS.Models;
using MIMS.Security;
using MIMS.Util;
using Dinota.Core.Data;
using Dinota.Core.Specification;
using Dinota.Domain.Filter;
using Dinota.Security.Membership;

namespace MIMS.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        protected const int DefaultPageSize = 20;

        protected const int DialogPageSize = 5;
        protected ExceptionTranslator ExceptionTranslator = new ExceptionTranslator();

        public IMembershipService MembershipService { get; set; }

        protected BaseController()
        {

        }

        protected BaseController(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        protected IDataContext DataContext { get; set; }

        protected Cache Cache { get { return ControllerContext.HttpContext.Cache; } }

        protected override void Initialize(RequestContext requestContext)
        {
            if (MembershipService == null)
            {
                MembershipService = new AccountMembershipService();
            }
            base.Initialize(requestContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is HttpRequestValidationException)
            {
                filterContext.Result = View("InvalidInput");
                filterContext.ExceptionHandled = true;
            }

            base.OnException(filterContext);
        }

        protected User MembershipUser
        {
            get { return MembershipService.GetUser(true); }
        }

        protected void ClearCacheEntry(string cacheKey)
        {
            Cache.Remove(cacheKey);
        }

        protected ISpecification<TEntity> AddUserFilter<TEntity>(IFilterable<TEntity> filterable, ISpecification<TEntity> specification)
            where TEntity : Entity
        {
            var currentUser = MembershipUser;

            return filterable.GetFilterBuilder().BuildUserFilter(specification, currentUser.UserAccount);
        }

        /// <summary>
        /// return an byte array of a given file
        /// </summary>
        /// <param name="fileDocument">posted file</param>
        /// <returns></returns>
        private byte[] GetFileStream(HttpPostedFileBase fileDocument)
        {
            var buffer = new byte[fileDocument.ContentLength];

            fileDocument.InputStream.Read(buffer, 0, buffer.Length);

            return buffer;

        }


        /// <summary>
        /// return Base64String of a given file
        /// </summary>
        /// <param name="imageFile">posted file</param>
        /// <returns></returns>
        private string GetBaseImage(HttpPostedFileBase imageFile)
        {
            var buffer = new byte[imageFile.ContentLength];

            imageFile.InputStream.Read(buffer, 0, buffer.Length);

            return Convert.ToBase64String(buffer);

        }

        

        public JsonResult UploadNonMsieFiles()
        {
            try
            {
                var fileName = Request.Headers["X-File-Name"];

                var directoryPath = Server.MapPath("~/Upload/");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var uploadedFilePath = string.Format("{0}{1}", directoryPath, fileName);

                Request.SaveAs(uploadedFilePath, false);

                return Json(fileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusDescription = e.Message;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public static byte[] StreamToByteArray(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
