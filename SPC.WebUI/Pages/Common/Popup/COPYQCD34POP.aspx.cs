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

namespace SPC.WebUI.Pages.Common.Popup
{
    public partial class COPYQCD34POP : WebUIBasePage
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
                // devGrid.JSProperties["cpResultCode"] = "";
                // devGrid.JSProperties["cpResultMsg"] = "";
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

        #endregion

        #region 사용자이벤트

        #region txtITEMCDS_Init
        /// <summary>
        /// txtITEMCDS_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCDS_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupItemSearch('S')");
        }
        #endregion

        #region txtITEMCDT_Init
        /// <summary>
        /// txtITEMCDT_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCDT_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupItemSearch('T')");
        }
        #endregion

        #region txtWORKCDS_Init
        /// <summary>
        /// txtWORKCDS_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtWORKCDS_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWorkSearch('S')");
        }
        #endregion

        #region txtWORKCDT_Init
        /// <summary>
        /// txtWORKCDT_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtWORKCDT_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWorkSearch('T')");
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
            bool bExecute = false;
            string oParam = e.Parameter;
            string[] oSPs = new string[1];
            object[] oParameters = new object[1];

            string resultMsg = String.Empty;

            if (oParam.Equals("Exists"))
            {
                // 기존에 사용중인 검사기준서가 있는가??
                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("T_ITEMCD", txtITEMCDT.Text);
                    oParamDic.Add("T_WORKCD", txtWORKCDT.Text);
                    bExecute = biz.QCD34_PROC_EXISTS(oParamDic);
                }

                if (true == bExecute)
                {
                    devCallback.JSProperties["cpProcType"] = "Exists";
                    devCallback.JSProperties["cpResultCode"] = "0";
                    devCallback.JSProperties["cpResultMsg"] = "이미 사용중인 검사기준서가 존재합니다.\r계속해서 진행하시면 기존 검사기준서를 삭제한 후 복사가 진행됩니다.\n계속해서 진행하려면 확인을 누르세요";
                }
                else
                {
                    using (BSIFBiz biz = new BSIFBiz())
                    {
                        oParamDic = new Dictionary<string, string>();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("S_ITEMCD", txtITEMCDS.Text);
                        oParamDic.Add("S_WORKCD", txtWORKCDS.Text);
                        oParamDic.Add("T_ITEMCD", txtITEMCDT.Text);
                        oParamDic.Add("T_WORKCD", txtWORKCDT.Text);
                        oParamDic.Add("F_USER", gsUSERID);
                        oParamDic.Add("F_OUTMSG", "OUTPUT");

                        oSPs[0] = "USP_QCD34_COPY";
                        oParameters[0] = (object)oParamDic;

                        bExecute = biz.PROC_BATCH_UPDATE(oSPs, oParameters, out oOutParamList, out resultMsg);
                    }

                    if (!bExecute)
                    {
                        devCallback.JSProperties["cpProcType"] = "Copy";
                        devCallback.JSProperties["cpResultCode"] = "0";
                        devCallback.JSProperties["cpResultMsg"] = "검사기준 복사중 장애가 발생하였습니다.\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
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
                            procResult = new string[] { "1", "검사기준 복사가 완료되었습니다." };
                        }

                        devCallback.JSProperties["cpProcType"] = "Copy";
                        devCallback.JSProperties["cpResultCode"] = procResult[0];
                        devCallback.JSProperties["cpResultMsg"] = procResult[1];
                    }
                }
            }
            else if (oParam.Equals("Copy"))
            {
                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("S_ITEMCD", txtITEMCDS.Text);
                    oParamDic.Add("S_WORKCD", txtWORKCDS.Text);
                    oParamDic.Add("T_ITEMCD", txtITEMCDT.Text);
                    oParamDic.Add("T_WORKCD", txtWORKCDT.Text);
                    oParamDic.Add("F_USER", gsUSERID);
                    oParamDic.Add("F_OUTMSG", "OUTPUT");

                    oSPs[0] = "USP_QCD34_COPY";
                    oParameters[0] = (object)oParamDic;

                    bExecute = biz.PROC_BATCH_UPDATE(oSPs, oParameters, out oOutParamList, out resultMsg);
                }

                if (!bExecute)
                {
                    devCallback.JSProperties["cpProcType"] = "Copy";
                    devCallback.JSProperties["cpResultCode"] = "0";
                    devCallback.JSProperties["cpResultMsg"] = "검사기준 복사중 장애가 발생하였습니다.\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
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
                        procResult = new string[] { "1", "검사기준 복사가 완료되었습니다." };
                    }

                    devCallback.JSProperties["cpProcType"] = "Copy";
                    devCallback.JSProperties["cpResultCode"] = procResult[0];
                    devCallback.JSProperties["cpResultMsg"] = procResult[1];
                }
            }
        }
        #endregion

        #endregion
    }
}