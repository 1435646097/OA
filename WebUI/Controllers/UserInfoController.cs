using BLL;
using DAL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class UserInfoController : Controller
    {
        IUserInfoBLL userInfoBLL = new UserInfoBLL();
        // GET: UserInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUserInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;//当前页码。
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;//每页显示记录数。
            int totalCount;
            IQueryable<UserInfo> list = userInfoBLL.LoadPageEntity<int>(u => u.DelFlag == 0, u => u.ID, pageSize, pageIndex, out totalCount, true);
            return Json(new { rows= list, total= totalCount });
        }
    }
}