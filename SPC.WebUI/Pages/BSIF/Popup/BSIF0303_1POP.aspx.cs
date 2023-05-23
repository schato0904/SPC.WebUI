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
    public partial class BSIF0303_1POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        int nPageSize = 0;


        //int nCurrPage = 0;

        int nCurrPage
        {
            get
            {
                return (int)Session["BSIF0303_1POP_1"];
            }
            set
            {
                Session["BSIF0303_1POP_1"] = value;
            }
        }

        public string[] keyFields = new string[6];
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
            //
            // 객체 초기화
            //SetDefaultObject();         
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
                ucPager.PagerDataBind();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                devGrid.JSProperties["cpResultCode2"] = "";
                devGrid.JSProperties["cpResultMsg2"] = "";

                devCallback.JSProperties["cpIDCD"] = "";
                devCallback.JSProperties["cpIDNM"] = "";
                devCallback.JSProperties["cpMODELNM"] = "";
            }


            GetRequest();



            //nPageSize = int.Parse(hdnPageSize.Text);
            //nCurrPage = int.Parse(hdnCurrPage.Text);






            if ((String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false")))
            {
                //검사기준목록을 구한다
                //if (FIRSTchk.Text == "2")
                //{
                //    GetQCD34_NEW_LST(nPageSize > 0 ? nPageSize : ucPager.GetPageSize(), nCurrPage > 0 ? nCurrPage : 1, false);
                //}
                //else
                //{
                //    GetQCD34_NEW_LST2(nPageSize > 0 ? nPageSize : ucPager.GetPageSize(), nCurrPage > 0 ? nCurrPage : 1, false);
                //}

                GetQCD34_NEW_LST(nPageSize > 0 ? nPageSize : ucPager.GetPageSize(), nCurrPage > 0 ? nCurrPage : 1, false);

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

            //oParamDic.Add("F_ITEMCD", keyFields[0]); //keyFields[0]
            //oParamDic.Add("F_INSPCD", keyFields[4]); //keyFields[4]
            //oParamDic.Add("F_WORKCD", keyFields[3]); //keyFields[3]

            //ucItem.GetItemCD();

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
            nCurrPage = 0;
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
            rdoDATAUNIT.SelectedIndex = 0;
            rdoRESULTGUBUN.SelectedIndex = 1;   //성적서출력
            rdoACCEPT.SelectedIndex = 1;        //관리한계기준
            rdoHIPISNG.SelectedIndex = 1;       //전송유무
            rdoSAMPLECHK.SelectedIndex = 1;     //초중종사용여부
            ddlGETDATA.SelectedIndex = -1;       //측정방법
            txtSIRYO.Text = "1";                //시료수
            ddlRANK.SelectedIndex = -1;          //품질수준
            txtDEFECTS_N.Text = "1";            //측정횟수
            txtDISPLAYNO.ClientSideEvents.Init = "fn_OnControlDisableBox";  //검사순서



        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 검사기준 전체 갯수를 구한다

        #region GetQCD34_CNT
        Int32 GetQCD34_CNT()
        {


            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_BANCD", "");
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_INSPCD", GetInspectionCD());
                oParamDic.Add("F_WORKCD", GetWorkCD());
                oParamDic.Add("F_MEASYESNO", (this.ddlMEASYESNO.Value ?? "").ToString());
                totalCnt = biz.GetQCD34_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region GetQCD34_CNT2
        Int32 GetQCD34_CNT2()
        {

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_BANCD", "");
                oParamDic.Add("F_ITEMCD", keyFields[0]); //keyFields[0]
                oParamDic.Add("F_INSPCD", keyFields[4]); //keyFields[4]
                oParamDic.Add("F_WORKCD", keyFields[3]); //keyFields[3]
                totalCnt = biz.GetQCD34_CNT(oParamDic);
            }

            return totalCnt;

        }
        #endregion

        #endregion

        #region 검사기준목록을 구한다

        #region GetQCD34_NEW_LST
        void GetQCD34_NEW_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;





            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_BANCD", "");
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_INSPCD", GetInspectionCD());
                oParamDic.Add("F_WORKCD", GetWorkCD());
                oParamDic.Add("F_MEASYESNO", (this.ddlMEASYESNO.Value ?? "").ToString());
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                ds = biz.GetQCD34_NEW_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;
            if (bCallback)
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
                    //ucPager.TotalItems = GetQCD34_CNT();
                    // ucPager.PagerDataBind();
                }
                else
                {
                    devGrid.JSProperties["cpResultCode"] = "pager";
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, GetQCD34_CNT());
                }
            }




        }
        #endregion

        #region GetQCD34_NEW_LST2
        void GetQCD34_NEW_LST2(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_BANCD", "");
                oParamDic.Add("F_ITEMCD", keyFields[0]); //
                oParamDic.Add("F_INSPCD", keyFields[4]); //
                oParamDic.Add("F_WORKCD", keyFields[3]); //
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                ds = biz.GetQCD34_NEW_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;
            if (bCallback)
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

                }
                else
                {
                    devGrid.JSProperties["cpResultCode"] = "pager";
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, GetQCD34_CNT2());
                }
            }


        }
        #endregion

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

        #region EditForm ASPxRadioButtonList DataBind
        /// <summary>
        /// ASPxRadioButtonList_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void ASPxRadioButtonList_DataBind(DevExpress.Web.ASPxRadioButtonList rdoButtonList, string CommonCode)
        {
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
            //int nCurrPage = 0;
            int nPageSize = 0;

            //if (e.Parameters.ToString() == "FIRST")
            //{
            //    nCurrPage = ucPager.GetCurrPage();
            //    nPageSize = ucPager.GetPageSize();
            //    // 검사기준목록을 구한다
            //    GetQCD34_NEW_LST2(nPageSize, nCurrPage, true);
            //}
            //else
            //{
                
            //}

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                bool bExecute = false;

                if (e.Parameters.ToString() == "NEW")
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_BANCD", "");
                    oParamDic.Add("F_LINECD", "");
                    oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                    oParamDic.Add("F_INSPCD", txtINSPCD.Text);
                    //oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
                    oParamDic.Add("F_STANDARD", txtSTANDARD.Text);
                    oParamDic.Add("F_RANK", txtRANK.Text);
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
                    oParamDic.Add("F_UNIT", txtUNIT.Text);
                    //oParamDic.Add("F_ACCEPT_SEQ", rdoACCEPT.Value == null ? "" : rdoACCEPT.SelectedItem.Value.ToString()); //rdoACCEPT
                    oParamDic.Add("F_ACCEPT_SEQ", txtACCEPT.Text);
                    oParamDic.Add("F_GETDATA", txtGETDATA.Text);
                    oParamDic.Add("F_PORT", txtPORT.Text);
                    oParamDic.Add("F_CHANNEL", txtCHANNEL.Text);
                    oParamDic.Add("F_GETTIME", "0");    // 사용안함
                    oParamDic.Add("F_GETTYPE", chkGETTYPE.Checked ? "1" : "0");
                    oParamDic.Add("F_ZIG", txtZIG.Text);
                    oParamDic.Add("F_DATAUNIT", "0");
                    oParamDic.Add("F_DEFECTS_N", txtDEFECTS_N.Text);
                    //oParamDic.Add("F_DISPLAYNO", txtDISPLAYNO.Text);
                    oParamDic.Add("F_AIRCK", txtAIRCK.Text);
                    oParamDic.Add("F_LOADTF", txtLOADTF.Text);
                    oParamDic.Add("F_MEASYESNO", chkMEASYESNO.Checked ? "1" : "0");
                    oParamDic.Add("F_SAMPLECHK", rdoSAMPLECHK.Value == null ? "" : rdoSAMPLECHK.SelectedItem.Value.ToString());
                    oParamDic.Add("F_RESULTGUBUN", rdoRESULTGUBUN.Value == null ? "" : rdoRESULTGUBUN.SelectedItem.Value.ToString());
                    oParamDic.Add("F_IMPORT", chkIMPORT.Checked ? "1" : "0");
                    oParamDic.Add("F_IMAGESEQ", txtIMAGESEQ.Text);
                    oParamDic.Add("F_JCYCLECD", txtJCYCLECD.Text);
                    oParamDic.Add("F_QCYCLECD", txtQCYCLECD.Text);
                    oParamDic.Add("F_HCOUNT", txtHCOUNT.Text);
                    oParamDic.Add("F_SINGLECHK", txtSINGLECHK.Text);
                    oParamDic.Add("F_MEASCD1", txtMEASCD1.Text);
                    oParamDic.Add("F_MEASURE", "");
                    oParamDic.Add("F_RESULTSTAND", txtRESULTSTAND.Text);
                    oParamDic.Add("F_TMAX", txtTMAX.Text);
                    oParamDic.Add("F_TMIN", txtTMIN.Text);
                    oParamDic.Add("F_HIPISNG", rdoHIPISNG.Value == null ? "" : rdoHIPISNG.SelectedItem.Value.ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    using (BSIFBiz biz = new BSIFBiz())
                    {
                        bExecute = biz.QCD34_PROC_INS(oParamDic);
                    }

                    if (!bExecute)
                    {
                        procResult = new string[] { "1", "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                    }
                    else
                    {
                        procResult = new string[] { "1", "저장이 완료되었습니다." };
                    }
                }
                else if (e.Parameters.ToString() == "EDIT")
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_BANCD", "");
                    oParamDic.Add("F_LINECD", "");
                    oParamDic.Add("F_ITEMCD", txtITEMCD.Text); //
                    oParamDic.Add("F_INSPCD", txtINSPCD.Text);
                    oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
                    oParamDic.Add("F_STANDARD", txtSTANDARD.Text);
                    oParamDic.Add("F_RANK", txtRANK.Text);
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
                    oParamDic.Add("F_UNIT", txtUNIT.Text);
                    //oParamDic.Add("F_ACCEPT_SEQ", rdoACCEPT.Value == null ? "" : rdoACCEPT.SelectedItem.Value.ToString()); //rdoACCEPT
                    oParamDic.Add("F_ACCEPT_SEQ", txtACCEPT.Text);
                    oParamDic.Add("F_GETDATA", txtGETDATA.Text);
                    oParamDic.Add("F_PORT", txtPORT.Text);
                    oParamDic.Add("F_CHANNEL", txtCHANNEL.Text);
                    oParamDic.Add("F_GETTIME", "0");    // 사용안함
                    oParamDic.Add("F_GETTYPE", chkGETTYPE.Checked ? "1" : "0");
                    oParamDic.Add("F_ZIG", txtZIG.Text);
                    oParamDic.Add("F_DATAUNIT", "0");
                    oParamDic.Add("F_DEFECTS_N", txtDEFECTS_N.Text);
                    oParamDic.Add("F_DISPLAYNO", txtDISPLAYNO.Text);
                    oParamDic.Add("F_AIRCK", txtAIRCK.Text);
                    oParamDic.Add("F_LOADTF", txtLOADTF.Text);
                    oParamDic.Add("F_MEASYESNO", chkMEASYESNO.Checked ? "1" : "0");
                    oParamDic.Add("F_SAMPLECHK", rdoSAMPLECHK.Value == null ? "" : rdoSAMPLECHK.SelectedItem.Value.ToString());
                    oParamDic.Add("F_RESULTGUBUN", rdoRESULTGUBUN.Value == null ? "" : rdoRESULTGUBUN.SelectedItem.Value.ToString());
                    oParamDic.Add("F_IMPORT", chkIMPORT.Checked ? "1" : "0");
                    oParamDic.Add("F_IMAGESEQ", txtIMAGESEQ.Text);
                    oParamDic.Add("F_JCYCLECD", txtJCYCLECD.Text);
                    oParamDic.Add("F_QCYCLECD", txtQCYCLECD.Text);
                    oParamDic.Add("F_HCOUNT", txtHCOUNT.Text);
                    oParamDic.Add("F_SINGLECHK", txtSINGLECHK.Text);
                    oParamDic.Add("F_MEASCD1", txtMEASCD1.Text);
                    oParamDic.Add("F_MEASURE", "");
                    oParamDic.Add("F_RESULTSTAND", txtRESULTSTAND.Text);
                    oParamDic.Add("F_TMAX", txtTMAX.Text);
                    oParamDic.Add("F_TMIN", txtTMIN.Text);
                    oParamDic.Add("F_HIPISNG", rdoHIPISNG.Value == null ? "" : rdoHIPISNG.SelectedItem.Value.ToString());

                    using (BSIFBiz biz = new BSIFBiz())
                    {
                        bExecute = biz.QCD34_PROC_UPD(oParamDic);
                    }

                    if (!bExecute)
                    {
                        procResult = new string[] { "1", "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                    }
                    else
                    {
                        procResult = new string[] { "1", "저장이 완료되었습니다." };
                    }
                }
                else if (e.Parameters.ToString() == "DELETE")
                {
                    string[] oSPs = new string[1];
                    object[] oParameters = new object[1];

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                    oParamDic.Add("F_INSPCD", txtINSPCD.Text);
                    oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
                    oParamDic.Add("F_KEMARK", String.Format("Data Deleted by {0}", gsUSERNM));
                    oParamDic.Add("F_USER", gsUSERID);
                    oParamDic.Add("F_OUTMSG", "OUTPUT");

                    oSPs[0] = "USP_QCD34_DEL_CHK";
                    oParameters[0] = (object)oParamDic;

                    bool bExecute1 = false;
                    string resultMsg = String.Empty;

                    using (BSIFBiz biz = new BSIFBiz())
                    {
                        bExecute1 = biz.PROC_BATCH_UPDATE(oSPs, oParameters, out oOutParamList, out resultMsg);
                    }

                    if (!bExecute1)
                    {
                        procResult = new string[] { "1", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
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
                            procResult = new string[] { "1", sb_OutMsg.ToString() };
                        }
                        else
                        {
                            procResult = new string[] { "1", "저장이 완료되었습니다." };
                        }
                    }
                }

                devGrid.JSProperties["cpResultCode2"] = procResult[0];
                devGrid.JSProperties["cpResultMsg2"] = procResult[1];
            }
            nCurrPage = ucPager.GetCurrPage();
            nPageSize = ucPager.GetPageSize();
            // 검사기준목록을 구한다
            GetQCD34_NEW_LST(nPageSize, nCurrPage, true);
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
            //string param = Request.Params.Get("__CALLBACKPARAM");
            //if (!String.IsNullOrEmpty(param))
            //{
            //    if (!param.Contains("CANCELEDIT"))
            //    {
            //        DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            //        devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = devComboBox.ID.Equals("ddlINSPCD") ? "선택하세요" : "선택안함", Value = "", Selected = true });
            //    }
            //}

            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = devComboBox.ID.Equals("ddlINSPCD") ? "선택하세요" : "선택안함", Value = "", Selected = true });
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_ItemPopUP()");
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_MeainspPopUP()");
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_WorkcdPopUP()");
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
                        ds = biz.GetQCD01_LST(oParamDic, out errMsg);
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

       

        #endregion
    }
}