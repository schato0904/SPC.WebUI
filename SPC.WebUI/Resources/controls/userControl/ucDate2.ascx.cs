using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucDate2 : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        public bool DateTimeOnly { get; set; }
        public bool SingleDate { get; set; }
        public int MaxDate { get; set; }
        public bool TodayFromDate { get; set; }
        public bool MonthOnly { get; set; }
        public int MaxMonth { get; set; }
        public bool CurrentWeekOnly { get; set; }   // 금일 포함된 금주의 일요일부터 당일까지 Min~Max지정
        public string Changed { get; set; }         // 값 변경시 연결 js함수 지정
        public int TodayFromDiff { get; set; }
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
            if (!Page.IsCallback)
            {
                // 객체 초기화
                SetDefaultObject();
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
            // Request
            GetRequest();

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();
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
            // 검색일자 지정여부 확인
            string sStartDate = CommonHelper.GetAppSectionsString("startDate");
            string sEndDate = CommonHelper.GetAppSectionsString("endDate");
            DateTime dtStart, dtEnd;

            bool ConvertSTDT = DateTime.TryParse(sStartDate, out dtStart);
            bool ConvertEDDT = DateTime.TryParse(sEndDate, out dtEnd);

            dtStart = !ConvertSTDT || !ConvertEDDT ? DateTime.Now : dtStart;
            dtEnd = !ConvertSTDT || !ConvertEDDT ? DateTime.Now : dtEnd;

            string sDate = dtStart.ToString("yyyyMMdd").Substring(0, 6) + "01";

            DateTime fromdt = DateTime.ParseExact(sDate + "080000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            DateTime todt = Convert.ToDateTime(dtEnd.ToString("yyyy-MM-dd HH:mm:ss"));

            if (MaxDate != 0)
            {
                sDate = dtStart.AddDays(-MaxDate).ToString("yyyyMMdd");
                fromdt = DateTime.ParseExact(sDate + "080000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            }

            if (TodayFromDiff != 0)
            {
                sDate = dtStart.AddDays(-TodayFromDiff).ToString("yyyyMMdd");
                fromdt = DateTime.ParseExact(sDate + "080000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            }

            if (TodayFromDate)
            {
                sDate = dtStart.ToString("yyyyMMdd");
                fromdt = DateTime.ParseExact(sDate + "080000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            }

            if (!DateTimeOnly)
            {
                this.txtFROMDT2.DisplayFormatString = "yyyy-MM-dd";
                this.txtFROMDT2.EditFormatString = "yyyy-MM-dd";

                this.txtTODT2.DisplayFormatString = "yyyy-MM-dd";
                this.txtTODT2.EditFormatString = "yyyy-MM-dd";

                hidUCFROMDT2.Text = fromdt.ToString("yyyy-MM-dd");
                hidUCTODT2.Text = todt.ToString("yyyy-MM-dd");
            }
            else
            {
                this.txtFROMDT2.DisplayFormatString = "yyyy-MM-dd hh:mm:ss";
                this.txtFROMDT2.EditFormatString = "yyyy-MM-dd hh:mm:ss";

                this.txtTODT2.DisplayFormatString = "yyyy-MM-dd hh:mm:ss";
                this.txtTODT2.EditFormatString = "yyyy-MM-dd hh:mm:ss";

                hidUCFROMDT2.Text = fromdt.ToString("yyyy-MM-dd hh:mm:ss");
                hidUCTODT2.Text = todt.ToString("yyyy-MM-dd hh:mm:ss");
            }

            if (MonthOnly)
            {
                if (MaxMonth != 0)
                {
                    sDate = dtStart.AddMonths(-MaxMonth + 1).ToString("yyyyMMdd");
                    fromdt = DateTime.ParseExact(sDate + "080000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                }

                this.txtFROMDT2.DisplayFormatString = "yyyy-MM";
                this.txtFROMDT2.EditFormatString = "yyyy-MM";

                this.txtTODT2.DisplayFormatString = "yyyy-MM";
                this.txtTODT2.EditFormatString = "yyyy-MM";

                hidUCFROMDT2.Text = fromdt.ToString("yyyy-MM");
                hidUCTODT2.Text = todt.ToString("yyyy-MM");

                //this.txtFROMDT.DisplayFormatString = "";
            }

            this.txtFROMDT2.Date = fromdt;
            this.txtTODT2.Date = todt;

            if (SingleDate)
            {
                this.txtFROMDT2.Date = todt;
                this.Fromdiv.Style.Add("width", "98%");
                this.Todiv.Style.Add("display", "none");
            }

            if (CurrentWeekOnly)
            {
                this.txtFROMDT2.MinDate = dtStart.Date.AddDays(-(double)DateTime.Today.DayOfWeek);
                this.txtFROMDT2.MaxDate = dtStart.Date;
                this.txtTODT2.MinDate = dtEnd.Date.AddDays(-(double)DateTime.Today.DayOfWeek);
                this.txtTODT2.MaxDate = dtEnd.Date;
            }
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

        #endregion

        #region 사용자이벤트

        #region ItemCallback Callback
        /// <summary>
        /// ItemCallback_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void ITEMCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            
        }
        #endregion

        #region txtDate_Init
        /// <summary>
        /// txtDate_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtDate_Init(object sender, EventArgs e)
        {
            
        }
        #endregion

        #endregion
    }
}