using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BLL;
using IBLL;
using Model;
using Model.Search;

namespace WebUI.Controllers
{
    public class ArticleInfoController : Controller
    {
        IArticelClassBLL ArticelClassBLL = new ArticelClassBLL();
        IArticelBLL ArticelBLL = new ArticelBLL();
        // GET: ArticleInfo
        public ActionResult Index()
        {
            ViewBag.classInfo = LoadSelectData();
            return View();
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetArticelInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : Convert.ToInt32(Request["rows"]);
            int totalCount = 0;
            ArticleInfoSearch articleInfoSearch = new ArticleInfoSearch()
            {
                ArticelAuthor = Request["txtArticelAuthor"],
                ArticelClassID = Request["articelClassId"],
                ArticeTitle = Request["txtArticeTitle"],
                FormDatepicker = Request["formDatepicker"],
                ToDatepicker = Request["toDatepicker"],
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            IQueryable<Articel> list = ArticelBLL.GetArticelList(articleInfoSearch);
            var temp = from a in list
                       select new { ID = a.ID, Title = a.Title, Author = a.Author, Origin = a.Origin, AddDate = a.AddDate, ClassName = from b in a.ArticelClass select b.ClassName };
            return Json(new { total = articleInfoSearch.TotalCount, rows = temp }, JsonRequestBehavior.AllowGet);
        }
        private string LoadSelectData()
        {
            var articleParentList = ArticelClassBLL.LoadEntity(a => a.DelFlag == 0 && a.ParentId == 0);
            StringBuilder sb = new StringBuilder();
            foreach (var articleParent in articleParentList)
            {
                sb.Append("<option value='" + articleParent.ID + "'>--" + articleParent.ClassName + "</option>");
                var articleChildList = ArticelClassBLL.LoadEntity(a => a.ParentId == articleParent.ID);
                foreach (var articleChild in articleChildList)
                {
                    sb.Append("<option value='" + articleChild.ID + "'>------" + articleChild.ClassName + "</option>");
                }
            }
            return sb.ToString();
        }
    }
}