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
    public partial class UserInfoBLL : BaseBLL<UserInfo>, IUserInfoBLL
    {
        //public override void GetCurrentDAL()
        //{
        //    this.CurrentDAL = this.DbSession.UserInfoDAL;

        //}
        public IQueryable<UserInfo> LoadSearchPage(UserInfoSearch userInfoSearch)
        {
            IQueryable<UserInfo> list = CurrentDAL.LoadEntity(u => u.DelFlag == 0);

            if (!string.IsNullOrEmpty(userInfoSearch.UserName))
            {
                list = list.Where(u => u.UName.Contains(userInfoSearch.UserName));
            }
            if (!string.IsNullOrEmpty(userInfoSearch.Remark))
            {
                list = list.Where(u => u.Remark.Contains(userInfoSearch.Remark));
            }
            userInfoSearch.TotalCount = list.Count();
            return list.OrderBy<UserInfo, int>(u => u.ID).Skip<UserInfo>((userInfoSearch.PageIndex-1) * userInfoSearch.PageSize).Take<UserInfo>(userInfoSearch.PageSize);
        }

    }
}
