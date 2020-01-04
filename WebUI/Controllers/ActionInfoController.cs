using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ActionInfoController : Controller
    {
        // GET: ActionInfo
        IActionInfoBLL ActionInfoBLL = new ActionInfoBLL();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetActionInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : Convert.ToInt32(Request["rows"]);
            int totalCount;

            IQueryable<ActionInfo> list = ActionInfoBLL.LoadPageEntity<int>(a => a.DelFlag == 0, a => a.ID, pageSize, pageIndex, out totalCount, true);
            var temp = from a in list
                       select new { id = a.ID, name = a.ActionInfoName, subTime = a.SubTime, remark = a.Remark, sort = a.Sort, actionTypeEnum = a.ActionTypeEnum, httpMethod = a.HttpMethod };
            return Json(new { rows = temp, total = totalCount });
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult PictureFileUpload()
        {
            HttpPostedFileBase file = Request.Files[0];
            string fileName = Path.GetFileName(file.FileName);
            string fileExt = Path.GetExtension(fileName).ToLower();
            if (fileExt == ".jpg" || fileExt == ".png")
            {
                string newFileNmae = Guid.NewGuid().ToString();
                string directory = "/Picture/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                string filePath;
                if (!Directory.Exists(Server.MapPath(directory)))
                {
                    Directory.CreateDirectory(Server.MapPath(directory));
                }
                filePath = directory + newFileNmae + fileExt;
                file.SaveAs(Server.MapPath(filePath));
                return Content("ok:" + filePath);
            }
            else
            {
                return Content("no:请上传正确的文件");
            }
        }
        /// <summary>
        /// 完成添加
        /// </summary>
        /// <returns></returns>
        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            actionInfo.DelFlag = 0;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.ModifiedOn = DateTime.Now.ToString();
            actionInfo.Url = actionInfo.Url.ToLower();
            ActionInfoBLL.AddEntity(actionInfo);
            return Content("ok");
        }
    }
}