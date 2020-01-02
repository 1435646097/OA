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
            return list.OrderBy<UserInfo, int>(u => u.ID).Skip<UserInfo>((userInfoSearch.PageIndex - 1) * userInfoSearch.PageSize).Take<UserInfo>(userInfoSearch.PageSize);
        }

        public bool SetUserRoleInfo(List<int> roleIdList, int uid)
        {
            UserInfo userInfo = this.LoadEntity(u => u.ID == uid).FirstOrDefault();
            if (userInfo != null)
            {
                userInfo.RoleInfo.Clear();
                foreach (int id in roleIdList)
                {
                    RoleInfo roleInfo = this.DbSession.RoleInfoDAL.LoadEntity(r => r.ID == id).FirstOrDefault();
                    userInfo.RoleInfo.Add(roleInfo);
                }
                return this.DbSession.SaveChanges();
            }
            return false;
        }
    }
}
