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
    public partial class ucModelDDL : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sModelCD = String.Empty;
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
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
                GetQCD17_LST();
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
            sModelCD = Request.QueryString.Get("BANCD") ?? "";
            hidMODELCD.Text = sModelCD;
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

        #region 모델코드를 구한다
        void GetQCD17_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                Page.oParamDic.Add("F_COMPCD", GetCompCD());
                Page.oParamDic.Add("F_FACTCD", GetFactCD());
                Page.oParamDic.Add("F_STATUS", "1");

                ds = biz.GetQCD17_LST(Page.oParamDic, out errMsg);
            }

            ddlMODEL.DataSource = ds;
            //ddlMODEL.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlMODEL DataBound
        /// <summary>
        /// ddlMODEL_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlMODEL_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            ddlMODEL.Items.Insert(0, item);

            ddlMODEL.SelectedIndex = ddlMODEL.Items.FindByValue(sModelCD).Index;
        }
        #endregion

        #region ddlMODEL Callback
        /// <summary>
        /// ddlMODEL_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlMODEL_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            // 모델코드를 구한다
            GetQCD17_LST();
            ddlMODEL.DataBind();
        }
        #endregion

        #endregion
    }
}