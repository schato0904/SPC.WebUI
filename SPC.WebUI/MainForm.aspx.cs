using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI
{
    public partial class MainForm : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        protected string m_sSiteTitle = String.Empty;
        #endregion

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WebForm_Init();
            }
        }

        #region 사용자 정의 메소드

        #region WebForm Init
        void WebForm_Init()
        {
            m_sSiteTitle = String.Format("{0} :: {1}", gsCOMPNM, GetMessage("PROGRAM_TITLE"));
        }
        #endregion

        #endregion
    }
}