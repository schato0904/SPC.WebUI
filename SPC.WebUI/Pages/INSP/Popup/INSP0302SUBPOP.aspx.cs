using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.INSP.Biz;

namespace SPC.WebUI.Pages.INSP.Popup
{
    public partial class INSP0302SUBPOP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Dictionary<string, string> PopParam = new Dictionary<string, string>();
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
                devGrid1.JSProperties["cpResultCode"] = "";
                devGrid1.JSProperties["cpResultMsg"] = "";
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
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
            PopParam = DeserializeJSON(Request.QueryString.Get("PopParam"));

            schF_ITEMCD.Text = GetDicValue(PopParam, "F_ITEMCD");
            schF_WORKCD.Text = GetDicValue(PopParam, "F_WORKCD");
            schF_MOLDNO.Text = GetDicValue(PopParam, "F_MOLDNO");
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

        #region 품명, 공정명
        DataSet QWK03A_INSP0302POP_GET_NM(Dictionary<string, object> paramDic, out string errMsg)
        {
            if (ds != null) ds.Clear();
            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", GetDicValue(paramDic, "F_ITEMCD"));
                oParamDic.Add("F_WORKCD", GetDicValue(paramDic, "F_WORKCD"));

                ds = biz.QWK03A_INSP0302POP_GET_NM(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 샘플검사(중간검사-EF)목록
        void QWK03A_INSP0302_SUB_LST(Dictionary<string, object> paramDic)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", GetDicValue(paramDic, "F_ITEMCD"));
                oParamDic.Add("F_WORKCD", GetDicValue(paramDic, "F_WORKCD"));
                oParamDic.Add("F_MOLDNO", GetDicValue(paramDic, "F_MOLDNO"));

                ds = biz.QWK03A_INSP0302_SUB_LST(oParamDic, out errMsg);
            }

            devGrid1.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid1.DataBind();
            }
        }
        #endregion

        #region 상세목록
        void QWK03A_INSP0302_DETAIL_LST(Dictionary<string, object> paramDic)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", GetDicValue(paramDic, "F_ITEMCD"));
                oParamDic.Add("F_WORKCD", GetDicValue(paramDic, "F_WORKCD"));
                oParamDic.Add("F_WORKDATE", GetDicValue(paramDic, "F_WORKDATE"));
                oParamDic.Add("F_TSERIALNO", GetDicValue(paramDic, "F_TSERIALNO"));

                ds = biz.QWK03A_INSP0302_DETAIL_LST(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid2.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid1_CustomCallback
        /// <summary>
        /// devGrid1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            Dictionary<string, object> paramDic;
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameters); // 웹에서 전달된 파라미터
            }
            else
            {
                paramDic = new Dictionary<string, object>();
            }
            QWK03A_INSP0302_SUB_LST(paramDic);
        }
        #endregion

        #region devGrid1_HtmlDataCellPrepared
        /// <summary>
        /// devGrid1_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_WORKNM" };

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
                }
            }
        }
        #endregion

        #region devGrid2_CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            Dictionary<string, object> paramDic;
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameters); // 웹에서 전달된 파라미터
            }
            else
            {
                paramDic = new Dictionary<string, object>();
            }
            QWK03A_INSP0302_DETAIL_LST(paramDic);
        }
        #endregion

        #region devGrid2_CustomColumnDisplayText
        /// <summary>
        /// devGrid2_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid2_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            if (e.Column.FieldName.Equals("F_NGOKCHK"))
            {
                e.EncodeHtml = false;
                switch (e.Value.ToString())
                {
                    case "0": e.DisplayText = @"<span style='color:blue;'>OK</span>"; break;
                    case "1": e.DisplayText = @"<span style='color:red;'>NG</span>"; break;
                    case "2": e.DisplayText = @"<span style='color:dimgray;'>OC</span>"; break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            bool bExecute = false;
            Dictionary<string, object> oDataDic = null;
            string errMsg = String.Empty;   // 오류 메시지
            string pkey = String.Empty;
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            switch (paramDic["ACTION"].ToString())
            {
                case "GET_NM":
                    Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                    ds = QWK03A_INSP0302POP_GET_NM(paramDic, out errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        ISOK = false;
                        msg = errMsg;
                    }
                    else if (!bExistsDataSet(ds))
                    {
                        ISOK = false;
                        msg = "조회된 데이터가 없습니다.";
                    }
                    else
                    {
                        ISOK = true;
                        msg = "";
                        DataTable dtTemp = ds.Tables[0].Copy();
                        // 조회한 데이터를 Dictionary 형태로 변환
                        DataRow dr = dtTemp.Rows[0];
                        PAGEDATA = dtTemp.Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                    }
                    result["ISOK"] = ISOK;
                    result["MSG"] = msg;
                    result["PAGEDATA"] = PAGEDATA;
                    e.Result = jss.Serialize(result);
                    break;
            }
        }
        #endregion

        #endregion
    }
}