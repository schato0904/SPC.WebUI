using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.WERD.Biz;

namespace SPC.WebUI.Pages.WERDDACO
{
    public partial class WERD_DACO1001 : WebUIBasePage
    {

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        protected string oSetParam = String.Empty;
        string sLINECD = String.Empty;
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        #endregion

        #region 프로퍼티
        DataSet dsGrid
        {
            get
            {
                return (DataSet)Session["WERD_DACO1001"];
            }
            set
            {
                Session["WERD_DACO1001"] = value;
            }
        }
        #endregion

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

            if (!Page.IsCallback)
            {
                // 반목록을 구한다
                GetQWK110_LST();
            }

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
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                ucPager.TotalItems = 0;
                ucPager.PagerDataBind();
            }

            WERD_DACO1001_LST(1, 1, true);
        }
        #endregion

        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        {
            oSetParam = Request.QueryString.Get("oSetParam") ?? "";
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

        #region 함수

        #region Data총갯수
        Int32 WERD_DACO1001_CNT()
        {
            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", "");
                oParamDic.Add("F_FACTCD", "");
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_LINECD", GetLineCD());
                //oParamDic.Add("F_WORKCD", GetWorkCD());
                oParamDic.Add("F_COMPANYCD", (this.hidCOMP.Value ?? "").ToString());
                oParamDic.Add("F_TYPE", (this.TYPE.Value ?? "").ToString());
                totalCnt = biz.WERD_DACO1001_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region WERD_DACO1001_LST
        void WERD_DACO1001_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", "");
                oParamDic.Add("F_FACTCD", "");
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_LINECD", GetLineCD());
                //oParamDic.Add("F_WORKCD", GetWorkCD());
                oParamDic.Add("F_COMPANYCD", (this.hidCOMP.Value ?? "").ToString());
                oParamDic.Add("F_TYPE", (this.TYPE.Value ?? "").ToString());
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                ds = biz.WERD_DACO1001_LST(oParamDic, out errMsg);
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
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, WERD_DACO1001_CNT());
                }
            }
        }
        #endregion

        #region 라인목록을 구한다
        void GetQWK110_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();

                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());


                ds = biz.GetQWK110_LST(oParamDic, out errMsg);
            }

            ddlCOMP.DataSource = ds;
            ddlCOMP.DataBind();
        }
        #endregion

        #endregion

        #region 이벤트

        #region ddlLINE_DataBound
        protected void ddlLINE_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            ddlCOMP.Items.Insert(0, item);

            ddlCOMP.SelectedIndex = ddlCOMP.Items.FindByValue(sLINECD).Index;
        }
        #endregion

        #region ddlLINE_Callback
        protected void ddlLINE_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            GetQWK110_LST();
        }
        #endregion

        #region COMP_Callback
        protected void COMP_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //QWK110_LST();
        }
        #endregion

        #region devGridExporter_RenderBrick
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            if (devGridDataColumn != null && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
            }
        }
        #endregion

        #region btnExport_Click
        protected void btnExport_Click(object sender, EventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;
            WERD_DACO1001_LST(nPageSize, nCurrPage, true);

            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} DATA조회", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devGrid_BatchUpdate
        protected void devGrid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;
            int erroridx = 0;
            string reInsert = null;

            int idx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            #region Batch Insert
            if (e.InsertValues.Count > 0)
            {
            }
            #endregion

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();

                    oParamDic.Add("F_WORKDATE", (Value.NewValues["F_WORKDATE"] ?? "").ToString());
                    oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_LINECD", (Value.NewValues["F_LINECD"] ?? "").ToString());
                    oParamDic.Add("F_COMPANYCD", (Value.NewValues["F_COMPANYCD"] ?? "").ToString());
                    oParamDic.Add("F_ITEMCD", (Value.NewValues["F_ITEMCD"] ?? "").ToString());
                    oParamDic.Add("F_ERRORCD", (Value.NewValues["F_ERRORCD"] ?? "").ToString());
                    oParamDic.Add("F_NGCOUNT", (Value.NewValues["F_NGCOUNT"] ?? "").ToString());
                    oParamDic.Add("F_NGTIME", (Value.NewValues["F_NGTIME"] ?? "").ToString());
                    oParamDic.Add("F_INSUSER", (Value.NewValues["F_INSUSER"] ?? "").ToString());
                    oParamDic.Add("F_INSDT", (Value.NewValues["F_INSDT"] ?? "").ToString());

                    oSPs[idx] = "USP_WERD_DACO1001_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            if (e.DeleteValues.Count > 0)
            {
            }
            #endregion
            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                bExecute = biz.PROC_QCD103_BATCH(oSPs, oParameters, out resultMsg);
            }
            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 부적합 유형 목록을 구한다
                WERD_DACO1001_LST(nPageSize, nCurrPage, true);
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];

            #endregion

            e.Handled = true;
        }
        #endregion

        #region devGrid_InitNewRow
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }
        #endregion

        #region devGrid_CustomCallback
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

            WERD_DACO1001_LST(nPageSize, nCurrPage, true);
            devGrid.DataBind();
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {

        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {

        }
        #endregion

        #endregion

    }
}