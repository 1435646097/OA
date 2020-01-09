using IBLL;
using Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo UserInfo { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            IApplicationContext ctx = ContextRegistry.GetContext();
            //if (Session["userInfo"] == null)
            if (Request.Cookies["sessionId"] == null)
            {
                // filterContext.HttpContext.Response.Redirect("/Login/Index");
                if (Request.Cookies["cp1"] != null)
                {
                    string userName = Request.Cookies["cp1"].Value;//获取Cookie存储的用户名
                    //判断用户是否正确.
                    IUserInfoBLL userInfoBLL = (IUserInfoBLL)ctx.GetObject("UserInfoBLL");
                    UserInfo userInfo = userInfoBLL.LoadEntity(u => u.UName == userName).FirstOrDefault();
                    if (!Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        UserInfo = userInfo;
                        filterContext.Result = Redirect(Url.Action("Index", "Login"));//注意跳转.
                    }
                    filterContext.Result = Redirect(Url.Action("Index", "Home"));//注意跳转.
                }
                else
                {
                    filterContext.Result = Redirect(Url.Action("Index", "Login"));//注意跳转.
                }
            }
            else
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                Object obj = MemcacheHelper.Get(sessionId);
                if (obj != null)
                {
                    UserInfo userInfo = SerializeHelper.DeSerializeToObj<UserInfo>(obj.ToString());
                    UserInfo = userInfo;
                    Common.MemcacheHelper.Set(sessionId, SerializeHelper.SerializeToString(userInfo), DateTime.Now.AddMinutes(20));
                }
                else
                {
                    filterContext.Result = Redirect(Url.Action("Index", "Login"));//注意跳转.
                }
            }

            //过滤非菜单权限。
            if (UserInfo != null)
            {
                //留后门，发布一定要删除
                if (UserInfo.UName == "itcast")
                {
                    return;
                }

                string url = Request.Url.AbsolutePath.ToString().ToLower();//获取当前请求的URL地址。
                string httpMethod = Request.HttpMethod;//获取请求的方式。
                //查找url地址对应的权限信息。
                IActionInfoBLL actionInfoService = (IActionInfoBLL)ctx.GetObject("ActionInfoBLL");
                var actionInfo = actionInfoService.LoadEntity(a => a.Url == url && a.HttpMethod == httpMethod).FirstOrDefault();
                if (actionInfo == null)
                {
                    filterContext.Result = Redirect("/Error.html");
                    return;
                }
                //判断登录用户是否有actionInfo的访问权限。
                //也是按照两条线进行过滤。
                //1先按照用户-->权限这条进行过滤.
                IUserInfoBLL userInfoBLL = (IUserInfoBLL)ctx.GetObject("UserInfoBLL");
                var userInfo = userInfoBLL.LoadEntity(u => u.ID == UserInfo.ID).FirstOrDefault();//获取登录用户信息。
                var userAction = (from a in userInfo.R_UserInfo_ActionInfo
                                  where a.ActionInfoID == actionInfo.ID
                                  select a
                                    ).FirstOrDefault();
                if (userAction != null)//如果成立，表示登录用户有userInfo这个权限，但是考虑是否 被禁止。
                {
                    if (userAction.IsPass)//表示允许，后面就不要校验了，直接访问用户请求的Url地址。
                    {
                        return;
                    }
                    else
                    {
                        filterContext.Result = Redirect("/Error.html");
                        return;
                    }
                }

                //2:按照用户-->角色--->权限进行校验.
                var loginUserRoles = userInfo.RoleInfo;
                var loginUserAction = (from r in loginUserRoles
                                       from a in r.ActionInfo
                                       where a.ID == actionInfo.ID
                                       select a).Count();
                if (loginUserAction < 1)
                {
                    filterContext.Result = Redirect("/Error.html");
                    return;
                }
            }
        }
    }
}