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

namespace SPC.WebUI.Pages.BSIF.Popup
{
    public partial class BSIF0303POPNEW : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string[] keyFields = new string[4];
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
                EditForm.JSProperties["cpResultCode"] = "";
                EditForm.JSProperties["cpResultMsg"] = "";
            }
            ASPxRadioButtonList_DataBind(rdoACCEPT, "AAC2");
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
        {
            // 검사분류
            AspxCombox_DataBind(ddlINSPCD, "AAC5");

            // QC검사주기
            AspxCombox_DataBind(ddlQCYCLECD, "AAC3");

            // 현장검사주기
            AspxCombox_DataBind(ddlJCYCLECD, "AAC3");

            // 품질수준
            AspxCombox_DataBind(ddlRANK, "AAD2");

            // 상,하한편측
            AspxCombox_DataBind(ddlSINGLECHK, "AAD7");

            // 공차기호
            AspxCombox_DataBind(ddlUNIT, "AAC1");

            // 관리한계기준
            ASPxRadioButtonList_DataBind(rdoACCEPT, "AAC2");

            // 설비규격공차(장비기호) - 사용안함

            // 계측기
            AspxCombox_DataBind(ddlAIRCK, "AAD8");

            // 측정포트
            AspxCombox_DataBind(ddlPORT, "AAD4");

            // 측정방법
            AspxCombox_DataBind(ddlGETDATA, "AAD5");

            // 설비구분
            AspxCombox_DataBind(ddlLOADTF, "AAD6");
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
            txtDISPLAYNO.ClientSideEvents.Init = "fn_OnControlDisableBox";  //검사순서
        }
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

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(DevExpress.Web.ASPxComboBox ddlComboBox, string CommonCode)
        {
            ddlComboBox.DataBound += ddlComboBox_DataBound;

            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();
        }
        #endregion

        #region EditForm ASPxRadioButtonList DataBind
        /// <summary>
        /// ASPxRadioButtonList_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void ASPxRadioButtonList_DataBind(DevExpress.Web.ASPxRadioButtonList rdoButtonList, string CommonCode)
        {
            rdoButtonList.DataBound += rdoButtonList_DataBound;

            rdoButtonList.TextField = String.Format("COMMNM{0}", gsLANGTP);
            rdoButtonList.ValueField = "COMMCD";
            rdoButtonList.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            rdoButtonList.DataBind();
        }
        #endregion

        #region ASPxCheckBox_Checked
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="CheckBoxID">CheckBoxID</param>
        void ASPxCheckBox_Checked(DevExpress.Web.ASPxCheckBox chkBOX, string ColumnID)
        {
            int nIndex = int.Parse(editingRowVisibleIndex.Text);
            if (nIndex >= 0)
            {
                chkBOX.Checked = devGrid.GetRowValues(nIndex, ColumnID).ToString().Equals("1");
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

        #region ddlComboBox_DataBound
        /// <summary>
        /// ddlComboBox_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlComboBox_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = sender as DevExpress.Web.ASPxComboBox;
            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem("선택하세요", ""));
            ddlComboBox.SelectedIndex = 0;
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
            DevExpress.Web.ASPxRadioButtonList rdoButtonList = sender as DevExpress.Web.ASPxRadioButtonList;
            rdoButtonList.SelectedIndex = -1;
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWorkSearch('FORM')");
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
            oParamDic.Add("F_ITEMCD", e.Values["F_ITEMCD"].ToString());
            oParamDic.Add("F_INSPCD", e.Values["F_INSPCD"].ToString());
            oParamDic.Add("F_SERIALNO", e.Values["F_SERIALNO"].ToString());

            string[] oQuerys = { "USP_QCD34_DEL", "USP_QCD34B_DEL", "USP_QCD34A_INS", "USP_QWK03A_DEL" };
            object[] oParams = new object[4];
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

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ITEMCD", e.Values["F_ITEMCD"].ToString());
            oParamDic.Add("F_WORKCD", e.Values["F_WORKCD"].ToString());
            oParamDic.Add("F_INSPCD", e.Values["F_INSPCD"].ToString());
            oParamDic.Add("F_SERIALNO", e.Values["F_SERIALNO"].ToString());
            oParamDic.Add("F_KEMARK", String.Format("Data Deleted by {0}", gsUSERNM));
            oParamDic.Add("F_USER", gsUSERID);
            oParams[3] = (object)oParamDic;

            bool bExecute = false;

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

            e.Cancel = true;

            if (true == bExecute)
            {
                // 검사기준목록을 구한다
                GetQCD34_LST();
            }
        }
        #endregion

        #region EditForm_Callback
        /// <summary>
        /// EditForm_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void EditForm_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');
            /*
             * 0 : Action Type
             * 1 : ITEMCD
             * 2 : WORKCD
             * 3 : SERIALNO
             */
            string reMsg = "";
            if (oParams[0].Equals("GET"))
            {
                SetDefaultObject();

                string errMsg = String.Empty;

                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", oParams[1].ToString());
                    oParamDic.Add("F_WORKCD", oParams[2].ToString());
                    oParamDic.Add("F_SERIALNO", oParams[3].ToString());
                    ds = biz.GetQCD34_POPNEW_LST(oParamDic, out errMsg);
                }

                txtITEMCD.Text = ds.Tables[0].Rows[0]["F_ITEMCD"].ToString();
                txtITEMNM.Text = ds.Tables[0].Rows[0]["F_ITEMNM"].ToString();
                ddlINSPCD.Value = ds.Tables[0].Rows[0]["F_INSPCD"].ToString();
                txtMEAINSPCD.Text = ds.Tables[0].Rows[0]["F_MEAINSPCD"].ToString();
                txtINSPDETAIL.Text = ds.Tables[0].Rows[0]["F_INSPDETAIL"].ToString();
                txtMODELNM.Text = ds.Tables[0].Rows[0]["F_MODELNM"].ToString();
                txtDISPLAYNO.Text = ds.Tables[0].Rows[0]["F_DISPLAYNO"].ToString();
                txtWORKCD.Text = ds.Tables[0].Rows[0]["F_WORKCD"].ToString();
                txtWORKNM.Text = ds.Tables[0].Rows[0]["F_WORKNM"].ToString();
                txtSERIALNO.Text = ds.Tables[0].Rows[0]["F_SERIALNO"].ToString();
                txtSTANDARD.Text = ds.Tables[0].Rows[0]["F_STANDARD"].ToString();
                txtMAX.Text = ds.Tables[0].Rows[0]["F_MAX"].ToString();
                txtMIN.Text = ds.Tables[0].Rows[0]["F_MIN"].ToString();
                chkZERO.Checked = ds.Tables[0].Rows[0]["F_ZERO"].ToString() == "1" ? true : false;
                txtZIG.Text = ds.Tables[0].Rows[0]["F_ZIG"].ToString();
                ddlQCYCLECD.Value = ds.Tables[0].Rows[0]["F_QCYCLECD"].ToString();
                ddlJCYCLECD.Value = ds.Tables[0].Rows[0]["F_JCYCLECD"].ToString();
                txtHCOUNT.Text = ds.Tables[0].Rows[0]["F_HCOUNT"].ToString();
                txtSIRYO.Text = ds.Tables[0].Rows[0]["F_SIRYO"].ToString();
                ddlRANK.Value = ds.Tables[0].Rows[0]["F_RANKCD"].ToString();
                chkMEASYESNO.Checked = ds.Tables[0].Rows[0]["F_MEASYESNO"].ToString() == "1" ? true : false;
                chkIMPORT.Checked = ds.Tables[0].Rows[0]["F_IMPORT"].ToString() == "1" ? true : false;
                ddlSINGLECHK.Value = ds.Tables[0].Rows[0]["F_SINGLECHK"].ToString();
                ddlUNIT.Value = ds.Tables[0].Rows[0]["F_UNITCD"].ToString();
                txtIMAGESEQ.Text = ds.Tables[0].Rows[0]["F_IMAGESEQ"].ToString();
                rdoRESULTGUBUN.Value = ds.Tables[0].Rows[0]["F_RESULTGUBUN"].ToString();
                txtUCLX.Text = ds.Tables[0].Rows[0]["F_UCLX"].ToString();
                txtLCLX.Text = ds.Tables[0].Rows[0]["F_LCLX"].ToString();
                txtUCLR.Text = ds.Tables[0].Rows[0]["F_UCLR"].ToString();
                txtTMAX.Text = ds.Tables[0].Rows[0]["F_TMAX"].ToString();
                txtTMIN.Text = ds.Tables[0].Rows[0]["F_TMIN"].ToString();
                rdoACCEPT.Value = ds.Tables[0].Rows[0]["F_ACCEPT_SEQ"].ToString();
                rdoHIPISNG.Value = ds.Tables[0].Rows[0]["F_HIPISNG"].ToString();
                txtRESULTSTAND.Text = ds.Tables[0].Rows[0]["F_RESULTSTAND"].ToString();
                rdoSAMPLECHK.Value = ds.Tables[0].Rows[0]["F_SAMPLECHK"].ToString();
                ddlAIRCK.Value = ds.Tables[0].Rows[0]["F_AIRCK"].ToString();
                chkGETTYPE.Checked = ds.Tables[0].Rows[0]["F_GETTYPE"].ToString() == "1" ? true : false;
                ddlPORT.Value = ds.Tables[0].Rows[0]["F_PORTCD"].ToString();
                txtCHANNEL.Text = ds.Tables[0].Rows[0]["F_CHANNEL"].ToString();
                ddlGETDATA.Value = ds.Tables[0].Rows[0]["F_GETDATACD"].ToString();
                ddlLOADTF.Value = ds.Tables[0].Rows[0]["F_LOADTF"].ToString();
                txtDEFECTS_N.Text = ds.Tables[0].Rows[0]["F_DEFECTS_N"].ToString();
                txtMEASCD1.Text = ds.Tables[0].Rows[0]["F_MEASCD1"].ToString();
            }
            else if (oParams[0].Equals("INS"))
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", "");
                oParamDic.Add("F_LINECD", "");
                oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                oParamDic.Add("F_INSPCD", ddlINSPCD.Value.ToString());
                oParamDic.Add("F_STANDARD", txtSTANDARD.Text);
                oParamDic.Add("F_RANK", ddlRANK.Value == null ? "" : ddlRANK.Value.ToString());
                oParamDic.Add("F_MIN", txtMIN.Text);
                oParamDic.Add("F_MAX", txtMAX.Text);
                oParamDic.Add("F_MEAINSPCD", txtMEAINSPCD.Text);
                oParamDic.Add("F_WORKCD", txtWORKCD.Text);
                oParamDic.Add("F_MACHCD", "");    // 사용안함
                oParamDic.Add("F_UCLX", txtUCLX.Text);
                oParamDic.Add("F_LCLX", txtLCLX.Text);
                oParamDic.Add("F_UCLR", txtUCLR.Text);
                oParamDic.Add("F_LCLR", "");
                oParamDic.Add("F_SIRYO", txtSIRYO.Text);
                oParamDic.Add("F_ZERO", chkZERO.Checked ? "1" : "0");
                oParamDic.Add("F_UNIT", ddlUNIT.Value == null ? "" : ddlUNIT.Value.ToString());
                oParamDic.Add("F_ACCEPT_SEQ", txtACCEPT.Text == null ? "" : txtACCEPT.Text);
                oParamDic.Add("F_GETDATA", ddlGETDATA.Value == null ? "" : ddlGETDATA.Value.ToString());
                oParamDic.Add("F_PORT", ddlPORT.Value == null ? "" : ddlPORT.Value.ToString());
                oParamDic.Add("F_CHANNEL", txtCHANNEL.Text);
                oParamDic.Add("F_GETTIME", "0");    // 사용안함
                oParamDic.Add("F_GETTYPE", chkGETTYPE.Checked ? "1" : "0");
                oParamDic.Add("F_ZIG", txtZIG.Text);
                oParamDic.Add("F_DATAUNIT", "0");
                oParamDic.Add("F_DEFECTS_N", txtDEFECTS_N.Text);
                //oParamDic.Add("F_DISPLAYNO", txtDISPLAYNO.Text);
                oParamDic.Add("F_AIRCK", ddlAIRCK.Value == null ? "" : ddlAIRCK.Value.ToString());
                oParamDic.Add("F_LOADTF", ddlLOADTF.Value == null ? "" : ddlLOADTF.Value.ToString());
                oParamDic.Add("F_MEASYESNO", chkMEASYESNO.Checked ? "1" : "0");
                oParamDic.Add("F_SAMPLECHK", rdoSAMPLECHK.Value == null ? "" : rdoSAMPLECHK.SelectedItem.Value.ToString());
                oParamDic.Add("F_RESULTGUBUN", rdoRESULTGUBUN.Value == null ? "" : rdoRESULTGUBUN.SelectedItem.Value.ToString());
                oParamDic.Add("F_IMPORT", chkIMPORT.Checked ? "1" : "0");
                oParamDic.Add("F_IMAGESEQ", txtIMAGESEQ.Text);
                oParamDic.Add("F_JCYCLECD", ddlJCYCLECD.Value == null ? "" : ddlJCYCLECD.Value.ToString());
                oParamDic.Add("F_QCYCLECD", ddlQCYCLECD.Value == null ? "" : ddlQCYCLECD.Value.ToString());
                oParamDic.Add("F_HCOUNT", txtHCOUNT.Text);
                oParamDic.Add("F_SINGLECHK", ddlSINGLECHK.Value == null ? "" : ddlSINGLECHK.Value.ToString());
                oParamDic.Add("F_MEASCD1", txtMEASCD1.Text);
                oParamDic.Add("F_MEASURE", ddlAIRCK.Text);
                oParamDic.Add("F_RESULTSTAND", txtRESULTSTAND.Text);
                oParamDic.Add("F_TMAX", txtTMAX.Text);
                oParamDic.Add("F_TMIN", txtTMIN.Text);
                oParamDic.Add("F_HIPISNG", rdoHIPISNG.Value == null ? "" : rdoHIPISNG.SelectedItem.Value.ToString());
                oParamDic.Add("F_USER", gsUSERID);

                bool bExecute = false;

                using (BSIFBiz biz = new BSIFBiz())
                {
                    bExecute = biz.QCD34_PROC_INS(oParamDic);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                    EditForm.JSProperties["cpResultCode"] = procResult[0];
                    EditForm.JSProperties["cpResultMsg"] = procResult[1];
                }

                if (true == bExecute)
                {
                    // 검사기준목록을 구한다
                    GetQCD34_LST();
                }
            }
            else if (oParams[0].Equals("UPD"))
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", "");
                oParamDic.Add("F_LINECD", "");
                oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                oParamDic.Add("F_INSPCD", ddlINSPCD.Value.ToString());
                oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
                oParamDic.Add("F_STANDARD", txtSTANDARD.Text);
                oParamDic.Add("F_RANK", ddlRANK.Value == null ? "" : ddlRANK.Value.ToString());
                oParamDic.Add("F_MIN", txtMIN.Text);
                oParamDic.Add("F_MAX", txtMAX.Text);
                oParamDic.Add("F_MEAINSPCD", txtMEAINSPCD.Text);
                oParamDic.Add("F_WORKCD", txtWORKCD.Text);
                oParamDic.Add("F_MACHCD", "");    // 사용안함
                oParamDic.Add("F_UCLX", txtUCLX.Text);
                oParamDic.Add("F_LCLX", txtLCLX.Text);
                oParamDic.Add("F_UCLR", txtUCLR.Text);
                oParamDic.Add("F_LCLR", "");
                oParamDic.Add("F_SIRYO", txtSIRYO.Text);
                oParamDic.Add("F_ZERO", chkZERO.Checked ? "1" : "0");
                oParamDic.Add("F_UNIT", ddlUNIT.Value == null ? "" : ddlUNIT.Value.ToString());
                oParamDic.Add("F_ACCEPT_SEQ", txtACCEPT.Text == null ? "" : txtACCEPT.Text);
                oParamDic.Add("F_GETDATA", ddlGETDATA.Value == null ? "" : ddlGETDATA.Value.ToString());
                oParamDic.Add("F_PORT", ddlPORT.Value == null ? "" : ddlPORT.Value.ToString());
                oParamDic.Add("F_CHANNEL", txtCHANNEL.Text);
                oParamDic.Add("F_GETTIME", "0");    // 사용안함
                oParamDic.Add("F_GETTYPE", chkGETTYPE.Checked ? "1" : "0");
                oParamDic.Add("F_ZIG", txtZIG.Text);
                oParamDic.Add("F_DATAUNIT", "0");
                oParamDic.Add("F_DEFECTS_N", txtDEFECTS_N.Text);
                oParamDic.Add("F_DISPLAYNO", txtDISPLAYNO.Text);
                oParamDic.Add("F_AIRCK", ddlAIRCK.Value == null ? "" : ddlAIRCK.Value.ToString());
                oParamDic.Add("F_LOADTF", ddlLOADTF.Value == null ? "" : ddlLOADTF.Value.ToString());
                oParamDic.Add("F_MEASYESNO", chkMEASYESNO.Checked ? "1" : "0");
                oParamDic.Add("F_SAMPLECHK", rdoSAMPLECHK.Value == null ? "" : rdoSAMPLECHK.SelectedItem.Value.ToString());
                oParamDic.Add("F_RESULTGUBUN", rdoRESULTGUBUN.Value == null ? "" : rdoRESULTGUBUN.SelectedItem.Value.ToString());
                oParamDic.Add("F_IMPORT", chkIMPORT.Checked ? "1" : "0");
                oParamDic.Add("F_IMAGESEQ", txtIMAGESEQ.Text);
                oParamDic.Add("F_JCYCLECD", ddlJCYCLECD.Value == null ? "" : ddlJCYCLECD.Value.ToString());
                oParamDic.Add("F_QCYCLECD", ddlQCYCLECD.Value == null ? "" : ddlQCYCLECD.Value.ToString());
                oParamDic.Add("F_HCOUNT", txtHCOUNT.Text);
                oParamDic.Add("F_SINGLECHK", ddlSINGLECHK.Value == null ? "" : ddlSINGLECHK.Value.ToString());
                oParamDic.Add("F_MEASCD1", txtMEASCD1.Text);
                oParamDic.Add("F_MEASURE", ddlAIRCK.Text);
                oParamDic.Add("F_RESULTSTAND", txtRESULTSTAND.Text);
                oParamDic.Add("F_TMAX", txtTMAX.Text);
                oParamDic.Add("F_TMIN", txtTMIN.Text);
                oParamDic.Add("F_HIPISNG", rdoHIPISNG.Value == null ? "" : rdoHIPISNG.SelectedItem.Value.ToString());

                bool bExecute = false;

                using (BSIFBiz biz = new BSIFBiz())
                {
                    bExecute = biz.QCD34_PROC_UPD(oParamDic);
                }

                if (!bExecute)
                {
                    reMsg = "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.";
                }
                else
                {
                    reMsg = "저장이 완료되었습니다.";
                }

                if (true == bExecute)
                {
                    // 검사기준목록을 구한다
                    GetQCD34_LST();
                }
            }

            EditForm.JSProperties["cpResultCode"] = oParams[0];
            EditForm.JSProperties["cpResultMsg"] = reMsg;
        }
        #endregion

        #endregion
    }
}