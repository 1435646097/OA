using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : BaseController//Controller
    {
        public ActionResult Index()
        {
            ViewBag.Userinfo = UserInfo.UName;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        IUserInfoBLL userInfoBLL { get; set; }
        public ActionResult GetList()
        {
            IQueryable<UserInfo> list = userInfoBLL.LoadEntity(u => true);
            ViewData["list"] = list;
            return View();
        }
        public ActionResult Test()
        {
            int a = 10;
            int b = 0;
            int c = a / b;
            return View();
        }
    }
}