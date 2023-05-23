using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.WKSD.Biz;
namespace SPC.WebUI.Pages.WKSD
{
    public partial class WKSD0101_MULTI : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
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

            

            //hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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
            if ((String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false")))
            {
                // 작업표준서 목록을 구한다
                DWK01_LST();
            }

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
        {
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

        #region 작업표준서 목록을 구한다
        void DWK01_LST()
        {
            string errMsg = String.Empty;

            using (WKSDBiz biz = new WKSDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", GetItemCD());

                ds = biz.DWK01_LST(oParamDic, out errMsg);
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

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(DevExpress.Web.ASPxGridView grid, string ComboBoxID, string CommonCode)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = grid.FindEditFormTemplateControl(ComboBoxID) as DevExpress.Web.ASPxComboBox;
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
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
            // 작업표준서 목록을 구한다
            DWK01_LST();
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
            string[] sTooltipFields = { "F_ITEMNM", "F_WORKNM" };

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
            if (e.VisibleRowIndex < 0) return;

            DataRow dtRow = devGrid.GetDataRow(e.VisibleRowIndex);

            if (e.Column.FieldName.Equals("F_DOCCD"))
            {
                e.DisplayText = GetCommonCodeText(CachecommonCode["AA"]["AAD1"][e.Value.ToString()]);
            }
        }
        #endregion

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            string param = Request.Params.Get("__CALLBACKPARAM");
            if (!String.IsNullOrEmpty(param))
            {
                if (!param.Contains("CANCELEDIT"))
                {
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;

                    //DevExpressLib.devTextBox(grid, "txtITEMCD").Text = devGrid.GetRowValues(0, "F_ITEMCD").ToString();
                    //DevExpressLib.devTextBox(grid, "txtITEMNM").Text = devGrid.GetRowValues(0, "F_ITEMNM").ToString();
                    //DevExpressLib.devTextBox(grid, "txtMODELNM").Text = devGrid.GetRowValues(0, "F_MODELNM").ToString();
                    //DevExpressLib.devRadioButtonList(grid, "rdoDATAUNIT").SelectedIndex = 0;
                    //DevExpressLib.devRadioButtonList(grid, "rdoRESULTGUBUN").SelectedIndex = 1;
                    //DevExpressLib.devRadioButtonList(grid, "rdoACCEPT").SelectedIndex = 1;
                    //DevExpressLib.devRadioButtonList(grid, "rdoHIPISNG").SelectedIndex = 1;
                    //DevExpressLib.devSpinEdit(grid, "txtSIRYO").Text = "1";
                    //DevExpressLib.devSpinEdit(grid, "txtDEFECTS_N").Text = "1";
                    //DevExpressLib.devTextBox(grid, "txtDISPLAYNO").ClientSideEvents.Init = "fn_OnControlDisableBox";
                }
            }
        }
        #endregion

        #region devGrid HtmlEditFormCreated
        /// <summary>
        /// devGrid_HtmlEditFormCreated
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditFormEventArgs</param>
        protected void devGrid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridViewEditFormEventArgs e)
        {
            string param = Request.Params.Get("__CALLBACKPARAM");
            if (!String.IsNullOrEmpty(param))
            {
                if (!param.Contains("CANCELEDIT"))
                {
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;

                    // 검사분류
                    AspxCombox_DataBind(grid, "ddlDOCCD", "AAD1");
                }
            }
        }
        #endregion

        #region ddlComboBox_DataBound
        /// <summary>
        /// ddlComboBox_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlComboBox_DataBound(object sender, EventArgs e)
        {
            string param = Request.Params.Get("__CALLBACKPARAM");
            if (!String.IsNullOrEmpty(param))
            {
                if (!param.Contains("CANCELEDIT"))
                {
                    DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
                    string rowValue = String.Empty;

                    if (!param.Contains("ADDNEWROW"))
                    {
                        int editingRowVisibleIndex = devGrid.EditingRowVisibleIndex;

                        if (editingRowVisibleIndex >= 0)
                        {
                            switch (devComboBox.ID)
                            {
                                case "ddlDOCCD":       // 검사분류
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_DOCCD").ToString();
                                    break;
                            }
                        }
                    }

                    devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = devComboBox.ID.Equals("ddlDOCCD") ? "선택하세요" : "선택안함", Value = "", Selected = true });

                    if (devComboBox.Items.FindByValue(rowValue) != null)
                        devComboBox.SelectedIndex = devComboBox.Items.FindByValue(rowValue).Index;
                }
            }
        }
        #endregion

        #region txtITEMCD_Init
        /// <summary>
        /// txtITEMCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupItemSearch('FORM','T')");
        }
        #endregion

        #region txtWORKCD_Init
        /// <summary>
        /// txtWORKCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtWORKCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWork_()");
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;
            string[] oParams = e.Parameter.Split('|');
            bool bExecute = false;

            switch (oParams[0])
            {
                case "ITEM":  // 품목
                    devCallback.JSProperties["cpIDCD"] = "ITEMCD";
                    devCallback.JSProperties["cpIDNM"] = "ITEMNM";
                    devCallback.JSProperties["cpMODELNM"] = "MODELNM";

                    using (CommonBiz biz = new CommonBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_ITEMCD", oParams[1]);
                        oParamDic.Add("F_STATUS", "1");
                        ds = biz.GetQCD01_POPUP_LST(oParamDic, out errMsg);
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        devCallback.JSProperties["cpCODE"] = ds.Tables[0].Rows[0]["F_ITEMCD"].ToString();
                        devCallback.JSProperties["cpTEXT"] = ds.Tables[0].Rows[0]["F_ITEMNM"].ToString();
                        devCallback.JSProperties["cpMODEL"] = ds.Tables[0].Rows[0]["F_MODELNM"].ToString();
                        devCallback.JSProperties["cpREQUEST"] = "ITEM";

                        bExecute = true;
                    }
                    break;
                case "WORK":      // 공정
                    devCallback.JSProperties["cpIDCD"] = "WORKCD";
                    devCallback.JSProperties["cpIDNM"] = "WORKNM";
                    devCallback.JSProperties["cpMODELNM"] = "";

                    using (CommonBiz biz = new CommonBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_WORKCD", oParams[1]);
                        oParamDic.Add("F_STATUS", "1");
                        ds = biz.GetQCD74_LST(oParamDic, out errMsg);
                    }

                    if (ds.Tables[0].Rows.Count.Equals(1))
                    {
                        devCallback.JSProperties["cpCODE"] = ds.Tables[0].Rows[0]["F_WORKCD"].ToString();
                        devCallback.JSProperties["cpTEXT"] = ds.Tables[0].Rows[0]["F_WORKNM"].ToString();
                        devCallback.JSProperties["cpBANCD"] = ds.Tables[0].Rows[0]["F_BANCD"].ToString();
                        devCallback.JSProperties["cpLINECD"] = ds.Tables[0].Rows[0]["F_LINECD"].ToString();
                        devCallback.JSProperties["cpMODEL"] = "";
                        devCallback.JSProperties["cpREQUEST"] = "WORK";

                        bExecute = true;
                    }
                    break;
            }

            if (!bExecute)
            {
                devCallback.JSProperties["cpCODE"] = "";
                devCallback.JSProperties["cpTEXT"] = "";
                devCallback.JSProperties["cpMODEL"] = "";
                devCallback.JSProperties["cpBANCD"] = "";
                devCallback.JSProperties["cpLINECD"] = "";
                devCallback.JSProperties["cpREQUEST"] = "";
            }
        }
        #endregion

        #region devGrid_RowInserting
        /// <summary>
        /// devGrid_RowInserting
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInsertingEventArgs</param>
        protected void devGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_BANCD", e.NewValues["F_BANCD"] == null ? "" : e.NewValues["F_BANCD"].ToString());
            oParamDic.Add("F_LINECD", e.NewValues["F_LINECD"] == null ? "" : e.NewValues["F_LINECD"].ToString());
            oParamDic.Add("F_WORKCD", e.NewValues["F_WORKCD"] == null ? "" : e.NewValues["F_WORKCD"].ToString());
            oParamDic.Add("F_ITEMCD", e.NewValues["F_ITEMCD"] == null ? "" : e.NewValues["F_ITEMCD"].ToString());
            oParamDic.Add("F_DOCCD", DevExpressLib.devComboBox(devGrid, "ddlDOCCD").SelectedItem.Value.ToString());
            oParamDic.Add("F_MAKEDATE", e.NewValues["F_MAKEDATE"] == null ? "" : e.NewValues["F_MAKEDATE"].ToString());
            oParamDic.Add("F_DOCNO", e.NewValues["F_DOCNO"] == null ? "" : e.NewValues["F_DOCNO"].ToString());
            oParamDic.Add("F_DESC", e.NewValues["F_DESC"] == null ? "" : e.NewValues["F_DESC"].ToString());
            oParamDic.Add("F_USER", gsUSERID);
            oParamDic.Add("F_WORKCDS", hidWorkcd.Text);

            bool bExecute = false;

            using (WKSDBiz biz = new WKSDBiz())
            {
                bExecute = biz.PROC_DWK01_INS(oParamDic);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }
            else
            {
                devGrid.CancelEdit();
            }

            e.Cancel = true;

            if (true == bExecute)
            {
                // 작업표준서 목록을 구한다
                DWK01_LST();
            }
        }
        #endregion

        #region devGrid_RowUpdating
        /// <summary>
        /// devGrid_RowUpdating
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataUpdatingEventArgs</param>
        protected void devGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_BANCD", e.NewValues["F_BANCD"] == null ? "" : e.NewValues["F_BANCD"].ToString());
            oParamDic.Add("F_LINECD", e.NewValues["F_LINECD"] == null ? "" : e.NewValues["F_LINECD"].ToString());
            oParamDic.Add("F_WORKCD", e.NewValues["F_WORKCD"] == null ? "" : e.NewValues["F_WORKCD"].ToString());
            oParamDic.Add("F_ITEMCD", e.NewValues["F_ITEMCD"] == null ? "" : e.NewValues["F_ITEMCD"].ToString());
            oParamDic.Add("F_DOCCD", DevExpressLib.devComboBox(devGrid, "ddlDOCCD").SelectedItem.Value.ToString());
            oParamDic.Add("F_MAKEDATE", e.NewValues["F_MAKEDATE"] == null ? "" : e.NewValues["F_MAKEDATE"].ToString());
            oParamDic.Add("F_DOCNO", e.NewValues["F_DOCNO"] == null ? "" : e.NewValues["F_DOCNO"].ToString());
            oParamDic.Add("F_DESC", e.NewValues["F_DESC"] == null ? "" : e.NewValues["F_DESC"].ToString());
            oParamDic.Add("F_USER", gsUSERID);
            oParamDic.Add("F_WORKCDS", hidWorkcd.Text);
            bool bExecute = false;

            using (WKSDBiz biz = new WKSDBiz())
            {
                bExecute = biz.PROC_DWK01_UPD(oParamDic);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "수정 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }
            else
            {
                devGrid.CancelEdit();
            }

            e.Cancel = true;

            if (true == bExecute)
            {
                // 작업표준서 목록을 구한다
                DWK01_LST();
            }
        }
        #endregion

        #region devGrid_RowDeleting
        /// <summary>
        /// devGrid_RowDeleting
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataDeletingEventArgs</param>
        protected void devGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_BANCD", e.Values["F_BANCD"] == null ? "" : e.Values["F_BANCD"].ToString());
            oParamDic.Add("F_LINECD", e.Values["F_LINECD"] == null ? "" : e.Values["F_LINECD"].ToString());
            oParamDic.Add("F_WORKCD", e.Values["F_WORKCD"] == null ? "" : e.Values["F_WORKCD"].ToString());
            oParamDic.Add("F_ITEMCD", e.Values["F_ITEMCD"] == null ? "" : e.Values["F_ITEMCD"].ToString());
            oParamDic.Add("F_DOCCD", e.Values["F_DOCCD"] == null ? "" : e.Values["F_DOCCD"].ToString());
            oParamDic.Add("F_USER", gsUSERID);
            bool bExecute = false;

            using (WKSDBiz biz = new WKSDBiz())
            {
                bExecute = biz.PROC_DWK01_DEL(oParamDic);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }

            e.Cancel = true;

            if (true == bExecute)
            {
                // 작업표준서 목록을 구한다
                DWK01_LST();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 작업표준서등록정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}