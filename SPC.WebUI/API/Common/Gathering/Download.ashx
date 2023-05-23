<%@ WebHandler Language="C#" Class="Download" %>

using System;
using System.Web;
using System.Configuration;
using System.IO;
using System.Data;
using CTF.Web.Framework.Helper;

public class Download : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        
        string defaultPath = String.Format(CommonHelper.GetAppSectionsString("subPath"), "APPLICATION");

        try
        {
            string m_sFileName = context.Request.Params["name"];
            string m_sFileExtend = Path.GetExtension(m_sFileName.ToLower());

            if (isImage(m_sFileExtend) == true || isPDF(m_sFileExtend) == true || isWAV(m_sFileExtend) == true)
            {
                string m_sCompCode = context.Request.Params["code"];
                string m_sGNB = context.Request.Params["gbn"] ?? "PDF";

                defaultPath = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), m_sCompCode, m_sGNB);
                string downfile = String.Format("{0}{1}", defaultPath, m_sFileName);

                if (!File.Exists(downfile))
                {
                    throw new Exception("서버에서 파일이 삭제되었거나 찾을 수 없습니다");
                }
                else
                {
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = isImage(m_sFileName.ToLower()) == true ? CommonHelper.GetMimeType(m_sFileExtend) : "application/octet-stream";
                    response.AddHeader("Content-Disposition", String.Format("attachment; filename={0};", m_sFileName));
                    response.TransmitFile(downfile);
                    response.Flush();
                }
            }
            else
            {

                string m_sFileVer = context.Request.Params["ver"];

                if (String.IsNullOrEmpty(m_sFileVer))
                {
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.dll;", m_sFileName));
                    response.TransmitFile(String.Concat(defaultPath, m_sFileName, ".dll"));
                    response.Flush();
                }
                else
                {
                    int m_nFileVer = int.Parse(m_sFileVer.Replace(".", ""));

                    // 버전체크
                    FileStream fs = new FileStream(String.Concat(defaultPath, m_sFileName, ".txt"), FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string ver = sr.ReadLine();
                    int nVer = int.Parse(ver.Replace(".", ""));
                    sr.Close();
                    sr.Dispose();
                    fs.Close();
                    fs.Dispose();

                    // 버전이 다른경우 다운로드
                    if (m_nFileVer < nVer)
                    {
                        response.ClearContent();
                        response.Clear();
                        response.ContentType = "application/octet-stream";
                        response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.exe;", m_sFileName));
                        response.TransmitFile(String.Concat(defaultPath, m_sFileName, ".exe"));
                        response.Flush();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogWrite(ex);
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

    bool isWAV(string m_sFileExtend)
    {
        switch (m_sFileExtend)
        {
            default:
                return false;
            case ".wav":
                return true;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}