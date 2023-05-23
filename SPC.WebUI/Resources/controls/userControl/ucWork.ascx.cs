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
    public partial class ucWork : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
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
            if (!Page.IsCallback)
            {
                // 반목록을 구한다
                GetQCD74_LST();
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

        #region 공정목록을 구한다
        void GetQCD74_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
                Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
                Page.oParamDic.Add("F_STATUS", "1");

                ds = biz.GetQCD74_LST(Page.oParamDic, out errMsg);
            }

            ddlWORK.DataSource = ds;
            //ddlWORK.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlWORK DataBound
        /// <summary>
        /// ddlWORK_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlWORK_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true };
            ddlWORK.Items.Insert(0, item);
        }
        #endregion

        #region ddlWORK Callback
        /// <summary>
        /// ddlWORK_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlWORK_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            Page.oParamDic.Clear();
            //Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
            //Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
            Page.oParamDic.Add("F_COMPCD", GetCompCD());
            Page.oParamDic.Add("F_FACTCD", GetFactCD());
            Page.oParamDic.Add("F_STATUS", "1");
            
            #region 반
            string _BANCD = GetBanCD();
            if(!String.IsNullOrEmpty(_BANCD))
                Page.oParamDic.Add("F_BANCD", _BANCD);
            #endregion

            #region 라인
            string _LINECD = GetLineCD();
            if (!String.IsNullOrEmpty(_LINECD))
                Page.oParamDic.Add("F_LINECD", _LINECD);
            #endregion

            #region 품목
            string _ITEMCD = GetItemCD();
            if (!String.IsNullOrEmpty(_ITEMCD))
                Page.oParamDic.Add("F_ITEMCD", _ITEMCD);
            #endregion

            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                ds = biz.GetQCD74_LST(Page.oParamDic, out errMsg);
            }

            ddlWORK.DataSource = ds;
            ddlWORK.DataBind();
        }
        #endregion

        #endregion
    }
}