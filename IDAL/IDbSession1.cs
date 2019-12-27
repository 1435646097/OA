 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
	public partial interface IDbSession
    {

	
		IActionInfoDAL ActionInfoDAL{get;}
	
		IDepartmentDAL DepartmentDAL{get;}
	
		IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL{get;}
	
		IRoleInfoDAL RoleInfoDAL{get;}
	
		IUserInfoDAL UserInfoDAL{get;}
	}	
}