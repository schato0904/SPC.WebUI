using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.FITM.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class uc4M : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
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
                QCD40_LST();
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
        void QCD40_LST()
        {
            string errMsg = String.Empty;

            using (FITMBiz biz = new FITMBiz())
            {
                Page.oParamDic.Clear();
                Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
                Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
                Page.oParamDic.Add("F_STATUS", "1");

                ds = biz.QCD40_TBL_LST(Page.oParamDic, out errMsg);
            }

            ddl4M.DataSource = ds;
            //ddl4M.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddl4M DataBound
        /// <summary>
        /// ddl4M_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddl4M_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true };
            ddl4M.Items.Insert(0, item);
        }
        #endregion

        #region ddl4M Callback
        /// <summary>
        /// ddl4M_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddl4M_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            Page.oParamDic.Clear();
            Page.oParamDic.Add("F_COMPCD", GetCompCD());
            Page.oParamDic.Add("F_FACTCD", GetFactCD());
            Page.oParamDic.Add("F_STATUS", "1");

            string errMsg = String.Empty;

            using (FITMBiz biz = new FITMBiz())
            {
                ds = biz.QCD40_TBL_LST(Page.oParamDic, out errMsg);
            }

            ddl4M.DataSource = ds;
            ddl4M.DataBind();
        }
        #endregion

        #endregion
    }
}