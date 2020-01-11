using IBLL;
using Model;
using Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class ArticelBLL : BaseBLL<Articel>, IArticelBLL
    {
        public IQueryable<Articel> GetArticelList(ArticleInfoSearch articleInfoSearch)
        {
            IQueryable<Articel> ArticleInfoList = this.DbSession.ArticelDAL.LoadEntity(a => a.DelFlag == 0);

            if (!string.IsNullOrEmpty(articleInfoSearch.ArticelClassID))
            {
                int articleClassId = Convert.ToInt32(articleInfoSearch.ArticelClassID);
                ArticleInfoList = from a in ArticleInfoList
                                  from b in a.ArticelClass
                                  where b.ID == articleClassId
                                  select a;
            }
            if (!string.IsNullOrEmpty(articleInfoSearch.ArticeTitle))
            {
                ArticleInfoList = ArticleInfoList.Where(a => a.Title.Contains(articleInfoSearch.ArticeTitle));
            }
            if (!string.IsNullOrEmpty(articleInfoSearch.ArticelAuthor))
            {
                ArticleInfoList = ArticleInfoList.Where(a => a.Author.Contains(articleInfoSearch.ArticelAuthor));
            }
            if (!string.IsNullOrEmpty(articleInfoSearch.ToDatepicker) && !string.IsNullOrEmpty(articleInfoSearch.FormDatepicker))
            {
                DateTime fromDate = Convert.ToDateTime(articleInfoSearch.FormDatepicker);
                DateTime toDate = Convert.ToDateTime(articleInfoSearch.ToDatepicker);
                ArticleInfoList = ArticleInfoList.Where(a => a.AddDate >= fromDate && a.AddDate <= toDate);
            }
            articleInfoSearch.TotalCount = ArticleInfoList.Count();
            IQueryable<Articel> temp = ArticleInfoList.OrderBy<Articel, int>(a => a.ID).Skip<Articel>((articleInfoSearch.PageIndex - 1) * articleInfoSearch.PageSize).Take<Articel>(articleInfoSearch.PageSize);
            return temp;
        }
    }
}
