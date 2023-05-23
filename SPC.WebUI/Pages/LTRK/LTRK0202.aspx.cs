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
    public partial class LTRK0202 : WebUIBasePage
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
                oParamDic.Add("F_ORDERDATE", GetFromDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                ds = biz.QPM22_LST(oParamDic, out errMsg);
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

        #region 작업지시 상태변경
        bool QPM22_STATUS_CHG(Dictionary<string, string> paramDic, string sSTATUS, out string errMsg)
        {
            bool bExecute = false;

            errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERGROUP", paramDic["F_ORDERGROUP"]);
                oParamDic.Add("F_ORDERDATE", paramDic["F_ORDERDATE"]);
                oParamDic.Add("F_ORDERNO", paramDic["F_ORDERNO"]);
                oParamDic.Add("F_STATUS", sSTATUS);
                oParamDic.Add("F_USER", gsUSERID);

                bExecute = biz.QPM22_STATUS_CHG(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 작업지시 삭제
        bool QPM22_DEL(Dictionary<string, string> paramDic, out string errMsg)
        {
            bool bExecute = false;

            errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERGROUP", paramDic["F_ORDERGROUP"]);
                oParamDic.Add("F_ORDERDATE", paramDic["F_ORDERDATE"]);
                oParamDic.Add("F_ORDERNO", paramDic["F_ORDERNO"]);
                oParamDic.Add("F_USER", gsUSERID);

                bExecute = biz.QPM22_DEL(oParamDic, out errMsg);
            }

            return bExecute;
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
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "F_ORDERGROUP", "F_ORDERDATE", "F_ORDERNO", "F_STATUS") as object[];
            bool bEnabled = true;
            switch (rowValues[3].ToString())
            {
                default:
                    bEnabled = true;
                    break;
                case "AAE601":
                case "AAE605":
                case "AAE606":
                case "AAE607":
                    bEnabled = false;
                    break;
            }
            btnLink.Enabled = bEnabled;
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnClose('{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowValues[2]);
        }
        #endregion

        #region btnDelete_Init
        /// <summary>
        /// btnDelete_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnDelete_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxButton btnLink = (DevExpress.Web.ASPxButton)sender;
            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)btnLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "F_ORDERGROUP", "F_ORDERDATE", "F_ORDERNO", "F_STATUS", "F_USEDCNT") as object[];
            bool bEnabled = true;
            switch (rowValues[3].ToString())
            {
                default:
                    bEnabled = true;
                    break;
                case "AAE604":
                case "AAE605":
                case "AAE606":
                case "AAE607":
                    bEnabled = false;
                    break;
            }
            btnLink.Enabled = bEnabled;
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnDelete('{0}', '{1}', '{2}', '{3}'); }}",
                rowValues[0],
                rowValues[1],
                rowValues[2],
                Convert.ToDecimal(rowValues[2]) > Convert.ToDecimal(0) ? "0" : "1");
        }
        #endregion

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

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string dayIDX = String.Empty;
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            string errMsg = String.Empty;   // 오류 메시지
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> paramDic = jss.Deserialize<Dictionary<string, string>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            if (paramDic["ACTION"] == "CLOSE")
            {
                if (!QPM22_STATUS_CHG(paramDic, "AAE606", out errMsg))
                {
                    ISOK = false;
                    result["ISOK"] = ISOK;
                    result["MSG"] = errMsg;
                }
                else
                {
                    ISOK = true;
                    result["ISOK"] = ISOK;
                    result["MSG"] = "마감처리 되었습니다.";
                }
                e.Result = jss.Serialize(result);
            }
            else if (paramDic["ACTION"] == "DELETE")
            {
                if (!QPM22_DEL(paramDic, out errMsg))
                {
                    ISOK = false;
                    result["ISOK"] = ISOK;
                    result["MSG"] = errMsg;
                }
                else
                {
                    ISOK = true;
                    result["ISOK"] = ISOK;
                    result["MSG"] = "삭제처리 되었습니다.";
                }
                e.Result = jss.Serialize(result);
            }
        }
        #endregion

        #endregion
    }
}