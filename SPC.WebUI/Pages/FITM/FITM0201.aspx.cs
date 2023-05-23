using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.FITM.Biz;

namespace SPC.WebUI.Pages.FITM
{
    public partial class FITM0201 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
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

            new ASPxGridViewCellMerger(devGrid, "F_BANNM|F_LINENM|F_WORKNM");

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 초중종모니터링목록을 구한다
                FIRSTITEM01_MORNITORING_LST();
            }

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

        #region 초중종모니터링목록을 구한다
        void FIRSTITEM01_MORNITORING_LST()
        {
            string errMsg = String.Empty;

            using (FITMBiz biz = new FITMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDT", GetFromDt());
                oParamDic.Add("F_BANCD", GetBanCD());
                ds = biz.FIRSTITEM01_MORNITORING_LST(oParamDic, out errMsg);
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
                if (IsCallback)
                    devGrid.DataBind();
            }
        }
        #endregion

        #region 초중종모니터링목록을 구한다
        void FIRSTITEM01_MORNITORING_EXCEL_LST()
        {
            string errMsg = String.Empty;

            using (FITMBiz biz = new FITMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDT", GetFromDt());
                oParamDic.Add("F_BANCD", GetBanCD());
                ds = biz.FIRSTITEM01_MORNITORING_LST(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds;

            
        }
        #endregion

        #region 색깔
        System.Drawing.Color GetColor(string sType)
        {
            switch (sType)
            {
                default:
                    switch(sType.Substring(0,1))
                    {
                        default:
                            return System.Drawing.Color.White;
                        case "N":
                            return System.Drawing.Color.FromArgb(0xC0, 0xC3, 0xB9);
                    }
                    
                case "0":
                case "OK0":
                case "NG0":
                    return System.Drawing.Color.Blue;
                case "1":
                case "OK1":
                case "NG1":
                    return System.Drawing.Color.Orange;
                case "2":
                case "OK2":
                case "NG2":
                    return System.Drawing.Color.Green;
                case "R0":
                case "R1":
                case "R2":
                    return System.Drawing.Color.FromArgb(0xF0, 0xF4, 0xE7);
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
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 초중종모니터링목록을 구한다
            FIRSTITEM01_MORNITORING_LST();
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
            if (e.VisibleRowIndex < 0) return;

            if (e.Column.FieldName.IndexOf("F_TIMEZONE") < 0) return;

            if (e.Column.FieldName.IndexOf("F_TIMEZONE") >= 0)
            {
                e.EncodeHtml = false;
                string displayText = String.Empty;
                if (!String.IsNullOrEmpty(e.Value.ToString()))
                {
                    displayText = e.Value.ToString();
                    if (displayText.Equals("R0"))
                        displayText = @"초";
                    else if (displayText.Equals("R1"))
                        displayText = "중";
                    else if (displayText.Equals("R2"))
                        displayText = @"종";
                    else if (!displayText.Substring(0, 1).Equals("R") && !displayText.Substring(0, 2).Equals("NG") && !displayText.Substring(0, 2).Equals("OK"))
                        displayText = @"비";
                    else
                        displayText = e.Value.ToString().Length >= 2 ? e.Value.ToString().Substring(0, 2) : "";
                }
                e.DisplayText = displayText;
            }
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
            if (e.DataColumn.FieldName.IndexOf("F_TIMEZONE") >= 0)
            {
                if (e.CellValue != null)
                {
                    string value = e.CellValue.ToString();

                    if (!String.IsNullOrEmpty(value))
                    {
                        System.Drawing.Color color = GetColor(value);

                        if (!value.Substring(0, 1).Equals("R"))
                        {
                            e.Cell.ForeColor = System.Drawing.Color.White;
                            e.Cell.Font.Size = new FontUnit(8);
                            e.Cell.Attributes.Add("ondblclick", String.Format("fn_OnPopupList('{0}','{1}')", e.DataColumn.Caption, e.VisibleIndex));
                            e.Cell.Style.Add("cursor", "pointer");
                        }

                        e.Cell.BackColor = color;

                        if (value.Substring(0, 2).Equals("NG"))
                        {
                            e.Cell.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (!value.Substring(0, 1).Equals("R") && !value.Substring(0, 2).Equals("NG") && !value.Substring(0, 2).Equals("OK"))
                        {
                            e.Cell.Attributes.Add("data-toggle", "tooltip");
                            e.Cell.Attributes.Add("title", value.Substring(1, value.Length - 1));
                        }
                    }
                }
            }
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
            FIRSTITEM01_MORNITORING_LST();
        }
        #endregion

        #region devGrid2 CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid2_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            if (e.Column.FieldName.IndexOf("F_TIMEZONE") < 0) return;

            if (e.Column.FieldName.IndexOf("F_TIMEZONE") >= 0)
            {
                e.EncodeHtml = false;
                string displayText = String.Empty;
                if (!String.IsNullOrEmpty(e.Value.ToString()))
                {
                    displayText = e.Value.ToString();
                    if (displayText.Equals("R0"))
                        displayText = @"초";
                    else if (displayText.Equals("R1"))
                        displayText = "중";
                    else if (displayText.Equals("R2"))
                        displayText = @"종";
                    else if (!displayText.Substring(0, 1).Equals("R") && !displayText.Substring(0, 2).Equals("NG") && !displayText.Substring(0, 2).Equals("OK"))
                        displayText = @"비";
                    else
                        displayText = e.Value.ToString().Length >= 2 ? e.Value.ToString().Substring(0, 2) : "";
                }
                e.DisplayText = displayText;
            }
        }
        #endregion

        #region devGrid2 HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid2_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName.IndexOf("F_TIMEZONE") >= 0)
            {
                if (e.CellValue != null)
                {
                    string value = e.CellValue.ToString();

                    if (!String.IsNullOrEmpty(value))
                    {
                        System.Drawing.Color color = GetColor(value);

                        if (!value.Substring(0, 1).Equals("R"))
                        {
                            e.Cell.ForeColor = System.Drawing.Color.White;
                            e.Cell.Font.Size = new FontUnit(8);
                            e.Cell.Attributes.Add("ondblclick", String.Format("fn_OnPopupList('{0}','{1}')", e.DataColumn.Caption, e.VisibleIndex));
                            e.Cell.Style.Add("cursor", "pointer");
                        }

                        e.Cell.BackColor = color;

                        if (value.Substring(0, 2).Equals("NG"))
                        {
                            e.Cell.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (!value.Substring(0, 1).Equals("R") && !value.Substring(0, 2).Equals("NG") && !value.Substring(0, 2).Equals("OK"))
                        {
                            e.Cell.Attributes.Add("data-toggle", "tooltip");
                            e.Cell.Attributes.Add("title", value.Substring(1, value.Length - 1));
                        }
                    }
                }
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
            else if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE01") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE02") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE03") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE04") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE05") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE06") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE07") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE08") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE09") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE10") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE11") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE12") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE13") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE14") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE15") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE16") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE17") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE18") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE19") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE20") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE21") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE22") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE23") && e.RowType == DevExpress.Web.GridViewRowType.Data
                  || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_TIMEZONE24") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"</span>", "").Replace(@"<span style='font-weight:bold;'>", "");
                e.Column.Width = 5;
                
            }

            if (devGridDataColumn != null && devGridDataColumn.FieldName.IndexOf("F_TIMEZONE") >= 0 && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                if (!String.IsNullOrEmpty(e.Value.ToString()))
                {
                    if (!e.Value.ToString().Substring(0, 1).Equals("R"))
                    {
                        string value = e.Value.ToString();

                        System.Drawing.Color color = GetColor(value);

                        e.BrickStyle.ForeColor = System.Drawing.Color.White;
                        e.BrickStyle.Font = new System.Drawing.Font(e.BrickStyle.Font.FontFamily, 8);
                        e.BrickStyle.BackColor = color;
                    }
                    else
                    {
                        e.Text = String.Empty;
                    }
                }
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
            FIRSTITEM01_MORNITORING_EXCEL_LST();
            devGrid2.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 초중종모니터링정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}