using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.BSIF.Biz;
using SPC.SYST.Biz;
using SPC.Common.Biz;
using SPC.IPCM.Biz;

namespace SPC.WebUI.Pages.IPCM.Popup
{
    public partial class IPCM0101POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string F_COMPCD = String.Empty;
        string F_FACTCD = String.Empty;
        string F_ITEMCD = String.Empty;
        string F_ITEMNM = String.Empty;
        string F_WORKCD = String.Empty;
        string F_WORKNM = String.Empty;
        string F_INSPCD = String.Empty;
        string F_INSPNM = String.Empty;
        string F_STDT = String.Empty;
        string F_EDDT = String.Empty;
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
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
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
            F_COMPCD = Request.QueryString.Get("P_COMPCD");
            F_FACTCD = Request.QueryString.Get("P_FACTCD");
            F_ITEMCD = Request.QueryString.Get("P_ITEMCD");
            F_ITEMNM = Request.QueryString.Get("P_ITEMNM");
            F_WORKCD = Request.QueryString.Get("P_WORKCD");
            F_WORKNM = Request.QueryString.Get("P_WORKNM");
            F_INSPCD = Request.QueryString.Get("P_INSPCD");
            F_INSPNM = Request.QueryString.Get("P_INSPNM");
            F_STDT = Request.QueryString.Get("P_STDT");
            F_EDDT = Request.QueryString.Get("P_EDDT");
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
        {
            // 부모업체인 경우 관리번호 입력하지 않는다
            if (!gsVENDOR)
            {
                txtCTRLNO.ClientSideEvents.Init = "fn_OnControlDisableBox";
            }

            DateTime dtToday = DateTime.Today;
            txtRQDATE.Date = dtToday;
            txtRQRCDT.Date = dtToday.AddDays(7);
            txtOCSTDT.Date = Convert.ToDateTime(F_STDT);
            txtOCEDDT.Date = Convert.ToDateTime(F_EDDT);

            ucComp.compParam = F_COMPCD;
            ucFact.factParam = F_FACTCD;

            // 반정보를 셋팅한다
            txtBANCD.Text = F_WORKCD.Substring(0, 2);

            // 라인정보를 셋팅한다
            txtLINECD.Text = F_WORKCD.Substring(2, 2);

            // 품목정보를 셋팅한다
            txtITEMCD.Text = F_ITEMCD;
            txtITEMNM.Text = F_ITEMNM;

            // 공정정보를 셋팅한다
            txtWORKCD.Text = F_WORKCD;
            txtWORKNM.Text = F_WORKNM;

            // 부적합유형
            SetUNSTTP();

            // 대책부서
            SetDEPARTCD();

            // 모델명을 셋팅한다
            SetModelNM();
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 부적합유형
        void SetUNSTTP()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                ds = biz.QCD103_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            { }
            else
            {
                if (!bExistsDataSet(ds))
                { }
                else
                {
                    ddlUNSTTP.TextField = "F_TRNM";
                    ddlUNSTTP.ValueField = "F_TRCD";
                    ddlUNSTTP.DataSource = ds;
                    ddlUNSTTP.DataBind();
                }
            }
        }
        #endregion

        #region 대책부서
        void SetDEPARTCD()
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", F_COMPCD);
                oParamDic.Add("F_FACTCD", F_FACTCD);
                oParamDic.Add("F_CODEGROUP", "20");
                oParamDic.Add("F_LANGTYPE", gsLANGTP);
                ds = biz.SYCOD01_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            { }
            else
            {
                if (!bExistsDataSet(ds))
                { }
                else
                {
                    ddlDEPARTCD.TextField = "F_CODENM";
                    ddlDEPARTCD.ValueField = "F_CODE";
                    ddlDEPARTCD.DataSource = ds;
                    ddlDEPARTCD.DataBind();
                }
            }
        }
        #endregion

        #region 모델명을 셋팅한다
        void SetModelNM()
        {
            string errMsg = String.Empty;
            string modelCD = String.Empty;
            string modelNM = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", F_COMPCD);
                oParamDic.Add("F_FACTCD", F_FACTCD);
                oParamDic.Add("F_ITEMCD", F_ITEMCD);
                ds = biz.QCD17_MODELNM_GET(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            { }
            else
            {
                if (!bExistsDataSet(ds))
                { }
                else
                {
                    modelCD = ds.Tables[0].Rows[0]["F_MODELCD"].ToString();
                    modelNM = ds.Tables[0].Rows[0]["F_MODELNM"].ToString();
                }
            }

            txtMODELCD.Text = modelCD;
            txtMODELNM.Text = modelNM;
        }
        #endregion

        #endregion

        #region 사용자이벤트

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

        #region ddlComboBox_DataBound
        /// <summary>
        /// ddlComboBox_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlComboBox_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
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

            if (oParams[0].Equals("SAVE"))
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", F_COMPCD);
                oParamDic.Add("F_FACTCD", F_FACTCD);
                oParamDic.Add("F_CTRLNO", txtCTRLNO.Text);
                oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                oParamDic.Add("F_MODELCD", txtMODELCD.Text);
                oParamDic.Add("F_BANCD", txtBANCD.Text);
                oParamDic.Add("F_LINECD", txtLINECD.Text);
                oParamDic.Add("F_WORKCD", txtWORKCD.Text);
                oParamDic.Add("F_LOTNO", txtLOTNO.Text);
                oParamDic.Add("F_UNSTTP", hidUNSTTP.Text);
                oParamDic.Add("F_DEPARTCD", hidDEPARTCD.Text);
                oParamDic.Add("F_MEASGD", ddlMEASGD.SelectedItem.Value.ToString());
                oParamDic.Add("F_OCSTDT", txtOCSTDT.Date.ToString("yyyy-MM-dd"));
                oParamDic.Add("F_OCEDDT", txtOCEDDT.Date.ToString("yyyy-MM-dd"));
                oParamDic.Add("F_RQRCDT", txtRQRCDT.Date.ToString("yyyy-MM-dd"));
                oParamDic.Add("F_BIMPROVE", "0");
                oParamDic.Add("F_PROGRESS", "1");
                oParamDic.Add("F_BVENDOR", !gsVENDOR ? "0" : "1");
                oParamDic.Add("F_RQDATE", txtRQDATE.Date.ToString("yyyy-MM-dd"));
                oParamDic.Add("F_RQCPCD", gsCOMPCD);
                oParamDic.Add("F_RQFTCD", gsFACTCD);
                oParamDic.Add("F_RQUSID", gsUSERID);
                oParamDic.Add("F_RQUSNM", gsUSERNM);
                oParamDic.Add("F_RQTXT1", txtRQTXT1.Text);
                oParamDic.Add("F_RQTXT2", txtRQTXT2.Text);
                oParamDic.Add("F_RQTXT3", txtRQTXT3.Text);
                oParamDic.Add("F_RQTXT4", txtRQTXT4.Text);
                oParamDic.Add("F_RQTXT5", txtRQTXT5.Text);
                oParamDic.Add("F_RQFILE", txtIMAGESEQ.Text);

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
                    procResult = new string[] { "99", "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                    devCallback.JSProperties["cpResultCode"] = procResult[0];
                    devCallback.JSProperties["cpResultMsg"] = procResult[1];
                }
                else
                {
                    procResult = new string[] { "save", "등록이 완료되었습니다" };
                    devCallback.JSProperties["cpResultCode"] = procResult[0];
                    devCallback.JSProperties["cpResultMsg"] = procResult[1];
                }
            }
            else
            {
                switch (oParams[0])
                {
                    case "ITEM":  // 품목
                        devCallback.JSProperties["cpIDCD"] = "ITEMCD";
                        devCallback.JSProperties["cpIDNM"] = "ITEMNM";
                        devCallback.JSProperties["cpMODELNM"] = "MODELNM";

                        using (CommonBiz biz = new CommonBiz())
                        {
                            oParamDic.Clear();
                            oParamDic.Add("F_COMPCD", F_COMPCD);
                            oParamDic.Add("F_FACTCD", F_FACTCD);
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
                            oParamDic.Add("F_COMPCD", F_COMPCD);
                            oParamDic.Add("F_FACTCD", F_FACTCD);
                            oParamDic.Add("F_WORKCD", oParams[1]);
                            oParamDic.Add("F_STATUS", "1");
                            ds = biz.GetQCD74_LST(oParamDic, out errMsg);
                        }

                        if (!String.IsNullOrEmpty(errMsg))
                        {
                            devCallback.JSProperties["cpResultCode"] = "99";
                            devCallback.JSProperties["cpResultMsg"] = errMsg;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count.Equals(1))
                            {
                                devCallback.JSProperties["cpCODE"] = ds.Tables[0].Rows[0]["F_WORKCD"].ToString();
                                devCallback.JSProperties["cpTEXT"] = ds.Tables[0].Rows[0]["F_WORKNM"].ToString();
                                devCallback.JSProperties["cpMODEL"] = "";

                                bExecute = true;
                            }
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

                        if (!String.IsNullOrEmpty(errMsg))
                        {
                            devCallback.JSProperties["cpResultCode"] = "99";
                            devCallback.JSProperties["cpResultMsg"] = errMsg;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count.Equals(1))
                            {
                                devCallback.JSProperties["cpCODE"] = ds.Tables[0].Rows[0]["F_MEAINSPCD"].ToString();
                                devCallback.JSProperties["cpTEXT"] = ds.Tables[0].Rows[0]["F_INSPDETAIL"].ToString();

                                bExecute = true;
                            }
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
        }
        #endregion

        #endregion
    }
}