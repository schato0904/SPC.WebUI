using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.MEAS.Biz;

using DevExpress.Web;

namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS2001 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
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
            // Request
            GetRequest();

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                ucPager.TotalItems = 0;
                ucPager.PagerDataBind();

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
        { }
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
        {
        }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        {
            string errMsg = String.Empty;
            int toYear = DateTime.Now.AddYears(5).Year;

            for (int i = 2010; i <= toYear; i++) {
                srcF_YEAR.Items.Add(new ListEditItem(i.ToString(), i));
            }

            srcF_YEAR.Value = DateTime.Now.Year;

            //     SSA1	계측기분류코드 F_EQUIPDIVCD
            GetCommonCodeList("SS01", srcF_EQUIPDIVCD);

            //     SSA4	검교정구분코드 F_FIXTYPECD
            GetCommonCodeList("SS04", srcF_FIXTYPECD);

            //     SSA5	교정분야코드 F_FIXDIVCD
            GetCommonCodeList("SS05", srcF_FIXDIVCD);

            GetTeamCodeList(srcF_TEAMCD);
            GetTermList(srcF_TERMMMONTH);

        }
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
        {
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공통, 팀, 반, 공정에 대한 코드, 분류를 구한다

        void GetCommonCodeList(string groupCD, DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            if (!String.IsNullOrEmpty(groupCD))
            {
                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_GROUPCD", groupCD);
                    oParamDic.Add("F_LANGTYPE", gsLANGTP);

                    ds = biz.GetCommonCodeList(oParamDic, out errMsg);
                }
            }
            else
                ds = null;

            comboBox.TextField = "F_COMMNM";
            comboBox.ValueField = "F_COMMCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }

        void GetTeamCodeList(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.MEAS1001_TEAM_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_TEAMNM";
            comboBox.ValueField = "F_TEAMCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }

        void GetTermList(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.MEAS1001_TERM_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_TERMNM";
            comboBox.ValueField = "F_TERMCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }
        #endregion

        #region 검색조건 생성

        void CreateParameters()
        {
            oParamDic.Clear();
            oParamDic.Add("F_YEAR", (this.srcF_YEAR.Value ?? "").ToString());
            oParamDic.Add("F_EQUIPDIVCD", (srcF_EQUIPDIVCD.Value ?? "").ToString());            
            oParamDic.Add("F_FIXTYPECD", (srcF_FIXTYPECD.Value ?? "").ToString());
            oParamDic.Add("F_FIXDIVCD", (srcF_FIXDIVCD.Value ?? "").ToString());
            oParamDic.Add("F_TEAMCD", (srcF_TEAMCD.Value ?? "").ToString());
            oParamDic.Add("F_TERMMONTH", (srcF_TERMMMONTH.Value ?? "").ToString());
        }

        #endregion

        #region 검사기준이력조회 총 갯수
        Int32 MEAS2001_CNT()
        {
            using (MEASBiz biz = new MEASBiz())
            {
                CreateParameters();
                totalCnt = biz.MEAS2001_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region 그리드 목록 조회(품목)
        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void MEAS2001_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                CreateParameters();

                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.MEAS2001_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bCallback)
                {
                    // Pager Setting
                    ucPager.TotalItems = 0;
                    ucPager.PagerDataBind();
                }
                else
                {
                    devGrid.JSProperties["cpResultCode"] = "pager";
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, MEAS2001_CNT());
                }
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// 계측기보유현황 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                if (e.Parameters.Equals("clear"))
                {
                    this.devGrid.DataSourceID = null;
                    this.devGrid.DataSource = null;
                    this.devGrid.DataBind();
                    devGrid.JSProperties["cpResultCode"] = "pager";
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", 50, 1, 0);
                    return;
                }

                string[] oParams = new string[2];
                foreach (string oParam in e.Parameters.Split(';'))
                {
                    oParams = oParam.Split('=');
                    if (oParams[0].Equals("PAGESIZE"))
                        nPageSize = Convert.ToInt32(oParams[1]);
                    else if (oParams[0].Equals("CURRPAGE"))
                        nCurrPage = Convert.ToInt32(oParams[1]);
                }
            }
            else
            {
                nCurrPage = 1;
                nPageSize = ucPager.GetPageSize();
            }

            MEAS2001_LST(nPageSize, nCurrPage, true);
            devGrid.DataBind();
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            MEAS2001_LST(MEAS2001_CNT(), 1, false);
            devGridExporter.WriteXlsToResponse(String.Format("년검교정계획_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<BR/>", " ");
            }
        }
        #endregion

        protected void devGrid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }
            string strJudge = e.GetValue("F_COLOR").ToString();
            if (strJudge == "RED")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else if (strJudge == "BLUE")
            {
                e.Row.ForeColor = System.Drawing.Color.Blue;
            }
        }

        #endregion
    }
}