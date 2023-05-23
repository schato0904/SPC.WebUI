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
    public partial class ucComp : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sCOMPCD = String.Empty;
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        private string _masterChk = null;
        public string masterChk { get { return this._masterChk; } set { this._masterChk = value; } }
        private string _compParam = String.Empty;
        public string compParam { get { return this._compParam; } set { this._compParam = value; } }
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
                hidCOMPCD.Text = !Page.gsVENDOR ? String.Empty : Page.gsCOMPCD;

                // 공장목록을 구한다
                QCM01_LST();
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
            sCOMPCD = Request.QueryString.Get("COMPCD") ?? "";
            hidCOMPCD.Text = sCOMPCD;
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

        #region 공장목록을 구한다
        void QCM01_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                if (!Page.gsVENDOR)
                    Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
                Page.oParamDic.Add("F_MASTERCHK", masterChk);

                ds = biz.QCM01_LST(Page.oParamDic, out errMsg);
            }

            ddlCOMP.DataSource = ds;
            //ddlCOMP.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlCOMP DataBound
        /// <summary>
        /// ddlCOMP_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlCOMP_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            ddlCOMP.Items.Insert(0, item);

            ddlCOMP.SelectedIndex = ddlCOMP.Items.FindByValue(sCOMPCD).Index;
        }
        #endregion

        #endregion
    }
}