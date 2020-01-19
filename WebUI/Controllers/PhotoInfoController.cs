using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using IBLL;

namespace WebUI.Controllers
{
    public class PhotoInfoController : Controller
    {
        IPhotoInfoBLL PhotoInfoBLL = new PhotoInfoBLL();
        // GET: PhotoInfo
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取图片详情
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPhotoInfo()
        {
            var photoList = PhotoInfoBLL.LoadEntity(a => a.DelFlag == 0);
            var temp = from p in photoList
                       select new { ID = p.ID, Title = p.Title, Author = p.Author, Origin = p.Orgin, AddDate = p.AddDate,ImgUrl=p.PicUrls };
     
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}