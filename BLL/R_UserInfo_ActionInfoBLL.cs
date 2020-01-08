using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class R_UserInfo_ActionInfoBLL : BaseBLL<R_UserInfo_ActionInfo>, IR_UserInfo_ActionInfoBLL
    {
        public bool Delete(int userId, int actionId)
         {
            R_UserInfo_ActionInfo r_UserInfo_ActionInfo = this.DbSession.R_UserInfo_ActionInfoDAL.LoadEntity(r => r.UserInfoID == userId && r.ActionInfoID == actionId).FirstOrDefault();
            if (r_UserInfo_ActionInfo != null)
            {
                this.DbSession.R_UserInfo_ActionInfoDAL.DeleteEntity(r_UserInfo_ActionInfo);
                return this.DbSession.SaveChanges();
            }
            return false;
        }
    }
}
