 

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
	
	public partial class ActionInfoBLL :BaseBLL<ActionInfo>,IActionInfoBLL
    {
        
        public override void GetCurrentDAL()
        {
            this.CurrentDAL = this.DbSession.ActionInfoDAL;

        }
    }   
	
	public partial class DepartmentBLL :BaseBLL<Department>,IDepartmentBLL
    {
        
        public override void GetCurrentDAL()
        {
            this.CurrentDAL = this.DbSession.DepartmentDAL;

        }
    }   
	
	public partial class R_UserInfo_ActionInfoBLL :BaseBLL<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoBLL
    {
        
        public override void GetCurrentDAL()
        {
            this.CurrentDAL = this.DbSession.R_UserInfo_ActionInfoDAL;

        }
    }   
	
	public partial class RoleInfoBLL :BaseBLL<RoleInfo>,IRoleInfoBLL
    {
        
        public override void GetCurrentDAL()
        {
            this.CurrentDAL = this.DbSession.RoleInfoDAL;

        }
    }   
	
	public partial class UserInfoBLL :BaseBLL<UserInfo>,IUserInfoBLL
    {
        
        public override void GetCurrentDAL()
        {
            this.CurrentDAL = this.DbSession.UserInfoDAL;

        }
    }   
	
}