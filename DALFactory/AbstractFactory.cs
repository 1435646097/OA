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
    public class AbstractFactory
    {
        static readonly string assmblyPath = ConfigurationManager.AppSettings["assemblyPath"];
        static readonly string nameSpace = ConfigurationManager.AppSettings["nameSpace"];

        public static IUserInfoDAL CreateIUserInfoDAL()
        {
            string fullName = nameSpace + ".UserInfoDAL";
            return CreateInstances(fullName) as IUserInfoDAL;
        }
        public static object CreateInstances(string fullName)
        {
            Assembly assembly = Assembly.Load(assmblyPath);
            return assembly.CreateInstance(fullName);
        }
    }
}
