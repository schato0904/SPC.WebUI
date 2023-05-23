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
    public partial class LTRK0501 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string sYearFirstDate = String.Empty;
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
        {
            srcF_EMONTH.Text = DateTime.Today.ToString("yyyy-MM");
            sYearFirstDate = String.Format("{0}-01-01", DateTime.Today.Year);
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 조회
        void QPM01_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_SMONTH", GetFromDt());
                oParamDic.Add("F_EMONTH", DateTime.Today.ToString("yyyy-MM"));
                ds = biz.QPM01_LST(oParamDic, out errMsg);
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

        #region btnClose_Init
        /// <summary>
        /// btnClose_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnClose_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxButton btnLink = (DevExpress.Web.ASPxButton)sender;
            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)btnLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "F_YYYYMM", "F_ISCLOSE") as object[];
            btnLink.Enabled = Convert.ToBoolean(rowValues[1]);
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnClose('{0}'); }}",
                rowValues[0]);
        }
        #endregion

        #region btnCancel_Init
        /// <summary>
        /// btnCancel_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnCancel_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxButton btnLink = (DevExpress.Web.ASPxButton)sender;
            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)btnLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "F_YYYYMM", "F_ISCANCEL") as object[];
            btnLink.Enabled = Convert.ToBoolean(rowValues[1]);
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnCancel('{0}'); }}",
                rowValues[0]);
        }
        #endregion

        #region btnLink_Init
        /// <summary>
        /// btnLink_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnLink_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxButton btnLink = (DevExpress.Web.ASPxButton)sender;
            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)btnLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnPopupLTRK0501POP('{0}'); }}",
                devGrid.GetRowValues(rowVisibleIndex, "F_YYYYMM").ToString());
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
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                bool bExecute = false;
                string errMsg = String.Empty;
                procResult = new string[] { "2", "Unknown Error" };
                string[] aParams = e.Parameters.Split('|');
                List<string> oSPs = new List<string>();
                List<object> oParameters = new List<object>();

                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_YYYYMM", aParams[0]);
                oParamDic.Add("F_USER", gsUSERID);
                oParamDic.Add("F_ISCLOSE", aParams[1]);

                // 월마감
                oSPs.Add("USP_QPM01_INS_UPD");
                oParameters.Add((object)oParamDic);

                // 월마감이력
                oSPs.Add("USP_QPM02_INS");
                oParameters.Add((object)oParamDic);

                // 제품별 월마감정보
                oSPs.Add("USP_QPM03_INS_DEL");
                oParameters.Add((object)oParamDic);

                using (LTRKBiz biz = new LTRKBiz())
                {
                    bExecute = biz.PROC_CLOSE_BATCH(oSPs.ToArray(), oParameters.ToArray(), out errMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("작업 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", errMsg) };
                }
                else
                {
                    procResult = new string[] { "1", "작업이 완료되었습니다." };

                    // 조회
                    QPM01_LST();
                }

                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }
            else
            {
                // 조회
                QPM01_LST();
            }
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
            DevExpressLib.GetBoolString(new string[] { "F_ISREALCLOSE" }, "마감함", "마감안함", e);
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_UPDDT") || e.Column.FieldName.Equals("F_USERNM"))
            {
                if (!Convert.ToBoolean(devGrid.GetRowValues(e.VisibleRowIndex, "F_ISREALCLOSE")))
                    e.DisplayText = String.Empty;
            }
        }
        #endregion

        #endregion
    }
}