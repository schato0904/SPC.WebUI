using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.API.Common.Gathering
{
    /// <summary>
    /// ImageView의 요약 설명입니다.
    /// </summary>
    public class ImageView : IHttpHandler
    {
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

        string defaultPath = String.Format(CommonHelper.GetAppSectionsString("subPath"), "APPLICATION");

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string m_sFileName = context.Request.Params["name"];
                string m_sFileExtend = Path.GetExtension(m_sFileName.ToLower());

                if (isImage(m_sFileExtend) == true)
                {
                    string m_sCompCode = context.Request.Params["code"];
                    string m_sGNB = context.Request.Params["gbn"] ?? "PDF";

                    defaultPath = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), m_sCompCode, m_sGNB) + m_sFileName;

                    if (!File.Exists(defaultPath))
                        throw new Exception("이미지를 찾을 수 없습니다.");

                    response.ContentType = "text/html";
                    response.Write("<html>\x0A");
                    response.Write("<head>\x0A");
                    response.Write("<title>SPC::Image Viewer</title>\x0A");
                    response.Write("<script type=\"text/javascript\" src=\"../../../Resources/jquery/jquery-1.10.2.min.js\"></script>\x0A");
                    response.Write("<script type=\"text/javascript\" src=\"../../../Resources/jquery/resize_image_by_window.js\"></script>\x0A");
                    response.Write("<script type=\"text/javascript\">$(document).ready(function() { $('#imgPanel').load(function() { $('#imgPanel').resizeimg(); }); });</script>\x0A");
                    //response.Write("<style type=\"text/css\">\x0A");
                    //response.Write("html, body {width: 100%; height: 100%; padding: 0px; margin: 0px;}\x0A");
                    //response.Write("#content {width: 100%; height: 100%; position: absolute; top: 0; bottom: 0; overflow: scroll;;}\x0A");
                    //response.Write("</style>\x0A");
                    response.Write("</head>\x0A");
                    response.Write("<body>\x0A");
                    response.Write(String.Format("<img id=\"imgPanel\" src=\"Download.ashx?code={0}&name={1}&gbn={2}\" />\x0A", m_sCompCode, m_sFileName, m_sGNB));
                    //response.Write(String.Format("<div id=\"content\"><img id=\"imgPanel\" src=\"Download.ashx?code={0}&name={1}\" /></div>\x0A", m_sCompCode, m_sFileName));
                    response.Write("</body>\x0A");
                    response.Write("</html>\x0A");
                }
                else
                {
                    throw new Exception("이미지파일만 사용가능합니다");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogWrite(ex);
                response.Write(String.Format("<script type=\"text/javascript\">alert('이미지를 불러오는 중 장애가 발생했습니다.\\r장애원인 : {0}');</script>", ex.Message));
            }
            response.End();
        }

        bool isImage(string m_sFileExtend)
        {
            switch (m_sFileExtend)
            {
                default:
                    return false;
                case ".bmp":
                case ".jpg":
                case ".gif":
                case ".png":
                    return true;
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