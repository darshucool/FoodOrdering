using System.Web.Mvc;

namespace MIMS.Models
{
    [Bind(Exclude = "SortPrefix, PageSize")]
    [System.Diagnostics.DebuggerDisplay("Column = {Column} Page = {Page}")]
    public class ListModel
    {
        public ListModel()
        {
            SortPrefix = string.Empty;
            //todo: populate page size at controller
            PageSize = 10;
        }

        /// <summary>
        /// List sort property name
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// List sort direction
        /// </summary>
        public string Direction { get; set; }

        public int? Page { get; set; }

        public string SortPrefix { get; set; }

        public int PageSize { get; set; }
    }
}