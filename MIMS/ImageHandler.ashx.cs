using System;
using System.Web;
using Dinota.Core.Specification;
using Dinota.DataAccces.Context;


namespace AlfasiWeb
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        /// <summary>
        /// Making of reponse of each project-image
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            var strUid = context.Request.QueryString.Get("Uid");
            var strTablename = context.Request.QueryString.Get("TableName");

            if (string.IsNullOrEmpty(strUid)) return;

            var uid = Int32.Parse(strUid);

            var domainContext = new DomainContext("Alfasi", "System");

            using (domainContext)
            {
                if (strTablename == "ProjectHirachy")
                {
                    
                }

                else
                {
                   
                }
               
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}