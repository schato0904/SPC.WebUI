using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.API.Common.Gathering
{
    /// <summary>
    /// PDFView의 요약 설명입니다.
    /// </summary>
    public class PDFView : IHttpHandler
    {
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

        string defaultPath = String.Format(CommonHelper.GetAppSectionsString("subPath"), "APPLICATION");

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string m_sCompCode = context.Request.Params["code"];
                string m_sDataGBN = context.Request.Params["gbn"] ?? "PDF";
                string m_sFileName = context.Request.Params["name"];
                string m_sFileExtend = Path.GetExtension(m_sFileName.ToLower());

                if (isPDF(m_sFileExtend) == true)
                {
                    defaultPath = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), m_sCompCode, m_sDataGBN);

                    if (!File.Exists(String.Concat(defaultPath, m_sFileName)))
                        throw new Exception("PDF 파일을 찾을 수 없습니다.");

                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "application/pdf";
                    response.TransmitFile(String.Concat(defaultPath, m_sFileName));
                    response.Flush();
                }
                else
                {
                    throw new Exception("PDF파일만 사용가능합니다");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogWrite(ex);
                response.Write(String.Format("<script type=\"text/javascript\">alert('PDF를 불러오는 중 장애가 발생했습니다.\\r장애원인 : {0}');</script>", ex.Message));
            }
            response.End();
        }

        bool isPDF(string m_sFileExtend)
        {
            switch (m_sFileExtend)
            {
                default:
                    return false;
                case ".pdf":
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