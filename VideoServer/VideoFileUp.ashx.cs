using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Common;
using Model;

namespace VideoServer
{
    /// <summary>
    /// VideoFileUp 的摘要说明
    /// </summary>
    public class VideoFileUp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string fileName = context.Request["fileName"];
            string fileExt = context.Request["ext"];
            using (FileStream fileStream = File.OpenRead("d:/" + fileName + fileExt))
            {

                VideoFileModel videoFileModel = new VideoFileModel()
                {
                    VideoFileExt = fileExt,
                    VideoFileName = fileName
                };
                RedisHelper.EnQueue("VideoFileInfo", SerializeHelper.SerializeToString(videoFileModel));
            
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}