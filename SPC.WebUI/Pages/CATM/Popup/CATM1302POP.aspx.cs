using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using DevExpress.Web;
using SPC.WebUI.Common;
using SPC.CATM.Biz;
using SPC.Common.Biz;
using SPC.WebUI.Common.Library;

namespace SPC.WebUI.Pages.CATM.Popup
{
    public partial class CATM1302POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string F_COMPCD     = String.Empty;
        string F_FACTCD     = String.Empty;
        string F_WORKDATE   = String.Empty;
        string F_MACHNM     = String.Empty;
        string F_ITEMCD     = String.Empty;
        string F_WORKNO     = String.Empty;
        string F_MOLDNO     = String.Empty;
        string F_MOLDNTH    = String.Empty;
        string F_LOTNO      = String.Empty;
        string F_PRODCOUNT  = String.Empty;
        string F_ERRCOUNT   = String.Empty;


        
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
                devGrid.DataBind();
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
            F_COMPCD    = oKeyFields[0];
            F_FACTCD    = oKeyFields[1];            
            F_WORKDATE  = oKeyFields[2];
            F_MACHNM    = oKeyFields[3];
            F_ITEMCD    = oKeyFields[4];
            F_WORKNO    = oKeyFields[5];
            F_MOLDNO    = oKeyFields[6];
            F_MOLDNTH   = oKeyFields[7];
            F_LOTNO     = oKeyFields[8];
            F_PRODCOUNT = oKeyFields[9];
            F_ERRCOUNT  = oKeyFields[10];


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
            schF_WORKDATE.Text  = F_WORKDATE.ToString();
            schF_MACHNM.Text    = F_MACHNM.ToString();
            schF_ITEMCD.Text    = F_ITEMCD.ToString();
            schF_WORKNO.Text    = F_WORKNO.ToString();
            schF_MOLDNO.Text    = F_MOLDNO.ToString();
            schF_MOLDNTH.Text   = F_MOLDNTH.ToString();
            schF_LOTNO.Text     = F_LOTNO.ToString();
            schF_PRODCOUNT.Text = F_PRODCOUNT.ToString();
            schF_ERRCOUNT.Text  = F_ERRCOUNT.ToString();

        }
        #endregion

        

        #endregion

        

        #region 사용자 정의 함수     

        #region CATM1302POP_LST1
        void CATM1302POP_LST1()
        {
            string errMsg = String.Empty;

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD",F_COMPCD);
                oParamDic.Add("F_FACTCD", F_FACTCD);
                oParamDic.Add("F_WORKNO", F_WORKNO);

                ds = biz.USP_CATM1302_POP(oParamDic, out errMsg);
            }

            DataTable dt1 = ds.Tables[0]; // 부적합유형
            DataTable dt2 = ds.Tables[1]; // 손실시간

            devGrid.DataSource = dt1;
            devGrid2.DataSource = dt2;

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;

                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                {
                    devGrid.DataBind();
                    devGrid2.DataBind();
                }

            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            CATM1302POP_LST1();
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }
        #endregion

        #endregion
    }
}