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
    public partial class ucRDOCommonCode : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sCommonCodeCD = String.Empty;
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        public string targetParams { get; set; }
        private bool _bUsedNullText = false;
        public bool bUsedNullText { get { return this._bUsedNullText; } set { this._bUsedNullText = value; } }
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
                // 공통코드목록을 구한다
                GetQCD72_LST();
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
            sCommonCodeCD = Request.QueryString.Get("COMMONCODECD") ?? "";
            hidCOMMONCODECD.Text = sCommonCodeCD;
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

        #region 공통코드목록을 구한다
        void GetQCD72_LST()
        {
            string[] arrCode = targetParams.Split('|');
            CommonCode.CodeDic codeDic = null;

            foreach (string code in arrCode)
            {
                if (codeDic == null)
                    codeDic = Page.CachecommonCode[code];
                else
                    codeDic = codeDic[code];
            }

            rdoCOMMONCODE.TextField = String.Format("COMMNM{0}", Page.gsLANGTP);
            rdoCOMMONCODE.ValueField = "COMMCD";
            rdoCOMMONCODE.DataSource = codeDic.codeGroup.Values;
            //rdoCOMMONCODE.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region rdoCOMMONCODE DataBound
        /// <summary>
        /// rdoCOMMONCODE_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void rdoCOMMONCODE_DataBound(object sender, EventArgs e)
        {
            if (bUsedNullText)
            {
                DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
                rdoCOMMONCODE.Items.Insert(0, item);
            }

            rdoCOMMONCODE.SelectedIndex = rdoCOMMONCODE.Items.FindByValue(sCommonCodeCD).Index;
        }
        #endregion

        #endregion
    }
}