using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.PLCM.Biz;
using System.Text;
using DevExpress.Web;

namespace SPC.WebUI.Pages.Common.Popup
{
    public partial class COPYPHIL34POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티

        // 스텝
        DataTable CachedData1
        {
            get { return Session["COPYPHIL34POP_RE"] as DataTable; }
            set { Session["COPYPHIL34POP_RE"] = value; }
        }

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
        {
            BindCombo(schF_MACHCDS);
            BindCombo(schF_MACHCDT);
            BindCombo2(txtWORKCDS);
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

        // 콤보박스 목록 조회
        void BindCombo(ASPxComboBox c)
        {
            var dic = new Dictionary<string, string>();
            string errMsg = string.Empty;
            DataTable dt = this.GetDataCombo(dic, out errMsg);
            c.DataSource = dt;
            c.TextField = "F_MACHNM";
            c.ValueField = "F_MACHCD";
            c.DataBind();
            c.Items.Insert(0, new ListEditItem("--선택--", ""));
            c.SelectedIndex = 0;
        }

        // 콤보박스 목록 조회
        void BindCombo2(ASPxComboBox c)
        {
            var dic = new Dictionary<string, string>();
            string errMsg = string.Empty;
            DataTable dt = this.GetDataCombo2(dic, out errMsg);
            c.DataSource = dt;
            c.TextField = "F_RECIPID";
            c.ValueField = "F_RECIPID";
            c.DataBind();
            c.Items.Insert(0, new ListEditItem("--선택--", ""));
            c.SelectedIndex = 0;
        }


        #region 설비 목록 조회
        /// <summary>
        /// 우측 내용 조회(추진일정 목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataCombo(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHTYPECD"] = "";
                oParamDic["F_USEYN"] = "1";
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.PLC01_LST(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region 레시피 목록 조회
        /// <summary>
        /// 우측 내용 조회(추진일정 목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataCombo2(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCDS.Value ?? "").ToString();
                ds = biz.PLC04_LST(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region STEP구하기
        void GetSTEP()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCDS.Value ?? "").ToString();
                ds = biz.PLC06_LST(oParamDic, out errMsg);
            }
            CachedData1 = ds.Tables[0];

            schF_STEPS.DataSource = CachedData1;
            schF_STEPS.TextField = "F_STEPNM";
            schF_STEPS.ValueField = "F_STEP";
            schF_STEPS.DataBind();
            schF_STEPS.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "" });

        }
        #endregion

        #region STEP2구하기
        void GetSTEP2()
        {
            schF_STEPT.DataSource = CachedData1;
            schF_STEPT.TextField = "F_STEPNM";
            schF_STEPT.ValueField = "F_STEP";
            schF_STEPT.DataBind();
            schF_STEPT.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "" });

        }
        #endregion

        #region 사용자이벤트

        #region txtWORKCDS_Callback

        protected void txtWORKCDS_Callback(object sender, CallbackEventArgsBase e)
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCDS.Value ?? "").ToString();
                oParamDic["F_COPYYN"] = "1";
                ds = biz.PLC04_LST(oParamDic, out errMsg);
            }
            txtWORKCDS.DataSource = ds;
            txtWORKCDS.TextField = "F_RECIPID";
            txtWORKCDS.ValueField = "F_RECIPID";
            txtWORKCDS.DataBind();
            txtWORKCDS.Items.Insert(0, new ListEditItem("--선택--", ""));
            txtWORKCDS.SelectedIndex = 0;
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
                using (PLCMBiz biz = new PLCMBiz())
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_MACHCD", (this.schF_MACHCDT.Value ?? "").ToString());
                    oParamDic.Add("F_RECIPID", txtWORKCDT.Text);
                    bExecute = biz.PHIL34_PROC_EXISTS(oParamDic);
                }

                if (true == bExecute)
                {
                    devCallback.JSProperties["cpProcType"] = "Exists";
                    devCallback.JSProperties["cpResultCode"] = "0";
                    devCallback.JSProperties["cpResultMsg"] = "이미 사용중인 레시피의 검사기준서가 존재합니다.\r계속해서 진행하시면 기존 검사기준서를 삭제한 후 복사가 진행됩니다.\n계속해서 진행하려면 확인을 누르세요";
                }
                else
                {
                    using (PLCMBiz biz = new PLCMBiz())
                    {
                        oParamDic = new Dictionary<string, string>();
                        oParamDic.Add("S_MACHCD", (this.schF_MACHCDS.Value ?? "").ToString());
                        oParamDic.Add("S_STEP", (this.schF_STEPS.Value ?? "").ToString());
                        oParamDic.Add("S_WORKCD", (this.txtWORKCDS.Value ?? "").ToString());
                        oParamDic.Add("T_MACHCD", (this.schF_MACHCDT.Value ?? "").ToString());
                        oParamDic.Add("T_STEP", (this.schF_STEPT.Value ?? "").ToString());
                        oParamDic.Add("T_WORKCD", txtWORKCDT.Text);
                        oParamDic.Add("F_OUTMSG", "OUTPUT");

                        oSPs[0] = "USP_PHIL34_COPY";
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
        }
        #endregion

        protected void schF_STEPS_Callback(object sender, CallbackEventArgsBase e)
        {
            GetSTEP();
        }

        protected void schF_STEPT_Callback(object sender, CallbackEventArgsBase e)
        {
            GetSTEP2();
        }

        #endregion
    }
        #endregion
}