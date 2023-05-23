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
    public partial class ucItem2 : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        private bool _usedModel = false;
        public bool usedModel { get { return _usedModel; } set { _usedModel = value; } }
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

        #endregion

        #region 사용자이벤트

        #region ItemCallback Callback
        /// <summary>
        /// ItemCallback_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void ITEM2Callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;
            
            using(CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                //Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
                //Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
                Page.oParamDic.Add("F_COMPCD", GetCompCD());
                Page.oParamDic.Add("F_FACTCD", GetFactCD());
                Page.oParamDic.Add("F_ITEMCD", txtITEMCD2.Text);
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

            ITEMCallback2.JSProperties["cpITEMCD2"] = ITEMCD;
            ITEMCallback2.JSProperties["cpITEMNM2"] = ITEMNM;
            ITEMCallback2.JSProperties["cpMODELCD2"] = MODELCD;
            ITEMCallback2.JSProperties["cpMODELNM2"] = MODELNM;
        }
        #endregion

        #region txtITEMCD2_Init
        /// <summary>
        /// txtITEMCD2_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCD2_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupUCItemSearch2()");
        }
        #endregion

        #endregion
    }
}