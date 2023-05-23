using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.LTRK.Biz;

namespace SPC.WebUI.Pages.LTRK.Popup
{
    public partial class LTRK0201POP02 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string sORDERGROUP = String.Empty;
        protected string sORDERDATE = String.Empty;
        DataTable dtList = new DataTable();
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

            // 작업지시그룹
            QPM21_LST();

            // 작업지시
            //QPM22_LST();

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
        {
            sORDERGROUP = Request.Params.Get("code");
            sORDERDATE = Request.Params.Get("date");
        }
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

        #region 작업지시그룹
        void QPM21_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", sORDERDATE);
                oParamDic.Add("F_EDDT", sORDERDATE);
                oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
                ds = biz.QPM21_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('작업지시 마스터 데이터를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else if (!bExistsDataSet(ds))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('작업지시 마스터 데이터를 구하는 중 장애가 발생하였습니다.\\r데이터가 없습니다');", true);
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                srcF_ORDERDATE.Text = dr["F_ORDERDATE"].ToString();
                srcF_INSDT.Text = Convert.ToDateTime(dr["F_INSDT"]).ToString("yyyy-MM-dd HH:mm:ss");
                srcDATA_ORIGIN_NAME.Text = dr["DATA_ORIGIN_NAME"].ToString();
                srcF_USERNM.Text = dr["F_USERNM"].ToString();
                hidF_STATUS.Text = dr["F_STATUS"].ToString();
                switch (hidF_STATUS.Text)
                {
                    case "0":
                        srcF_STATUS.ForeColor = System.Drawing.Color.Red;
                        srcF_STATUS.Text = "등록취소";
                        break;
                    case "1":
                        srcF_STATUS.ForeColor = System.Drawing.Color.Blue;
                        srcF_STATUS.Text = "등록완료";
                        break;
                    case "2":
                        srcF_STATUS.ForeColor = System.Drawing.Color.Red;
                        srcF_STATUS.Text = "등록전취소";
                        break;
                    case "8":
                        srcF_STATUS.ForeColor = System.Drawing.Color.Orange;
                        srcF_STATUS.Text = "임시등록";
                        break;
                    case "9":
                        srcF_STATUS.Text = "등록중";
                        break;
                }
            }
        }
        #endregion

        #region 작업지시
        void QPM22_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERDATE", sORDERDATE);
                oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
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

        #region 작업지시그룹 및 작업지시 상태변경
        bool QPM21_QPM22_STATUS_CHG(string sSTATUS, out string errMsg)
        {
            bool bExecute = false;
            errMsg = String.Empty;

            List<string> oSPs = new List<string>();
            List<object> oParameters = new List<object>();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
            oParamDic.Add("F_ORDERDATE", sORDERDATE);
            oParamDic.Add("F_STATUS", sSTATUS);
            oParamDic.Add("F_USER", gsUSERID);

            oSPs.Add("USP_QPM21_STATUS_CHG");
            oParameters.Add(oParamDic);

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
            oParamDic.Add("F_ORDERDATE", sORDERDATE);
            oParamDic.Add("F_STATUS", sSTATUS.Equals("1") ? "AAE603" : "AAE607");
            oParamDic.Add("F_USER", gsUSERID);

            oSPs.Add("USP_QPM22_STATUS_CHG");
            oParameters.Add(oParamDic);

            using (LTRKBiz biz = new LTRKBiz())
            {
                bExecute = biz.PROC_QPM22_BATCH(oSPs.ToArray(), oParameters.ToArray(), out errMsg);
            }

            return bExecute;
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

        #region 작업지시 전체상태변경
        bool QPM22_STATUS_CHG_ALL(Dictionary<string, string> paramDic, string sSTATUS, out string errMsg)
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
                oParamDic.Add("F_STATUS", sSTATUS);
                oParamDic.Add("F_USER", gsUSERID);

                bExecute = biz.QPM22_STATUS_CHG_ALL(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 작업지시 전체마감
        bool QPM22_CLOSE_ALL(Dictionary<string, string> paramDic, string sSTATUS, out string errMsg)
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
                oParamDic.Add("F_STATUS", sSTATUS);
                oParamDic.Add("F_USER", gsUSERID);

                bExecute = biz.QPM22_CLOSE_ALL(oParamDic, out errMsg);
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
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnDelete('{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowValues[2]);
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 작업지시
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

            if (paramDic["ACTION"] == "CLOSEALL")
            {
                if (!QPM22_STATUS_CHG_ALL(paramDic, "AAE606", out errMsg))
                {
                    ISOK = false;
                    result["ISOK"] = ISOK;
                    result["MSG"] = errMsg;
                }
                else
                {
                    ISOK = true;
                    result["ISOK"] = ISOK;
                    result["MSG"] = "전체 마감처리 되었습니다.";
                }
                e.Result = jss.Serialize(result);
            }
            else if (paramDic["ACTION"] == "CLOSE")
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