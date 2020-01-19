 

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
	
		IArticelDAL ArticelDAL{get;}
	
		IArticelClassDAL ArticelClassDAL{get;}
	
		IArticelCommentDAL ArticelCommentDAL{get;}
	
		IDepartmentDAL DepartmentDAL{get;}
	
		IPhotoInfoDAL PhotoInfoDAL{get;}
	
		IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL{get;}
	
		IRoleInfoDAL RoleInfoDAL{get;}
	
		ISensitiveWordDAL SensitiveWordDAL{get;}
	
		IUserInfoDAL UserInfoDAL{get;}
	
		IVideoClassDAL VideoClassDAL{get;}
	
		IVideoFileInfoDAL VideoFileInfoDAL{get;}
	}	
}