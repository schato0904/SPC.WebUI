using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.WebUI.Common.Library;
using SPC.INSP.Biz;

namespace SPC.WebUI.Pages.INSP.Popup
{
    public partial class INSP0101REPORT_foseco : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        Control reportControl = null;
        string[] procResult = { "2", "Unknown Error" };
        string strParams = String.Empty;

        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        #endregion

        #region 프로퍼티

        #endregion

        #endregion

        #region 이벤트

        #region Page Init
        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            // Request
            GetRequest();

            // 성적서정보
            INS_ALL_GET();
        }
        #endregion

        #region Page Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();
            }
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        {
            strParams = Request.QueryString.Get("pPARAMS");
        }
        #endregion

        #region Web Init
        /// <summary>
        /// Web_Init
        /// </summary>
        void Web_Init()
        {
            // DefaultButton 세팅
            SetDefaultButton();

            // 객체 초기화
            SetDefaultObject();

            // 클라이언트 스크립트
            SetClientScripts();

            // 서버 컨트럴 객체에 기초값 세팅
            SetDefaultValue();
        }
        #endregion

        #region DefaultButton 세팅
        /// <summary>
        /// SetDefaultButton
        /// </summary>
        void SetDefaultButton()
        { }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        { }
        #endregion

        #region 클라이언트 스크립트
        /// <summary>
        /// SetClientScripts
        /// </summary>
        void SetClientScripts()
        { }
        #endregion

        #region 서버 컨트럴 객체에 기초값 세팅
        /// <summary>
        /// SetDefaultValue
        /// </summary>
        void SetDefaultValue()
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 성적서정보
        void INS_ALL_GET()
        {
            string errMsg = String.Empty;
            //F_COMPCD;F_FACTCD;F_WORKDATE;F_WORKTIME;F_ITEMCD;F_WORKCD;F_TSERIALNO;F_LOTNO;F_REPORT
            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", strParams.Split('|')[0]);
                oParamDic.Add("F_FACTCD", strParams.Split('|')[1]);
                oParamDic.Add("F_WORKDATE", strParams.Split('|')[2]);
                oParamDic.Add("F_ITEMCD", strParams.Split('|')[4]);
                oParamDic.Add("F_WORKCD", strParams.Split('|')[5]);
                oParamDic.Add("F_TSERIALNO", strParams.Split('|')[6]);
                oParamDic.Add("F_LOTNO", strParams.Split('|')[7]);
                oParamDic.Add("F_ISSUER", strParams.Split('|')[10]);
                oParamDic.Add("F_REPORTDATE", strParams.Split('|')[11]);

                if (strParams.Split('|')[8] == "xtraReport1")
                    ds = biz.QWK03A_INSP0101_FOSECO_REPORT_LST(oParamDic, out errMsg);
                else
                    ds = biz.QWK03A_INSP0101_FOSECO_REPORT_LST2(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('성적서데이터를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else
            {
                if (!bExistsDataSetWhitoutCount(ds) || ds.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('성적서데이터를 구하는 중 장애가 발생하였습니다.');", true);
                }
                else
                {
                    dtHead = ds.Tables[0];
                    dtGroup = ds.Tables[0];
                    dtData = ds.Tables[0];

                    // Load Report UserControl
                    LoadReportUserControl();
                }
            }
        }
        #endregion

        #region Load Report UserControl
        void LoadReportUserControl()
        {
            if (strParams.Split('|')[5].Equals("cybertechfriend"))
            {
                reportControl = Page.LoadControl("~/Resources/report/form/report.ascx", dtHead, dtGroup, dtData);
            }
            else
            {
                string userReportAssem = String.Format("Resources.report.form.{0}.{1}", gsLOGINPGMID, strParams.Split('|')[8]);
                string userControlPath = String.Format("~/Resources/report/form/{0}/report.ascx", gsLOGINPGMID);
                //임시로.....
                //string userReportAssem = String.Format("Resources.report.form.{0}.{1}", "hkpi", strParams.Split('|')[5]);
                //string userControlPath = String.Format("~/Resources/report/form/{0}/report.ascx", "hkpi");

                if (Type.GetType(userReportAssem) != null || !System.IO.File.Exists(Server.MapPath(userControlPath)))
                    reportControl = Page.LoadControl("~/Resources/report/form/report.ascx", dtHead, dtGroup, dtData);
                else
                {
                    reportControl = Page.LoadControl(userControlPath, dtHead, dtGroup, dtData, gsLOGINPGMID, strParams.Split('|')[8]);
                    //임시로.....
                    //reportControl = Page.LoadControl(userControlPath, dtHead, dtGroup, dtData, "hkpi", strParams.Split('|')[5]);
                }
            }

            reportControl.ID = "reportForm";

            pHolder.Controls.Add(reportControl);
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #endregion
    }
}