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

namespace SPC.WebUI.Pages.Common.Popup
{
    public partial class WORKPOP_MULTI : WebUIBasePage
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
            hidOpenType.Text = Request.QueryString.Get("TYPE") ?? "";

            if (!Page.IsCallback)
            {
                // Request
                GetRequest();

                // 검사항목검색 목록
                //QCD74_POPUP_LST();
            }

            //new ASPxGridViewCellMerger(devGrid, "F_BANCD|F_BANNM");
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
        {
            txtITEMCD.Text = Request.QueryString.Get("ITEMCD") ?? "";
            txtWORKCD.Text = Request.QueryString.Get("WORKCD") ?? "";
            txtMACHGUBUN.Text = Request.QueryString.Get("MACHGUBUN") ?? "";
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

        #region 검사항목검색 목록
        void QCD74_POPUP_LST(string parentParams)
        {
            string errMsg = String.Empty;

            oParamDic.Clear();
            if (gsVENDOR)
            {
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
            }
            else
            {
                string[] parentParam = parentParams.Split('|');
                oParamDic.Add("F_COMPCD", parentParam[0]);
                oParamDic.Add("F_FACTCD", parentParam[1]);
            }

            if (hidOpenType.Text.Equals("S"))
            {
                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic.Add("F_BANCD", GetBanCD());
                    oParamDic.Add("F_LINECD", GetLineCD());
                    oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                    oParamDic.Add("F_WORKCD", txtWORKCD.Text);
                    oParamDic.Add("F_STATUS", "1");
                    ds = biz.QCD74_LST(oParamDic, out errMsg);
                }
            }
            else if (hidOpenType.Text.Equals("INS") || hidOpenType.Text.Equals("T"))
            {
                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic.Add("F_BANCD", GetBanCD());
                    oParamDic.Add("F_LINECD", GetLineCD());
                    oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                    oParamDic.Add("F_WORKCD", txtWORKCD.Text);
                    oParamDic.Add("F_STATUS", "1");
                    oParamDic.Add("F_GUBUN", "1");
                    ds = biz.QCD74_LST(oParamDic, out errMsg);
                }
            }
            else
            {
                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                    oParamDic.Add("F_WORKCD", txtWORKCD.Text);
                    oParamDic.Add("F_BANCD", GetBanCD());
                    oParamDic.Add("F_LINECD", GetLineCD());
                    oParamDic.Add("F_MACHGUBUN", txtMACHGUBUN.Text);
                    oParamDic.Add("F_STATUS", "1");
                    ds = biz.GetQCD74_LST(oParamDic, out errMsg);
                }
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

        void QCD74_POPUP_LST()
        {
            string errMsg = String.Empty;

            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ITEMCD", "");
            oParamDic.Add("F_WORKCD", "");
            oParamDic.Add("F_BANCD", "");
            oParamDic.Add("F_LINECD", "");
            oParamDic.Add("F_MACHGUBUN", "");
            oParamDic.Add("F_STATUS", "1");

            using (CommonBiz biz = new CommonBiz())
            {
                ds = biz.GetQCD74_LST(oParamDic, out errMsg);
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

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] Params = e.Parameters.Split('/');

            if (Params[0] == "ALL")
            {

                QCD74_POPUP_LST();
            }
            else
            {
                QCD74_POPUP_LST(Params[1]);

            }
        }
        #endregion

        #endregion
    }
}