using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : BaseController//Controller
    {
        // GET: Home
        IUserInfoBLL UserInfoBLL { get; set; }
        IRoleInfoBLL RoleInfoBLL = new RoleInfoBLL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            IQueryable<UserInfo> list = UserInfoBLL.LoadEntity(u => true);
            ViewData["list"] = list;
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult GetMenu()
        {
            //获取出当前的用户
            UserInfo userInfo = UserInfoBLL.LoadEntity(u => u.ID == UserInfo.ID).FirstOrDefault();
            var roleInfo = userInfo.RoleInfo;
            //获取当前用户的角色的权限
            var userRoleInfo = (from r in roleInfo
                                from a in r.ActionInfo
                                where a.ActionTypeEnum == 1
                                select a).ToList();
            //获取用户权限
            var logUserAction = from r in userInfo.R_UserInfo_ActionInfo
                                select r.ActionInfo;
            var logUserEnum = (from r in logUserAction
                               where r.ActionTypeEnum == 1
                               select r).ToList();
            //将用户角色权限与用户权限两条线连接起来
            logUserEnum.AddRange(userRoleInfo);
            //用户禁止的权限
            var forbidAction = (from a in userInfo.R_UserInfo_ActionInfo
                                where a.IsPass == false
                                select a.ActionInfoID).ToList();
            //过滤掉用户禁止的权限
            var userActions = logUserEnum.Where(a => !forbidAction.Contains(a.ID));
            //去除重
            var lastUserAction = userActions.Distinct(new ActionEqualityComparer());
            //返回json
            var temp = from a in lastUserAction
                       select new { icon = a.MenuIcon, title = a.ActionInfoName, url = a.Url };
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}