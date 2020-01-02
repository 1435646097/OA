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
                       select new { id=r.ID,name = r.RoleName, subTime = r.SubTime, remark = r.Remark, sort = r.Sort };
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

    }
}