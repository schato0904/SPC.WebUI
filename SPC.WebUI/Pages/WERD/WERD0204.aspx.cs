using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using DevExpress.Web;
using SPC.WERD.Biz;

namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD0204 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        private List<string> lstCOMMCD = new List<string>();
        private int totalPrdCnt = 0;
        private int totalPpm = 0;
        private int totalInspCnt = 0;
        private int totalNgCnt = 0;
        private double totalRate = 0;
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
            new ASPxGridViewCellMerger(devGrid, "F_MODELCD|F_MODELNM|F_WORKCD|F_WORKNM");
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

        #region 검사기준이력조회 총 갯수
        Int32 WERD0204IN01M_CNT()
        {
            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt());
                oParamDic.Add("F_TODT", this.ucDate.GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                totalCnt = biz.WERD0204_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region 그리드 목록 조회(품목)
        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void WERD0204LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt());
                oParamDic.Add("F_TODT", this.ucDate.GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                ds = biz.WERD0204_LST(oParamDic, out errMsg);
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
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, WERD0204IN01M_CNT());
                }
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>

        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //DevExpressLib.GetUsedString(new string[] { "F_USEYN" }, e);
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// 저장 처리
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            var result = new Dictionary<string, string>();
            var btnType = e.Parameter;
        }
        #endregion

        #region devGrid1_CustomCallback
        /// <summary>
        /// 거래처별 품목 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region devGrid_CustomCallback
        /// <summary>
        /// 품목 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
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

            WERD0204LST(nPageSize, nCurrPage, true);
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
            int nCurrPage = 1;
            int nPageSize = 100000;

            WERD0204LST(nPageSize, nCurrPage, true);
            devGridExporter.WriteXlsToResponse(String.Format("모델별검사현황_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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

        protected void devGrid_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxSummaryItem item = e.Item as ASPxSummaryItem;
            int currRow = e.RowHandle;

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            {

                lstCOMMCD = new List<string>();
                totalPrdCnt = 0;
                totalInspCnt = 0;
                totalNgCnt = 0;
                totalPpm = 0;

            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {

                string commCd = e.FieldValue.ToString();//grid.GetRowValues(currRow, "F_COMMCD").ToString();
                lstCOMMCD.Add(commCd);

                totalPrdCnt += Convert.ToInt32(grid.GetRowValues(currRow, "F_PRODUCTQTY"));
                totalInspCnt += Convert.ToInt32(grid.GetRowValues(currRow, "F_INSPQTY"));
                totalNgCnt += Convert.ToInt32(grid.GetRowValues(currRow, "F_NGQTY"));
                totalPpm += Convert.ToInt32(grid.GetRowValues(currRow, "F_PPM"));





            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                if (lstCOMMCD.Count == 0) return;


                switch (item.FieldName)
                {
                    case "F_PRODUCTQTY":
                        e.TotalValue = totalPrdCnt;
                        break;
                    case "F_INSPQTY":
                        e.TotalValue = totalInspCnt;
                        break;
                    case "F_NGQTY":
                        e.TotalValue = totalNgCnt;
                        break;
                    case "F_PPM":
                        e.TotalValue = Math.Round(totalNgCnt * 1000000.0 / totalPrdCnt, 0);
                        break;
                    case "F_NGRATE":
                        totalRate = totalNgCnt * 100.0 / totalPrdCnt;
                        if (!double.IsNaN(totalRate))
                            e.TotalValue = Math.Round(totalRate, 2);
                        break;
                }






            }
        }


        #endregion


    }
        #endregion
}