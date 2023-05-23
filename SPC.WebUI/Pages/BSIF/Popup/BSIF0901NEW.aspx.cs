using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SPC.WebUI.Common;
using SPC.BSIF.Biz;

namespace SPC.WebUI.Pages.BSIF.Popup
{
    public partial class BSIF0901NEW : WebUIBasePage
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

            // 작지목록
            MPPLN03V_SPC_LST();
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

        #region 작지목록
        void MPPLN03V_SPC_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkCD());

                ds = biz.MPPLN03V_SPC_LST(oParamDic, out errMsg);
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

        #region 채번
        string WORK01_IDX()
        {
            string result = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                result = biz.WORK01_IDX(oParamDic);
            }

            return result;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameters.Split('|');

            if (oParams[0].Equals("SEARCH"))
            {
                // 작지목록
                MPPLN03V_SPC_LST();
            }
            else if (oParams[0].Equals("SAVE"))
            {
                string idx = WORK01_IDX();
                List<string> oSPList = new List<string>();
                List<object> oParamList = new List<object>();

                // 업무지시
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKID", idx);
                oParamDic.Add("F_WORKTXT", F_WORKTXT.Text);
                oParamDic.Add("F_USERID", gsUSERID);
                oSPList.Add("USP_WORK01_INS");
                oParamList.Add((object)oParamDic);

                foreach (string PLANCD in oParams[1].Split(','))
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_WORKID", idx);
                    oParamDic.Add("F_PLANCD", PLANCD);
                    oParamDic.Add("F_USERID", gsUSERID);
                    oSPList.Add("USP_WORK02_INS");
                    oParamList.Add((object)oParamDic);
                }

                #region Database Execute
                bool bExecute = false;
                string resultMsg = String.Empty;

                using (BSIFBiz biz = new BSIFBiz())
                {
                    bExecute = biz.PROC_QCD34_BATCH(oSPList.ToArray(), oParamList.ToArray(), out resultMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };
                }

                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
                #endregion
            }
        }
        #endregion

        #endregion
    }
}