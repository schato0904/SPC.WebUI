using CTF.Web.Framework.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SPC.WebUI.API.Common.WinSvc
{
    /// <summary>
    /// Publisher의 요약 설명입니다.
    /// </summary>
    public class Publisher : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string attFolder = context.Request.Params["folder"];
            string attFileName = context.Request.Params["filename"];
            string fullPath = String.Format(CommonHelper.GetAppSectionsString("subPath"), attFolder);
            string downfile = String.Format("{0}{1}", fullPath, attFileName);

            try
            {
                if (!File.Exists(downfile))
                {
                    LogHelper.LogWrite("서버에서 파일이 삭제되었거나 찾을 수 없습니다");
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
                }
            }
            catch (Exception e)
            {
                LogHelper.LogWrite(e);
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