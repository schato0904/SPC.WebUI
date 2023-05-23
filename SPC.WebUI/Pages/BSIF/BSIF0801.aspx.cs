using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.BSIF.Biz;
using DevExpress.Web;
using System.Data.Common;


namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0801 : WebUIBasePage
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
                // 라인목록을 구한다
                QCDLEVEL_LST();
            }
            
            // Grid Columns Sum Width
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
            QCDPLANT_LST(this.ddlPLANT);
            QCDJUYA_LST(this.ddlJUYA);
            
            //AspxCombox_DataBind(this.ddlPLANT, "AAG1");
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공장정보를 불러온다
        void QCDPLANT_LST(DevExpress.Web.ASPxComboBox comboBox)
        {
            AspxCombox_DataBind(comboBox, "AAG1");
            comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
           
        }
        #endregion

        #region 주야정보를 불러온다
        void QCDJUYA_LST(DevExpress.Web.ASPxComboBox comboBox)
        {
            AspxCombox_DataBind(comboBox, "AAG2");
            comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });

        }
        #endregion

        #region 숙련도 정보를 구한다
        void QCDLEVEL_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_PLANT", (this.ddlPLANT.Value ?? "").ToString());
                oParamDic.Add("F_JUYA", (this.ddlJUYA.Value ?? "").ToString());


                ds = biz.QCDLEVEL_LST(oParamDic, out errMsg);
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
                                case "ddlPLANT":      //공장코드
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_PLANT").ToString();
                                    break;
                                case "ddlJUYA":      //주야간구분
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_JUYA").ToString();
                                    break;
                            }
                        }
                    }

                    devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });

                    if (devComboBox.Items.FindByValue(rowValue) != null)
                        devComboBox.SelectedIndex = devComboBox.Items.FindByValue(rowValue).Index;
                    //AspxCombox_DataBind(devComboBox, "AAG1");
                }
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
            e.NewValues["F_STATUS"] = true;            
        }
        #endregion

        #region devGrid_CellEditorInitialize
        /// <summary>
        /// devGrid_CellEditorInitialize
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_PLANT")) return;

            if (e.Column.FieldName.Equals("F_PLANT"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                QCDPLANT_LST(comboBox);
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
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

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 라인목록을 구한다
            QCDLEVEL_LST();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 숙련도공정현황정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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
            
        }
        #endregion

        protected void devGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            List<DbParameter> oParamDB = new List<DbParameter>();
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_PLANT", DevExpressLib.devTextBox(devGrid, "hidPLANT").Text));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_SEQ", e.NewValues["F_SEQ"] == null ? "" : e.NewValues["F_SEQ"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_WORKNM", e.NewValues["F_WORKNM"] == null ? "" : e.NewValues["F_WORKNM"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_ITEMNM", e.NewValues["F_ITEMNM"] == null ? "" : e.NewValues["F_ITEMNM"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_WORKMAN", e.NewValues["F_WORKMAN"] == null ? "" : e.NewValues["F_WORKMAN"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_LEVEL", e.NewValues["F_LEVEL"] == null ? "" : e.NewValues["F_LEVEL"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_JUYA", e.NewValues["F_JUYA"] == null ? "" : e.NewValues["F_JUYA"].ToString()));
            if (Session["data"] != null)
            {
                oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_IMAGE", Session["data"]));
            }
            Session["data"] = null;

            string[] oSPs = new string[] { "USP_QCDLEVEL_INS" };
            object[] _oParamDic = new object[1];
            _oParamDic[0] = (object)(oParamDB.ToArray());

            string errMsg = String.Empty;
            bool bExecute = false;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.QCDLEVEL_IMAGE_UPD(oSPs, _oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                procResult = new string[] { "99", String.Format("등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r에러내용 : {0}", errMsg) };
                devCallback.JSProperties["cpResultCode"] = procResult[0];
                devCallback.JSProperties["cpResultMsg"] = procResult[1];
            }
            else if (!bExecute)
            {
                procResult = new string[] { "99", "수정 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
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
                QCDLEVEL_LST();
            }
        }

        protected void devGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
            int editingRowVisibleIndex = grid.EditingRowVisibleIndex;

            List<DbParameter> oParamDB = new List<DbParameter>();
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_PLANT", DevExpressLib.devTextBox(devGrid, "hidPLANT").Text));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_NO", grid.GetRowValues(editingRowVisibleIndex, "F_NO").ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_SEQ", e.NewValues["F_SEQ"] == null ? "" : e.NewValues["F_SEQ"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_WORKNM", e.NewValues["F_WORKNM"] == null ? "" : e.NewValues["F_WORKNM"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_ITEMNM", e.NewValues["F_ITEMNM"] == null ? "" : e.NewValues["F_ITEMNM"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_WORKMAN", e.NewValues["F_WORKMAN"] == null ? "" : e.NewValues["F_WORKMAN"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_LEVEL", e.NewValues["F_LEVEL"] == null ? "" : e.NewValues["F_LEVEL"].ToString()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_STATUS", e.NewValues["F_STATUS"] == null ? "" : e.NewValues["F_STATUS"].ToString().Trim()));
            oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_JUYA", e.NewValues["F_JUYA"] == null ? "" : e.NewValues["F_JUYA"].ToString()));
            if (Session["data"] != null)
            {
                oParamDB.Add(new System.Data.SqlClient.SqlParameter("@F_IMAGE", Session["data"]));
            }
            Session["data"] = null;

            string[] oSPs = new string[] { "USP_QCDLEVEL_UPD" };
            object[] _oParamDic = new object[1];
            _oParamDic[0] = (object)(oParamDB.ToArray());

            string errMsg = String.Empty;
            bool bExecute = false;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.QCDLEVEL_IMAGE_UPD(oSPs, _oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                procResult = new string[] { "99", String.Format("등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r에러내용 : {0}", errMsg) };
                devCallback.JSProperties["cpResultCode"] = procResult[0];
                devCallback.JSProperties["cpResultMsg"] = procResult[1];
            }
            else if (!bExecute)
            {
                procResult = new string[] { "99", "수정 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
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
                QCDLEVEL_LST();
            }
        }

        protected void devGrid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridViewEditFormEventArgs e)
        {
            string param = Request.Params.Get("__CALLBACKPARAM");
            if (!String.IsNullOrEmpty(param))
            {
                if (!param.Contains("CANCELEDIT"))
                {
                    string errMsg = String.Empty;
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
                    DevExpress.Web.ASPxComboBox ddlComboBox = null;
                    DevExpress.Web.ASPxComboBox ddlComboBox2 = null;
                    DevExpress.Web.ASPxTextBox txtBox = null;
                    string sCOMPCD = String.Empty;
                    string sFACTCD = String.Empty;

                    int editingRowVisibleIndex = grid.EditingRowVisibleIndex;

                    ddlComboBox = grid.FindEditFormTemplateControl("ddlPLANT") as DevExpress.Web.ASPxComboBox;
                    ddlComboBox2 = grid.FindEditFormTemplateControl("ddlJUYA") as DevExpress.Web.ASPxComboBox;

                    AspxCombox_DataBind(ddlComboBox, "AAG1");
                    AspxCombox_DataBind(ddlComboBox2, "AAG2");

                }
            }
        }

        #region uploadFILEIMAGE_FileUploadComplete
        /// <summary>
        /// uploadFILEIMAGE_FileUploadComplete
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">FileUploadCompleteEventArgs</param>
        protected void uploadFILEIMAGE_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if ((sender as DevExpress.Web.ASPxUploadControl).IsValid)
                Session["data"] = (sender as ASPxUploadControl).FileBytes;
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
                        devCallback.JSProperties["cpMODEL"] = "";

                        bExecute = true;
                    }
                    break;
                case "MEAINSP": //검사항목
                    devCallback.JSProperties["cpIDCD"] = "MEAINSPCD";
                    devCallback.JSProperties["cpIDNM"] = "INSPDETAIL";

                    using (CommonBiz biz = new CommonBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_MEAINSPCD", oParams[1]);
                        oParamDic.Add("F_STATUS", "1");
                        ds = biz.GetQCD33_POPUP_LST(oParamDic, out errMsg);
                    }

                    if (ds.Tables[0].Rows.Count.Equals(1))
                    {
                        devCallback.JSProperties["cpCODE"] = ds.Tables[0].Rows[0]["F_MEAINSPCD"].ToString();
                        devCallback.JSProperties["cpTEXT"] = ds.Tables[0].Rows[0]["F_INSPDETAIL"].ToString();

                        bExecute = true;
                    }
                    break;
            }

            if (!bExecute)
            {
                devCallback.JSProperties["cpCODE"] = "";
                devCallback.JSProperties["cpTEXT"] = "";
                devCallback.JSProperties["cpMODEL"] = "";
            }
        }
        #endregion

        protected void devGrid_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            oParamDic.Clear();
            oParamDic.Add("F_PLANT", e.Values["F_PLANT"] == null ? "" : e.Values["F_PLANT"].ToString());
            oParamDic.Add("F_NO", e.Values["F_NO"] == null ? "" : e.Values["F_NO"].ToString());
            
            bool bExecute = false;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.QCDLEVEL_DEL(oParamDic);
            }

           if (!bExecute)
            {
                procResult = new string[] { "99", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }

           e.Cancel = true;

            if (true == bExecute)
            {
                // 작업표준서 목록을 구한다
                QCDLEVEL_LST();
            }

        }

        #endregion



    }
}