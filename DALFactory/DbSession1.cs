 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IDAL;

namespace DALFactory
{
	 public partial class DbSession:IDbSession
    {
	
		//private IActionInfoDal _ActionInfoDal;
        public IActionInfoDAL ActionInfoDAL { get => AbstractFactory.CreateIActionInfoDAL(); }
       
	
		//private IArticelDal _ArticelDal;
        public IArticelDAL ArticelDAL { get => AbstractFactory.CreateIArticelDAL(); }
       
	
		//private IArticelClassDal _ArticelClassDal;
        public IArticelClassDAL ArticelClassDAL { get => AbstractFactory.CreateIArticelClassDAL(); }
       
	
		//private IArticelCommentDal _ArticelCommentDal;
        public IArticelCommentDAL ArticelCommentDAL { get => AbstractFactory.CreateIArticelCommentDAL(); }
       
	
		//private IDepartmentDal _DepartmentDal;
        public IDepartmentDAL DepartmentDAL { get => AbstractFactory.CreateIDepartmentDAL(); }
       
	
		//private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL { get => AbstractFactory.CreateIR_UserInfo_ActionInfoDAL(); }
       
	
		//private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDAL RoleInfoDAL { get => AbstractFactory.CreateIRoleInfoDAL(); }
       
	
		//private ISensitiveWordDal _SensitiveWordDal;
        public ISensitiveWordDAL SensitiveWordDAL { get => AbstractFactory.CreateISensitiveWordDAL(); }
       
	
		//private IUserInfoDal _UserInfoDal;
        public IUserInfoDAL UserInfoDAL { get => AbstractFactory.CreateIUserInfoDAL(); }
       
	}	
}