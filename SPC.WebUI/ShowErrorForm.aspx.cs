using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPC.WebUI
{
    public partial class ShowErrorForm : System.Web.UI.Page
    {
        protected string m_sErrorCode = String.Empty;
        protected string m_sErrorPage = String.Empty;
        protected string m_sErrorMsg = String.Empty;
        protected string m_sErrorTab = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                m_sErrorCode = Request.QueryString.Get("ErrorCode");
                m_sErrorPage = Request.QueryString.Get("ErrorPage");
                m_sErrorMsg = Request.QueryString.Get("ErrorMsg");
                if (Request.QueryString.Get("pPARAM") != null && !String.IsNullOrEmpty(Request.QueryString.Get("pPARAM")))
                {
                    string[] pPARAM = Request.QueryString.Get("pPARAM").Split('|');
                    if (pPARAM.Length.Equals(7))
                        m_sErrorTab = pPARAM[3];
                }
            }
        }
    }
}