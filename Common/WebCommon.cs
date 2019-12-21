using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class WebCommon
    {
        //public static void Show()
        //{
        //    HttpContext.Current.Response.Write("ssss");

        //}
        /// <summary>
        /// 对字符串进行MD5运算
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5Buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5Buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            md5.Clear();
            return sb.ToString();
        }
        /// <summary>
        /// 判断Cookie
        /// </summary>
        public static bool ValidateCookieInfo(UserInfo userInfo)
        {
            bool isSucess = false;
            HttpContext context = HttpContext.Current;
            if (userInfo != null)
            {
                if (context.Request.Cookies["cp2"] != null)
                {
                    string userPwd = context.Request.Cookies["cp2"].Value;
                    if (userInfo.UPwd == userPwd)
                    {
                        context.Session["userInfo"] = userInfo;
                        isSucess = true;
                        return isSucess;
                    }
                }
            }
            context.Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
            context.Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            return isSucess;
        }
    }
}
