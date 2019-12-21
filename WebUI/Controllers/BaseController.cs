using IBLL;
using Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class BaseController:Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["userInfo"] == null)
            {
                // filterContext.HttpContext.Response.Redirect("/Login/Index");
                if (Request.Cookies["cp1"] != null)
                {
                    string userName = Request.Cookies["cp1"].Value;//获取Cookie存储的用户名
                    //判断用户是否正确.
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    IUserInfoBLL userInfoBLL = (IUserInfoBLL)ctx.GetObject("UserInfoService");
                    UserInfo userInfo = userInfoBLL.LoadEntity(u => u.UName == userName).FirstOrDefault();
                    if (!Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        filterContext.Result = Redirect(Url.Action("Index", "Login"));//注意跳转.
                    }
                }
                else
                {
                    filterContext.Result = Redirect(Url.Action("Index", "Login"));//注意跳转.
                }
            }
        }
    }
}