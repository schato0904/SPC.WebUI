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

namespace SPC.WebUI.Pages.IPCM.Popup
{
    public partial class IPCM0103POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string F_COMPCD = String.Empty;
        string F_FACTCD = String.Empty;
        string F_INDXNO = String.Empty;
        string F_ITEMCD = String.Empty;
        string F_RQUSID = String.Empty;
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
            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
                devCallbackPanel.JSProperties["cpResultCode"] = "";
                devCallbackPanel.JSProperties["cpResultMsg"] = "";
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
            string[] oKeyFields = Request.QueryString.Get("KEYFIELDS").Split('|');
            F_COMPCD = oKeyFields[0];
            F_FACTCD = oKeyFields[1];
            F_INDXNO = oKeyFields[2];
            F_ITEMCD = oKeyFields[3];
            F_RQUSID = oKeyFields[4];
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

        #region 부적합유형
        void SetUNSTTP(string COMPCD, string FACTCD)
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", COMPCD);
                oParamDic.Add("F_FACTCD", FACTCD);
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

        #region 이상제기정보를 구한다
        void QWK100_GET()
        {
            string errMsg = String.Empty;

            using (IPCMBiz biz = new IPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", F_COMPCD);
                oParamDic.Add("F_FACTCD", F_FACTCD);
                oParamDic.Add("F_INDXNO", F_INDXNO);
                if (!gsVENDOR)
                {
                    oParamDic.Add("F_RQCPCD", gsCOMPCD);
                    oParamDic.Add("F_RQFTCD", gsFACTCD);
                }
                else
                {
                    oParamDic.Add("F_RQCPCD", "");
                    oParamDic.Add("F_RQFTCD", "");
                }
                oParamDic.Add("F_BVENDOR", !gsVENDOR ? "0" : "1");
                oParamDic.Add("F_LANGTP", gsLANGTP);

                ds = biz.QWK100_GET(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devCallbackPanel.JSProperties["cpResultCode"] = "0";
                devCallbackPanel.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devCallbackPanel.JSProperties["cpResultCode"] = "0";
                    devCallbackPanel.JSProperties["cpResultMsg"] = "이상제기 정보를 찾을 수 없습니다.\r일시적인 서버 장애이거나 해당 이상제기가 삭제되었을 수 있습니다.";
                }
                else
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dtRow = ds.Tables[0].Rows[0];

                    string F_RQCPCD = !Convert.ToBoolean(dtRow["F_BVENDOR"]) ? dtRow["F_RQCPCD"].ToString() : F_COMPCD;
                    string F_RQFTCD = !Convert.ToBoolean(dtRow["F_BVENDOR"]) ? dtRow["F_RQFTCD"].ToString() : F_FACTCD;

                    // 부적합유형
                    SetUNSTTP(F_RQCPCD, F_RQFTCD);

                    // 대책부서
                    SetDEPARTCD();

                    // 모델명을 셋팅한다
                    SetModelNM();

                    txtCTRLNO.Text = dtRow["F_CTRLNO"].ToString();
                    txtRQDATE.Date = Convert.ToDateTime(dtRow["F_RQDATE"]);
                    txtRQRCDT.Date = Convert.ToDateTime(dtRow["F_RQRCDT"]);
                    txtOCSTDT.Date = Convert.ToDateTime(dtRow["F_OCSTDT"]);
                    txtOCEDDT.Date = Convert.ToDateTime(dtRow["F_OCEDDT"]);
                    txtITEMCD.Text = dtRow["F_ITEMCD"].ToString();
                    txtITEMNM.Text = dtRow["F_ITEMNM"].ToString();
                    txtBANCD.Text = dtRow["F_BANCD"].ToString();
                    txtLINECD.Text = dtRow["F_LINECD"].ToString();
                    txtWORKCD.Text = dtRow["F_WORKCD"].ToString();
                    txtWORKNM.Text = dtRow["F_WORKNM"].ToString();
                    txtMODELCD.Text = dtRow["F_MODELCD"].ToString();
                    txtMODELNM.Text = dtRow["F_MODELNM"].ToString();
                    txtLOTNO.Text = dtRow["F_LOTNO"].ToString();
                    hidUNSTTP.Text = dtRow["F_UNSTTP"].ToString();
                    ddlUNSTTP.SelectedIndex = ddlUNSTTP.Items.FindByValue(hidUNSTTP.Text).Index;
                    hidDEPARTCD.Text = dtRow["F_DEPARTCD"].ToString();
                    ddlDEPARTCD.SelectedIndex = ddlDEPARTCD.Items.FindByValue(hidDEPARTCD.Text).Index;
                    ddlMEASGD.SelectedIndex = ddlMEASGD.Items.FindByValue(dtRow["F_MEASGD"].ToString()).Index;
                    txtRQFILE.Text = dtRow["F_RQFILE"].ToString();
                    txtRQTXT1.Text = dtRow["F_RQTXT1"].ToString();
                    txtRQTXT2.Text = dtRow["F_RQTXT2"].ToString();
                    txtRQTXT3.Text = dtRow["F_RQTXT3"].ToString();
                    txtRQTXT4.Text = dtRow["F_RQTXT4"].ToString();
                    txtRQTXT5.Text = dtRow["F_RQTXT5"].ToString();

                    txtCTRLNO.ReadOnly = true;
                    txtRQDATE.ReadOnly = true;
                    txtRQRCDT.ReadOnly = true;
                    txtOCSTDT.ReadOnly = true;
                    txtOCEDDT.ReadOnly = true;
                    txtITEMCD.ReadOnly = true;
                    txtITEMNM.ReadOnly = true;
                    txtBANCD.ReadOnly = true;
                    txtLINECD.ReadOnly = true;
                    txtWORKCD.ReadOnly = true;
                    txtWORKNM.ReadOnly = true;
                    txtMODELCD.ReadOnly = true;
                    txtMODELNM.ReadOnly = true;
                    txtLOTNO.ReadOnly = true;
                    ddlUNSTTP.ReadOnly = true;
                    ddlDEPARTCD.ReadOnly = true;
                    ddlMEASGD.ReadOnly = true;
                    txtRQTXT1.ReadOnly = true;
                    txtRQTXT2.ReadOnly = true;
                    txtRQTXT3.ReadOnly = true;
                    txtRQTXT4.ReadOnly = true;
                    txtRQTXT5.ReadOnly = true;

                    txtRSUSNM.Text = dtRow["F_RSUSNM"].ToString();
                    txtRSFILE.Text = dtRow["F_RSFILE"].ToString();
                    txtRSTXT1.Text = dtRow["F_RSTXT1"].ToString();
                    txtRSTXT2.Text = dtRow["F_RSTXT2"].ToString();
                    txtRSTXT3.Text = dtRow["F_RSTXT3"].ToString();
                    txtRSTXT4.Text = dtRow["F_RSTXT4"].ToString();

                    txtRSUSNM.ReadOnly = true;
                    txtRSTXT1.ReadOnly = true;
                    txtRSTXT2.ReadOnly = true;
                    txtRSTXT3.ReadOnly = true;
                    txtRSTXT4.ReadOnly = true;

                    // 이상제기자만 완료가능
                    if (dtRow["F_RQCPCD"].ToString().Equals(gsCOMPCD) && dtRow["F_RQFTCD"].ToString().Equals(gsFACTCD) && dtRow["F_RQUSID"].ToString().Equals(gsUSERID))
                    {
                        bSaveUsed.Text = "1";
                    }

                    // 완료처리된 경우 수정불가
                    if (dtRow["F_PROGRESS"].ToString().Equals("3"))
                    {
                        bSaveUsed.Text = "9";
                    }

                    if (dtRow["F_PROGRESS"].ToString().Equals("3"))
                        rdoIMPROVE.SelectedIndex = rdoIMPROVE.Items.FindByValue(!Convert.ToBoolean(dtRow["F_BIMPROVE"]) ? "0" : "1").Index;
                }
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
                oParamDic.Add("F_INDXNO", F_INDXNO);
                oParamDic.Add("F_BIMPROVE", rdoIMPROVE.SelectedItem.Value.ToString());
                oParamDic.Add("F_CFDATE", DateTime.Today.ToString("yyyy-MM-dd"));
                oParamDic.Add("F_CFCPCD", gsCOMPCD);
                oParamDic.Add("F_CFFTCD", gsFACTCD);
                oParamDic.Add("F_CFUSID", gsUSERID);
                oParamDic.Add("F_CFUSNM", gsUSERNM);
                oParamDic.Add("F_CFTXT1", txtCFTXT1.Text);

                using (IPCMBiz biz = new IPCMBiz())
                {
                    bExecute = biz.QWK100_STEP3_UPD(oParamDic, out errMsg);
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

                    QWK100_GET();
                }
            }
        }
        #endregion

        #region devCallbackPanel_Callback
        /// <summary>
        /// devCallbackPanel_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            QWK100_GET();
        }
        #endregion

        #endregion
    }
}