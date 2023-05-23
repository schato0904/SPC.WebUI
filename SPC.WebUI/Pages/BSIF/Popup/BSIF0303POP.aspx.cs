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
using System.Text;

namespace SPC.WebUI.Pages.BSIF.Popup
{
    public partial class BSIF0303POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        string[] keyFields = new string[3];
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

            if ((String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false")))
            {
                // 검사기준목록을 구한다
                GetQCD34_LST();
            }

            hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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
        {
            keyFields = Request.QueryString.Get("keyFields").Split('|');
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

        #region 검사기준목록을 구한다
        void GetQCD34_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_ITEMCD", keyFields[0]);
                oParamDic.Add("F_INSPCD", keyFields[4]);
                oParamDic.Add("F_WORKCD", keyFields[3]);

                ds = biz.GetQCD34_POPUP_LST(oParamDic, out errMsg);
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

        #region 계측기목록을 구한다
        DataSet GetQC30_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MEASURECD", "");
                ds = biz.GetQC30_LST(oParamDic, out errMsg);
            }

            return ds;
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

        #region EditForm ASPxRadioButtonList DataBind
        /// <summary>
        /// ASPxRadioButtonList_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void ASPxRadioButtonList_DataBind(DevExpress.Web.ASPxGridView grid, string ComboBoxID, string CommonCode)
        {
            DevExpress.Web.ASPxRadioButtonList rdoButtonList = grid.FindEditFormTemplateControl(ComboBoxID) as DevExpress.Web.ASPxRadioButtonList;
            rdoButtonList.TextField = String.Format("COMMNM{0}", gsLANGTP);
            rdoButtonList.ValueField = "COMMCD";
            rdoButtonList.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
        }
        #endregion

        #region ASPxCheckBox_Checked
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="CheckBoxID">CheckBoxID</param>
        void ASPxCheckBox_Checked(DevExpress.Web.ASPxGridView grid, string CheckBoxID, string ColumnID, int editingRowVisibleIndex)
        {
            DevExpress.Web.ASPxCheckBox chkBOX = grid.FindEditFormTemplateControl(CheckBoxID) as DevExpress.Web.ASPxCheckBox;
            chkBOX.Checked = grid.GetRowValues(editingRowVisibleIndex, ColumnID).ToString().Equals("1");
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
            // 검사기준목록을 구한다
            GetQCD34_LST();
            //devGrid.DataBind();
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
            string[] sTooltipFields = { "F_ITEMNM", "F_BANNM", "F_LINENM", "F_WORKNM" };

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

            // Zero Setting
            if (e.Column.FieldName.Equals("F_ZERO"))
            {
                e.DisplayText = e.Value.ToString().Equals("1") ? "Yes" : "No";
                return;
            }

            // 측정제외
            if (e.Column.FieldName.Equals("F_MEASYESNO"))
            {
                e.DisplayText = e.Value.ToString().Equals("1") ? "Yes" : "No";
                return;
            }

            //// 검사규격 Double 변환
            //string[] sConvertFields = { "F_STANDARD", "F_MIN", "F_MAX", "F_UCLX", "F_LCLX", "F_UCLR", "F_TMAX", "F_TMIN" };

            //bool bConvertFields = false;

            //foreach (string sConvertField in sConvertFields)
            //{
            //    if (e.Column.FieldName.Equals(sConvertField))
            //    {
            //        bConvertFields = true;
            //        break;
            //    }
            //}

            //if (!bConvertFields) return;

            //double resultValue = 0.0;

            //foreach (string sConvertField in sConvertFields)
            //{
            //    if (e.Column.FieldName.Equals(sConvertField))
            //    {
            //        if (e.Value != null && !String.IsNullOrEmpty(e.Value.ToString()))
            //        {
            //            if (double.TryParse(e.Value.ToString(), out resultValue))
            //                e.DisplayText = Math.Round(resultValue, 1).ToString("0.0");
            //        }
            //    }
            //}
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
                    
                    DevExpressLib.devTextBox(grid, "txtITEMCD").Text = devGrid.GetRowValues(0, "F_ITEMCD").ToString();
                    DevExpressLib.devTextBox(grid, "txtITEMNM").Text = devGrid.GetRowValues(0, "F_ITEMNM").ToString();
                    DevExpressLib.devTextBox(grid, "txtMODELNM").Text = devGrid.GetRowValues(0, "F_MODELNM").ToString();
                    DevExpressLib.devRadioButtonList(grid, "rdoDATAUNIT").SelectedIndex = 0;
                    DevExpressLib.devRadioButtonList(grid, "rdoRESULTGUBUN").SelectedIndex = 1;
                    DevExpressLib.devRadioButtonList(grid, "rdoACCEPT").SelectedIndex = 1;
                    DevExpressLib.devRadioButtonList(grid, "rdoHIPISNG").SelectedIndex = 1;
                    DevExpressLib.devRadioButtonList(grid, "rdoSAMPLECHK").SelectedIndex = 1;
                    DevExpressLib.devSpinEdit(grid, "txtSIRYO").Text = "1";
                    DevExpressLib.devComboBox(grid, "ddlRANK").SelectedIndex = 2;
                    DevExpressLib.devSpinEdit(grid, "txtDEFECTS_N").Text = "1";
                    DevExpressLib.devTextBox(grid, "txtDISPLAYNO").ClientSideEvents.Init = "fn_OnControlDisableBox";
                    DevExpressLib.devSpinEdit(grid, "txtSAMPLENO").ClientSideEvents.Init = "fn_OnControlDisableBox";
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
                    AspxCombox_DataBind(grid, "ddlINSPCD", "AAC5");

                    // QC검사주기
                    AspxCombox_DataBind(grid, "ddlQCYCLECD", "AAC3");

                    // 현장검사주기
                    AspxCombox_DataBind(grid, "ddlJCYCLECD", "AAC3");

                    // 품질수준
                    AspxCombox_DataBind(grid, "ddlRANK", "AAD2");

                    // 상,하한편측
                    AspxCombox_DataBind(grid, "ddlSINGLECHK", "AAD7");

                    // 공차기호
                    AspxCombox_DataBind(grid, "ddlUNIT", "AAC1");

                    // 관리한계기준
                    ASPxRadioButtonList_DataBind(grid, "rdoACCEPT", "AAC2");

                    // 설비규격공차(장비기호) - 사용안함

                    // 계측기
                    AspxCombox_DataBind(grid, "ddlAIRCK", "AAD8");

                    // 측정포트
                    AspxCombox_DataBind(grid, "ddlPORT", "AAD4");

                    // 측정방법
                    AspxCombox_DataBind(grid, "ddlGETDATA", "AAD5");

                    // 설비구분
                    AspxCombox_DataBind(grid, "ddlLOADTF", "AAD6");

                    if (!param.Contains("ADDNEWROW"))
                    {
                        //chkGETTYPE
                        int editingRowVisibleIndex = grid.EditingRowVisibleIndex;

                        if (editingRowVisibleIndex >= 0)
                        {
                            // Zero Setting
                            ASPxCheckBox_Checked(grid, "chkZERO", "F_ZERO", editingRowVisibleIndex);

                            // 측정제외
                            ASPxCheckBox_Checked(grid, "chkMEASYESNO", "F_MEASYESNO", editingRowVisibleIndex);

                            // 중요항목
                            ASPxCheckBox_Checked(grid, "chkIMPORT", "F_IMPORT", editingRowVisibleIndex);

                            // 수작업여부
                            ASPxCheckBox_Checked(grid, "chkGETTYPE", "F_GETTYPE", editingRowVisibleIndex);
                        }
                    }
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
                                case "ddlINSPCD":       // 검사분류
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_INSPCD").ToString();
                                    break;
                                case "ddlQCYCLECD":      // QC검사주기
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_QCYCLECD").ToString();
                                    break;
                                case "ddlJCYCLECD":      // 현장검사주기
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_JCYCLECD").ToString();
                                    break;
                                case "ddlRANK":         // 품질수준
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_RANK").ToString();
                                    break;
                                case "ddlSINGLECHK":    // 상,하한편측
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_SINGLECHK").ToString();
                                    break;
                                case "ddlUNIT":         // 공차기호
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_UNIT").ToString();
                                    break;
                                case "ddlAIRCK":        // 계측기
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_AIRCK").ToString();
                                    break;
                                case "ddlPORT":         // 측정포트
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_PORT").ToString();
                                    break;
                                case "ddlGETDATA":      // 측정방법
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_GETDATA").ToString();
                                    break;
                                case "ddlLOADTF":       // 설비구분
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_LOADTF").ToString();
                                    break;
                            }
                        }
                    }

                    devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = devComboBox.ID.Equals("ddlINSPCD") ? "선택하세요" : "선택안함", Value = "", Selected = true });

                    if (devComboBox.Items.FindByValue(rowValue) != null)
                        devComboBox.SelectedIndex = devComboBox.Items.FindByValue(rowValue).Index;
                }
            }
        }
        #endregion

        #region rdoButtonList_DataBound
        /// <summary>
        /// rdoButtonList_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void rdoButtonList_DataBound(object sender, EventArgs e)
        {
            string param = Request.Params.Get("__CALLBACKPARAM");
            if (!String.IsNullOrEmpty(param))
            {
                if (!param.Contains("CANCELEDIT") && !param.Contains("ADDNEWROW"))
                {
                    int editingRowVisibleIndex = devGrid.EditingRowVisibleIndex;

                    if (editingRowVisibleIndex >= 0)
                    {
                        string rowValue = String.Empty;

                        DevExpress.Web.ASPxRadioButtonList devRadioButtonList = sender as DevExpress.Web.ASPxRadioButtonList;

                        switch (devRadioButtonList.ID)
                        {
                            case "rdoDATAUNIT":     // ??
                                rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_DATAUNIT").ToString();
                                break;
                            case "rdoACCEPT":       // 관리한계기준
                                rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_ACCEPT_SEQ").ToString();
                                break;
                            case "rdoRESULTGUBUN":  // 성적서 출력 유무
                                rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_RESULTGUBUN").ToString();
                                break;
                            case "rdoHIPISNG":      // HIPIS 전송
                                rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_HIPISNG").ToString();
                                rowValue = !String.IsNullOrEmpty(rowValue) ? rowValue : "0";
                                break;
                            case "rdoSAMPLECHK":    // 초중종관리
                                rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_SAMPLECHK").ToString();
                                rowValue = !String.IsNullOrEmpty(rowValue) ? rowValue : "0";
                                break;
                        }

                        if (!String.IsNullOrEmpty(rowValue))
                        {
                            if (devRadioButtonList.Items.FindByValue(rowValue) != null)
                                devRadioButtonList.SelectedIndex = devRadioButtonList.Items.FindByValue(rowValue).Index;
                        }
                    }
                }
            }
        }
        #endregion

        #region txtMEAINSPCD_Init
        /// <summary>
        /// txtMEAINSPCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtMEAINSPCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupMeainspSearch()");
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWorkSearch('INS')");
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

            devCallback.JSProperties["cpBANCD"] = "";
            devCallback.JSProperties["cpLINECD"] = "";
            
            switch (oParams[0])
            {
                case "MEAINSP":  // 검상항목
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
                case "WORK":      // 공정
                    devCallback.JSProperties["cpIDCD"] = "WORKCD";
                    devCallback.JSProperties["cpIDNM"] = "WORKNM";

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

                        bExecute = true;
                    }
                    break;
            }

            if (!bExecute)
            {
                devCallback.JSProperties["cpCODE"] = "";
                devCallback.JSProperties["cpTEXT"] = "";
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
            oParamDic.Add("F_ITEMCD", e.NewValues["F_ITEMCD"] == null ? "" : e.NewValues["F_ITEMCD"].ToString());
            oParamDic.Add("F_INSPCD", DevExpressLib.devComboBox(devGrid, "ddlINSPCD").SelectedItem.Value.ToString());
            oParamDic.Add("F_STANDARD", e.NewValues["F_STANDARD"] == null ? "" : e.NewValues["F_STANDARD"].ToString());
            oParamDic.Add("F_RANK", DevExpressLib.devComboBox(devGrid, "ddlRANK").SelectedItem.Value.ToString());
            oParamDic.Add("F_MIN", e.NewValues["F_MIN"] == null ? "" : e.NewValues["F_MIN"].ToString());
            oParamDic.Add("F_MAX", e.NewValues["F_MAX"] == null ? "" : e.NewValues["F_MAX"].ToString());
            oParamDic.Add("F_MEAINSPCD", e.NewValues["F_MEAINSPCD"] == null ? "" : e.NewValues["F_MEAINSPCD"].ToString());
            oParamDic.Add("F_WORKCD", e.NewValues["F_WORKCD"] == null ? "" : e.NewValues["F_WORKCD"].ToString());
            oParamDic.Add("F_MACHCD", "");    // 사용안함
            oParamDic.Add("F_UCLX", e.NewValues["F_UCLX"] == null ? "" : e.NewValues["F_UCLX"].ToString());
            oParamDic.Add("F_LCLX", e.NewValues["F_LCLX"] == null ? "" : e.NewValues["F_LCLX"].ToString());
            oParamDic.Add("F_UCLR", e.NewValues["F_UCLR"] == null ? "" : e.NewValues["F_UCLR"].ToString());
            oParamDic.Add("F_LCLR", e.NewValues["F_LCLR"] == null ? "" : e.NewValues["F_LCLR"].ToString());
            oParamDic.Add("F_SIRYO", e.NewValues["F_SIRYO"] == null ? "" : e.NewValues["F_SIRYO"].ToString());
            oParamDic.Add("F_ZERO", !DevExpressLib.devCheckBox(devGrid, "chkZERO").Checked ? "0" : "1");
            oParamDic.Add("F_UNIT", DevExpressLib.devComboBox(devGrid, "ddlUNIT").SelectedItem.Value.ToString());
            oParamDic.Add("F_ACCEPT_SEQ", DevExpressLib.devRadioButtonList(devGrid, "rdoACCEPT").SelectedItem.Value.ToString());
            oParamDic.Add("F_GETDATA", DevExpressLib.devComboBox(devGrid, "ddlGETDATA").SelectedItem.Value.ToString());
            //oParamDic.Add("F_GETCNT", "");    // 사용안함
            oParamDic.Add("F_PORT", DevExpressLib.devComboBox(devGrid, "ddlPORT").SelectedItem.Value.ToString());
            oParamDic.Add("F_CHANNEL", e.NewValues["F_CHANNEL"] == null ? "" : e.NewValues["F_CHANNEL"].ToString());
            oParamDic.Add("F_GETTIME", "0");    // 사용안함
            oParamDic.Add("F_GETTYPE", !DevExpressLib.devCheckBox(devGrid, "chkGETTYPE").Checked ? "0" : "1");
            oParamDic.Add("F_ZIG", e.NewValues["F_ZIG"] == null ? "" : e.NewValues["F_ZIG"].ToString());
            oParamDic.Add("F_DATAUNIT", DevExpressLib.devRadioButtonList(devGrid, "rdoDATAUNIT").SelectedItem.Value.ToString());
            //oParamDic.Add("F_SINGLE", "");    // 사용안함
            //oParamDic.Add("F_HOMSU", "");    // 사용안함
            oParamDic.Add("F_DEFECTS_N", e.NewValues["F_DEFECTS_N"] == null ? "" : e.NewValues["F_DEFECTS_N"].ToString());
            //oParamDic.Add("F_DISPLAYNO", e.NewValues["F_DISPLAYNO"] == null ? "" : e.NewValues["F_DISPLAYNO"].ToString());
            //oParamDic.Add("F_PRINT", "");    // 사용안함
            oParamDic.Add("F_AIRCK", DevExpressLib.devComboBox(devGrid, "ddlAIRCK").SelectedItem.Value.ToString());
            //oParamDic.Add("F_DANGA", "");    // 사용안함
            oParamDic.Add("F_LOADTF", DevExpressLib.devComboBox(devGrid, "ddlLOADTF").SelectedItem.Value.ToString());
            //oParamDic.Add("F_BUHO", "");    // 사용안함
            oParamDic.Add("F_MEASYESNO", !DevExpressLib.devCheckBox(devGrid, "chkMEASYESNO").Checked ? "0" : "1");
            oParamDic.Add("F_SAMPLECHK", DevExpressLib.devRadioButtonList(devGrid, "rdoSAMPLECHK").SelectedItem.Value.ToString());
            //oParamDic.Add("F_SAMPLENO", e.NewValues["F_SAMPLENO"] == null ? "" : e.NewValues["F_SAMPLENO"].ToString());
            oParamDic.Add("F_RESULTGUBUN", DevExpressLib.devRadioButtonList(devGrid, "rdoRESULTGUBUN").SelectedItem.Value.ToString());
            oParamDic.Add("F_IMPORT", !DevExpressLib.devCheckBox(devGrid, "chkIMPORT").Checked ? "0" : "1");
            oParamDic.Add("F_IMAGESEQ", e.NewValues["F_IMAGESEQ"] == null ? "" : e.NewValues["F_IMAGESEQ"].ToString());
            //oParamDic.Add("F_POINTX", "");    // 사용안함
            //oParamDic.Add("F_POINTY", "");    // 사용안함
            oParamDic.Add("F_JCYCLECD", DevExpressLib.devComboBox(devGrid, "ddlJCYCLECD").SelectedItem.Value.ToString());
            oParamDic.Add("F_QCYCLECD", DevExpressLib.devComboBox(devGrid, "ddlQCYCLECD").SelectedItem.Value.ToString());
            oParamDic.Add("F_HCOUNT", e.NewValues["F_HCOUNT"] == null ? "" : e.NewValues["F_HCOUNT"].ToString());
            oParamDic.Add("F_SINGLECHK", DevExpressLib.devComboBox(devGrid, "ddlSINGLECHK").SelectedItem.Value.ToString());

            oParamDic.Add("F_MEASCD1", e.NewValues["F_MEASCD1"] == null ? "" : e.NewValues["F_MEASCD1"].ToString());
            oParamDic.Add("F_MEASURE", e.NewValues["F_MEASURE"] == null ? "" : e.NewValues["F_MEASURE"].ToString());
            oParamDic.Add("F_RESULTSTAND", e.NewValues["F_RESULTSTAND"] == null ? "" : e.NewValues["F_RESULTSTAND"].ToString());
            oParamDic.Add("F_TMAX", e.NewValues["F_TMAX"] == null ? "" : e.NewValues["F_TMAX"].ToString());
            oParamDic.Add("F_TMIN", e.NewValues["F_TMIN"] == null ? "" : e.NewValues["F_TMIN"].ToString());
            oParamDic.Add("F_HIPISNG", DevExpressLib.devRadioButtonList(devGrid, "rdoHIPISNG").SelectedItem.Value.ToString());
            oParamDic.Add("F_USER", gsUSERID);

            bool bExecute = false;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.QCD34_PROC_INS(oParamDic);
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
                // 검사기준목록을 구한다
                GetQCD34_LST();
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
            oParamDic.Add("F_ITEMCD", e.NewValues["F_ITEMCD"] == null ? "" : e.NewValues["F_ITEMCD"].ToString());
            oParamDic.Add("F_INSPCD", DevExpressLib.devComboBox(devGrid, "ddlINSPCD").SelectedItem.Value.ToString());
            oParamDic.Add("F_SERIALNO", e.NewValues["F_SERIALNO"] == null ? "" : e.NewValues["F_SERIALNO"].ToString());
            oParamDic.Add("F_STANDARD", e.NewValues["F_STANDARD"] == null ? "" : e.NewValues["F_STANDARD"].ToString());
            oParamDic.Add("F_RANK", DevExpressLib.devComboBox(devGrid, "ddlRANK").SelectedItem.Value.ToString());
            oParamDic.Add("F_MIN", e.NewValues["F_MIN"] == null ? "" : e.NewValues["F_MIN"].ToString());
            oParamDic.Add("F_MAX", e.NewValues["F_MAX"] == null ? "" : e.NewValues["F_MAX"].ToString());
            oParamDic.Add("F_MEAINSPCD", e.NewValues["F_MEAINSPCD"] == null ? "" : e.NewValues["F_MEAINSPCD"].ToString());
            oParamDic.Add("F_WORKCD", e.NewValues["F_WORKCD"] == null ? "" : e.NewValues["F_WORKCD"].ToString());
            oParamDic.Add("F_MACHCD", "");    // 사용안함
            oParamDic.Add("F_UCLX", e.NewValues["F_UCLX"] == null ? "" : e.NewValues["F_UCLX"].ToString());
            oParamDic.Add("F_LCLX", e.NewValues["F_LCLX"] == null ? "" : e.NewValues["F_LCLX"].ToString());
            oParamDic.Add("F_UCLR", e.NewValues["F_UCLR"] == null ? "" : e.NewValues["F_UCLR"].ToString());
            oParamDic.Add("F_LCLR", e.NewValues["F_LCLR"] == null ? "" : e.NewValues["F_LCLR"].ToString());
            oParamDic.Add("F_SIRYO", e.NewValues["F_SIRYO"] == null ? "" : e.NewValues["F_SIRYO"].ToString());
            oParamDic.Add("F_ZERO", !DevExpressLib.devCheckBox(devGrid, "chkZERO").Checked ? "0" : "1");
            oParamDic.Add("F_UNIT", DevExpressLib.devComboBox(devGrid, "ddlUNIT").SelectedItem.Value.ToString());
            oParamDic.Add("F_ACCEPT_SEQ", DevExpressLib.devRadioButtonList(devGrid, "rdoACCEPT").SelectedItem.Value.ToString());
            oParamDic.Add("F_GETDATA", DevExpressLib.devComboBox(devGrid, "ddlGETDATA").SelectedItem.Value.ToString());
            //oParamDic.Add("F_GETCNT", "");    // 사용안함
            oParamDic.Add("F_PORT", DevExpressLib.devComboBox(devGrid, "ddlPORT").SelectedItem.Value.ToString());
            oParamDic.Add("F_CHANNEL", e.NewValues["F_CHANNEL"] == null ? "" : e.NewValues["F_CHANNEL"].ToString());
            oParamDic.Add("F_GETTIME", "0");    // 사용안함
            oParamDic.Add("F_GETTYPE", !DevExpressLib.devCheckBox(devGrid, "chkGETTYPE").Checked ? "0" : "1");
            oParamDic.Add("F_ZIG", e.NewValues["F_ZIG"] == null ? "" : e.NewValues["F_ZIG"].ToString());
            oParamDic.Add("F_DATAUNIT", DevExpressLib.devRadioButtonList(devGrid, "rdoDATAUNIT").SelectedItem.Value.ToString());
            //oParamDic.Add("F_SINGLE", "");    // 사용안함
            //oParamDic.Add("F_HOMSU", "");    // 사용안함
            oParamDic.Add("F_DEFECTS_N", e.NewValues["F_DEFECTS_N"] == null ? "" : e.NewValues["F_DEFECTS_N"].ToString());
            oParamDic.Add("F_DISPLAYNO", e.NewValues["F_DISPLAYNO"] == null ? "" : e.NewValues["F_DISPLAYNO"].ToString());
            //oParamDic.Add("F_PRINT", "");    // 사용안함
            oParamDic.Add("F_AIRCK", DevExpressLib.devComboBox(devGrid, "ddlAIRCK").SelectedItem.Value.ToString());
            //oParamDic.Add("F_DANGA", "");    // 사용안함
            oParamDic.Add("F_LOADTF", DevExpressLib.devComboBox(devGrid, "ddlLOADTF").SelectedItem.Value.ToString());
            //oParamDic.Add("F_BUHO", "");    // 사용안함
            oParamDic.Add("F_MEASYESNO", !DevExpressLib.devCheckBox(devGrid, "chkMEASYESNO").Checked ? "0" : "1");
            oParamDic.Add("F_SAMPLECHK", DevExpressLib.devRadioButtonList(devGrid, "rdoSAMPLECHK").SelectedItem.Value.ToString());
            oParamDic.Add("F_SAMPLENO", e.NewValues["F_SAMPLENO"] == null ? "" : e.NewValues["F_SAMPLENO"].ToString());
            oParamDic.Add("F_RESULTGUBUN", DevExpressLib.devRadioButtonList(devGrid, "rdoRESULTGUBUN").SelectedItem.Value.ToString());
            oParamDic.Add("F_IMPORT", !DevExpressLib.devCheckBox(devGrid, "chkIMPORT").Checked ? "0" : "1");
            oParamDic.Add("F_IMAGESEQ", e.NewValues["F_IMAGESEQ"] == null ? "" : e.NewValues["F_IMAGESEQ"].ToString());
            //oParamDic.Add("F_POINTX", "");    // 사용안함
            //oParamDic.Add("F_POINTY", "");    // 사용안함
            oParamDic.Add("F_JCYCLECD", DevExpressLib.devComboBox(devGrid, "ddlJCYCLECD").SelectedItem.Value.ToString());
            oParamDic.Add("F_QCYCLECD", DevExpressLib.devComboBox(devGrid, "ddlQCYCLECD").SelectedItem.Value.ToString());
            oParamDic.Add("F_HCOUNT", e.NewValues["F_HCOUNT"] == null ? "" : e.NewValues["F_HCOUNT"].ToString());
            oParamDic.Add("F_SINGLECHK", DevExpressLib.devComboBox(devGrid, "ddlSINGLECHK").SelectedItem.Value.ToString());
            oParamDic.Add("F_MEASCD1", e.NewValues["F_MEASCD1"] == null ? "" : e.NewValues["F_MEASCD1"].ToString());
            oParamDic.Add("F_MEASURE", e.NewValues["F_MEASURE"] == null ? "" : e.NewValues["F_MEASURE"].ToString());
            oParamDic.Add("F_RESULTSTAND", e.NewValues["F_RESULTSTAND"] == null ? "" : e.NewValues["F_RESULTSTAND"].ToString());
            oParamDic.Add("F_TMAX", e.NewValues["F_TMAX"] == null ? "" : e.NewValues["F_TMAX"].ToString());
            oParamDic.Add("F_TMIN", e.NewValues["F_TMIN"] == null ? "" : e.NewValues["F_TMIN"].ToString());
            oParamDic.Add("F_HIPISNG", DevExpressLib.devRadioButtonList(devGrid, "rdoHIPISNG").SelectedItem.Value.ToString());

            bool bExecute = false;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.QCD34_PROC_UPD(oParamDic);
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
                // 검사기준목록을 구한다
                GetQCD34_LST();
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
            string[] oSPs = new string[1];
            object[] oParameters = new object[1];

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ITEMCD", e.Values["F_ITEMCD"].ToString());
            oParamDic.Add("F_INSPCD", e.Values["F_INSPCD"].ToString());
            oParamDic.Add("F_SERIALNO", e.Values["F_SERIALNO"].ToString());
            oParamDic.Add("F_OUTMSG", "OUTPUT");

            oSPs[0] = "USP_QCD34_DEL_CHK";
            oParameters[0] = (object)oParamDic;

            bool bExecute = false;
            string resultMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.PROC_QCD73_BATCH(oSPs, oParameters, out oOutParamList, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                StringBuilder sb_OutMsg = new StringBuilder();
                foreach (Dictionary<string, object> _oOutParamDic in oOutParamList)
                {
                    foreach (KeyValuePair<string, object> _oOutPair in _oOutParamDic)
                    {
                        if (!String.IsNullOrEmpty(_oOutPair.Value.ToString()))
                            sb_OutMsg.AppendFormat("{0}", _oOutPair.Value);
                    }
                }

                if (!String.IsNullOrEmpty(sb_OutMsg.ToString()))
                {
                    procResult = new string[] { "2", sb_OutMsg.ToString() };
                }
                else
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", e.Values["F_ITEMCD"].ToString());
                    oParamDic.Add("F_INSPCD", e.Values["F_INSPCD"].ToString());
                    oParamDic.Add("F_SERIALNO", e.Values["F_SERIALNO"].ToString());

                    string[] oQuerys = { "USP_QCD34_DEL", "USP_QCD34B_DEL", "USP_QCD34A_INS"};
                    object[] oParams = new object[3];
                    oParams[0] = (object)oParamDic;
                    oParams[1] = (object)oParamDic;

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", e.Values["F_ITEMCD"].ToString());
                    oParamDic.Add("F_INSPCD", e.Values["F_INSPCD"].ToString());
                    oParamDic.Add("F_SERIALNO", e.Values["F_SERIALNO"].ToString());
                    oParamDic.Add("F_KEMARK", String.Format("Data Deleted by {0}", gsUSERNM));
                    oParamDic.Add("F_USER", gsUSERID);
                    oParams[2] = (object)oParamDic;

                    using (BSIFBiz biz = new BSIFBiz())
                    {
                        bExecute = biz.QCD34_PROC_DEL(oQuerys, oParams);
                    }

                    if (!bExecute)
                    {
                        procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                        devGrid.JSProperties["cpResultCode"] = procResult[0];
                        devGrid.JSProperties["cpResultMsg"] = procResult[1];
                    }

                    

                    if (true == bExecute)
                    {
                        // 검사기준목록을 구한다
                        GetQCD34_LST();
                    }
                }
            }

            e.Cancel = true;
        }
        #endregion

        #endregion
    }
}