using BLL;
using IBLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class VideoInfoController : Controller
    {
        IVideoFileInfoBLL VideoFileInfoBLL = new VideoFileInfoBLL();
        // GET: VideoInfo
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询视频文件的基本信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVideoInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int rows = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);
            int totalCount = 0;



            var list = VideoFileInfoBLL.LoadPageEntity<int>(v => v.DelFlag == 0, v => v.ID, rows, pageIndex, out totalCount, true);
            var temp = from v in list
                       select new { ID = v.ID, Title = v.Title, Author = v.Author, Origin = v.Orgin, AddDate = v.AddDate, ClassName = from a in v.VideoClass select a.ClassName };
            return Json(temp);
        }
        /// <summary>
        /// 添加视频
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAddVideo()
        {
            return View();
        }

        /// <summary>
        /// 接收视频信息
        /// </summary>
        /// <returns></returns>
        public ActionResult VideoFileUp()
        {
            HttpPostedFileBase file = Request.Files["FileData"];
            string fileName = Path.GetFileName(file.FileName);
            string fileExt = Path.GetExtension(file.FileName);
            if (fileExt==".avi")
            {
                string newFileName = Guid.NewGuid().ToString();
                WebClient client = new WebClient();
                client.UploadData("http://localhost:56995/VideoFileUp.ashx?fileName=" + newFileName + "&ext=" + fileExt, StreamToByte(file.InputStream));
            }
            return Content("no:只能上传avi格式的视频");
        }
        private byte[] StreamToByte(Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
        /// <summary>
        /// 接收从视频转换服务器发送过来的转换后的FLV视频。
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVideoFile()
        {
            string fileName = Request["fileName"];
            string t = Request["t"];
            if (t == "flv")
            {
                using (FileStream fileStream = System.IO.File.OpenWrite(Request.MapPath("/Videos/Flv/" + fileName + ".flv")))
                {
                    Request.InputStream.CopyTo(fileStream);
                    return Content("ok");
                }
            }
            else
            {
                using (FileStream fileStream = System.IO.File.OpenWrite(Request.MapPath("/Videos/Flv/" + fileName + ".jpg")))
                {
                    Request.InputStream.CopyTo(fileStream);
                    return Content("ok");
                }
            }


        }
    }
}