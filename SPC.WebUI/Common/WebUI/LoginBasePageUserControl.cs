using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace SPC.WebUI.Common
{
    public class LoginBasePageUserControl : System.Web.UI.UserControl
    {
        #region 생성자
        public LoginBasePageUserControl() : base()
        {
        }
        #endregion

        #region Cast WebUIBasePage
        public new LoginBasePage Page
        {
            get { return (LoginBasePage)base.Page; }
        }
        #endregion
    }
}
