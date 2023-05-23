using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;
using System.Collections;

namespace SPC.WebUI.Pages.DIOF
{
    public partial class DIOF0501_cheongmyung : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string[] arrDays = new string[31];
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
                devGridResp.JSProperties["cpResultCode"] = "";
                devGridResp.JSProperties["cpResultMsg"] = "";
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
        {
            int iDay = 0;
            //DateTime sDate = DateTime.Parse(GetFromDt() + "-01");
            //for (iDay = 0; iDay < 31; iDay++) arrDays[iDay] = "";
            //for (iDay = 0; iDay < DateTime.DaysInMonth(sDate.Year, sDate.Month); iDay++) arrDays[iDay] = (iDay + 1).ToString();
            for (iDay = 0; iDay < 31; iDay++) arrDays[iDay] = (iDay + 1).ToString();
        }
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

        #region 설비조회
        void RetrieveMachList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", schF_BANCD.GetValue());
                oParamDic.Add("F_LINECD", schF_LINECD.GetValue());
                oParamDic.Add("F_MACHKIND", schF_MACHKIND.GetValue());
                oParamDic.Add("F_MACHNM", "");
                ds = biz.QCD_MACH21_LST(oParamDic, out errMsg);
            }

            schF_MACHIDX.DataSource = ds;
            schF_MACHIDX.DataBind();
        }
        #endregion

        #region 설비점검시트조회
        void RetrieveList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                string sMONTH = GetFromDt();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MONTH", !String.IsNullOrEmpty(sMONTH) ? sMONTH : DateTime.Today.ToString("yyyy-MM"));
                oParamDic.Add("F_MACHIDX", (schF_MACHIDX.Value ?? "").ToString());
                ds = biz.QWK_MACH23_LST_CM(oParamDic, out errMsg);
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

        #region 설비이상조치조회
        void RetrieveRespList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                string sMONTH = GetFromDt();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_TYPE", "M");
                oParamDic.Add("F_DATE", !String.IsNullOrEmpty(sMONTH) ? sMONTH : DateTime.Today.ToString("yyyy-MM"));
                oParamDic.Add("F_MACHIDX", (schF_MACHIDX.Value ?? "").ToString());
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QWK_MACH24_LST2(oParamDic, out errMsg);
            }

            devGridResp.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridResp.JSProperties["cpResultCode"] = "0";
                devGridResp.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGridResp.DataBind();
            }
        }
        #endregion

        #region 일별체크정보조회
        DataSet RetrieveConfirmList(out string resultMsg)
        {
            resultMsg = String.Empty;

            if (ds != null) ds.Clear();

            using (FDCKBiz biz = new FDCKBiz())
            {
                string sMONTH = GetFromDt();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MONTH", !String.IsNullOrEmpty(sMONTH) ? sMONTH : DateTime.Today.ToString("yyyy-MM"));
                oParamDic.Add("F_MACHIDX", (schF_MACHIDX.Value ?? "").ToString());
                ds = biz.QWK_MACH22_CFM_LST(oParamDic, out resultMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 설비점검기준조회
            RetrieveList();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_INSPIDX")
                || e.Column.FieldName.Equals("F_NUMBER")
                || e.Column.FieldName.Equals("F_INSPNO")
                || e.Column.FieldName.Equals("F_INSPNM")
                || e.Column.FieldName.Equals("F_INSPREMARK")) return;

            if (e.Column.FieldName.Equals("F_CYCLENM"))
            {
                string sValue = "";
                object obj = devGrid.GetRowValues(e.VisibleRowIndex, "F_CYCLECD", "F_NUMBER", "F_CHASU");

                if (obj != null)
                {
                    string[] rowValues = ((IEnumerable)obj).Cast<object>().Select(x => x.ToString()).ToArray();

                    switch (rowValues[0])
                    {
                        case "AAG401":
                            if (rowValues[2].Equals("1")) sValue = "1일"; else sValue = String.Format("{0}차", rowValues[1]);
                            break;
                        case "AAG407":
                            if (rowValues[1].Equals("1")) sValue = "주간"; else sValue = "야간";
                            break;
                        default:
                            sValue = e.Value.ToString();
                            break;
                    }
                }

                e.DisplayText = sValue;
            }
            else
            {
                string value = e.Value.ToString();

                if (value.Equals("X") || value.Equals("--"))
                    e.DisplayText = "";
                else
                    e.DisplayText = value.Split('|')[0];
            }
        }
        #endregion

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName.Equals("F_INSPIDX")
                || e.DataColumn.FieldName.Equals("F_NUMBER")
                || e.DataColumn.FieldName.Equals("F_INSPNO")
                || e.DataColumn.FieldName.Equals("F_INSPNM")
                || e.DataColumn.FieldName.Equals("F_INSPREMARK")
                || e.DataColumn.FieldName.Equals("F_CYCLENM")) return;

            string value = e.CellValue.ToString();

            if (!value.Equals("X") && !value.Equals("--"))
            {
                string sJudge = value.Split('|')[1];

                if (sJudge.Equals("AAG701"))
                {
                    //e.Cell.BackColor = System.Drawing.Color.Green;
                    //e.Cell.ForeColor = System.Drawing.Color.White;
                    e.Cell.ForeColor = System.Drawing.Color.Black;
                }
                else if (sJudge.Equals("AAG702"))
                {
                    //e.Cell.BackColor = System.Drawing.Color.Red;
                    //e.Cell.ForeColor = System.Drawing.Color.White;
                    e.Cell.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion

        #region devGridResp_CustomCallback
        /// <summary>
        /// devGridResp_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridResp_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 불량처리조회
            RetrieveRespList();
        }
        #endregion

        #region devGridResp_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGridResp_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_INSPNM", "F_NGREMK", "F_RESPREMK" };

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

        #region schF_MACHIDX_Callback
        /// <summary>
        /// schF_MACHIDX_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgsBase</param>
        protected void schF_MACHIDX_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            // 설비조회
            RetrieveMachList();
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
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            string errMsg = String.Empty;   // 오류 메시지
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값
            Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
            
            if (ds != null) ds.Clear();
            ds = RetrieveConfirmList(out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                ISOK = false;
                msg = errMsg;
            }
            else if (!bExistsDataSet(ds))
            {
                ISOK = false;
                msg = "NO DATA";
            }
            else
            {
                ISOK = true;
                msg = string.Empty;
                // 조회한 데이터를 Dictionary 형태로 변환
                PAGEDATA = ds.Tables[0].AsEnumerable()
                    .ToDictionary<DataRow, string, object>(row => row.Field<int>(0).ToString(), row => row.Field<object>(0));
            }
            result["ISOK"] = ISOK;
            result["MSG"] = msg;
            result["PAGEDATA"] = PAGEDATA;
            e.Result = jss.Serialize(result);
        }
        #endregion

        #endregion
    }
}