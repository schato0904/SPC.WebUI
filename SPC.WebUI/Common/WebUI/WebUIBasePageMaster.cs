using System;

namespace SPC.WebUI.Common
{
    public class WebUIBasePageMaster : System.Web.UI.MasterPage
    {
        #region Cast WebUIBasePage
        public new WebUIBasePage Page
        {
            get { return (WebUIBasePage)base.Page; }
        }
        #endregion
	}
}