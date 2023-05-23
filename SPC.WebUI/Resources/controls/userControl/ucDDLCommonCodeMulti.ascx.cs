using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using System.Data;
using SPC.WebUI.Common;
using SPC.SYST.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucDDLCommonCodeMulti : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sCOMMCD = String.Empty;
        private string _F_CODEGROUP = string.Empty;
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        public string targetParams { get; set; }
        public string targetFuncs { get; set; }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public string F_CODEGROUP { get { return this._F_CODEGROUP; } set { this._F_CODEGROUP = value; } }

        private string _ClientInstanceName = string.Empty;
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public string ClientInstanceName { get { return _ClientInstanceName; } set { this._ClientInstanceName = value; } }
        public string JsonParameter = string.Empty;
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
                //GetQCD72_LST();
                this.GetData();
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
            sCOMMCD = Request.QueryString.Get("COMMCD") ?? "";
            hidCOMMCD.Text = sCOMMCD;
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
        {
            this.ClientInstanceName = string.IsNullOrEmpty(this.ClientInstanceName) ? this.ClientID : this.ClientInstanceName;
            this.hidCOMMCD.ClientInstanceName = string.Format("{0}_hidCOMMCD", this.ClientInstanceName);
            this.ddlCOMMCD.ClientInstanceName = string.Format("{0}_ddlCOMMCD", this.ClientInstanceName);
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공통코드목록을 구한다
        void GetCOMMCD_LST(Dictionary<string, string> pDic)
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

            DevExpress.Web.ASPxComboBox comboBox = this.ddlCOMMCD;
            comboBox.TextField = String.Format("COMMNM{0}", Page.gsLANGTP);
            comboBox.ValueField = "COMMCD";
            comboBox.DataSource = codeDic.codeGroup.Values;
        }
        #endregion

        #region 데이터 조회
        /// <summary>
        /// 데이터 조회 후, 사용한 파라미터 구성하여 JSON형태로 보관
        /// </summary>
        void GetData()
        {
            Dictionary<string, string> pDic = new Dictionary<string, string>();
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            if (!string.IsNullOrEmpty(this.JsonParameter))
            {
                pDic = jss.Deserialize<Dictionary<string, string>>(this.JsonParameter);
            }
            this.GetCOMMCD_LST(pDic);
            this.JsonParameter = jss.Serialize(pDic);
        }
        #endregion

        #region Get, Set 메소드
        public string GetValue()
        {
            return this.hidCOMMCD.Text;
        }

        public void SetValue(string value)
        {
            this.hidCOMMCD.Text = value;
            this.ddlCOMMCD.Value = value;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlCOMMCD DataBound
        /// <summary>
        /// ddlCOMMCD_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlCOMMCD_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            ddlCOMMCD.Items.Insert(0, item);

            ddlCOMMCD.SelectedIndex = ddlCOMMCD.Items.FindByValue(sCOMMCD).Index;
        }
        #endregion

        #endregion
    }
}