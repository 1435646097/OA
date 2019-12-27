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
        public DbContext DB { get => DbContextFactory.CreateDbContext(); }

        //public IUserInfoDAL UserInfoDAL { get => AbstractFactory.CreateIUserInfoDAL(); }


        public bool SaveChanges()
        {
            return DB.SaveChanges() > 0;
        }
    }
}
