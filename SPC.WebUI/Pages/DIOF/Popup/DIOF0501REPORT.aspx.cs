using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.WebUI.Pages.DIOF.Report;
using SPC.FDCK.Biz;

namespace SPC.WebUI.Pages.DIOF.Popup
{
    public partial class DIOF0501REPORT : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string m_sMonth = String.Empty;
        string m_sMachIDX = String.Empty;
        string m_sMachNM = String.Empty;
        DataTable dtCheckSheet = null;
        DataTable dtResponse = null;
        DataTable dtConfirm = null;
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

            // 설비명
            GetMachInfomathion();

            // 설비점검시트조회
            RetrieveCheckSheetList();

            // 설비이상조치조회
            RetrieveResponseList();

            // 일별체크정보조회
            RetrieveConfirmList();

            DIOF0501RPT report = new DIOF0501RPT(m_sMonth, m_sMachIDX, m_sMachNM, dtCheckSheet, dtResponse, dtConfirm);
            devDocument.Report = report;

            // Grid Columns Sum Width
            // hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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

                // Grid Callback Init
                // devGrid.JSProperties["cpResultCode"] = "";
                // devGrid.JSProperties["cpResultMsg"] = "";
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
            m_sMonth = Request.QueryString.Get("MONTH");
            m_sMachIDX = Request.QueryString.Get("IDX");
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

        #region 설비명
        void GetMachInfomathion()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", this.m_sMachIDX);
                ds = biz.QCD_MACH21_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
                Server.Transfer("~/Pages/ERROR/Report.aspx");

            if (!bExistsDataSet(ds))
                Server.Transfer("~/Pages/ERROR/Report.aspx");
            else
                m_sMachNM = ds.Tables[0].Rows[0]["F_MACHNM"].ToString();
        }
        #endregion

        #region 설비점검시트조회
        void RetrieveCheckSheetList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MONTH", this.m_sMonth);
                oParamDic.Add("F_MACHIDX", this.m_sMachIDX);
                ds = biz.QWK_MACH23_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
                Server.Transfer("~/Pages/ERROR/Report.aspx");

            if (!bExistsDataSet(ds))
                Server.Transfer("~/Pages/ERROR/Report.aspx");
            else
                dtCheckSheet = ds.Tables[0];
        }
        #endregion

        #region 설비이상조치조회
        void RetrieveResponseList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_TYPE", "M");
                oParamDic.Add("F_DATE", this.m_sMonth);
                oParamDic.Add("F_MACHIDX", this.m_sMachIDX);
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QWK_MACH24_LST2(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
                Server.Transfer("~/Pages/ERROR/Report.aspx");

            if (!bExistsDataSetWhitoutCount(ds))
                Server.Transfer("~/Pages/ERROR/Report.aspx");
            else
                dtResponse = ds.Tables[0];
        }
        #endregion

        #region 일별체크정보조회
        void RetrieveConfirmList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MONTH", this.m_sMonth);
                oParamDic.Add("F_MACHIDX", this.m_sMachIDX);
                ds = biz.QWK_MACH22_CFM_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
                Server.Transfer("~/Pages/ERROR/Report.aspx");

            if (!bExistsDataSetWhitoutCount(ds))
                Server.Transfer("~/Pages/ERROR/Report.aspx");
            else
                dtConfirm = ds.Tables[0];
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #endregion
    }
}