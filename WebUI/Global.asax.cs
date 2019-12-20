using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebUI.Models;

namespace WebUI
{
    public class MvcApplication : SpringMvcApplication/*System.Web.HttpApplication*/
    {
        protected void Application_Start()
        {
            string logPath = Server.MapPath("/log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyArributeFilter.errorQueue.Count>0)
                    {
                        Exception ex = null;
                        bool result = MyArributeFilter.errorQueue.TryDequeue(out ex);
                        if (ex!=null&&result)
                        {
                            string fullPath = logPath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                            File.AppendAllText(fullPath, ex.ToString());
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            },logPath);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
