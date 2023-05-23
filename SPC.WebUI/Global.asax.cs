using System;
using System.Collections.Generic;
using System.Web.Hosting;

using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // DevExpress CallbackError Event Handler
            DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler(Application_Error);

            // Virtual Path Create For MasterPage
            MasterPageVirtualPathProvider vpp = new MasterPageVirtualPathProvider();
            HostingEnvironment.RegisterVirtualPathProvider(vpp);

            // 전역캐쉬생성
            CommonCode commonCode = new CommonCode();
            commonCode.LoadCommonCode();
            CacheHelper.Add(commonCode, "SPCCommonCode");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Use HttpContext.Current to get a Web request processing helper 
            System.Web.HttpServerUtility server = System.Web.HttpContext.Current.Server;
            Exception exception = server.GetLastError();

            LogHelper.LogWrite(exception);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}