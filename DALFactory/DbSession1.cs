 

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
       
	
		//private IDepartmentDal _DepartmentDal;
        public IDepartmentDAL DepartmentDAL { get => AbstractFactory.CreateIDepartmentDAL(); }
       
	
		//private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL { get => AbstractFactory.CreateIR_UserInfo_ActionInfoDAL(); }
       
	
		//private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDAL RoleInfoDAL { get => AbstractFactory.CreateIRoleInfoDAL(); }

       
	}	
}