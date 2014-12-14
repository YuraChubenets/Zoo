using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zoo.WebUI.helpers
{
    public class PageTimeAttribute : ActionFilterAttribute
    {
        Stopwatch watch;

        public PageTimeAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            watch = new Stopwatch();
            watch.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            watch.Stop();
            filterContext.HttpContext.Response.Write(String.Format("Прошло {0} мс ", watch.ElapsedMilliseconds));
            base.OnActionExecuted(filterContext);
        }
    }
}