using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Text.RegularExpressions;

namespace BLL
{
    public partial class SensitiveWordBLL : BaseBLL<SensitiveWord>, ISensitiveWordBLL
    {
        /// <summary>
        /// 是否为禁用词
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool IsForbidWord(string msg)
        {
            List<string> list = null;
            object obj = MemcacheHelper.Get("forbidWord");
            if (obj == null)
            {
                list = this.DbSession.SensitiveWordDAL.LoadEntity(a => a.IsForbid == true).Select(a => a.WordPattern).ToList();
                string str = SerializeHelper.SerializeToString(list);
                MemcacheHelper.Set("forbidWord", str);
            }
            else
            {
                list = SerializeHelper.DeSerializeToObj<List<string>>(obj.ToString());
            }
            string regex = string.Join("|", list.ToArray());
            return Regex.IsMatch(msg, regex);
        }
        /// <summary>
        /// 是否为审查词
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool IsModWord(string msg)
        {
            List<string> list = null;
            object obj = MemcacheHelper.Get("modWord");
            if (obj == null)
            {
                list = this.DbSession.SensitiveWordDAL.LoadEntity(a => a.IsMod == true).Select(a => a.WordPattern).ToList();
                string str = SerializeHelper.SerializeToString(list);
                MemcacheHelper.Set("modWord", str);
            }
            else
            {
                list = SerializeHelper.DeSerializeToObj<List<string>>(obj.ToString());
            }
            string regex = string.Join("|", list.ToArray());
            regex = regex.Replace(@"\", @"\\").Replace("{2}", "{1,2}");
            return Regex.IsMatch(msg, regex);
        }
        public string ReplaceWord(string msg)
        {
            List<SensitiveWord> list = null;
            object obj = MemcacheHelper.Get("replaceWord");
            if (obj == null)
            {
                list = this.DbSession.SensitiveWordDAL.LoadEntity(a => !a.IsMod && !a.IsForbid).ToList();
                string str = SerializeHelper.SerializeToString(list);
                MemcacheHelper.Set("replaceWord", str);
            }
            else
            {
                list = SerializeHelper.DeSerializeToObj<List<SensitiveWord>>(obj.ToString());
            }
            foreach (SensitiveWord item in list)
            {
                msg = msg.Replace(item.WordPattern, item.ReplaceWord);
            }
            return msg;

        }
    }
}
