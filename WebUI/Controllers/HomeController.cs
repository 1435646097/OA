using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        IUserInfoBLL userInfoBLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            IQueryable<UserInfo> list = userInfoBLL.LoadEntity(u => true);
            ViewData["list"] = list;
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }
    }
}