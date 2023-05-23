using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Web.Security;
using DevExpress.Web;

namespace SPC.WebUI.Resources.controls.login
{
    public partial class login : SPC.WebUI.Common.LoginBasePageUserControl
    {
        #region Event

        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtUserID.Text = Request.Cookies["WebID"] == null ? "" : Request.Cookies["WebID"].Value;
                this.chkSaveUserID.Checked = Request.Cookies["WebID"] == null ? false : true;
                
                WebForm_Init();
            }
        }
        #endregion

        #region WebForm Init
        void WebForm_Init()
        {
            // 기본언어셋 선택
            this.ddlLangClsCd.SelectedValue = Page.InitLangClsCd;

            // Set Default Button
            this.Page.Form.DefaultButton = btnSubmit.UniqueID;
        }
        #endregion

        #endregion
    }
}