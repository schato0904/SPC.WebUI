using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucNavi : WebUIBasePageUserControl
    {
        protected string sProgramID = String.Empty;
        protected string sToolbars = String.Empty;
        protected string sAuthority = String.Empty;
        protected string sNavigation = String.Empty;
        protected string sPopup = String.Empty;
        protected string sFrame = String.Empty;
        string[] oParams = null;

        protected void Page_Init(object sender, EventArgs e)
        {
            oParams = Request.QueryString.Get("pParam").Split('|');

            if (!IsPostBack && !Page.IsCallback && !Page.gsUSERID.Equals("cyber") && (oParams[6].Equals("1") || oParams[6].Equals("2")))
            {
                // 사용자 접속로그
                PROC_SYUSRLOG();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] oParams = Request.QueryString.Get("pParam").Split('|');
            sProgramID = oParams[3];
            sToolbars = oParams[5];
            sAuthority = oParams[6];

            string MODULE1 = oParams[0];
            string MODULE2 = oParams[1];
            string PGMNM = oParams[4];

            string MODULE1NM = Page.GetCommonCodeText(MODULE1);
            string MODULE2NM = Page.GetCommonCodeText(MODULE2);

            if (!string.IsNullOrEmpty(MODULE1NM))
            {
                sNavigation = sNavigation + MODULE1NM;
            }

            if (!string.IsNullOrEmpty(sNavigation) && !string.IsNullOrEmpty(MODULE2NM))
            {
                sNavigation = String.Format("{0} > {1}", sNavigation, MODULE2NM);
            }
            else
            {
                sNavigation = sNavigation + MODULE2NM;
            }

            if (!string.IsNullOrEmpty(sNavigation))
            {
                sNavigation = String.Format("{0} > {1}", sNavigation, PGMNM);
            }
            else
            {
                sNavigation = sNavigation + PGMNM;
            }

            sPopup = Request.QueryString.Get("bPopup") ?? "false";
            sFrame = Request.QueryString.Get("bFrame") ?? "true";
        }

        #region 사용자 접속로그
        void PROC_SYUSRLOG()
        {
            string[] oParams = Request.QueryString.Get("pParam").Split('|');

            bool bExecute = false;

            using (CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
                Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
                Page.oParamDic.Add("F_PGMID", oParams[3]);
                Page.oParamDic.Add("F_USERID", Page.gsUSERID);
                bExecute = biz.PROC_SYUSRLOG(Page.oParamDic);
            }
        }
        #endregion

        #region txtHeaderNotice_Init
        protected void txtHeaderNotice_Init(object sender, EventArgs e)
        {
            this.txtHeaderNotice.ClientInstanceName = this.txtHeaderNotice.UniqueID;
        }
        #endregion
    }
}