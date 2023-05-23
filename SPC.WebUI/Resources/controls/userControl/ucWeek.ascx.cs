using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucWeek : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        public bool SingleWeek { get; set; }    // 주차 선택 하나만 표시
        public string Changed { get; set; }     // 값 변경시 연결 js함수 지정
        private string _nullText = "선택";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        private string _defaultWeek = "";
        public string defaultWeek { get { return this._defaultWeek; } set { this._defaultWeek = value; } }
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

            if (!Page.IsCallback)
            {
                // 반목록을 구한다
                GetQCD107_LST();
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
            DateTime date = DateTime.Today;

            spin_year.Text = date.Year.ToString();
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
            if (SingleWeek)
            {
                this.Fromdiv.Style.Add("width", "68%");
                this.Todiv.Style.Add("display", "none");
            }

            // jsProperty에 금주 주차값 설정
            this.ddlWeek1.JSProperties["cpCURR_WEEK"] = this.ddlWeek1.JSProperties["cpCURR_WEEK"]??"";
            this.ddlWeek2.JSProperties["cpCURR_WEEK"] = this.ddlWeek2.JSProperties["cpCURR_WEEK"]??"";
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
        {
            
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 반목록을 구한다
        void GetQCD107_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                Page.oParamDic.Add("F_YYYY", this.spin_year.Text);
                ds = biz.GetQCD107_LST(Page.oParamDic, out errMsg);
            }

            ddlWeek1.DataSource = ds;
            ddlWeek2.DataSource = ds;
            //ddlWeek1.DataBind();
            if (ds != null && ds.Tables.Count > 0)
            {
                var curr_week = (ds.Tables[0].Compute("MAX(CURR_WEEK)", "") ?? "").ToString();
                ddlWeek1.JSProperties["cpCURR_WEEK"] = curr_week;
                ddlWeek2.JSProperties["cpCURR_WEEK"] = curr_week;
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlWeek1 DataBound
        /// <summary>
        /// ddlWeek1_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlWeek1_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            ddlWeek1.Items.Insert(0, item);

            if (ddlWeek1.Items.Count > 1)
            {
                ddlWeek1.SelectedIndex = ddlWeek1.Items.FindByValue(defaultWeek).Index;
                hidWeek1.Text = defaultWeek;
            }
        }
        #endregion

        #region ddlWeek1 Callback
        /// <summary>
        /// ddlWeek1_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlWeek1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            // 반목록을 구한다
            GetQCD107_LST();
            ddlWeek1.DataBind();
        }
        #endregion

        #region ddlWeek2 DataBound
        /// <summary>
        /// ddlWeek1_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlWeek2_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            ddlWeek2.Items.Insert(0, item);

            if (ddlWeek2.Items.Count > 1)
            {
                ddlWeek2.SelectedIndex = ddlWeek2.Items.FindByValue(defaultWeek).Index;
                hidWeek2.Text = defaultWeek;
            }
        }
        #endregion

        #region ddlWeek2 Callback
        /// <summary>
        /// ddlWeek1_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlWeek2_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            // 반목록을 구한다
            GetQCD107_LST();
            ddlWeek2.DataBind();
        }
        #endregion

        #endregion
    }
}