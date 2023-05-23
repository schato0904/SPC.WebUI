using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.TISP.Biz;

namespace SPC.WebUI.Pages.TISP.Popup
{
    public partial class TISP0103POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        string[] keyFields = new string[3];
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
                //// 라인모니터링 팝업 조회
                //QWK09A_TISP0103_POPUP_LST(50, 1, true);
            }

            // Grid Columns Sum Width
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
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                ucPager.TotalItems = 0;
                ucPager.PagerDataBind();
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
            hidCOMP.Text = keyFields[0];
            hidFACT.Text = keyFields[1];
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

        #region Data총갯수
        Int32 QWK09A_TISP0103_POPUP_CNT()
        {
            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", keyFields[0]);
                oParamDic.Add("F_FACTCD", keyFields[1]);
                oParamDic.Add("F_ITEMCD", keyFields[2]);
                oParamDic.Add("F_WORKCD", keyFields[3]);
                oParamDic.Add("F_FROMDT", keyFields[4]);
                oParamDic.Add("F_TODT", keyFields[5]);
                
                totalCnt = biz.QWK09A_TISP0103_POPUP_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region 라인모니터링 팝업 조회
        void QWK09A_TISP0103_POPUP_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", keyFields[0]);
                oParamDic.Add("F_FACTCD", keyFields[1]);
                oParamDic.Add("F_ITEMCD", keyFields[2]);
                oParamDic.Add("F_WORKCD", keyFields[3]);
                oParamDic.Add("F_FROMDT", keyFields[4]);
                oParamDic.Add("F_TODT", keyFields[5]);
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());

                ds = biz.QWK09A_TISP0103_POPUP_LST(oParamDic, out errMsg);
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
                if (!bCallback)
                {
                    // Pager Setting
                    ucPager.TotalItems = QWK09A_TISP0103_POPUP_CNT();
                    ucPager.PagerDataBind();
                }
                else
                {
                    devGrid.JSProperties["cpResultCode"] = "pager";
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, QWK09A_TISP0103_POPUP_CNT());
                }
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["F_STATUS"] = true;
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
            DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                string[] oParams = new string[2];
                foreach (string oParam in e.Parameters.Split(';'))
                {
                    oParams = oParam.Split('=');
                    if (oParams[0].Equals("PAGESIZE"))
                        nPageSize = Convert.ToInt32(oParams[1]);
                    else if (oParams[0].Equals("CURRPAGE"))
                        nCurrPage = Convert.ToInt32(oParams[1]);
                }
            }
            else
            {
                nCurrPage = 1;
                nPageSize = ucPager.GetPageSize();
            }

            QWK09A_TISP0103_POPUP_LST(nPageSize, nCurrPage, true);
            devGrid.DataBind();
        }
        #endregion

        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            string strJudge = e.GetValue("F_NGOKCHK").ToString();

            if (strJudge == "1")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Row.ForeColor = System.Drawing.Color.Blue;
            }
        }

        #endregion
    }
}