using Model;
using Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface IUserInfoBLL:IBaseBLL<UserInfo>
    {
        IQueryable<UserInfo> LoadSearchPage(UserInfoSearch userInfoSearch);
    }
}
