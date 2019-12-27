﻿using BLL;
using DAL;
using IBLL;
using Model;
using Model.Search;
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
        /// <summary>
        /// 加载用户数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;//当前页码。
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;//每页显示记录数。

            UserInfoSearch userInfoSearch = new UserInfoSearch()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Remark = Request["remark"],
                UserName = Request["name"]
            };
            IQueryable<UserInfo> list = userInfoBLL.LoadSearchPage(userInfoSearch);
            var temp = from u in list
                       select new { ID = u.ID, UName = u.UName, UPwd = u.UPwd, Remark = u.Remark };
            return Json(new { rows = temp, total = userInfoSearch.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteUser()
        {
            string strId = Request["strId"];//1,2,3,4
            string[] splitId = strId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in splitId)
            {
                int id = Convert.ToInt32(item);
                UserInfo entity = userInfoBLL.CurrentDAL.LoadEntity(u => u.ID == id).FirstOrDefault();
                entity.DelFlag = 1;
                userInfoBLL.CurrentDAL.EditEntity(entity);
            }
            if (userInfoBLL.DbSession.SaveChanges())
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser(UserInfo entity)
        {
            entity.ModifiedOn = DateTime.Now;
            entity.DelFlag = 0;
            entity.SubTime = DateTime.Now;
            userInfoBLL.AddEntity(entity);
            return Content("ok");
        }
        /// <summary>
        /// 展示用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUser()
        {
            int id = Convert.ToInt32(Request["id"]);
            UserInfo entity = userInfoBLL.LoadEntity(u => u.ID == id).FirstOrDefault();
            return Json(entity);
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUser(UserInfo entity)
        {
            entity.ID = Convert.ToInt32(Request["id"]);
            entity.ModifiedOn = DateTime.Now;
            return Content(userInfoBLL.EditEntity(entity) ? "ok" : "no");
        }
    }
}