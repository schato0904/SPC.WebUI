using System;
using System.Collections.Generic;
using SPC.WebUI.Common;
using System.Web.UI.WebControls;

namespace SPC.WebUI.Resources.controls.login.YOUIN
{
    public partial class login : LoginBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 기본생성자
        public login() { }
        #endregion

        #region 부모컨트럴의 값을 전달받기 위한 생성자
        public login(ctfLoginInfo _LoginInfo)
        {
            LoginInfo = _LoginInfo;
        }
        #endregion

        #region 변수
        protected ctfLoginInfo LoginInfo = new ctfLoginInfo();
        #endregion

        #endregion

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
            //if (LoginInfo.F_USEMULTILANG)
            //{
            //    // 한국어
            //    if (LoginInfo.F_USELANGKR)
            //    {
            //        this.ddlLangClsCd.Items.Add(new ListItem("한국어", "ko-KR"));
            //    }

            //    // 중국어
            //    if (LoginInfo.F_USELANGKR)
            //    {
            //        this.ddlLangClsCd.Items.Add(new ListItem("中國語", "zh-CN"));
            //    }

            //    // 영국어
            //    if (LoginInfo.F_USELANGKR)
            //    {
            //        this.ddlLangClsCd.Items.Add(new ListItem("English", "en-US"));
            //    }

            //    // 기본언어셋 선택
            //    this.ddlLangClsCd.SelectedValue = Page.InitLangClsCd;
            //}

            // Set Default Button
            this.Page.Form.DefaultButton = btnSubmit.UniqueID;
        }
        #endregion

        #endregion
    }
}