using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface IR_UserInfo_ActionInfoBLL : IBaseBLL<R_UserInfo_ActionInfo>
    {

        bool Delete(int userId, int actionId);
    }
}
