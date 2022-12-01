using System;
using MIMS.Models;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;

namespace MIMS.Util
{
    public static class ListModelExtensions
    {
        public static GridSortOptions GetGridSortOptions(this ListModel listModel)
        {
            if (listModel == null)
                throw new ArgumentNullException("listModel");

            var gridSortOptions = new GridSortOptions();

            if (!string.IsNullOrWhiteSpace(listModel.Column))
            {
                gridSortOptions.Column = listModel.Column;
            }

            if (!string.IsNullOrWhiteSpace(listModel.Direction))
            {
                if (listModel.Direction.Equals("Ascending", StringComparison.InvariantCultureIgnoreCase))
                {
                    gridSortOptions.Direction = SortDirection.Ascending;
                }
                else if (listModel.Direction.Equals("Descending", StringComparison.InvariantCultureIgnoreCase))
                {
                    gridSortOptions.Direction = SortDirection.Descending;
                }
                else
                {
                    //if input is not formatted correctly assume Ascending
                    gridSortOptions.Direction = SortDirection.Ascending;
                }
            }

            return gridSortOptions;
        }
    }
}