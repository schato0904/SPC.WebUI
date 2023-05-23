<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using CTF.Web.Framework.Helper;

public class Upload : IHttpHandler {
    
    private readonly UploadHelper upload = new UploadHelper();
    string file_full = String.Empty;
    string file_name = String.Empty;
    
    public void ProcessRequest (HttpContext context) {

        System.Web.HttpContext ct = System.Web.HttpContext.Current;
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

        string Message = "";

        try
        {
            string m_sCompCode = context.Request.Params["code"];
            string defaultPath = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), m_sCompCode, "PDF");
            upload.BaseDirectory = defaultPath;
            upload.MaxUploadSize = 1024 * 10 * 10;
            upload.overWrite = true;

            int nCount = ct.Request.Files.Count;

            if (nCount <= 0)
            {
                throw new Exception("No Attached Files");
            }

            for (int i = 0; i < nCount; i++)
            {
                HttpPostedFile postedFile = ct.Request.Files[i];
                file_full = String.Format("{0}{1}", defaultPath, upload.UploadFileWithOutSub(postedFile));
                file_name = System.IO.Path.GetFileName(file_full);
            }

            Message = "Success";
        }
        catch (Exception ex)
        {
            Message = ("Failure");
            LogHelper.LogWrite(ex);
        }
        finally
        {
            response.Write(Message);
            response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}