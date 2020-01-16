using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BLL;
using Common;
using IBLL;
using Model;
using Model.Search;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ArticleInfoController : Controller
    {
        IArticelClassBLL ArticelClassBLL = new ArticelClassBLL();
        IArticelBLL ArticelBLL = new ArticelBLL();
        IArticelCommentBLL ArticelCommentBLL = new ArticelCommentBLL();
        ISensitiveWordBLL SensitiveWordBLL = new SensitiveWordBLL();
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
        /// <summary>
        /// 加载文章类别
        /// </summary>
        /// <returns></returns>
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
        public ActionResult ShowAddInfo()
        {
            ViewBag.classInfo = LoadSelectData();
            return View();
        }


        /// <summary>
        /// 加载相关新闻
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadLikeNews()
        {
            int id = int.Parse(Request["articelId"]);
            Articel article = ArticelBLL.LoadEntity(a => a.ID == id).FirstOrDefault();
            var articleList = (from a in article.ArticelClass
                               from b in a.Articel
                               where b.ID != id
                               select b).OrderBy(a => a.ID).Skip(0).Take(4);
            var temp = from a in articleList
                       select new { Id = a.ID, Title = a.Title, AddDate = a.AddDate };
            return Content(SerializeHelper.SerializeToString(temp));
        }
        /// <summary>
        /// 一站式静态化
        /// </summary>
        /// <returns></returns>
        public ActionResult setStaticPage()
        {
            var articleList = ArticelBLL.LoadEntity(a => a.DelFlag == 0);
            foreach (Articel article in articleList)
            {
                StaticPage(article);
            }
            return Content("ok");
        }
        /// <summary>
        /// 静态化页面
        /// </summary>
        /// <returns></returns>
        public void StaticPage(Articel article)
        {

            string htmlFile = Common.NVelocityHelper.RenderTemplate("ArticelTemplateInfo", article, "/ArticelTemplate/");
            DateTime addDate = article.AddDate;
            string dir = "/ArticelHtml/" + addDate.Year + "/" + addDate.Month + "/" + addDate.Day + "/";
            if (!Directory.Exists(Server.MapPath(dir)))
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }
            string fulePath = dir + article.ID + ".html";
            System.IO.File.WriteAllText(Server.MapPath(fulePath), htmlFile);
        }
        /// <summary>
        /// 添加评论信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddComment()
        {
            int id = int.Parse(Request["id"]);
            string msg = Request["msg"];
            if (SensitiveWordBLL.IsForbidWord(msg))
            {
                return Content("no:您的评论包含禁用词，不允许评论");
            }
            else if (SensitiveWordBLL.IsModWord(msg))
            {
                ArticelComment articelComment = new ArticelComment()
                {
                    AddDate = DateTime.Now,
                    ArticelID = id,
                    IsPass = 0,
                    Msg = msg
                };
                ArticelCommentBLL.AddEntity(articelComment);
                return Content("no:您的评论包含审查词，需要审核");
            }
            else
            {
                msg = SensitiveWordBLL.ReplaceWord(msg);
                ArticelComment articelComment = new ArticelComment()
                {
                    AddDate = DateTime.Now,
                    ArticelID = id,
                    IsPass = 1,
                    Msg = msg
                };
                ArticelCommentBLL.AddEntity(articelComment);
                return Content("ok:评论成功");
            }
        }
        public ActionResult LoadComment()
        {
            int id = int.Parse(Request["id"]);
            var CommentList = ArticelCommentBLL.LoadEntity(a => a.ArticelID == id&&a.IsPass==1).ToList();
            List<ArticleCommentViewModel> newList = new List<ArticleCommentViewModel>();
            foreach (var articleModel in CommentList)
            {
                ArticleCommentViewModel model = new ArticleCommentViewModel();
                TimeSpan ts = DateTime.Now - articleModel.AddDate;
                model.AddDate = WebCommon.GetTimespan(ts);
                model.Msg = articleModel.Msg;
                newList.Add(model);
            }
            return Content(Common.SerializeHelper.SerializeToString(newList));
        }
    }
}