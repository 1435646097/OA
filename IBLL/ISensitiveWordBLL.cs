using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface ISensitiveWordBLL:IBaseBLL<SensitiveWord>
    {
        bool IsForbidWord(string msg);
        bool IsModWord(string msg);
        string ReplaceWord(string msg);
    }
}
