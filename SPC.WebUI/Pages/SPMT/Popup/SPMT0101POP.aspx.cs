using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.SPMT.Biz;

namespace SPC.WebUI.Pages.SPMT.Popup
{
    public partial class SPMT0101POP : WebUIBasePage
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

        #region 마스터 키를 구한다
        string SHP01_MASTER_GET()
        {
            string F_GROUPCD = String.Empty;

            using (SPMTBiz biz = new SPMTBiz())
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                F_GROUPCD = biz.SHP01_MASTER_GET(oParamDic);
            }

            return F_GROUPCD;
        }
        #endregion

        #region 저장된 마스터 정보 구하기
        DataSet SHP01_GET(string F_GROUPCD, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPMTBiz biz = new SPMTBiz())
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_GROUPCD", F_GROUPCD);
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                ds = biz.SHP01_GET(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            // 마스터 키를 구한다
            string F_GROUPCD = SHP01_MASTER_GET();

            int idx = 0;

            int count = txtKEY.Text.Split(',').Length + 2;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_GROUPCD", F_GROUPCD);
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_SHIPCOMPCD", hidCOMPCODE.Text);
            oParamDic.Add("F_SHIPCOMPNM", hidCOMPNM.Text);
            oParamDic.Add("F_EONO", txtEONO.Text);
            oParamDic.Add("F_MATERLOTNO", txtMATERLOTNO.Text);
            oParamDic.Add("F_WORKLOTNO", txtWORKLOTNO.Text);
            oParamDic.Add("F_DCNT", txtDCNT.Text);
            oParamDic.Add("F_DIRECTOR", txtDIRECTOR.Text);
            oParamDic.Add("F_DATE", DateTime.Today.ToString("yyyy-MM-dd"));
            oParamDic.Add("F_USER", gsUSERID);

            oSPs[idx] = "USP_SHP01_INS";
            oParameters[idx] = (object)oParamDic;
            idx++;

            foreach (string Value in txtKEY.Text.Split(','))
            {
                string[] oParam = Value.Split('|');
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_GROUPCD", F_GROUPCD);
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDATE", oParam[0]);
                oParamDic.Add("F_BANCD", oParam[1]);
                oParamDic.Add("F_LINECD", oParam[2]);
                oParamDic.Add("F_ITEMCD", oParam[3]);
                oParamDic.Add("F_WORKCD", oParam[4]);
                oParamDic.Add("F_TSERIALNO", oParam[5]);

                oSPs[idx] = "USP_SHP02_INS";
                oParameters[idx] = (object)oParamDic;

                idx++;
            }

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_GROUPCD", F_GROUPCD);
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ADDROW", "0");

            oSPs[idx] = "USP_SHP03_INS";
            oParameters[idx] = (object)oParamDic;

            idx++;

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (SPMTBiz biz = new SPMTBiz())
            {
                bExecute = biz.PROC_SHIPMENT_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
            }

            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
            #endregion
        }
        #endregion

        #endregion
    }
}