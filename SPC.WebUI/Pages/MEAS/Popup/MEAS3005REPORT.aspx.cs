﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.WebUI.Common.Library;
using SPC.WebUI.Pages.ANLS.Report;
using SPC.ANLS.Biz;
using SPC.WebUI.Pages.MEAS.Report;

namespace SPC.WebUI.Pages.MEAS.Popup
{
    public partial class MEAS3005REPORT : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string KEYFIELDS = String.Empty;
        ImageUtils imgUtils = new ImageUtils();
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

            string[] oParams = KEYFIELDS.Split('|');
            DataTable dtChart = (DataTable)Session["MEAS3005_0"]; // 정보
            DataTable dtChart1 = (DataTable)Session["MEAS3005_1"]; // 정보
            DataTable dtChart2 = (DataTable)Session["MEAS3005_2"]; // 교정이력
            DataTable dtChart3 = (DataTable)Session["MEAS3005_3"]; // 부속품
            System.Drawing.Image dtimg = (System.Drawing.Image)Session["MEAS3005_IMG"]; // 부속품


            if (dtChart1 == null)
            {
                //Redirect HTTP errors to HttpError page
                Server.Transfer("~/Pages/ERROR/Report.aspx");
            }
            else
            {
                //System.Drawing.Image imgChartH = GetHChartImg(oParams, dtChart1);
                System.Drawing.Image imgChartH = null;

                MEAS3005RPT report = new MEAS3005RPT(oParams, dtChart, dtChart1, dtChart2, dtChart3, dtimg);
                devDocument.Report = report;

                // Grid Columns Sum Width
                // hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
            }
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
        {
            KEYFIELDS = HttpUtility.UrlDecode(Request.QueryString.Get("KEYFIELDS"));
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

        #endregion

        #region 사용자이벤트

        #endregion
    }
}