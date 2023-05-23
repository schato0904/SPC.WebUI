using System;
using System.Collections.Generic;
using System.Web;

using System.IO;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.API.Common
{
    /// <summary>
    /// FileDownload의 요약 설명입니다.
    /// </summary>
    public class FileDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string attFolder = context.Request.Params["attfolder"];
            string attFileName = context.Request.Params["attfilename"];
            string fullPath = String.Format(CommonHelper.GetAppSectionsString("subPath"), attFolder);
            string downfile = String.Format("{0}{1}", fullPath, attFileName);

            try
            {
                if (!File.Exists(downfile))
                {
                    throw new Exception("서버에서 파일이 삭제되었거나 찾을 수 없습니다");
                }
                else
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = CommonHelper.GetMimeType(Path.GetExtension(attFileName));
                    response.AddHeader("Content-Disposition", String.Format("attachment; filename={0};", attFileName));
                    response.TransmitFile(downfile);
                    response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //response.End();
                }
            }
            catch (Exception e)
            {
                HttpResponse response = HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();

                LogHelper.LogWrite(e);
                response.Write(String.Format("<script type=\"text/javascript\">alert('{0}'); window.open('', '_self', ''); window.close();</script>", e.Message));
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