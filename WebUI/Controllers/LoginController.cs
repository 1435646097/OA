﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        IBLL.IUserInfoBLL UserInfoBLL { get; set; }
        public ActionResult Index()
        {
            if (Request.Cookies["sessionId"] == null)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                if (Request.Cookies["cp1"] != null)
                {
                    string userName = Request.Cookies["cp1"].Value;//获取Cookie存储的用户名
                    var userInfo = UserInfoBLL.LoadEntity(u => u.UName == userName).FirstOrDefault();
                    if (Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCodeImage()
        {
            string vcode = Common.ValidateCode.CreateValidateCode(4);
            Session["vcode"] = vcode;
            byte[] buffer = Common.ValidateCode.CreateValidateGraphic(vcode);
            return File(buffer, "image/jpeg");
        }
        /// <summary>
        /// 完成用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin()
        {
            //校验验证码
            string validateCode = Session["vcode"] != null ? Session["vcode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误!!");
            }
            Session["vcode"] = null;
            string userCode = Request["vCode"];
            if (!validateCode.Equals(userCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误!!");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPwd))
            {
                string uPwd = Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(userPwd));//密码两次MD5加密
                var userInfo = UserInfoBLL.LoadEntity(u => u.UName == userName && u.UPwd == uPwd).FirstOrDefault();//校验用户名密码。
                if (userInfo != null)
                {
                    //Session["userInfo"] = userInfo;
                    string sessionId = Guid.NewGuid().ToString();
                    MemcacheHelper.Set(sessionId, SerializeHelper.SerializeToString(userInfo), DateTime.Now.AddMinutes(20));
                    Response.Cookies["sessionId"].Value = sessionId;
                    //判断复选框是否被选中.
                    if (!string.IsNullOrEmpty(Request["autoLogin"]))
                    {
                        HttpCookie cookie1 = new HttpCookie("cp1", userName);
                        HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(userPwd)));
                        cookie1.Expires = DateTime.Now.AddDays(7);
                        cookie2.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie1);
                        Response.Cookies.Add(cookie2);
                    }
                    return Content("ok:登录成功!!");
                }
                else
                {
                    return Content("no:用户名密码错误!!");
                }
            }
            else
            {
                return Content("no:用户名密码不能为空!!");
            }
        }
    }
}