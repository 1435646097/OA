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
        DbContext DB { get; }
        //IUserInfoDAL UserInfoDAL { get; }
        bool SaveChanges();
    }
}
