using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DbContextFactory
    {
        /// <summary>
        /// 保证DbContext线程内唯一
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateDbContext()
        {
            DbContext Db = (DbContext)CallContext.GetData("DbContext");
            if (Db==null)
            {
                Db = new ItcastCmsEntities();
                CallContext.SetData("DbContext", Db);
            }
            return Db;
        }
    }
}
