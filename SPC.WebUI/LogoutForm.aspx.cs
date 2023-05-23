using System;
using System.Web.Security;
using SPC.WebUI.Common;

namespace SPC.WebUI
{
    public partial class LogoutForm : WebUIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExecuteLogOut();
            }
        }

        #region 로그아웃
        void ExecuteLogOut()
        {
            FormsAuthentication.SignOut();
            Logout_Cookie();
        }
        #endregion
    }
}