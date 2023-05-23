using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;
using System.Text;

namespace SPC.WebUI.Pages.DIOF
{
    public partial class DIOF0102 : WebUIBasePage
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
                // 사용자조회
                RetrieveUserList(devGridWorker);
                RetrieveUserList(devGridManager);
            }

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
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
                devCallback.JSProperties["cpWorker"] = "";
                devCallback.JSProperties["cpManager"] = "";
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

        #region 설비조회
        void RetrieveList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", schF_BANCD.GetValue());
                oParamDic.Add("F_LINECD", schF_LINECD.GetValue());
                oParamDic.Add("F_MACHNM", schF_MACHNM.Text);
                ds = biz.QCD_MACH21_LST(oParamDic, out errMsg);
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

        #region 사용자조회
        void RetrieveUserList(DevExpress.Web.ASPxGridView aspxGridView)
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                ds = biz.QCD_MACH_USR_LST(oParamDic, out errMsg);
            }

            aspxGridView.DataSource = ds;
        }
        #endregion

        #region 담당자조회
        string RetrieveWorerkList()
        {
            string usrList = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                usrList = biz.QCD_MACH22_LST(oParamDic);
            }

            return usrList;
        }
        #endregion

        #region 확인자조회
        string RetrieveManagerList()
        {
            string usrList = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                usrList = biz.QCD_MACH23_LST(oParamDic);
            }

            return usrList;
        }
        #endregion

        #region 저장
        void SaveUserList()
        {
            List<string> oSPs = new List<string>();
            List<Dictionary<string, string>> oParams = new List<Dictionary<string, string>>();
            string resultMsg = String.Empty;
            bool bExecute = true;

            if (srcF_WORKER.Text != "")
            {
                #region 담당자 전체삭제
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParams.Add(oParamDic);
                oSPs.Add("USP_QCD_MACH22_DEL");
                #endregion

                foreach (string sWorker in srcF_WORKER.Text.Split('|'))
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_USERID", sWorker);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QCD_MACH22_INS");
                }
            }

            if (srcF_MANAGER.Text != "")
            {
                #region 확인자 전체삭제
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParams.Add(oParamDic);
                oSPs.Add("USP_QCD_MACH23_DEL");
                #endregion

                foreach (string sManager in srcF_MANAGER.Text.Split('|'))
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_USERID", sManager);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QCD_MACH23_INS");
                }
            }

            if (oSPs.Count > 0 && oParams.Count > 0 && oSPs.Count == oParams.Count)
            {
                using (FDCKBiz biz = new FDCKBiz())
                {
                    bExecute = biz.PROC_QCD_MACH_MULTI(oSPs.ToArray(), oParams.ToArray(), out resultMsg);
                }

                if(!String.IsNullOrEmpty(resultMsg))
                    bExecute = false;
            }

            if (!bExecute)
            {
                devCallback.JSProperties["cpResultCode"] = "0";
                devCallback.JSProperties["cpResultMsg"] = resultMsg;
            }
            else
            {
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 설비조회
            RetrieveList();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetBoolString(new string[] { "F_USEYN" }, "가동", "비가동", e);
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            if (e.Parameter.Equals("S"))
            {
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";

                // 담당자조회
                devCallback.JSProperties["cpWorker"] = RetrieveWorerkList();

                // 확인자조회
                devCallback.JSProperties["cpManager"] = RetrieveManagerList();
            }
            else if (e.Parameter.Equals("P"))
            {
                SaveUserList();
            }
        }
        #endregion

        #endregion
    }
}