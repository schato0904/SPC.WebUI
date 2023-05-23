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
using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.WERDDACO
{
    public partial class WERD_DACO2001 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        DataSet ds2 = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["WSTA0101"];
            }
            set
            {
                Session["WSTA0101"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["WSTA0101_1"];
            }
            set
            {
                Session["WSTA0101_1"] = value;
            }
        }
        DataTable dtgrid1
        {
            get
            {
                return (DataTable)Session["dtgrid1"];
            }
            set
            {
                Session["dtgrid1"] = value;
            }
        }
        DataTable dtgrid2
        {
            get
            {
                return (DataTable)Session["WSTA0101_2"];
            }
            set
            {
                Session["WSTA0101_2"] = value;
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
            dtgrid1 = null;
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

        #region Data조회
        void QWK04A_ADTR0402_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_MODEL", GetModelCD());
                oParamDic.Add("F_ITEMCHK", "2");
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_GBN", this.rdoGBN.SelectedItem.Value.ToString());
                oParamDic.Add("F_UNIT", this.rdoUNIT.SelectedItem.Value.ToString());
                ds = biz.WERD_DACO2001_LST(oParamDic, out errMsg);
            }


            // Pivot Fill
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(ds.Tables[0], "구분");


            dtgrid2 = dtPivotTable;
            devGrid.DataSource = dtPivotTable;
            devGrid.DataBind();

            dtChart1 = ds.Tables[1].Copy();
            dtgrid1 = ds.Tables[2].Copy();


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
                DevExpressLib.SetChartBarLineSeries(devChart1, "시료수", "F_SEQ", "F_GRAPH", System.Drawing.Color.LightBlue);

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetChartLegend(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, true, 0, 0, 0, 0, null, null);
                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisY.NumericScaleOptions.GridAlignment = NumericGridAlignment.Ones;
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
            string cellClickHandler = string.Format(@"onCellClick(devGrid,'{0}','{1}')", e.DataColumn.Collection.Count, e.DataColumn.Index);
            e.Cell.Attributes.Add("onclick", cellClickHandler); 
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
            QWK04A_ADTR0402_LST(nPageSize, nCurrPage, true);
            devGrid.DataBind();
            //dtChart1 = ds.Tables[1].Copy();
            //dtgrid1 = ds.Tables[2].Copy();
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
            //QWK04A_ADTR0103_LST();
            devGrid.DataSource = dtgrid2;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 품목별 Worst 정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            if (oParams.Length == 3)
            {
                devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
                if (oParams[2] == "C")
                {
                    devChart2.Series.Clear();
                    devChart2.Titles.Clear();
                }
            }
            else
            {
                devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

                string errMsg = String.Empty;

                using (WERDBiz biz = new WERDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", GetCompCD());
                    oParamDic.Add("F_FACTCD", GetFactCD());
                    oParamDic.Add("F_FRDT", GetFromDt());
                    oParamDic.Add("F_TODT", GetToDt());
                    oParamDic.Add("F_MODELCD", GetModelCD());
                    oParamDic.Add("F_ITEMCD", oParams[2]);
                    oParamDic.Add("F_BANCD", oParams[4]);
                    oParamDic.Add("F_LINECD", oParams[5]);
                    oParamDic.Add("F_GUBN", this.rdoGBN.SelectedItem.Value.ToString());
                    oParamDic.Add("F_UNIT", this.rdoUNIT.SelectedItem.Value.ToString());
                    ds = biz.WERD_DACO2001_PIECHART_LST(oParamDic, out errMsg);
                }

                if (bExistsDataSet(ds))
                {
                    dtChart2 = ds.Tables[0].Copy();

                    devChart2.Series.Clear();
                    devChart2.Titles.Clear();

                    Series series = new Series("", ViewType.Pie);

                    for (int i = 0; i < dtChart2.Rows.Count; i++)
                    {
                        series.Points.Add(new SeriesPoint(dtChart2.Rows[i]["F_INSPDETAIL"].ToString(), new double[] { Convert.ToDouble(dtChart2.Rows[i]["F_REJRATE"]) }));
                    }

                    ChartTitle ct = new ChartTitle();
                    ct.Text = oParams[3];
                    ct.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    devChart2.Titles.Add(ct);

                    series.Label.TextPattern = "{A}: {VP:p0}";

                    devChart2.Series.Add(series);
                    devChart2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                    devChart2.DataSource = dtChart2;
                    devChart2.DataBind();
                }
                else
                {
                    devChart2.Series.Clear();

                }
            }
        }

        #endregion
    }
}