using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace WebUI.Models
{
    public class MyArributeFilter: HandleErrorAttribute
    {
        public static ConcurrentQueue<Exception> errorQueue = new ConcurrentQueue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception ex = filterContext.Exception;
            errorQueue.Enqueue(ex);
            filterContext.HttpContext.Response.Redirect("/Home/Error");
        }
    }
}