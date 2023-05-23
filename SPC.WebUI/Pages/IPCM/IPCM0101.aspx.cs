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
using SPC.IPCM.Biz;
using SPC.SYST.Biz;

namespace SPC.WebUI.Pages.IPCM
{
    public partial class IPCM0101 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        DataSet ds2 = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string oSetParam = String.Empty;
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
            QWK100_NOPAGING_LST();

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                devGrid.DataBind();
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
            oSetParam = Request.QueryString.Get("oSetParam") ?? "";
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

        #region 품질이상제기 목록을 구한다
        void QWK100_NOPAGING_LST()
        {
            string errMsg = String.Empty;

            using (IPCMBiz biz = new IPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_STDATE", GetFromDt());
                oParamDic.Add("F_EDDATE", GetToDt());
                if (!gsVENDOR)
                {
                    oParamDic.Add("F_COMPCD", GetCompCD());
                    oParamDic.Add("F_FACTCD", GetFactCD());
                }
                else
                {
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                }
                oParamDic.Add("F_RQCPCD", gsCOMPCD);
                oParamDic.Add("F_RQFTCD", gsFACTCD);
                oParamDic.Add("F_PROGRESS", "1");
                oParamDic.Add("F_PROGRESSTP", "RQ");
                oParamDic.Add("F_BVENDOR", !gsVENDOR ? "0" : "1");
                oParamDic.Add("F_PROGRESSST", "");
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_LANGTP", gsLANGTP);

                ds = biz.QWK100_NOPAGING_LST(oParamDic, out errMsg);
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
            QWK100_NOPAGING_LST();
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

                    DateTime dtToday = DateTime.Today;
                    DevExpressLib.devDateEdit(grid, "txtRQDATE").Date = dtToday;
                    DevExpressLib.devDateEdit(grid, "txtRQRCDT").Date = dtToday.AddDays(7); ;
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
                                case "ddlMEASGD":       // 대책요청
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_MEASGD").ToString();
                                    break;
                                case "ddlUNSTTP":      //부적합유형
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_UNSTTP").ToString();
                                    break;
                                case "ddlDEPARTCD":      // 대책부서
                                    rowValue = devGrid.GetRowValues(editingRowVisibleIndex, "F_DEPARTCD").ToString();
                                    break;
                            }
                        }
                    }

                    devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });

                    if (devComboBox.Items.FindByValue(rowValue) != null)
                        devComboBox.SelectedIndex = devComboBox.Items.FindByValue(rowValue).Index;
                }
            }
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName.Equals("F_RQRCDT"))
            {
                if (e.CellValue != null)
                {
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
                    string sRSDATE = grid.GetRowValues(e.VisibleIndex, "F_RSDATE").ToString();
                    if (String.IsNullOrEmpty(sRSDATE))
                        sRSDATE = DateTime.Today.ToString("yyyy-MM-dd");

                    //int nDateDiff = UF.Date.DateDiff(e.CellValue.ToString(), sRSDATE);
                    long nDateDiff = DateTime.Parse(e.CellValue.ToString()).Ticks - DateTime.Parse(sRSDATE).Ticks;
                    nDateDiff = nDateDiff / 10000000 / 60 / 60 / 24 + 1;

                    if (nDateDiff <= 0)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                        e.Cell.Font.Bold = true;
                    }
                    else if (nDateDiff <= 7)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Blue;
                        e.Cell.Font.Bold = true;
                    }
                }
            }
            else
                return;
        }
        #endregion

        #region devGrid_HtmlEditFormCreated
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
                    string errMsg = String.Empty;
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
                    DevExpress.Web.ASPxComboBox ddlComboBox = null;
                    DevExpress.Web.ASPxTextBox txtBox = null;
                    string sCOMPCD = String.Empty;
                    string sFACTCD = String.Empty;
                    
                    // 부모업체인 경우
                    if (!gsVENDOR)
                    {
                        //  관리번호 입력하지 않는다
                        txtBox = grid.FindEditFormTemplateControl("txtCTRLNO") as DevExpress.Web.ASPxTextBox;
                        txtBox.ClientSideEvents.Init = "fn_OnControlDisableBox";

                        // 품목변경불가
                        txtBox = grid.FindEditFormTemplateControl("txtITEMCD") as DevExpress.Web.ASPxTextBox;
                        txtBox.ClientSideEvents.Init = "fn_OnControlDisableBox";

                        // 공정변경불가
                        txtBox = grid.FindEditFormTemplateControl("txtWORKCD") as DevExpress.Web.ASPxTextBox;
                        txtBox.ClientSideEvents.Init = "fn_OnControlDisableBox";
                    }

                    int editingRowVisibleIndex = grid.EditingRowVisibleIndex;

                    if (editingRowVisibleIndex >= 0)
                    {
                        if (!Convert.ToBoolean(grid.GetRowValues(editingRowVisibleIndex, "F_BVENDOR").ToString()))
                        {
                            sCOMPCD = grid.GetRowValues(editingRowVisibleIndex, "F_RQCPCD").ToString();
                            sFACTCD = grid.GetRowValues(editingRowVisibleIndex, "F_RQFTCD").ToString();
                        }
                        else
                        {
                            sCOMPCD = gsCOMPCD;
                            sFACTCD = gsFACTCD;
                        }
                    }
                    else
                    {
                        sCOMPCD = gsCOMPCD;
                        sFACTCD = gsFACTCD;
                    }
                    
                    ddlComboBox = grid.FindEditFormTemplateControl("ddlUNSTTP") as DevExpress.Web.ASPxComboBox;

                    using (BSIFBiz biz = new BSIFBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", sCOMPCD);
                        oParamDic.Add("F_FACTCD", sFACTCD);
                        ds = biz.QCD103_LST(oParamDic, out errMsg);
                    }
                    ddlComboBox.TextField = "F_TRNM";
                    ddlComboBox.ValueField ="F_TRCD";
                    ddlComboBox.DataSource = ds;
                    ddlComboBox.DataBind();

                    

                    ddlComboBox = grid.FindEditFormTemplateControl("ddlDEPARTCD") as DevExpress.Web.ASPxComboBox;

                    using (SYSTBiz biz = new SYSTBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_CODEGROUP", "20");
                        oParamDic.Add("F_LANGTYPE", gsLANGTP);
                        ds = biz.SYCOD01_LST(oParamDic, out errMsg);
                    }
                    ddlComboBox.TextField = "F_CODENM";
                    ddlComboBox.ValueField = "F_CODE";
                    ddlComboBox.DataSource = ds;
                    ddlComboBox.DataBind();
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

        #region txtITEMCD_Init
        /// <summary>
        /// txtITEMCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupItemSearch('FORM')");
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWorkSearch('FORM')");
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupInspectionItem()");
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
            oParamDic.Add("F_CTRLNO", e.NewValues["F_CTRLNO"] == null ? "" : e.NewValues["F_CTRLNO"].ToString());
            oParamDic.Add("F_ITEMCD", e.NewValues["F_ITEMCD"] == null ? "" : e.NewValues["F_ITEMCD"].ToString());
            oParamDic.Add("F_MODELCD", e.NewValues["F_MODELCD"] == null ? "" : e.NewValues["F_MODELCD"].ToString());
            oParamDic.Add("F_BANCD", e.NewValues["F_BANCD"] == null ? "" : e.NewValues["F_BANCD"].ToString());
            oParamDic.Add("F_LINECD", e.NewValues["F_LINECD"] == null ? "" : e.NewValues["F_LINECD"].ToString());
            oParamDic.Add("F_WORKCD", e.NewValues["F_WORKCD"] == null ? "" : e.NewValues["F_WORKCD"].ToString());
            oParamDic.Add("F_LOTNO", e.NewValues["F_LOTNO"] == null ? "" : e.NewValues["F_LOTNO"].ToString());
            oParamDic.Add("F_UNSTTP", DevExpressLib.devTextBox(devGrid, "hidUNSTTP").Text);
            oParamDic.Add("F_DEPARTCD", DevExpressLib.devTextBox(devGrid, "hidDEPARTCD").Text);
            oParamDic.Add("F_MEASGD", DevExpressLib.devComboBox(devGrid, "ddlMEASGD").SelectedItem.Value.ToString());
            oParamDic.Add("F_OCSTDT", DevExpressLib.devDateEdit(devGrid, "txtOCSTDT").Date.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_OCEDDT", DevExpressLib.devDateEdit(devGrid, "txtOCEDDT").Date.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_RQRCDT", DevExpressLib.devDateEdit(devGrid, "txtRQRCDT").Date.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_BIMPROVE", "0");
            oParamDic.Add("F_PROGRESS", "1");
            oParamDic.Add("F_BVENDOR", !gsVENDOR ? "0" : "1");
            oParamDic.Add("F_RQDATE", DevExpressLib.devDateEdit(devGrid, "txtRQDATE").Date.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_RQCPCD", gsCOMPCD);
            oParamDic.Add("F_RQFTCD", gsFACTCD);
            oParamDic.Add("F_RQUSID", gsUSERID);
            oParamDic.Add("F_RQUSNM", gsUSERNM);
            oParamDic.Add("F_RQTXT1", e.NewValues["F_RQTXT1"] == null ? "" : e.NewValues["F_RQTXT1"].ToString());
            oParamDic.Add("F_RQTXT2", e.NewValues["F_RQTXT2"] == null ? "" : e.NewValues["F_RQTXT2"].ToString());
            oParamDic.Add("F_RQTXT3", e.NewValues["F_RQTXT3"] == null ? "" : e.NewValues["F_RQTXT3"].ToString());
            oParamDic.Add("F_RQTXT4", e.NewValues["F_RQTXT4"] == null ? "" : e.NewValues["F_RQTXT4"].ToString());
            oParamDic.Add("F_RQTXT5", e.NewValues["F_RQTXT5"] == null ? "" : e.NewValues["F_RQTXT5"].ToString());
            oParamDic.Add("F_RQFILE", e.NewValues["F_RQFILE"] == null ? "" : e.NewValues["F_RQFILE"].ToString());

            string errMsg = String.Empty;
            bool bExecute = false;

            using (IPCMBiz biz = new IPCMBiz())
            {
                bExecute = biz.QWK100_STEP1_INS(oParamDic, out errMsg);
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
                QWK100_NOPAGING_LST();
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
            DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
            int editingRowVisibleIndex = grid.EditingRowVisibleIndex;
            
            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", grid.GetRowValues(editingRowVisibleIndex, "F_COMPCD").ToString());
            oParamDic.Add("F_FACTCD", grid.GetRowValues(editingRowVisibleIndex, "F_FACTCD").ToString());
            oParamDic.Add("F_INDXNO", grid.GetRowValues(editingRowVisibleIndex, "F_INDXNO").ToString());
            oParamDic.Add("F_CTRLNO", e.NewValues["F_CTRLNO"] == null ? "" : e.NewValues["F_CTRLNO"].ToString());
            oParamDic.Add("F_ITEMCD", e.NewValues["F_ITEMCD"] == null ? "" : e.NewValues["F_ITEMCD"].ToString());
            oParamDic.Add("F_MODELCD", e.NewValues["F_MODELCD"] == null ? "" : e.NewValues["F_MODELCD"].ToString());
            oParamDic.Add("F_BANCD", e.NewValues["F_BANCD"] == null ? "" : e.NewValues["F_BANCD"].ToString());
            oParamDic.Add("F_LINECD", e.NewValues["F_LINECD"] == null ? "" : e.NewValues["F_LINECD"].ToString());
            oParamDic.Add("F_WORKCD", e.NewValues["F_WORKCD"] == null ? "" : e.NewValues["F_WORKCD"].ToString());
            oParamDic.Add("F_LOTNO", e.NewValues["F_LOTNO"] == null ? "" : e.NewValues["F_LOTNO"].ToString());
            oParamDic.Add("F_UNSTTP", DevExpressLib.devTextBox(devGrid, "hidUNSTTP").Text);
            oParamDic.Add("F_DEPARTCD", DevExpressLib.devTextBox(devGrid, "hidDEPARTCD").Text);
            oParamDic.Add("F_MEASGD", DevExpressLib.devComboBox(devGrid, "ddlMEASGD").SelectedItem.Value.ToString());
            oParamDic.Add("F_OCSTDT", DevExpressLib.devDateEdit(devGrid, "txtOCSTDT").Date.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_OCEDDT", DevExpressLib.devDateEdit(devGrid, "txtOCEDDT").Date.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_RQRCDT", DevExpressLib.devDateEdit(devGrid, "txtRQRCDT").Date.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_BIMPROVE", "0");
            oParamDic.Add("F_PROGRESS", "1");
            oParamDic.Add("F_BVENDOR", !gsVENDOR ? "0" : "1");
            oParamDic.Add("F_RQDATE", DevExpressLib.devDateEdit(devGrid, "txtRQDATE").Date.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_RQCPCD", gsCOMPCD);
            oParamDic.Add("F_RQFTCD", gsFACTCD);
            oParamDic.Add("F_RQUSID", gsUSERID);
            oParamDic.Add("F_RQUSNM", gsUSERNM);
            oParamDic.Add("F_RQTXT1", e.NewValues["F_RQTXT1"] == null ? "" : e.NewValues["F_RQTXT1"].ToString());
            oParamDic.Add("F_RQTXT2", e.NewValues["F_RQTXT2"] == null ? "" : e.NewValues["F_RQTXT2"].ToString());
            oParamDic.Add("F_RQTXT3", e.NewValues["F_RQTXT3"] == null ? "" : e.NewValues["F_RQTXT3"].ToString());
            oParamDic.Add("F_RQTXT4", e.NewValues["F_RQTXT4"] == null ? "" : e.NewValues["F_RQTXT4"].ToString());
            oParamDic.Add("F_RQTXT5", e.NewValues["F_RQTXT5"] == null ? "" : e.NewValues["F_RQTXT5"].ToString());
            oParamDic.Add("F_RQFILE", e.NewValues["F_RQFILE"] == null ? "" : e.NewValues["F_RQFILE"].ToString());

            string errMsg = String.Empty;
            bool bExecute = false;

            using (IPCMBiz biz = new IPCMBiz())
            {
                bExecute = biz.QWK100_STEP1_UPD(oParamDic, out errMsg);
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
                QWK100_NOPAGING_LST();
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
            DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
            int rowIndex = devGrid.FindVisibleIndexByKeyValue(String.Format("{0}|{1}|{2}|{3}", e.Keys[0], e.Keys[1], e.Keys[2], e.Keys[3]));
            if (rowIndex < 0)
            {
                procResult = new string[] { "99", "삭제할 수 없는 Row 입니다" };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];

                e.Cancel = true;
            }
            else if (grid.GetRowValues(rowIndex, "F_RQUSID").ToString().Equals(gsUSERID))
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", grid.GetRowValues(rowIndex, "F_COMPCD").ToString());
                oParamDic.Add("F_FACTCD", grid.GetRowValues(rowIndex, "F_FACTCD").ToString());
                oParamDic.Add("F_INDXNO", grid.GetRowValues(rowIndex, "F_INDXNO").ToString());

                string errMsg = String.Empty;
                bool bExecute = false;

                using (IPCMBiz biz = new IPCMBiz())
                {
                    bExecute = biz.QWK100_STEP1_DEL(oParamDic, out errMsg);
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    procResult = new string[] { "99", String.Format("등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r에러내용 : {0}", errMsg) };
                    devCallback.JSProperties["cpResultCode"] = procResult[0];
                    devCallback.JSProperties["cpResultMsg"] = procResult[1];
                }
                else if (!bExecute)
                {
                    procResult = new string[] { "99", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                    devGrid.JSProperties["cpResultCode"] = procResult[0];
                    devGrid.JSProperties["cpResultMsg"] = procResult[1];
                }

                e.Cancel = true;

                if (true == bExecute)
                {
                    // 작업표준서 목록을 구한다
                    QWK100_NOPAGING_LST();
                }
            }
            else
            {
                procResult = new string[] { "99", "본인이 제기한 내용만 삭제할 수 있습니다" };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];

                e.Cancel = true;
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 품질이상제기정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}