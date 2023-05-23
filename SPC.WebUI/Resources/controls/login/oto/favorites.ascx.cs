using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Resources.controls.login.oto
{
    public partial class favorites : SPC.WebUI.Common.WebUIBasePageUserControl
    {
        protected string m_sByPassURL = String.Empty;
        protected string m_sUserEncryptInfo = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                m_sUserEncryptInfo = Page.UF.Encrypts.RSAEncryptString(Page.gsUSERID);
                m_sByPassURL = !String.IsNullOrEmpty(Page.gsBYPASSURL) ? String.Format("javascript:fn_OnPopupByPass('http://{0}API/Common/ByPass/bypass.ashx', '{1}');", Page.gsBYPASSURL, m_sUserEncryptInfo) : "javascript:void(0);";
            }
        }
    }
}