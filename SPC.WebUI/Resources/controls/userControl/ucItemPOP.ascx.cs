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
    public partial class ucItemPOP : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        bool _UseChainPopup = false;
        string _ClientInstanceID = string.Empty;
        #endregion

        #region 프로퍼티
        public string ParentCallback { get; set; }
        public string targetCtrls { get; set; }
        public string ClientInstanceID { get { return this._ClientInstanceID; } set { this._ClientInstanceID = value; } }
        public string UseCustID { get; set; }
        public string OnClose { get; set; }
        public bool NoCustId { get; set; }
        public bool IsRequired { get; set; }
        public string ITEMCD { get { return hidUCITEMCD.Text; } }
        public bool UseWork { get; set; }
        public bool UseChainPopup { get { return this._UseChainPopup; } set { this._UseChainPopup = value; } }
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
            // ClientInstanceID 값이 지정되지 않았을 경우, 페이지의 UniqueID값을 기본값으로 세팅
            if (string.IsNullOrWhiteSpace(this.ClientInstanceID)) this.ClientInstanceID = this.UniqueID;

            hidUCITEMCD.ClientInstanceName = ClientInstanceID + "hidUCITEMCD";
            txtUCITEMCD.ClientInstanceName = ClientInstanceID + "txtUCITEMCD";
            txtUCITEMNM.ClientInstanceName = ClientInstanceID + "txtUCITEMNM";
            UCITEMCallback.ClientInstanceName = ClientInstanceID + "UCITEMCallback";
            hidValues.ClientInstanceName = ClientInstanceID + "hidValues";
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
       // public override string GetValue()
        //{
        //    return this.hidUCITEMCD.Text;
        //}
        #endregion

        #region 사용자이벤트

        #region ITEMCallback Callback
        /// <summary>
        /// ITEMCallback_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void ITEMCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = string.Empty;
            string resultJson = string.Empty;

            DevExpress.Web.ASPxTextBox CtlITEMCD = this.txtUCITEMCD;

            using (CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                Page.oParamDic.Add("F_ITEMCD", CtlITEMCD.Text);
                if (UseWork)
                {
                    ds = biz.QCD01_WERD_LST(Page.oParamDic, out errMsg);
                }
                else
                {
                    ds = biz.GetQCD01_POPUP_LST(Page.oParamDic, out errMsg);
                }
            }

            string ITEMCD = String.Empty;
            string ITEMNM = String.Empty;

            if (true == Page.bExistsDataSet(ds))
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        ITEMCD = (string)dtRow["F_ITEMCD"];
                        ITEMNM = (string)dtRow["F_ITEMNM"];
                    }
                }
                resultJson = Page.SerializeJSON(GlobalFunction.FirstRowToDictionary(ds, false));
            }

            DevExpress.Web.ASPxCallback CtlCallBack = this.UCITEMCallback;

            CtlCallBack.JSProperties["cpITEMCD"] = ITEMCD;
            CtlCallBack.JSProperties["cpITEMNM"] = ITEMNM;
            CtlCallBack.JSProperties["cpResult"] = resultJson;
        }
        #endregion

        #region txtITEMCD_Init
        /// <summary>
        /// txtITEMCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCD_Init(object sender, EventArgs e)
        {
            //(sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", string.Format("{0}.fn_OnPopupUCITEMSearch()", this.ClientInstanceID));
            //(sender as DevExpress.Web.ASPxTextBox).ClientSideEvents.LostFocus = string.Format("{0}.fn_OnUCITEMLostFocus", this.ClientInstanceID);
            //(sender as DevExpress.Web.ASPxTextBox).ClientSideEvents.KeyUp = string.Format("{0}.fn_OnUCITEMKeyUp", this.ClientInstanceID);
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", string.Format("{0}.fn_OnPopupUCITEMSearch();", this.ClientInstanceID));
            (sender as DevExpress.Web.ASPxTextBox).ClientSideEvents.LostFocus = string.Format("function(s,e) {{{0}.fn_OnUCITEMLostFocus.apply({0},[s,e]);}}", this.ClientInstanceID);
            (sender as DevExpress.Web.ASPxTextBox).ClientSideEvents.KeyUp = string.Format("function(s,e) {{{0}.fn_OnUCITEMKeyUp.apply({0},[s,e]);}}", this.ClientInstanceID);
        }
        #endregion

        #region UCITEMCallback_Init
        /// <summary>
        /// UCITEMCallback_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UCITEMCallback_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxCallback).ClientSideEvents.EndCallback = string.Format("function(s,e) {{{0}.fn_OnUCITEMEndCallback.apply({0},[s,e]);}}", this.ClientInstanceID);
        }
        #endregion

        #region txtUCITEMNM_Init
        /// <summary>
        /// 품목명 초기화 : IsRequired속성 체크하여 유효성 구문 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtUCITEMNM_Init(object sender, EventArgs e)
        {
            if (this.IsRequired)
            {
                this.txtUCITEMNM.ValidationSettings.RequiredField.IsRequired = true;
                this.txtUCITEMNM.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
                this.txtUCITEMNM.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Right;
                this.txtUCITEMNM.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                //<ValidationSettings RequiredField-IsRequired="true" 
                //    ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>

            }
        }
        #endregion

        #endregion
    }
}