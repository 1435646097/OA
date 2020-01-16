 

using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
		
	public partial class ActionInfoDAL :BaseDAL<ActionInfo>,IActionInfoDAL
    {

    }
		
	public partial class ArticelDAL :BaseDAL<Articel>,IArticelDAL
    {

    }
		
	public partial class ArticelClassDAL :BaseDAL<ArticelClass>,IArticelClassDAL
    {

    }
		
	public partial class ArticelCommentDAL :BaseDAL<ArticelComment>,IArticelCommentDAL
    {

    }
		
	public partial class DepartmentDAL :BaseDAL<Department>,IDepartmentDAL
    {

    }
		
	public partial class R_UserInfo_ActionInfoDAL :BaseDAL<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDAL
    {

    }
		
	public partial class RoleInfoDAL :BaseDAL<RoleInfo>,IRoleInfoDAL
    {

    }
		
	public partial class SensitiveWordDAL :BaseDAL<SensitiveWord>,ISensitiveWordDAL
    {

    }
		
	public partial class UserInfoDAL :BaseDAL<UserInfo>,IUserInfoDAL
    {

    }
	
}