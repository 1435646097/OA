 

using IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IActionInfoDAL CreateIActionInfoDAL()
        {

           // string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            //var obj  = GetInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


           // return obj as IActionInfoDAL;
           string fullName = nameSpace + ".ActionInfoDAL";
           return CreateInstances(fullName) as IActionInfoDAL;
        }
		
	    public static IArticelDAL CreateIArticelDAL()
        {

           // string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ArticelDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            //var obj  = GetInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


           // return obj as IArticelDAL;
           string fullName = nameSpace + ".ArticelDAL";
           return CreateInstances(fullName) as IArticelDAL;
        }
		
	    public static IArticelClassDAL CreateIArticelClassDAL()
        {

           // string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ArticelClassDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            //var obj  = GetInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


           // return obj as IArticelClassDAL;
           string fullName = nameSpace + ".ArticelClassDAL";
           return CreateInstances(fullName) as IArticelClassDAL;
        }
		
	    public static IArticelCommentDAL CreateIArticelCommentDAL()
        {

           // string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ArticelCommentDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            //var obj  = GetInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


           // return obj as IArticelCommentDAL;
           string fullName = nameSpace + ".ArticelCommentDAL";
           return CreateInstances(fullName) as IArticelCommentDAL;
        }
		
	    public static IDepartmentDAL CreateIDepartmentDAL()
        {

           // string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".DepartmentDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            //var obj  = GetInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


           // return obj as IDepartmentDAL;
           string fullName = nameSpace + ".DepartmentDAL";
           return CreateInstances(fullName) as IDepartmentDAL;
        }
		
	    public static IR_UserInfo_ActionInfoDAL CreateIR_UserInfo_ActionInfoDAL()
        {

           // string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".R_UserInfo_ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            //var obj  = GetInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


           // return obj as IR_UserInfo_ActionInfoDAL;
           string fullName = nameSpace + ".R_UserInfo_ActionInfoDAL";
           return CreateInstances(fullName) as IR_UserInfo_ActionInfoDAL;
        }
		
	    public static IRoleInfoDAL CreateIRoleInfoDAL()
        {

           // string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".RoleInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            //var obj  = GetInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


           // return obj as IRoleInfoDAL;
           string fullName = nameSpace + ".RoleInfoDAL";
           return CreateInstances(fullName) as IRoleInfoDAL;
        }
		
	    public static IUserInfoDAL CreateIUserInfoDAL()
        {

           // string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".UserInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            //var obj  = GetInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


           // return obj as IUserInfoDAL;
           string fullName = nameSpace + ".UserInfoDAL";
           return CreateInstances(fullName) as IUserInfoDAL;
        }
	}
	
}