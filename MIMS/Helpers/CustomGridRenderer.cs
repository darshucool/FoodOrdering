using MvcContrib.UI.Grid;

namespace MIMS.Helpers
{
    public class CustomGridRenderer<T> : HtmlTableGridRenderer<T> where T : class
    {
        //id of the rendering table
        private readonly string _id;
        //to set the name of the seach-column-text field
        private int _count;
        //enabling column wise search
        private readonly bool _enableSearch;

        public CustomGridRenderer()
        {
        }


        public CustomGridRenderer(string id, bool enableSearch)
        {
            _id = id;
            _enableSearch = enableSearch;
        }

        protected override void RenderGridStart()
        {
            if (IsDataSourceEmpty()) return;

            if (!GridModel.Attributes.ContainsKey("class"))
            {
                GridModel.Attributes["class"] = "table table-hover tablesorter";

                if (!string.IsNullOrEmpty(_id))
                {
                    GridModel.Attributes["id"] = _id;
                }
            }

            var attrs = BuildHtmlAttributes(GridModel.Attributes);

            if (attrs.Length > 0)
                attrs = " " + attrs;

            RenderText(string.Format("<table {0} >", attrs));
        }

        protected override void RenderEmpty()
        {
            RenderText("There is no data available");
        }

        protected override void RenderHeadEnd()
        {
            if (!_enableSearch) return;
            RenderText("<tr>");
            var id = _id;

            foreach (var col in GridModel.Columns)
            {
                if (col.IsShowFilter)
                {
                    RenderText(
                        string.Format("<th>" +
                                      "<input style = 'width:100%;' type='text' placeholder=" + col.DisplayName +
                                      " name = '" + _count +
                                      "' onkeyup=\"CustomSearch(this, '" + id +
                                      "');\" onpropertychange=\"CustomSearch(this, '" + id + "');\"/></th>"));
                }
                else
                {
                    RenderText(
                        string.Format("<th></th>"));
                }

                _count++;
            }
            RenderText("</tr>");
        }

        protected override void RenderRowStart(GridRowViewData<T> rowData)
        {
            var attributes = GridModel.Sections.Row.Attributes(rowData);

            var attributeString = BuildHtmlAttributes(attributes);

            if (attributeString.Length > 0)
            {
                attributeString = " " + attributeString;
            }


            RenderText(string.Format("<tr{0}>", attributeString));
        }

        protected override void RenderHeaderText(GridColumn<T> column)
        {
            if (IsSortingEnabled && column.Sortable)
            {
                var sortColumnName = GenerateSortColumnName(column);

                var isSortedByThisColumn = GridModel.SortOptions.Column == sortColumnName;

                //if (isSortedByThisColumn)
                //{
                //    string sortClass = GridModel.SortOptions.Direction == SortDirection.Ascending ? "sort_asc" : "sort_desc";

                //    //bug fix: IE does not show sorted column indicator correctly
                //    RenderText(string.Format("<div class=\"{0}\">", sortClass));
                //    base.RenderHeaderText(column);
                //    RenderText("</div>");
                //}
                //else
                //{
                base.RenderHeaderText(column);
                //}
            }
            else
            {
                base.RenderHeaderText(column);
            }
        }
    }
}