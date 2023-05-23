using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucUser : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        public string strTitle = "사용자조회";
        string _ClientInstanceID = string.Empty;
        #endregion

        #region 프로퍼티
        public string ClientInstanceID
        {
            get { return string.IsNullOrEmpty(this._ClientInstanceID) ? this.UniqueID : this._ClientInstanceID; }
            set { this._ClientInstanceID = value; }
        }
        public bool IsRequired { get; set; }
        public string CD { get { return TEXT1.Text; } }
        public string NM { get { return TEXT2.Text; } }
        public string NMFULL { get { return TEXT3.Text; } }
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
            TEXT1.ClientInstanceName = TEXT1.UniqueID;
            TEXT2.ClientInstanceName = TEXT2.UniqueID;
            TEXT3.ClientInstanceName = TEXT3.UniqueID;

            // 유효성 세팅
            if (this.IsRequired)
            {
                this.TEXT2.ValidationSettings.RequiredField.IsRequired = true;
                this.TEXT2.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
                this.TEXT2.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Right;
                this.TEXT2.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
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
            //if (CustType == "AAG902")
            //{
            //    strTitle = "협력사조회";
            //}
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
        // 유저컨트롤 공통 재정의 메소드
        public string GetValue()
        {
            return TEXT1.Text;
        }

        // 유저컨트롤 로딩시 초기값 세팅할 때
        public void SetValues(string cd, string nm, string nmfull = "")
        {
            this.TEXT1.Text = cd;
            this.TEXT2.Text = nm;
            this.TEXT3.Text = nmfull;
        }
        #endregion

        #region 사용자이벤트
        #region 코드 항목 초기화 : 이벤트 매핑
        protected void TEXT1_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", string.Format("{0}.OpenPopup();", this.ClientInstanceID));
            (sender as DevExpress.Web.ASPxTextBox).ClientSideEvents.LostFocus = string.Format("function(s,e) {{{0}.LostFocus.apply({0},[s,e]);}}", this.ClientInstanceID);
        }
        #endregion
        #endregion
    }
}