using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTF.Web.Framework.Helper;
using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.WERD.Biz;



namespace SPC.WebUI.Pages.WERDBS
{
    public partial class WERDBS1001 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        protected string oSetParam = String.Empty;
        private DBHelper spcDB;
        static int CurrPage = 0;
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
                GetWork_LST();
                GetItem_LST();
                GetUser_LST();
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

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
        {

            try
            {
                oSetParam = Request.QueryString.Get("oSetParam") ?? "";
            }
            catch (Exception)
            {
                oSetParam = "";
            }
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

        #endregion

        #region 사용자 정의 함수

        #region Data총갯수
        Int32 WERDBS1001_CNT()
        {
            oParamDic.Clear();
            oParamDic.Add("F_FROMDT", GetFromDt());
            oParamDic.Add("F_TODT", GetToDt());
            oParamDic.Add("F_WORKCD", (workCombo.Value ?? "").ToString());//workCombo.Value.ToString()
            oParamDic.Add("F_FIRSTITEM", (ddlFIRSTITEM.Value ?? "").ToString());//ddlFIRSTITEM.Value.ToString()
            oParamDic.Add("F_ITEMCD", (itemCombo.Value ?? "").ToString());//itemCombo.Value.ToString()
            oParamDic.Add("F_USERCD", (userCombo.Value ?? "").ToString()); //userCombo.Value.ToString()
            oParamDic.Add("F_GUBUN", gubunCombo.Value.ToString()); //
            totalCnt = WERDBS1001_CNT(oParamDic);

            return totalCnt;

        }

        public Int32 WERDBS1001_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;



            resultCnt = WERDBS1001_CNT2(oParams);


            return resultCnt;
        }
        public Int32 WERDBS1001_CNT2(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERDBS1001_CNT"));
            }

            return resultCnt;
        }

        #endregion

        #region Data조회
        void WERDBS1001_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;


            oParamDic.Clear();
            oParamDic.Add("F_FROMDT", GetFromDt());
            oParamDic.Add("F_TODT", GetToDt());
            oParamDic.Add("F_WORKCD", (workCombo.Value ?? "").ToString());//workCombo.Value.ToString()
            oParamDic.Add("F_FIRSTITEM", (ddlFIRSTITEM.Value ?? "").ToString());//ddlFIRSTITEM.Value.ToString()
            oParamDic.Add("F_ITEMCD", (itemCombo.Value ?? "").ToString());//itemCombo.Value.ToString()
            oParamDic.Add("F_USERCD", (userCombo.Value ?? "").ToString()); //userCombo.Value.ToString()
            oParamDic.Add("F_GUBUN", gubunCombo.Value.ToString()); //
            oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
            oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
            ds = WERDBS1001_LST(oParamDic, out errMsg);

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
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, WERDBS1001_CNT());
                }
            }



        }


        public DataSet WERDBS1001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = WERDBS1001_LST2(oParams, out errMsg);


            return ds;
        }

        public DataSet WERDBS1001_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERDBS1001_LST", out errMsg);
            }

            return ds;
        }

        #endregion

  

        #endregion

        #region 사용자이벤트

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName.Equals("F_GUBUN"))
            {
                if (e.CellValue != null)
                {
                    switch (e.CellValue.ToString())
                    {
                        case "주간": e.Cell.ForeColor = System.Drawing.Color.Black; break;
                        case "야간": e.Cell.ForeColor = System.Drawing.Color.Blue; break;

                    }
                }
            }

            if (e.DataColumn.FieldName.Equals("F_FIRSTITEM"))
            {
                if (e.CellValue != null)
                {
                    switch (e.CellValue.ToString())
                    {
                        case "초품": e.Cell.ForeColor = System.Drawing.Color.Blue; break;
                        case "중품": e.Cell.ForeColor = System.Drawing.Color.Black; break;
                        case "종품": e.Cell.ForeColor = System.Drawing.Color.Red; break;

                    }
                }
            }
        }
        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
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

            CurrPage = nCurrPage;
            WERDBS1001_LST(nPageSize, CurrPage, true);
            devGrid.DataBind();

        }
        #endregion

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(DevExpress.Web.ASPxComboBox ddlComboBox, string CommonCode)
        {
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();
            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            string strJudge = e.GetValue("F_NGOKCHK").ToString();

            if (strJudge == "1")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else if (strJudge == "2")
            {
                e.Row.ForeColor = System.Drawing.Color.Blue;
            }
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
            }

        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;




            nCurrPage = 1;
            nPageSize = ucPager.GetPageSize();


            CurrPage = nCurrPage;

            WERDBS1001_LST(nPageSize, nCurrPage, true);

            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 생산관리조회", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });

            //WERDBS1001_EXCEL();
            //devGrid.DataBind();
            //devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 생산관리조회", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion

        #region Combo

        #region itemCombo_Callback()

        protected void itemCombo_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            GetItem_LST();

        }

        void GetItem_LST()
        {
            string errMsg = String.Empty;





            oParamDic.Clear();
            ds = GetItemBS_LST(oParamDic, out errMsg);


            itemCombo.DataSource = ds;

            itemCombo.DataBind();
            itemCombo.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });

        }

        public DataSet GetItemBS_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = GetItemBS_LST2(oParams, out errMsg);


            return ds;
        }

        public DataSet GetItemBS_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_GETBSITEM_LST", out errMsg);
            }

            return ds;
        }

      


        #endregion

        #region userCombo_Callback()

        protected void userCombo_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            GetUser_LST();
        }

        void GetUser_LST()
        {
            string errMsg = String.Empty;


            oParamDic.Clear();

            ds = GetUserBS_LST(oParamDic, out errMsg);

            userCombo.DataSource = ds;
            userCombo.DataBind();
            userCombo.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
        }

        public DataSet GetUserBS_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = GetUserBS_LST2(oParams, out errMsg);


            return ds;
        }

        public DataSet GetUserBS_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_GETBSUSER_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region workCombo

        protected void workCombo_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            GetWork_LST();
        }

        void GetWork_LST()
        {
            string errMsg = String.Empty;


            oParamDic.Clear();

            ds = GetWorkBS_LST(oParamDic, out errMsg);

            workCombo.DataSource = ds;
            workCombo.DataBind();
            workCombo.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
        }

        public DataSet GetWorkBS_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = GetWorkBS_LST2(oParams, out errMsg);


            return ds;
        }

        public DataSet GetWorkBS_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_GETBSWORK_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #endregion


    }
}