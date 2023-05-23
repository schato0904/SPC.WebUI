using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucDateMulti : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string _ClientInstanceName = string.Empty;
        #endregion

        #region 프로퍼티
        public string ClientInstanceName 
        {
            get { return string.IsNullOrEmpty(this._ClientInstanceName)?this.UniqueID:this._ClientInstanceName; }
            set { this._ClientInstanceName = value; }
        }
        public bool DateTimeOnly { get; set; }
        public bool SingleDate { get; set; }
        public int MaxDate { get; set; }
        public bool TodayFromDate { get; set; }
        public bool MonthOnly { get; set; }
        public int MaxMonth { get; set; }
        public bool CurrentWeekOnly { get; set; }   // 금일 포함된 금주의 일요일부터 당일까지 Min~Max지정
        public string OnChanged { get; set; }         // 값 변경시 연결 js함수 지정
        public string targetCtrls { get; set; }
        public string FieldName_Fromdt { get; set; }
        public string FieldName_Todt { get; set; }

        public string FROMDT
        {
            get
            {
                if (this.txtFROMDT.Value == null)
                    return string.Empty;

                return this.hidUCFROMDT.Text;
            }
        }

        public string TODT
        {
            get
            {
                if (this.txtTODT.Value == null)
                    return string.Empty;

                return this.hidUCTODT.Text;
            }
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

            SetClientScripts();
            
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
            //SetClientScripts();

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
            //<ClientSideEvents QueryCloseUp="fn_DateCheck" DateChanged="fn_DateCheck" Init="fn_UCDateInit" DropDown="fn_DropDown" />
            this.hidUCFROMDT.ClientInstanceName = this.hidUCFROMDT.UniqueID;
            this.hidUCTODT.ClientInstanceName = this.hidUCTODT.UniqueID;
            this.txtFROMDT.ClientInstanceName = this.txtFROMDT.UniqueID;
            this.txtTODT.ClientInstanceName = this.txtTODT.UniqueID;
            //this.txtFROMDT.ClientSideEvents.QueryCloseUp = string.Format("function(s,e){{ {0}.fn_DateCheck(s,e); }}", this.ClientInstanceID);
            //this.txtFROMDT.ClientSideEvents.DateChanged = string.Format("function(s,e){{ {0}.fn_FromDtChanged(s,e); }}", this.ClientInstanceID);
            //this.txtFROMDT.ClientSideEvents.LostFocus = string.Format("function(s,e){{ {0}.fn_FromDtChanged(s,e); }}", this.ClientInstanceID);
            //this.txtFROMDT.ClientSideEvents.Init = string.Format("function(s,e){{ {0}.fn_UCDateInit(s,e); }}", this.ClientInstanceID);
            //this.txtFROMDT.ClientSideEvents.DropDown = string.Format("function(s,e){{ {0}.fn_DropDown(s,e); }}", this.ClientInstanceID);
            //this.txtTODT.ClientSideEvents.QueryCloseUp = string.Format("function(s,e){{ {0}.fn_DateCheck(s,e); }}", this.ClientInstanceID);
            //this.txtTODT.ClientSideEvents.DateChanged = string.Format("function(s,e){{ {0}.fn_ToDtChanged(s,e); }}", this.ClientInstanceID);
            //this.txtTODT.ClientSideEvents.LostFocus = string.Format("function(s,e){{ {0}.fn_ToDtChanged(s,e); }}", this.ClientInstanceID);
            //this.txtTODT.ClientSideEvents.Init = string.Format("function(s,e){{ {0}.fn_UCDateInit(s,e); }}", this.ClientInstanceID);
            //this.txtTODT.ClientSideEvents.DropDown = string.Format("function(s,e){{ {0}.fn_DropDown(s,e); }}", this.ClientInstanceID);

            string sDate = DateTime.Now.ToString("yyyyMMdd").Substring(0,6)+"01";
            DateTime fromdt = DateTime.ParseExact(sDate + "080000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            DateTime todt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (MaxDate != 0 )
            {
                sDate = DateTime.Now.AddDays(-MaxDate).ToString("yyyyMMdd");
                fromdt = DateTime.ParseExact(sDate + "080000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            }

            if (TodayFromDate)
            {
                sDate = DateTime.Now.ToString("yyyyMMdd");
                fromdt = DateTime.ParseExact(sDate + "080000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            }

            if (!DateTimeOnly)
            {
                this.txtFROMDT.DisplayFormatString = "yyyy-MM-dd";
                this.txtFROMDT.EditFormatString = "yyyy-MM-dd";
                
                this.txtTODT.DisplayFormatString = "yyyy-MM-dd";
                this.txtTODT.EditFormatString = "yyyy-MM-dd";

                hidUCFROMDT.Text = fromdt.ToString("yyyy-MM-dd");
                hidUCTODT.Text = todt.ToString("yyyy-MM-dd");
            }
            else
            {
                this.txtFROMDT.DisplayFormatString = "yyyy-MM-dd hh:mm:ss";
                this.txtFROMDT.EditFormatString = "yyyy-MM-dd hh:mm:ss";

                this.txtTODT.DisplayFormatString = "yyyy-MM-dd hh:mm:ss";
                this.txtTODT.EditFormatString = "yyyy-MM-dd hh:mm:ss";

                hidUCFROMDT.Text = fromdt.ToString("yyyy-MM-dd hh:mm:ss");
                hidUCTODT.Text = todt.ToString("yyyy-MM-dd hh:mm:ss");
            }
            if (MonthOnly) 
            {
                this.txtFROMDT.DisplayFormatString = "yyyy-MM";
                this.txtFROMDT.EditFormatString = "yyyy-MM";

                this.txtTODT.DisplayFormatString = "yyyy-MM";
                this.txtTODT.EditFormatString = "yyyy-MM";

                hidUCFROMDT.Text = fromdt.ToString("yyyy-MM");
                hidUCTODT.Text = todt.ToString("yyyy-MM");

                //this.txtFROMDT.DisplayFormatString = "";
            }

            this.txtFROMDT.Date = fromdt;
            this.txtTODT.Date = todt;

            if (SingleDate)
            {
                this.txtFROMDT.Date = todt;
                //this.Fromdiv.Style.Add("width", "98%");
                //this.Todiv.Style.Add("display", "none");
            }

            if (CurrentWeekOnly)
            {
                this.txtFROMDT.MinDate = DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek);
                this.txtFROMDT.MaxDate = DateTime.Today;
                this.txtTODT.MinDate = DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek);
                this.txtTODT.MaxDate = DateTime.Today;
            }
        }
        #endregion

        #region 클라이언트 스크립트
        /// <summary>
        /// SetClientScripts
        /// </summary>
        void SetClientScripts()
        {
            //this.txtFROMDT.ClientSideEvents.DateChanged = string.Format("function(s,e){{ {0}.fn_FromDtChanged(s,e); }}", this.ClientInstanceName);
            //this.txtFROMDT.ClientSideEvents.LostFocus = string.Format("function(s,e){{ {0}.fn_FromDtChanged(s,e); }}", this.ClientInstanceName);
            //this.txtFROMDT.ClientSideEvents.Init = string.Format("function(s,e){{ {0}.fn_UCDateInit(s,e); }}", this.ClientInstanceName);
            //this.txtFROMDT.ClientSideEvents.DropDown = string.Format("function(s,e){{ {0}.fn_DropDown(s,e); }}", this.ClientInstanceName);

            //this.txtTODT.ClientSideEvents.DateChanged = string.Format("function(s,e){{ {0}.fn_ToDtChanged(s,e); }}", this.ClientInstanceName);
            //this.txtTODT.ClientSideEvents.LostFocus = string.Format("function(s,e){{ {0}.fn_ToDtChanged(s,e); }}", this.ClientInstanceName);
            //this.txtTODT.ClientSideEvents.Init = string.Format("function(s,e){{ {0}.fn_UCDateInit(s,e); }}", this.ClientInstanceName);
            //this.txtTODT.ClientSideEvents.DropDown = string.Format("function(s,e){{ {0}.fn_DropDown(s,e); }}", this.ClientInstanceName);

            if (SingleDate)
            {
                this.Fromdiv.Style.Add("width", "98%");
                this.Todiv.Style.Add("display", "none");
            }
        }
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
        public void Clear()
        {
            this.hidUCFROMDT.Text = "";
            this.txtFROMDT.Text = "";
            this.txtFROMDT.Value = null;
            this.hidUCTODT.Text = "";
            this.txtTODT.Text = "";
            this.txtTODT.Value = null;
        }

        public void SetValue(DateTime date1, DateTime date2)
        {
            this.txtFROMDT.Date = date1;
            this.txtFROMDT.Text = date1.ToShortDateString();
            this.hidUCFROMDT.Text = date1.ToShortDateString();
            this.txtTODT.Date = date2;
            this.txtTODT.Text = date2.ToShortDateString();
            this.hidUCTODT.Text = date2.ToShortDateString();
        }

        public void SetClientEnabled(bool enable)
        {
            this.txtFROMDT.ClientEnabled = enable;
            this.txtTODT.ClientEnabled = enable;
        }
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