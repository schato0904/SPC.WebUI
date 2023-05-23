using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Resources.frames
{
    public partial class rightDevFrame : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string errMessage = String.Empty;
        bool bBanChanged = false;
        #endregion

        #region 프로퍼티
        DataSet dsSession
        {
            get
            {
                return (DataSet)Session["rightDevFrame"];
            }
            set
            {
                Session["rightDevFrame"] = value;
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

            // 트리정보를 구한다
            //devTree.DataSource = DEVTREE_LST(out errMessage);
            if (!IsCallback && gsVENDOR)
                devTree.DataBind();
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

        #region 반선택값
        public string GetTreeBanCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = this.FindControl("ucBan") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidBANCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 트리정보 목록조회(DevExpress 용)
        DataSet DEVTREE_LST(out string errMsg)
        {
            string resultCode = String.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            UserControl _UserControl = Page.FindControl("ucCommonCodeDDL") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidCOMMONCODECD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", GetTreeBanCD());
                oParamDic.Add("F_MACHGUBUN", "");
                oParamDic.Add("F_LANGTP", gsLANGTP);

                ds = biz.DEVTREE_LST(oParamDic, out errMsg);
            }

            return ds;
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
            // 트리정보를 구한다
            devTree.DataBind();
        }
        #endregion

        protected void devTree_DataBound(object sender, EventArgs e)
        {
            string errMsg = String.Empty;

            //devTree.DataSource = DEVTREE_LST(out errMsg);

            //if (!String.IsNullOrEmpty(errMsg))
            //{
            //    // Grid Callback Init
            //    devTree.JSProperties["cpResultCode"] = "0";
            //    devTree.JSProperties["cpResultMsg"] = errMsg;
            //}

            DataView dView = null;

            if (!bExistsDataSet(dsSession))
            {
                dsSession = DEVTREE_LST(out errMsg);
                dView = dsSession.Tables[0].DefaultView;
            }
            else
            {
                string sBanCD = GetTreeBanCD();

                dView = dsSession.Tables[0].DefaultView;
                if (!sBanCD.Equals(""))
                    dView.RowFilter = String.Format("F_BANCD='{0}'", GetTreeBanCD());
                else
                    dView.RowFilter = String.Empty;
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devTree.JSProperties["cpResultCode"] = "0";
                devTree.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                // 트리정보를 구한다
                devTree.DataSource = dView;
            }
        }

        #endregion
    }
}