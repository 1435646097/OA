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
    public class ArticleClassController : Controller
    {
        IArticelClassBLL ArticelClassBLL = new ArticelClassBLL();
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowArticelClass()
        {
            IQueryable<ArticelClass> articel = ArticelClassBLL.LoadEntity(a => a.ParentId == 0);
            var temp = from t in articel
                       select new { id = t.ID, className = t.ClassName, Remark = t.Remark };
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}