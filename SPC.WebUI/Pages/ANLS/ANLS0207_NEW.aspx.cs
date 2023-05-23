using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ANLS.Biz;
using CTF.Web.Framework.Helper;
using System.Drawing;
using SPC.WebUI.Common.Library;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0207_NEW : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        private DBHelper spcDB;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["ANLS0207_NEW"];
            }
            set
            {
                Session["ANLS0207_NEW"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["ANLS0207_1_NEW"];
            }
            set
            {
                Session["ANLS0207_1_NEW"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["ANLS0207_2_NEW"];
            }
            set
            {
                Session["ANLS0207_2_NEW"] = value;
            }
        }
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

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                

                //ucPager.TotalItems = 0;
                //ucPager.PagerDataBind();
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
        /// <summary>23
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
            dtChart1 = null;
            dtChart2 = null;
            dtChart3 = null;
            //AspxCombox_DataBind(this.ddlINSPCD, "AAC5");
            //AspxCombox_DataBind(this.ddlJUDGE, "AAE2");
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

        #region Data조회 QWK03A_ANLS0207_1_LST
        void QWK03A_ANLS0207_1_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt().Substring(0, 7) + "-01");
                oParamDic.Add("F_EDDT", GetToDt().Substring(0, 7) + "-31");
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Value.ToString());
                ds = biz.ANLS0207_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    if (ds.Tables.Count > 0)
                        devGrid.DataSource = ds.Tables[0];
                devGrid.DataBind();
            }
        }
        #endregion

        #region Data조회 QWK03A_ANLS0207_2_LST
        void QWK03A_ANLS0207_2_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt1().Substring(0, 7) + "-01");
                oParamDic.Add("F_EDDT", GetToDt1().Substring(0, 7) + "-31");
                oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
                oParamDic.Add("F_ITEMCD", GetItemCD1());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO1.Value.ToString());
                ds = biz.ANLS0207_LST(oParamDic, out errMsg);
            }
            if (ds.Tables.Count == 0) { return; }

            //devGrid1.DataSource = ds.Tables[0];
            //devGrid1.DataBind();

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    if (ds.Tables.Count > 0)
                        devGrid1.DataSource = ds.Tables[0];
                devGrid1.DataBind();
            }
        }
        #endregion

        #region Data조회 QWK03A_ANLS0207_3_LST
        void QWK03A_ANLS0207_3_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt2().Substring(0, 7) + "-01");
                oParamDic.Add("F_EDDT", GetToDt2().Substring(0, 7) + "-31");
                oParamDic.Add("F_WORKCD", GetWorkPOPCD2());
                oParamDic.Add("F_ITEMCD", GetItemCD2());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO2.Value.ToString());
                ds = biz.ANLS0207_LST(oParamDic, out errMsg);
            }

            if (ds.Tables.Count == 0) { return; }
                        
            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (ds.Tables.Count > 0)
                    devGrid2.DataSource = ds.Tables[0];
                devGrid2.DataBind();
            }
        }
        #endregion

        #region 차트조회 QWK03A_ANLS0207_1_CHART_LST
        void QWK03A_ANLS0207_1_CHART_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt().Substring(0, 7) + "-01");
                oParamDic.Add("F_EDDT", GetToDt().Substring(0, 7) + "-31");
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Value.ToString());
                oParamDic.Add("F_SIRYO", "");
                oParamDic.Add("F_GBN", "0");

                ds = biz.ANLS0101_3(oParamDic, out errMsg);
            }

            //dtChart1 = ds.Tables[0].Copy();

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (ds.Tables.Count > 0)
                    dtChart1 = ds.Tables[0].Copy();
            }
        }
        #endregion

        #region 차트조회 QWK03A_ANLS0207_2_CHART_LST
        void QWK03A_ANLS0207_2_CHART_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt1().Substring(0, 7) + "-01");
                oParamDic.Add("F_EDDT", GetToDt1().Substring(0, 7) + "-31");
                oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
                oParamDic.Add("F_ITEMCD", GetItemCD1());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO1.Value.ToString());
                oParamDic.Add("F_SIRYO", "");
                oParamDic.Add("F_GBN", "0");

                ds = biz.ANLS0101_3(oParamDic, out errMsg);
            }

            //dtChart2 = ds.Tables[0].Copy();

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (ds.Tables.Count > 0)
                    dtChart2 = ds.Tables[0].Copy();
            }
        }
        #endregion

        #region 차트조회 QWK03A_ANLS0207_3_CHART_LST
        void QWK03A_ANLS0207_3_CHART_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt2().Substring(0, 7) + "-01");
                oParamDic.Add("F_EDDT", GetToDt2().Substring(0, 7) + "-31");
                oParamDic.Add("F_WORKCD", GetWorkPOPCD2());
                oParamDic.Add("F_ITEMCD", GetItemCD2());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO2.Value.ToString());
                oParamDic.Add("F_SIRYO", "");
                oParamDic.Add("F_GBN", "0");

                ds = biz.ANLS0101_3(oParamDic, out errMsg);
            }

            //dtChart3 = ds.Tables[0].Copy();

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (ds.Tables.Count > 0)
                    dtChart3 = ds.Tables[0].Copy();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devChart1_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart1 != null)
            {
                
            }

            DrawHistogram();
            
        }
        #endregion

        #region devChart2_CustomCallback
        /// <summary>
        /// devChart2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart2 != null)
            {


                

            }
            DrawHistogram2();

            
        }
        #endregion

        #region devChart3_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart3_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart3 != null)
            {
                //devChart3.Series.Clear();

                //DevExpressLib.SetChartBarLineSeries(devChart3, "시료수", "F_GBNNM", "F_SIRYO", System.Drawing.Color.LightBlue);
                //DevExpressLib.SetChartBarLineSeries(devChart3, "", "F_GBNNM", "F_VALUE", System.Drawing.Color.LightPink, 0.2);

                //devChart3.DataSource = dtChart3;
                //devChart3.DataBind();

                //DevExpressLib.SetCrosshairOptions(devChart3);
                //DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");

            }
            DrawHistogram3();
        }
        #endregion

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="Width">Int32</param>
        void devChart_ResizeTo(object sender, Int32 Width, Int32 Height)
        {
            if (Width >= 0 && Height >= 0)
            {
                DevExpress.XtraCharts.Web.WebChartControl chart = sender as DevExpress.XtraCharts.Web.WebChartControl;
                chart.Width = new Unit(Width);
                chart.Height = new Unit(Height);

                chart.JSProperties["cpFunction"] = "resizeTo";
                chart.JSProperties["cpChartWidth"] = Width.ToString();
            }
        }
        #endregion

        protected void txtMEAINSPCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupMeainspSearch()");
        }

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //e.NewValues["F_STATUS"] = true;
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.ClientSideEvents.Init = "fn_OnControlDisableBox";
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
        }
        #endregion

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_GB", "F_CP", "F_CPK", "F_RANGE" };

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
                }
            }
        }
        #endregion

        #region devGrid1 HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_YMNM", "F_CP", "F_CPK", "F_RANGE" };

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
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
                //nCurrPage = 1;
                //nPageSize = ucPager.GetPageSize();
            }
            QWK03A_ANLS0207_1_LST(nPageSize, nCurrPage, true);            
            //QWK03A_ANLS0207_1_CHART_LST(nPageSize, nCurrPage, true);
            devGrid.DataBind();
        }

        #endregion

        #region devGrid1 CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
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
                //nCurrPage = 1;
                //nPageSize = ucPager.GetPageSize();
            }
            QWK03A_ANLS0207_2_LST(nPageSize, nCurrPage, true);
            //QWK03A_ANLS0207_2_CHART_LST(nPageSize, nCurrPage, true);
            //DrawHistogram2();
            devGrid1.DataBind();
        }

        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
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
                //nCurrPage = 1;
                //nPageSize = ucPager.GetPageSize();
            }
            QWK03A_ANLS0207_3_LST(nPageSize, nCurrPage, true);
            QWK03A_ANLS0207_3_CHART_LST(nPageSize, nCurrPage, true);
            devGrid2.DataBind();
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

            //string strJudge = e.GetValue("F_NGOKCHK").ToString();

            //if (strJudge == "1")
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Red;
            //}
            //else if (strJudge == "2")
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Blue;
            //}
        }
        #endregion

        #region DrawHistogram
        void DrawHistogram()
        {
            bool bExecute1 = false;
            bool bExecute2 = false;
            StringBuilder sb = null;
            DataTable dt3 = new DataTable();
            string errMsg = String.Empty;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());

            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            //oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", "0");

            //DataController dc = new DataController();

            SPCChart sc = new SPCChart();
            DataSet ds = GetDataHistogram(oParamDic);
            var c = sc.GetHistogram(this.devChart1, ds);


            if (c == null)
            {
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 데이터가 없습니다.";
            }
            else
            {
                this.devChart1 = c;
                

            }


        }
        #endregion

        #region DrawHistogram
        void DrawHistogram2()
        {
            bool bExecute1 = false;
            bool bExecute2 = false;
            StringBuilder sb = null;
            DataTable dt3 = new DataTable();
            string errMsg = String.Empty;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", GetCompCD());
            oParamDic.Add("F_FACTCD", GetFactCD());
            oParamDic.Add("F_STDT", GetFromDt1().Substring(0, 7) + "-01");
            oParamDic.Add("F_EDDT", GetToDt1().Substring(0, 7) + "-31");

            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", (this.txtSERIALNO1.Value ?? "").ToString());
            //oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", "0");

            //DataController dc = new DataController();

            SPCChart sc = new SPCChart();
            DataSet ds = GetDataHistogram(oParamDic);
            var c = sc.GetHistogram(this.devChart2, ds);


            if (c == null)
            {
                devChart2.JSProperties["cpResultCode"] = "0";
                devChart2.JSProperties["cpResultMsg"] = "조회 조건에 맞는 데이터가 없습니다.";
            }
            else
            {
                this.devChart2 = c;

            }


        }
        #endregion

        #region DrawHistogram
        void DrawHistogram3()
        {
            bool bExecute1 = false;
            bool bExecute2 = false;
            StringBuilder sb = null;
            DataTable dt3 = new DataTable();
            string errMsg = String.Empty;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", GetCompCD());
            oParamDic.Add("F_FACTCD", GetFactCD());
            oParamDic.Add("F_STDT", GetFromDt2().Substring(0, 7) + "-01");
            oParamDic.Add("F_EDDT", GetToDt2().Substring(0, 7) + "-31");

            oParamDic.Add("F_ITEMCD", GetItemCD2());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD2());
            oParamDic.Add("F_SERIALNO", (this.txtSERIALNO2.Value ?? "").ToString());
            //oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", "0");

            //DataController dc = new DataController();

            SPCChart sc = new SPCChart();
            DataSet ds = GetDataHistogram(oParamDic);
            var c = sc.GetHistogram(this.devChart3, ds);


            if (c == null)
            {
                devChart3.JSProperties["cpResultCode"] = "0";
                devChart3.JSProperties["cpResultMsg"] = "조회 조건에 맞는 데이터가 없습니다.";
            }
            else
            {
                this.devChart3 = c;

            }


        }
        #endregion

        #region GetDataHistogram
        public DataSet GetDataHistogram(Dictionary<string, string> dicParam)
        {
            DataSet ds = new DataSet();
            string errMsg;


            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(dicParam);
                ds = spcDB.GetDataSet("USP_ANLS0203_1", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}