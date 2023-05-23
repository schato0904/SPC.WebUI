using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.LTRK.Biz;

namespace SPC.WebUI.Pages.LTRK
{
    public partial class LTRK0204 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataSet dsLTRK0204
        {
            get
            {
                return (DataSet)Session["LTRK0204"];
            }
            set
            {
                Session["LTRK0204"] = value;
            }
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
            // Request
            GetRequest();

            if (!IsPostBack)
                dsLTRK0204 = null;

            // 조회
            LTRK0204_MASTER();

            // Grid Columns Sum Width
            // hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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

                // Grid Callback Init
                devTree.JSProperties["cpResultCode"] = "";
                devTree.JSProperties["cpResultMsg"] = "";
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

        #region 조회
        void LTRK0204_MASTER()
        {
            string errMsg = String.Empty;

            if (dsLTRK0204 == null)
            {
                using (LTRKBiz biz = new LTRKBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_PRODUCTNO", srcF_PRODUCTNO.Text);
                    ds = biz.LTRK0204_MASTER(oParamDic, out errMsg);
                }

                dsLTRK0204 = ds;
            }

            devTree.DataSource = dsLTRK0204;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devTree.JSProperties["cpResultCode"] = "0";
                devTree.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devTree.DataBind();
            }
        }
        #endregion


        #endregion

        #region 사용자이벤트

        #region devTree_CustomCallback
        /// <summary>
        /// devTree_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">TreeListCustomCallbackEventArgs</param>
        protected void devTree_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            dsLTRK0204 = null;
            // 조회
            LTRK0204_MASTER();
        }
        #endregion

        #endregion
    }
}