using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.SYST.Biz;
using SPC.Common.Biz;
using SPC.BSIF.Biz;

namespace SPC.WebUI.Pages.PFRC
{
    public partial class PFRC0204 : WebUIBasePage
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
            // hidGridColumnsWidth.Text = DevExpressLib.devCallbackColumnWidth(devCallback).ToString();
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
                // 코드목록을 구한다
                rdoBAN.Value = QCM10_LST("AAH101");
                rdoLINE.Value = QCM10_LST("AAH102");
                rdoWORK.Value = QCM10_LST("AAH103");
                rdoITEM.Value = QCM10_LST("AAH104");
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

        #region 코드목록을 구한다
        string QCM10_LST(string strCommcd)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_GROUPCD", "AAH1");
                oParamDic.Add("F_COMMCD", strCommcd);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.GetCommonCodeList(oParamDic, out errMsg);
            }
            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devCallback.JSProperties["cpResultCode"] = "0";
                devCallback.JSProperties["cpResultMsg"] = errMsg;
            }

            return ds.Tables[0].Rows[0]["F_REMARK1"].ToString();
        }

        private void QCM10_UPD()
        {
            bool bExecute = false;
            string resultMsg = String.Empty;

            string[] oSPs = new string[4];
            object[] oParameters = new object[4];
            int idx = 0;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_GROUPCD", "AAH1");
            oParamDic.Add("F_COMMCD", "AAH101");
            oParamDic.Add("F_USER", gsUSERID);
            oParamDic.Add("F_REMARK1", rdoBAN.SelectedItem.Value.ToString());

            oSPs[idx] = "USP_QCM10_TREE_UPD";
            oParameters[idx] = (object)oParamDic;
            idx++;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_GROUPCD", "AAH1");
            oParamDic.Add("F_COMMCD", "AAH102");
            oParamDic.Add("F_USER", gsUSERID);
            oParamDic.Add("F_REMARK1", rdoLINE.SelectedItem.Value.ToString());

            oSPs[idx] = "USP_QCM10_TREE_UPD";
            oParameters[idx] = (object)oParamDic;
            idx++;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_GROUPCD", "AAH1");
            oParamDic.Add("F_COMMCD", "AAH103");
            oParamDic.Add("F_USER", gsUSERID);
            oParamDic.Add("F_REMARK1", rdoWORK.SelectedItem.Value.ToString());

            oSPs[idx] = "USP_QCM10_TREE_UPD";
            oParameters[idx] = (object)oParamDic;
            idx++;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_GROUPCD", "AAH1");
            oParamDic.Add("F_COMMCD", "AAH104");
            oParamDic.Add("F_USER", gsUSERID);
            oParamDic.Add("F_REMARK1", rdoITEM.SelectedItem.Value.ToString());

            oSPs[idx] = "USP_QCM10_TREE_UPD";
            oParameters[idx] = (object)oParamDic;
            idx++;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.PROC_BATCH_UPDATE(oSPs, oParameters, out oOutParamList, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "1", "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
            }

            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
        }
        #endregion
        
        #endregion

        #region 사용자이벤트

        #region devCallback CustomCallback
        /// <summary>
        /// devCallback_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devCallback_Callback(object sender, DevExpress.Web.CallbackEventArgs e)
        {
            string strParam = e.Parameter;

            if (strParam == "SELECT")
            {
                rdoBAN.Value = QCM10_LST("AAH101");
                rdoLINE.Value = QCM10_LST("AAH102");
                rdoWORK.Value = QCM10_LST("AAH103");
                rdoITEM.Value = QCM10_LST("AAH104");
            }
            else
            {
                QCM10_UPD();
            }

        }
        #endregion

        #endregion
    }
}