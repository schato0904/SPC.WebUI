﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.WSTA.Biz;
using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.WSTA
{
    public partial class WSTA0109 : WebUIBasePage
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
                return (DataTable)Session["WSTA0109"];
            }
            set
            {
                Session["WSTA0109"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["WSTA0109_1"];
            }
            set
            {
                Session["WSTA0109_1"] = value;
            }
        }
        DataTable dtGrid1
        {
            get
            {
                return (DataTable)Session["WSTA0109_2"];
            }
            set
            {
                Session["WSTA0109_2"] = value;
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

            using (WSTABiz biz = new WSTABiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Value.ToString());
                ds = biz.WSTA0109_LST(oParamDic, out errMsg);
            }

            dtChart1 = ds.Tables[0].Copy();
            dtChart1.Clear();
            dtChart2 = ds.Tables[0].Copy();

            if (dtChart2.Rows.Count > 0)
            {
                dtChart1.Rows.Add(" ", dtChart2.Rows[0][1], dtChart2.Rows[0][2], dtChart2.Rows[0][3]);
            }

            for (int i = 1; i < dtChart2.Rows.Count; i++)
            {
                dtChart1.ImportRow(dtChart2.Rows[i - 1]);
                dtChart1.Rows.Add();

                if (i != dtChart2.Rows.Count)
                {
                    dtChart1.Rows[i * 2][0] = dtChart2.Rows[i - 1][0];
                    dtChart1.Rows[i * 2][1] = dtChart2.Rows[i][1];
                    dtChart1.Rows[i * 2][2] = dtChart2.Rows[i][2];
                    dtChart1.Rows[i * 2][3] = dtChart2.Rows[i][3];
                }

                if (i == dtChart2.Rows.Count - 1)
                {
                    dtChart1.ImportRow(dtChart2.Rows[i]);
                }
            }

            // Pivot Fill
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(ds.Tables[0], "구분");

            dtGrid1 = dtPivotTable;
            devGrid.DataSource = dtPivotTable;
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
                if (oParams.Length > 2)
                {
                    switch (oParams[2].ToString())
                    {
                        case "0":
                            DevExpressLib.SetChartLineSeries(devChart1, "관리상한", "구분", "관리상한", System.Drawing.Color.DimGray, 3);
                            DevExpressLib.SetChartLineSeries(devChart1, "관리하한", "구분", "관리하한", System.Drawing.Color.DimGray, 3);
                            break;
                        case "1":
                            DevExpressLib.SetChartLineSeries(devChart1, "관리상한", "구분", "관리상한", System.Drawing.Color.DimGray, 3);
                            DevExpressLib.SetChartLineSeries(devChart1, "관리하한", "구분", "관리하한", System.Drawing.Color.DimGray, 3);
                            break;
                        case "2":
                            DevExpressLib.SetChartLineSeries(devChart1, "Range", "구분", "Range", System.Drawing.Color.DimGray, 3);
                            break;
                        default:
                            DevExpressLib.SetChartLineSeries(devChart1, "관리상한", "구분", "관리상한", System.Drawing.Color.DimGray, 3);
                            DevExpressLib.SetChartLineSeries(devChart1, "관리하한", "구분", "관리하한", System.Drawing.Color.DimGray, 3);
                            break;
                    }
                }
                else
                {
                    DevExpressLib.SetChartLineSeries(devChart1, "관리상한", "구분", "관리상한", System.Drawing.Color.DimGray, 3);
                    DevExpressLib.SetChartLineSeries(devChart1, "관리하한", "구분", "관리하한", System.Drawing.Color.DimGray, 3);
                }

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);

                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisX.VisualRange.MinValue = 0;
                diagram.AxisX.WholeRange.AlwaysShowZeroLevel = false;
                diagram.AxisX.WholeRange.SideMarginsValue = 0;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
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
            string[] sTooltipFields = { "F_ITEMNM", "F_WORKNM", "F_INSPDETAIL", "F_ITEMCD" };

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
            QWK04A_ADTR0402_LST(nPageSize, nCurrPage, true);
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

        #endregion
    }
}