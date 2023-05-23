using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.FITM.Biz;
using DevExpress.Web;

namespace SPC.WebUI.Pages.FITM
{
    public partial class FITM0102 : WebUIBasePage
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
                // 초픔변경사유목록을 구한다
                QCD40_TBL_LST();
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

        #region 색상테이블
        string[] GetColorSet(string sGBN)
        {
            switch (sGBN)
            {
                default:
                    return new string[] { "", "" };
                case "1":
                    return new string[] { "255,0,0", "#FF0000" };
                case "2":
                    return new string[] { "55,165,0", "#FFA500" };
                case "3":
                    return new string[] { "255,255,0", "#FFFF00" };
                case "4":
                    return new string[] { "0,128,0", "#008000" };
                case "5":
                    return new string[] { "0,0,255", "#0000FF" };
                case "6":
                    return new string[] { "38,38,194", "#2626C2" };
                case "7":
                    return new string[] { "138,43,226", "#8A2BE2" };
                case "8":
                    return new string[] { "192,192,192", "#C0C0C0" };
            }
        }
        #endregion

        #region 초픔변경사유목록을 구한다
        void QCD40_TBL_LST()
        {
            string errMsg = String.Empty;

            using (FITMBiz biz = new FITMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                //oParamDic.Add("F_STATUS", "1");
                ds = biz.QCD40_TBL_LST(oParamDic, out errMsg);
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

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["F_STATUS"] = "0";
        }
        #endregion

        #region devGrid HtmlEditFormCreated
        /// <summary>
        /// devGrid_HtmlEditFormCreated
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_COLORGBN")) return;

            if (e.Column.FieldName.Equals("F_COLORGBN"))
            {
                DevExpress.Web.ASPxComboBox ddlComboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                ddlComboBox.Items.Add("", "");
                ddlComboBox.Items.Add("1", "1");
                ddlComboBox.Items.Add("2", "2");
                ddlComboBox.Items.Add("3", "3");
                ddlComboBox.Items.Add("4", "4");
                ddlComboBox.Items.Add("5", "5");
                ddlComboBox.Items.Add("6", "6");
                ddlComboBox.Items.Add("7", "7");
                ddlComboBox.Items.Add("8", "8");
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
            DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
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
            if (e.DataColumn.FieldName.Equals("F_COLORGBN"))
            {
                if (e.CellValue != null)
                {
                    string[] colors = GetColorSet(e.CellValue.ToString())[0].Split(',');
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(Convert.ToInt32(colors[0]), Convert.ToInt32(colors[1]), Convert.ToInt32(colors[2]));
                    e.Cell.ForeColor = color;
                    e.Cell.BackColor = color;
                }
            }

            if (e.DataColumn.FieldName.Equals("F_FOURNM"))
            {
                
                //if(e.CellValue.ToString() != "" && e.CellValue.ToString() == "품목변경")
                e.Cell.Enabled = false;
                

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
            // 초픔변경사유목록을 구한다
            QCD40_TBL_LST();
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
                foreach (var Value in e.InsertValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_FOURNMKR", (Value.NewValues["F_FOURNM"] ?? "").ToString());
                    oParamDic.Add("F_FOURNMUS", (Value.NewValues["F_FOURNM"] ?? "").ToString());
                    oParamDic.Add("F_FOURNMCN", (Value.NewValues["F_FOURNM"] ?? "").ToString());
                    oParamDic.Add("F_COLORGBN", (Value.NewValues["F_COLORGBN"] ?? "").ToString());
                    oParamDic.Add("F_COLOR", GetColorSet((Value.NewValues["F_COLORGBN"] ?? "").ToString())[0]);
                    oParamDic.Add("F_COLOR2", GetColorSet((Value.NewValues["F_COLORGBN"] ?? "").ToString())[1]);
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD40_TBL_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
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
                    oParamDic.Add("F_FOURCD", (Value.Keys["F_FOURCD"] ?? "").ToString());
                    oParamDic.Add("F_FOURNMKR", (Value.NewValues["F_FOURNM"] ?? "").ToString());
                    oParamDic.Add("F_FOURNMUS", (Value.NewValues["F_FOURNM"] ?? "").ToString());
                    oParamDic.Add("F_FOURNMCN", (Value.NewValues["F_FOURNM"] ?? "").ToString());
                    oParamDic.Add("F_COLORGBN", (Value.NewValues["F_COLORGBN"] ?? "").ToString());
                    oParamDic.Add("F_COLOR", GetColorSet((Value.NewValues["F_COLORGBN"] ?? "").ToString())[0]);
                    oParamDic.Add("F_COLOR2", GetColorSet((Value.NewValues["F_COLORGBN"] ?? "").ToString())[1]);
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD40_TBL_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            if (e.DeleteValues.Count > 0)
            {
                foreach (var Value in e.DeleteValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_FOURCD", (Value.Keys["F_FOURCD"] ?? "").ToString());

                    oSPs[idx] = "USP_QCD40_TBL_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (FITMBiz biz = new FITMBiz())
            {
                bExecute = biz.QCD40_TBL_PROC(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 초픔변경사유목록을 구한다
                QCD40_TBL_LST();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 초품사유등록정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devGrid_CommandButtonInitialize
        protected void devGrid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
        {
            object grid = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "F_FOURCD");
            if (grid != null)
            {
                string value = grid.ToString();

                if (value == "01" || value == "02" || value == "05")
                {
                    e.Enabled = false;
                }
                else
                    e.Enabled = true;
            }
        }
        #endregion

        #endregion
    }
}