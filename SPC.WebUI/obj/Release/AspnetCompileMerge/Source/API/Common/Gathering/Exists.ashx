<%@ WebHandler Language="C#" Class="Exists" %>

using System;
using System.Web;
using CTF.Web.Framework.Helper;

public class Exists : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        System.Web.HttpContext ct = System.Web.HttpContext.Current;
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

        string Message = "";

        try
        {
            string m_sCompCode = context.Request.Params["code"];
            string m_sFileName = context.Request.Params["name"];

            string defaultPath = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), m_sCompCode, "PDF");
            
            string file_full = String.Concat(defaultPath, m_sFileName);

            if (System.IO.File.Exists(file_full))
            {
                Message = "OK";
            }
            else
            {
                Message = "NG";
            }
        }
        catch (Exception ex)
        {
            Message = ("ER");
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