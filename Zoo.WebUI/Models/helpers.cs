using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  System.Web.Helpers;
using System.Web.Mvc;

namespace Zoo.WebUI.Models
{
    public static class helpers
    {
        public static MvcHtmlString SortDirection(this HtmlHelper helper, ref WebGrid grid, string columName)
        {

            string html = "";
            if (grid.SortColumn == columName && grid.SortDirection == System.Web.Helpers.SortDirection.Ascending)
                html = "<";
            else if (grid.SortColumn == columName && grid.SortDirection == System.Web.Helpers.SortDirection.Descending)
                html = ">";
            else
                html = "";

                return MvcHtmlString.Create(html);

        }
    }
}