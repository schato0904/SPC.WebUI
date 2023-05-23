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
    public partial class ucItemMulti : WebUIBasePageUserControl
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
            get { return string.IsNullOrEmpty(this._ClientInstanceName) ? this.UniqueID : this._ClientInstanceName; }
            set { this._ClientInstanceName = value; }
        }
        public string OnChanged { get; set; }
        public string targetCtrls { get; set; }
        public string hidUCMODELCD { get; set; }
        public string txtUCMODELNM { get; set; }
        //private bool _usedModel = false;
        //public bool usedModel { get { return _usedModel; } set { _usedModel = value; } }
        public string machGubun { get; set; }
        public string searchOption { get; set; }

        public string ITEMCD
        {
            get { return this.hidITEMCD.Text; }
            set
            {
                this.hidITEMCD.Text = value;
                this.txtITEMCD.Text = value;
            }
        }

        public string ITEMNM
        {
            get { return this.txtITEMNM.Text; }
            set { this.txtITEMNM.Text = value; }
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
            this.hidITEMCD.ClientInstanceName = this.hidITEMCD.UniqueID;
            this.txtITEMCD.ClientInstanceName = this.txtITEMCD.UniqueID;
            this.txtITEMNM.ClientInstanceName = this.txtITEMNM.UniqueID;
            this.ITEMCallback.ClientInstanceName = this.ITEMCallback.UniqueID;
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
        /// <summary>
        /// 품목코드 반환
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            return hidITEMCD.Text;
        }
        /// <summary>
        /// 품목명 반환
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return txtITEMNM.Text;
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
            string errMsg = String.Empty;
            
            using(CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                //Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
                //Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
                Page.oParamDic.Add("F_COMPCD", GetCompCD());
                Page.oParamDic.Add("F_FACTCD", GetFactCD());
                Page.oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                Page.oParamDic.Add("F_STATUS", "1");
                ds = biz.GetQCD01_LST(Page.oParamDic, out errMsg);
            }

            string ITEMCD = String.Empty;
            string ITEMNM = String.Empty;
            string MODELCD = String.Empty;
            string MODELNM = String.Empty;

            if (true == Page.bExistsDataSet(ds))
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        ITEMCD = (string)dtRow["F_ITEMCD"];
                        ITEMNM = (string)dtRow["F_ITEMNM"];
                        MODELCD = dtRow["F_MODELCD"].ToString();
                        MODELNM = dtRow["F_MODELNM"].ToString();
                    }
                }
            }

            ITEMCallback.JSProperties["cpITEMCD"] = ITEMCD;
            ITEMCallback.JSProperties["cpITEMNM"] = ITEMNM;
            ITEMCallback.JSProperties["cpMODELCD"] = MODELCD;
            ITEMCallback.JSProperties["cpMODELNM"] = MODELNM;
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
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", string.Format("{0}.fn_OnPopupUCItemSearch();", this.ClientInstanceName));
        }
        #endregion

        #endregion
    }
}