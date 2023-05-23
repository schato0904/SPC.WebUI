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
    public partial class ucDDLSYCOD01 : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sSYCOD01CD = String.Empty;
        private string _F_CODEGROUP = string.Empty;
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        public string targetParams { get; set; }

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

        private bool _IsRequired = false;
        public bool IsRequired { get { return this._IsRequired; } set { this._IsRequired = value; } }
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
            sSYCOD01CD = Request.QueryString.Get("SYCOD01CD") ?? "";
            hidSYCOD01CD.Text = sSYCOD01CD;
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
            this.hidSYCOD01CD.ClientInstanceName = string.Format("{0}_hidSYCOD01CD", this.ClientInstanceName);
            this.ddlSYCOD01.ClientInstanceName = string.Format("{0}_ddlSYCOD01", this.ClientInstanceName);
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공통코드목록을 구한다
        void GetSYCOD01_LST(Dictionary<string, string> pDic)
        {
        //    this.SYCOD01_LST(this.ddlSYCOD01, this.F_CODEGROUP);
        //    this.ddlSYCOD01.DataBind();
        //}

        //void SYCOD01_LST(DevExpress.Web.ASPxComboBox comboBox, string F_CODEGROUP)
        //{
            DevExpress.Web.ASPxComboBox comboBox = this.ddlSYCOD01;
            string errMsg = String.Empty;
            string f_compcd = pDic.ContainsKey("F_COMPCD") ? pDic["F_COMPCD"] ?? Page.gsCOMPCD : Page.gsCOMPCD;
            string f_factcd = pDic.ContainsKey("F_FACTCD") ? pDic["F_FACTCD"] ?? Page.gsFACTCD : Page.gsFACTCD;
            //Dictionary<string, string> oParamDic = new Dictionary<string, string>();
            using (SYSTBiz biz = new SYSTBiz())
            {
                Page.oParamDic.Clear();
                Page.oParamDic.Add("F_COMPCD", f_compcd);
                Page.oParamDic.Add("F_FACTCD", f_factcd);
                Page.oParamDic.Add("F_CODEGROUP", this.F_CODEGROUP);
                Page.oParamDic.Add("F_LANGTYPE", Page.gsLANGTP);

                ds = biz.SYCOD01_LST(Page.oParamDic, out errMsg);
            }

            pDic["F_COMPCD"] = f_compcd;
            pDic["F_FACTCD"] = f_factcd;

            comboBox.DataSource = ds;
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
            this.GetSYCOD01_LST(pDic);
            this.JsonParameter = jss.Serialize(pDic);
        }
        #endregion

        #region Get, Set 메소드
        public string GetValue()
        {
            return this.hidSYCOD01CD.Text;
        }

        public void SetValue(string value)
        {
            this.hidSYCOD01CD.Text = value;
            this.ddlSYCOD01.Value = value;
        } 
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlSYCOD01 DataBound
        /// <summary>
        /// ddlSYCOD01_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlSYCOD01_DataBound(object sender, EventArgs e)
        {
                DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = _nullText, Value = "" };
                ddlSYCOD01.Items.Insert(0, item);
                ddlSYCOD01.SelectedIndex = ddlSYCOD01.Items.FindByValue(sSYCOD01CD).Index;
        }
        #endregion

        #region ddlSYCOD01_Callback
        protected void ddlSYCOD01_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            var ctrl = sender as DevExpress.Web.ASPxComboBox;
            if (!string.IsNullOrEmpty(e.Parameter))
            {
                this.JsonParameter = e.Parameter;
            }
            this.GetData();
            ctrl.DataBind();
            ctrl.JSProperties["cpJsonParameter"] = this.JsonParameter;
        } 
        #endregion

        #region ddlSYCOD01_Init
        protected void ddlSYCOD01_Init(object sender, EventArgs e)
        {
            var combo = sender as DevExpress.Web.ASPxComboBox;
            if (this._IsRequired)
            {
                combo.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                combo.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
                combo.ValidationSettings.RequiredField.IsRequired = true;
            }
        } 
        #endregion

        #endregion
    }
}