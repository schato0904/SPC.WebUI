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
    public partial class LTRK0203 : WebUIBasePage
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
            // Request
            GetRequest();

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
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
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
        void QPM22_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERSTDT", GetFromDt());
                oParamDic.Add("F_ORDEREDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                ds = biz.QPM22_LST_BY_BETWEENDATE(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region btnInput_Init
        /// <summary>
        /// btnInput_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnInput_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxButton btnLink = (DevExpress.Web.ASPxButton)sender;
            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)btnLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "F_ORDERGROUP", "F_ORDERDATE", "F_ORDERNO", "F_STATUS") as object[];
            bool bEnabled = true;
            switch (rowValues[3].ToString())
            {
                default:
                    bEnabled = true;
                    break;
                case "AAE607":
                    bEnabled = false;
                    break;
            }
            btnLink.Enabled = bEnabled;
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnPopupLTRK0202POP01('{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowValues[2]);
        }
        #endregion

        #region btnEarning_Init
        protected void btnEarning_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxButton btnLink = (DevExpress.Web.ASPxButton)sender;
            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)btnLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "F_ORDERGROUP", "F_ORDERDATE", "F_ORDERNO", "F_STATUS") as object[];
            bool bEnabled = true;
            switch (rowValues[3].ToString())
            {
                default:
                    bEnabled = true;
                    break;
                case "AAE607":
                    bEnabled = false;
                    break;
            }
            btnLink.Enabled = bEnabled;
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnPopupLTRK0202POP02('{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowValues[2]);
        }
        #endregion

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 조회
            QPM22_LST();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (!e.Column.FieldName.Equals("F_STATUS")) return;

            string sStatus = GetCommonCodeText(e.Value.ToString());
            switch (e.Value.ToString())
            {
                case "AAE601":
                    sStatus = String.Format(@"<span style='color:orange;'>{0}</span>", sStatus);
                    break;
                case "AAE602":
                case "AAE603":
                case "AAE604":
                    sStatus = String.Format(@"<span style='color:blue;'>{0}</span>", sStatus);
                    break;
                case "AAE605":
                case "AAE606":
                case "AAE607":
                    sStatus = String.Format(@"<span style='color:red;'>{0}</span>", sStatus);
                    break;
            }

            e.EncodeHtml = false;
            e.DisplayText = sStatus;
        }
        #endregion

        #endregion
    }
}