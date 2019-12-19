using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserInfoBLL : BaseBLL<UserInfo>,IUserInfoBLL
    {
        public override void GetCurrentDAL()
        {
            this.CurrentDAL = this.DbSession.UserInfoDAL;
        }
    }
}
