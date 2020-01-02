using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    public class DbSessionFactory
    {
        /// <summary>
        /// 保证DBSession线程内唯一
        /// </summary>
        /// <returns></returns>
        public static IDbSession CreateDbSession()
        {
            IDbSession Dbsession = (IDbSession)CallContext.GetData("DbSession");
            if (Dbsession == null)
            {
                Dbsession = new DbSession();
                CallContext.SetData("DbSession", Dbsession);
            }
            return Dbsession;
        }
    }
}
