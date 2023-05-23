using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ANLS.Biz;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0302 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["ANLS0302"];
            }
            set
            {
                Session["ANLS0302"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["ANLS0302_1"];
            }
            set
            {
                Session["ANLS0302_1"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["ANLS0302_2"];
            }
            set
            {
                Session["ANLS0302_2"] = value;
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

        #region Data조회 QWK03A_ANLS0302_1_LST
        void QWK03A_ANLS0302_1_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                //oParamDic.Add("F_FROMYM", GetFromDt());
                //oParamDic.Add("F_TOYM", GetToDt());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Value.ToString());
                ds = biz.ANLS0302_LST(oParamDic, out errMsg);
            }

            dtChart1 = ds.Tables[0].Copy();
            devGrid.DataSource = ds.Tables[0];
            devGrid.DataBind();

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
                    //ucPager.TotalItems = 0;
                    //ucPager.PagerDataBind();
                }
                else
                {
                    //devGrid.JSProperties["cpResultCode"] = "pager";
                    //devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, QWK04A_ADTR0402_CNT());
                }
            }
        }
        #endregion

        #region Data조회 QWK03A_ANLS0302_2_LST
        void QWK03A_ANLS0302_2_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt1());
                oParamDic.Add("F_TODT", GetToDt1());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
                oParamDic.Add("F_ITEMCD", GetItemCD1());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO1.Value.ToString());
                ds = biz.ANLS0302_1_LST(oParamDic, out errMsg);
            }
            if (ds.Tables.Count == 0) { return; }

            dtChart2 = ds.Tables[0].Copy();
            devGrid1.DataSource = ds.Tables[0];
            devGrid1.DataBind();

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bCallback)
                {
                    // Pager Setting
                    //ucPager.TotalItems = 0;
                    //ucPager.PagerDataBind();
                }
                else
                {
                    //devGrid.JSProperties["cpResultCode"] = "pager";
                    //devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, QWK04A_ADTR0402_CNT());
                }
            }
        }
        #endregion

        #region Data조회 QWK03A_ANLS0302_3_LST
        void QWK03A_ANLS0302_3_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt2());
                oParamDic.Add("F_TODT", GetToDt2());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD2());
                oParamDic.Add("F_ITEMCD", GetItemCD2());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO2.Value.ToString());
                ds = biz.ANLS0302_1_LST(oParamDic, out errMsg);
            }
            if (ds.Tables.Count == 0) { return; }

            dtChart3 = ds.Tables[0].Copy();
            devGrid2.DataSource = ds.Tables[0];
            devGrid2.DataBind();

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bCallback)
                {
                    // Pager Setting
                    //ucPager.TotalItems = 0;
                    //ucPager.PagerDataBind();
                }
                else
                {
                    //devGrid.JSProperties["cpResultCode"] = "pager";
                    //devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, QWK04A_ADTR0402_CNT());
                }
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
                devChart1.Series.Clear();
                DevExpressLib.SetChartBarLineSeries(devChart1, "CP", "F_GB", "F_CP", System.Drawing.Color.Blue);
                DevExpressLib.SetChartBarLineSeries(devChart1, "CPK", "F_GB", "F_CPK", System.Drawing.Color.Green);

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                //DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
            }
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

                devChart2.Series.Clear();
                //DevExpressLib.SetChartBarLineSeries(devChart2, "CP", "F_YMNM", "F_CP", System.Drawing.Color.Blue);
                //DevExpressLib.SetChartBarLineSeries(devChart2, "CPK", "F_YMNM", "F_CPK", System.Drawing.Color.Green);

                DevExpressLib.SetChartLineSeries(devChart2, "CP", "F_YMNM", "F_CP", System.Drawing.Color.Blue);
                DevExpressLib.SetChartLineSeries(devChart2, "CPK", "F_YMNM", "F_CPK", System.Drawing.Color.Green);
                DevExpressLib.SetChartLineSeries(devChart2, "RANGE", "F_YMNM", "F_RANGE", System.Drawing.Color.Orange);

                devChart2.DataSource = dtChart2;
                devChart2.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart2);
                //DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");

            }
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
                devChart3.Series.Clear();
                //DevExpressLib.SetChartBarLineSeries(devChart3, "CP", "F_YMNM", "F_CP", System.Drawing.Color.Blue);
                //DevExpressLib.SetChartBarLineSeries(devChart3, "CPK", "F_YMNM", "F_CPK", System.Drawing.Color.Green);

                DevExpressLib.SetChartLineSeries(devChart3, "CP", "F_YMNM", "F_CP", System.Drawing.Color.Blue);
                DevExpressLib.SetChartLineSeries(devChart3, "CPK", "F_YMNM", "F_CPK", System.Drawing.Color.Green);
                DevExpressLib.SetChartLineSeries(devChart3, "RANGE", "F_YMNM", "F_RANGE", System.Drawing.Color.Orange);

                devChart3.DataSource = dtChart3;
                devChart3.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart3);
                //DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");

            }
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
            QWK03A_ANLS0302_1_LST(nPageSize, nCurrPage, true);
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
            QWK03A_ANLS0302_2_LST(nPageSize, nCurrPage, true);
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
            QWK03A_ANLS0302_3_LST(nPageSize, nCurrPage, true);
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

        #endregion
    }
}