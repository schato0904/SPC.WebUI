using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucLineMulti : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sLINECD = String.Empty;
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }

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
                // 반목록을 구한다
                //QCD73_LST();
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
            sLINECD = Request.QueryString.Get("LINECD") ?? "";
            hidLINECD.Text = sLINECD;
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
            this.hidLINECD.ClientInstanceName = string.Format("{0}_hidLINECD", this.ClientInstanceName);
            this.ddlLINE.ClientInstanceName = string.Format("{0}_ddlLINE", this.ClientInstanceName);
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 반목록을 구한다
        void QCD73_LST(Dictionary<string, string> pDic)
        {
            string errMsg = String.Empty;
            string f_compcd = pDic.ContainsKey("F_COMPCD") ? pDic["F_COMPCD"] ?? Page.gsCOMPCD : Page.gsCOMPCD;
            string f_factcd = pDic.ContainsKey("F_FACTCD") ? pDic["F_FACTCD"] ?? Page.gsFACTCD : Page.gsFACTCD;
            string f_bancd = pDic.ContainsKey("F_BANCD") ? pDic["F_BANCD"] ?? "" : "";

            using (CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                //Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
                //Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
                Page.oParamDic.Add("F_COMPCD", f_compcd);
                Page.oParamDic.Add("F_FACTCD", f_factcd);
                Page.oParamDic.Add("F_BANCD", f_bancd);
                Page.oParamDic.Add("F_STATUS", "1");

                ds = biz.QCD73_LST(Page.oParamDic, out errMsg);
            }

            pDic["F_COMPCD"] = f_compcd;
            pDic["F_FACTCD"] = f_factcd;
            pDic["F_BANCD"] = f_bancd;

            ddlLINE.DataSource = ds;
            ddlLINE.DataBind();
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
            this.QCD73_LST(pDic);
            this.JsonParameter = jss.Serialize(pDic);
        }
        #endregion

        #region Get, Set 메소드
        public string GetValue()
        {
            return this.hidLINECD.Text;
        }

        public void SetValue(string value)
        {
            this.hidLINECD.Text = value;
            this.ddlLINE.Value = value;
        } 
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlLINE DataBound
        /// <summary>
        /// ddlLINE_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlLINE_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            ddlLINE.Items.Insert(0, item);

            ddlLINE.SelectedIndex = ddlLINE.Items.FindByValue(sLINECD).Index;
        }
        #endregion

        protected void ddlLINE_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //QCD73_LST();
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
    }
}