using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.BSIF.Biz;
using SPC.Common.Biz;
using System.IO;
using System.Data.Odbc;
using System.Data.OleDb;
using DevExpress.Web;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0303_EXCEL : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        bool bMultiUpload = false;
        string sDATA_GBN = String.Empty;
        string sDATA_REVNO = String.Empty;
        string sATTFILENO = String.Empty;
        string sAttachUniqueID = String.Empty;
        string sCOMPCD = String.Empty;
        string strSaveFile = String.Empty;
        object[] oParameters = null;
        string[] sErrorMsg = null;
        int nFileIdx = 0;
        int nErrorCnt = 0;
        private readonly UploadingUtils uploadingUtils = new UploadingUtils();
        Dictionary<string, string> oFileList = new Dictionary<string, string>();
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

            //if ((String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false")))
            //{
            //    // 검사기준목록을 구한다
            //    GetQCD34_LST(ucPager.GetPageSize(), 1, false);
            //}

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

                devCallback.JSProperties["cpIDCD"] = "";
                devCallback.JSProperties["cpIDNM"] = "";
                devCallback.JSProperties["cpMODELNM"] = "";
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

        #region 검사기준목록을 구한다
        void GetQCD34_LST(string strpath)
        {
            string errMsg = String.Empty;

            devGrid.DataSource = ReadExcelData(strpath);

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
            string strParam = e.Parameters;
            if (strParam == "insert")
            {
                InsertDataSet((DataSet) devGrid.DataSource);


                
            }
            else
            {
                GetQCD34_LST(strParam);
                devGrid.DataBind();
            }
        }
        #endregion

        private void InsertDataSet(DataSet GridDS)
        {
        }

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

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(object comboBox, string CommonCode)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = comboBox as DevExpress.Web.ASPxComboBox;
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();

            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
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
            //string param = Request.Params.Get("__CALLBACKPARAM");
            //if (!String.IsNullOrEmpty(param))
            //{
            //    if (!param.Contains("CANCELEDIT"))
            //    {
            //        DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;

            //        DevExpressLib.devRadioButtonList(grid, "rdoDATAUNIT").SelectedIndex = 0;
            //        DevExpressLib.devRadioButtonList(grid, "rdoRESULTGUBUN").SelectedIndex = 1;
            //        DevExpressLib.devRadioButtonList(grid, "rdoACCEPT").SelectedIndex = 1;
            //        DevExpressLib.devRadioButtonList(grid, "rdoHIPISNG").SelectedIndex = 1;
            //        DevExpressLib.devRadioButtonList(grid, "rdoSAMPLECHK").SelectedIndex = 1;
            //        DevExpressLib.devComboBox(grid, "ddlGETDATA").SelectedIndex = 1;
            //        DevExpressLib.devSpinEdit(grid, "txtSIRYO").Text = "1";
            //        DevExpressLib.devComboBox(grid, "ddlRANK").SelectedIndex = 2;
            //        DevExpressLib.devSpinEdit(grid, "txtDEFECTS_N").Text = "1";
            //        DevExpressLib.devTextBox(grid, "txtDISPLAYNO").ClientSideEvents.Init = "fn_OnControlDisableBox";
            //    }
            //}
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
                    devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = devComboBox.ID.Equals("ddlINSPCD") ? "선택하세요" : "선택안함", Value = "", Selected = true });
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupItemSearch('INS')");
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWorkSearchForm()");
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
            devCallback.JSProperties["cpMODEL"] = "";
            devCallback.JSProperties["cpMODELNM"] = "";

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
                case "EXCEL":
                    devCallback.JSProperties["cpIDCD"] = "";
                    devCallback.JSProperties["cpIDNM"] = "";
                    
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(String.Format("./Export/{0}EXPORT.aspx?pBANCD={1}&pITEMCD={2}&pWORKCD={3}&pINSPCD={4}",
                        oParams[1],
                        GetBanCD(),
                        GetItemCD(),
                        GetWorkCD(),
                        GetInspectionCD()));
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
            oParamDic.Add("F_BANCD", DevExpressLib.devTextBox(devGrid, "txtBANCD").Text);
            oParamDic.Add("F_LINECD", DevExpressLib.devTextBox(devGrid, "txtLINECD").Text);
            oParamDic.Add("F_ITEMCD", DevExpressLib.devTextBox(devGrid, "txtITEMCD").Text);
            oParamDic.Add("F_INSPCD", DevExpressLib.devComboBox(devGrid, "ddlINSPCD").SelectedItem.Value.ToString());
            oParamDic.Add("F_STANDARD", DevExpressLib.devTextBox(devGrid, "txtSTANDARD").Text);
            oParamDic.Add("F_RANK", DevExpressLib.devComboBox(devGrid, "ddlRANK").SelectedItem.Value.ToString());
            oParamDic.Add("F_MIN", DevExpressLib.devTextBox(devGrid, "txtMIN").Text);
            oParamDic.Add("F_MAX", DevExpressLib.devTextBox(devGrid, "txtMAX").Text);
            oParamDic.Add("F_MEAINSPCD", DevExpressLib.devTextBox(devGrid, "txtMEAINSPCD").Text);
            oParamDic.Add("F_WORKCD", DevExpressLib.devTextBox(devGrid, "txtWORKCD").Text);
            oParamDic.Add("F_MACHCD", "");    // 사용안함
            oParamDic.Add("F_UCLX", DevExpressLib.devTextBox(devGrid, "txtUCLX").Text);
            oParamDic.Add("F_LCLX", DevExpressLib.devTextBox(devGrid, "txtLCLX").Text);
            oParamDic.Add("F_UCLR", DevExpressLib.devTextBox(devGrid, "txtUCLR").Text);
            oParamDic.Add("F_LCLR", "");
            oParamDic.Add("F_SIRYO", DevExpressLib.devSpinEdit(devGrid, "txtSIRYO").Text);
            oParamDic.Add("F_ZERO", !DevExpressLib.devCheckBox(devGrid, "chkZERO").Checked ? "0" : "1");
            oParamDic.Add("F_UNIT", DevExpressLib.devComboBox(devGrid, "ddlUNIT").SelectedItem.Value.ToString());
            oParamDic.Add("F_ACCEPT_SEQ", DevExpressLib.devRadioButtonList(devGrid, "rdoACCEPT").SelectedItem.Value.ToString());
            oParamDic.Add("F_GETDATA", DevExpressLib.devComboBox(devGrid, "ddlGETDATA").SelectedItem.Value.ToString());
            //oParamDic.Add("F_GETCNT", "");    // 사용안함
            oParamDic.Add("F_PORT", DevExpressLib.devComboBox(devGrid, "ddlPORT").SelectedItem.Value.ToString());
            oParamDic.Add("F_CHANNEL", DevExpressLib.devSpinEdit(devGrid, "txtCHANNEL").Text);
            oParamDic.Add("F_GETTIME", "0");    // 사용안함
            oParamDic.Add("F_GETTYPE", !DevExpressLib.devCheckBox(devGrid, "chkGETTYPE").Checked ? "0" : "1");
            oParamDic.Add("F_ZIG", DevExpressLib.devTextBox(devGrid, "txtZIG").Text);
            oParamDic.Add("F_DATAUNIT", DevExpressLib.devRadioButtonList(devGrid, "rdoDATAUNIT").SelectedItem.Value.ToString());
            //oParamDic.Add("F_SINGLE", "");    // 사용안함
            //oParamDic.Add("F_HOMSU", "");    // 사용안함
            oParamDic.Add("F_DEFECTS_N", DevExpressLib.devSpinEdit(devGrid, "txtDEFECTS_N").Text);
            //oParamDic.Add("F_DISPLAYNO", e.NewValues["F_DISPLAYNO"] == null ? "" : e.NewValues["F_DISPLAYNO"].ToString());
            //oParamDic.Add("F_PRINT", "");    // 사용안함
            oParamDic.Add("F_AIRCK", DevExpressLib.devComboBox(devGrid, "ddlAIRCK").SelectedItem.Value.ToString());
            //oParamDic.Add("F_DANGA", "");    // 사용안함
            oParamDic.Add("F_LOADTF", DevExpressLib.devComboBox(devGrid, "ddlLOADTF").SelectedItem.Value.ToString());
            //oParamDic.Add("F_BUHO", "");    // 사용안함
            oParamDic.Add("F_MEASYESNO", !DevExpressLib.devCheckBox(devGrid, "chkMEASYESNO").Checked ? "0" : "1");
            oParamDic.Add("F_SAMPLECHK", DevExpressLib.devRadioButtonList(devGrid, "rdoSAMPLECHK").SelectedItem.Value.ToString());
            //oParamDic.Add("F_SAMPLENO", "");    // 사용안함
            oParamDic.Add("F_RESULTGUBUN", DevExpressLib.devRadioButtonList(devGrid, "rdoRESULTGUBUN").SelectedItem.Value.ToString());
            oParamDic.Add("F_IMPORT", !DevExpressLib.devCheckBox(devGrid, "chkIMPORT").Checked ? "0" : "1");
            oParamDic.Add("F_IMAGESEQ", DevExpressLib.devTextBox(devGrid, "txtIMAGESEQ").Text);
            //oParamDic.Add("F_POINTX", "");    // 사용안함
            //oParamDic.Add("F_POINTY", "");    // 사용안함
            oParamDic.Add("F_JCYCLECD", DevExpressLib.devComboBox(devGrid, "ddlJCYCLECD").SelectedItem.Value.ToString());
            oParamDic.Add("F_QCYCLECD", DevExpressLib.devComboBox(devGrid, "ddlQCYCLECD").SelectedItem.Value.ToString());
            oParamDic.Add("F_HCOUNT", DevExpressLib.devSpinEdit(devGrid, "txtHCOUNT").Text);
            oParamDic.Add("F_SINGLECHK", DevExpressLib.devComboBox(devGrid, "ddlSINGLECHK").SelectedItem.Value.ToString());
            oParamDic.Add("F_MEASCD1", DevExpressLib.devTextBox(devGrid, "txtMEASCD1").Text);
            oParamDic.Add("F_MEASURE", DevExpressLib.devTextBox(devGrid, "txtMEASURE").Text);
            oParamDic.Add("F_RESULTSTAND", DevExpressLib.devTextBox(devGrid, "txtRESULTSTAND").Text);
            oParamDic.Add("F_TMAX", DevExpressLib.devTextBox(devGrid, "txtTMAX").Text);
            oParamDic.Add("F_TMIN", DevExpressLib.devTextBox(devGrid, "txtTMIN").Text);
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
                //GetQCD34_LST(ucPager.GetPageSize(), 1, false);
            }
        }

        
        #endregion

        #region ReadExcelData
        public DataSet ReadExcelData(string path)
        {
            DataSet result = new DataSet();
            DataTable dt = null;

            if (!File.Exists(path)) return result;

            try
            {
                string workSheet = "";
                string connectString = "";

                if (path.ToLower().EndsWith(".csv"))
                {
                    string filePath = path.Substring(0, path.LastIndexOf(@"\"));
                    string fileName = path.Substring(path.LastIndexOf(@"\") + 1);
                    OdbcConnection conn = null;
                    OdbcDataAdapter da = null;
                    OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();
                    builder["Driver"] = "Microsoft Text Driver (*.txt; *.csv)";
                    builder["dbq"] = filePath;
                    conn = new OdbcConnection(builder.ConnectionString);
                    conn.Open();

                    string sQry = string.Format(@"SELECT * FROM [{0}]", fileName);
                    da = new OdbcDataAdapter(sQry, conn);
                    da.Fill(result);
                }
                else
                {
                    OleDbConnection conn = null;
                    OleDbDataAdapter da = null;
                    connectString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\";Extended Properties=\"Excel 8.0;HDR=Yes\"", path);
                    conn = new OleDbConnection(connectString);
                    conn.Open();

                    // 시트명 가져오기 (첫 시트만 처리)
                    dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (DataRow row in dt.Rows)
                    {
                        workSheet = row["TABLE_NAME"].ToString();
                        if (workSheet.Substring(0, 1).Equals("'")) workSheet = workSheet.Substring(1, workSheet.Length - 2);
                        break;
                    }

                    // 시트명이 있는 경우 DataSet 생성
                    if (!workSheet.Equals(""))
                    {
                        string sQry = string.Format(@"SELECT * FROM [{0}]", workSheet);
                        da = new OleDbDataAdapter(sQry, conn);
                        da.Fill(result);
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            return result;
        }
        #endregion

        #region devUploader_FileUploadComplete
        protected void devUploader_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                e.CallbackData = SavePostedFiles(e.UploadedFile);
            }
            catch (Exception ex)
            {
                sErrorMsg[nErrorCnt] = ex.Message;
                nErrorCnt++;
                e.IsValid = false;
            }
        }
        #endregion

        #region devUploader_FilesUploadComplete
        protected void devUploader_FilesUploadComplete(object sender, DevExpress.Web.FilesUploadCompleteEventArgs e)
        {
            int cnt = oParameters.Length;

            // 오류가 난 경우 파일을 삭제한다.
            if (nErrorCnt > 0)
            {
                foreach (KeyValuePair<string, string> pair in oFileList)
                {
                    uploadingUtils.RemoveFileWithDelay(pair.Key, pair.Value, 5);
                }

                e.CallbackData = String.Format("Error|{0}", String.Join("^", sErrorMsg));
            }
            else
            {
                e.CallbackData = String.Format("Success|{0}|{1}", sAttachUniqueID, strSaveFile);
            }
        }
        #endregion 

        #region 파일 저장 및 DB Parameter Collection Add
        string SavePostedFiles(UploadedFile uploadedFile)
        {
            if (!uploadedFile.IsValid)
                return String.Empty;

            if (oParameters == null)
                oParameters = new object[devUploader.UploadedFiles.Length];

            if (sErrorMsg == null)
                sErrorMsg = new string[devUploader.UploadedFiles.Length];

            sCOMPCD = Request.QueryString.Get("COMPCD") ?? gsCOMPCD;
            sDATA_GBN = Request.QueryString.Get("GBN") ?? "Z";
            sDATA_REVNO = Request.QueryString.Get("REVNO") ?? "0";
            sATTFILENO = Request.QueryString.Get("FILENO");
            if (String.IsNullOrEmpty(sATTFILENO))
                sATTFILENO = UF.Encrypts.GetUniqueKey();

            // 고유키생성
            if (String.IsNullOrEmpty(sAttachUniqueID))
                sAttachUniqueID = sATTFILENO;

            FileInfo fileInfo = new FileInfo(uploadedFile.FileName);

            // 저장할 파일 고유명
            string uniqueFileName = String.Concat(Guid.NewGuid().ToString().GetHashCode().ToString("x"), fileInfo.Extension);
            // 저장할 디렉토리
            string default_path = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), sCOMPCD, sDATA_GBN);

            uploadingUtils.BaseDirectory = default_path;
            uploadingUtils.MaxUploadSize = 104857600; // 100MB
            uploadingUtils.overWrite = false;
            uploadingUtils.saveFileName = Path.GetFileNameWithoutExtension(uniqueFileName);

            // 저장된 파일 전체경로(파일명포함)
            strSaveFile = String.Concat(default_path, uploadingUtils.UploadFileWithOutSub(uploadedFile));
            string fileLabel = Path.GetFileName(strSaveFile);
            string fileLength = uploadingUtils.GetFileSize(uploadedFile.ContentLength);

            // 저장을 위한 파라미터 생성
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("ATTFILENO", sAttachUniqueID);
            oParamDic.Add("DATA_NAME", fileLabel);
            oParamDic.Add("DATA_ORIGIN_NAME", fileInfo.Name);
            oParamDic.Add("DATA_EXTN", fileInfo.Extension);
            oParamDic.Add("ATTFILESIZE", uploadedFile.ContentLength.ToString());
            oParamDic.Add("INPUTID", gsUSERID);
            oParamDic.Add("DATA_GBN", sDATA_GBN);
            oParamDic.Add("DATA_REVNO", sDATA_REVNO);
            oParameters[nFileIdx] = (object)oParamDic;
            nFileIdx++;

            // 오류시 파일 삭제를 위한 Dictionary
            oFileList.Add(fileLabel, strSaveFile);

            return string.Format("{0} ({1})|{2}|{3}", fileLabel, fileLength, fileInfo.Name, strSaveFile);
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {

            //if (devGrid.DataSource == null) return;
            //if (IsPostBack) return;

            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            if (e.GetValue("상한값") == null) return;

            string strMax = e.GetValue("상한값").ToString();
            string strMin = e.GetValue("하한값").ToString();
            string strStandard = e.GetValue("규격").ToString();
            string strInspcd = e.GetValue("검사분류").ToString();


            if (strInspcd.Contains("치수"))
            {
                if (strMax == "" && strMin == "")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion

        #region devGrid_CellEditorInitialize
        protected void devGrid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("측정기기") ) return;

            string groupCode = String.Empty;

            switch (e.Column.FieldName)
            {
                default:
                    groupCode = String.Empty;
                    break;
                case "측정기기":         // 계측기
                    groupCode = "AAD8";
                    break;
            }

            //if (!String.IsNullOrEmpty(groupCode))
            //{
            //    AspxCombox_DataBind(e.Editor, groupCode);
            //}
        }
        #endregion  

        #endregion
    }
}