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
    public partial class FITM0101 : WebUIBasePage
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

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 초중종예약관리목록을 구한다
                FIRSTITEM01_RESERVE_LST();
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

        #region 초중종예약관리목록을 구한다
        void FIRSTITEM01_RESERVE_LST()
        {
            string errMsg = String.Empty;

            using (FITMBiz biz = new FITMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", GetBanCD());
                ds = biz.FIRSTITEM01_RESERVE_LST(oParamDic, out errMsg);
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

        #endregion

        #region 사용자이벤트

        #region devGrid HtmlEditFormCreated
        /// <summary>
        /// devGrid_HtmlEditFormCreated
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("F_TIMEZONE") < 0) return;

            if (e.Column.FieldName.IndexOf("F_TIMEZONE") >= 0)
            {
                DevExpress.Web.ASPxComboBox ddlComboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                ddlComboBox.Items.Add("", "");
                ddlComboBox.Items.Add("초", "0");
                ddlComboBox.Items.Add("중", "1");
                ddlComboBox.Items.Add("종", "2");
            }
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
            // 초중종예약관리목록을 구한다
            FIRSTITEM01_RESERVE_LST();
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
                    if (displayText.Equals("0"))
                        displayText = @"<span style='color:red;font-weight:bold;'>초</span>";
                    else if (displayText.Equals("1"))
                        displayText = "중";
                    else if (displayText.Equals("2"))
                        displayText = @"<span style='color:blue;font-weight:bold;'>종</span>";
                }
                e.DisplayText = displayText;
            }
        }
        #endregion

        #region devGrid BatchUpdate
        /// <summary>
        /// devGrid_BatchUpdate
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataBatchUpdateEventArgs</param>
        protected void devGrid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
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
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_BANCD", (Value.Keys["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_LINECD", (Value.Keys["F_LINECD"] ?? "").ToString());
                    oParamDic.Add("F_WORKCD", (Value.Keys["F_WORKCD"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE01", (Value.NewValues["F_TIMEZONE01"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE02", (Value.NewValues["F_TIMEZONE02"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE03", (Value.NewValues["F_TIMEZONE03"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE04", (Value.NewValues["F_TIMEZONE04"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE05", (Value.NewValues["F_TIMEZONE05"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE06", (Value.NewValues["F_TIMEZONE06"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE07", (Value.NewValues["F_TIMEZONE07"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE08", (Value.NewValues["F_TIMEZONE08"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE09", (Value.NewValues["F_TIMEZONE09"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE10", (Value.NewValues["F_TIMEZONE10"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE11", (Value.NewValues["F_TIMEZONE11"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE12", (Value.NewValues["F_TIMEZONE12"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE13", (Value.NewValues["F_TIMEZONE13"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE14", (Value.NewValues["F_TIMEZONE14"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE15", (Value.NewValues["F_TIMEZONE15"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE16", (Value.NewValues["F_TIMEZONE16"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE17", (Value.NewValues["F_TIMEZONE17"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE18", (Value.NewValues["F_TIMEZONE18"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE19", (Value.NewValues["F_TIMEZONE19"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE20", (Value.NewValues["F_TIMEZONE20"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE21", (Value.NewValues["F_TIMEZONE21"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE22", (Value.NewValues["F_TIMEZONE22"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE23", (Value.NewValues["F_TIMEZONE23"] ?? "").ToString());
                    oParamDic.Add("F_TIMEZONE24", (Value.NewValues["F_TIMEZONE24"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", "1");

                    oSPs[idx] = "USP_FIRSTITEM01_RESERVE_PROC";
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

            using (FITMBiz biz = new FITMBiz())
            {
                bExecute = biz.FIRSTITEM01_RESERVE_PROC(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 초중종예약관리목록을 구한다
                FIRSTITEM01_RESERVE_LST();
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
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
                e.Text = e.Text.Replace(@"</span>", "").Replace(@"<span style='color:red;font-weight:bold;'>", "").Replace(@"<span style='color:blue;font-weight:bold;'>", "");
                e.Column.Width = 5;
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 초중종예약관리정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}