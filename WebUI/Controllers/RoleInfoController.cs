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
    public class RoleInfoController : Controller
    {
        IRoleInfoBLL RoleInfoBLL = new RoleInfoBLL();
        IActionInfoBLL ActionInfoBLL = new ActionInfoBLL();
        // GET: RoleInfo
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 角色界面的查询
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : Convert.ToInt32(Request["rows"]);
            int totalCount;

            IQueryable<RoleInfo> list = RoleInfoBLL.LoadPageEntity<int>(r => r.DelFlag == 0, r => r.ID, pageSize, pageIndex, out totalCount, true);
            var temp = from r in list
                       select new { id = r.ID, name = r.RoleName, subTime = r.SubTime, remark = r.Remark, sort = r.Sort };
            return Json(new { rows = temp, total = totalCount });
        }
        /// <summary>
        /// 显示添加角色的详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAddRoleInfo()
        {
            return View();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = 0;
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            RoleInfoBLL.AddEntity(roleInfo);
            return Content("ok");
        }
        /// <summary>
        /// 为角色分配权限
        /// </summary>
        /// <returns></returns>
        public ActionResult setRoleActionInfo()
        {
            int rid = Convert.ToInt32(Request["rid"]);
            RoleInfo roleInfo = RoleInfoBLL.LoadEntity(r => r.ID == rid).FirstOrDefault();
            IQueryable<ActionInfo> actionInfoList = ActionInfoBLL.LoadEntity(r => r.DelFlag == 0);
            List<int> roleActionidList = (from r in roleInfo.ActionInfo
                                          select rid = r.ID).ToList();
            ViewBag.roleInfo = roleInfo;
            ViewBag.actionInfoList = actionInfoList;
            ViewBag.roleActionidList = roleActionidList;
            return View();
        }
        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <returns></returns>
        public ActionResult SetRoleAction()
        {
            int rid = Convert.ToInt32(Request["rid"]);
            string[] allKeys = Request.Form.AllKeys;
            List<int> actionID = new List<int>();
            foreach (string key in allKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    actionID.Add(Convert.ToInt32(key.Replace("cba_", "")));
                }
            }
            return Content(RoleInfoBLL.SetRoleActionInfo(rid, actionID) ? "ok" : "no");
        }
    }
}