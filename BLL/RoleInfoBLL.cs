using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class RoleInfoBLL : BaseBLL<RoleInfo>, IRoleInfoBLL
    {
        public bool SetRoleActionInfo(int rid, List<int> actionIDList)
        {
            RoleInfo roleInfo = this.DbSession.RoleInfoDAL.LoadEntity(r => r.ID == rid).FirstOrDefault();
            if (roleInfo!=null)
            {
                roleInfo.ActionInfo.Clear();
                foreach (int aID in actionIDList)
                {
                    ActionInfo actionInfo = this.DbSession.ActionInfoDAL.LoadEntity(a => a.ID == aID).FirstOrDefault();
                    roleInfo.ActionInfo.Add(actionInfo);
                }
                return this.DbSession.SaveChanges();
            }
            return false;
        }
    }
}
