using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;

namespace SPC.WebUI.Pages.DIOF
{
    public partial class DIOF0403 : WebUIBasePage
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

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 조회
                RetrieveList();
            }

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
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 조회
        void RetrieveList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDATE", GetFromDt());
                oParamDic.Add("F_EDDATE", GetToDt());
                oParamDic.Add("F_BANCD", schF_BANCD.GetValue());
                oParamDic.Add("F_LINECD", schF_LINECD.GetValue());
                oParamDic.Add("F_MACHKIND", schF_MACHKIND.GetValue());
                oParamDic.Add("F_MACHNM", schF_MACHNM.Text);
                oParamDic.Add("F_MACHIDX", (schF_MACHIDX.Value ?? "").ToString());
                oParamDic.Add("F_STATUS", schF_STATUS.GetValue());
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QWK_MACH24_LST(oParamDic, out errMsg);
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

        #region 뷰(선택한 설비 상세정보 조회)
        DataSet RetrieveDetail(string sREMEDYIDX, out string errMsg)
        {   
            errMsg = String.Empty;
            if (ds != null) ds.Clear();
            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDATE", GetFromDt());
                oParamDic.Add("F_EDDATE", GetToDt());
                oParamDic.Add("F_REMEDYIDX", sREMEDYIDX);
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QWK_MACH24_LST(oParamDic, out errMsg);
            }
            return ds;
        }
        #endregion

        #region 수정
        private bool QWK_MACH24_UPD(out string resultMsg)
        {
            resultMsg = String.Empty;
            bool bExecute = false;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_REMEDYIDX", srcF_REMEDYIDX.Text);
                oParamDic.Add("F_INSPIDX", srcF_INSPIDX.Text);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_NUMBER", srcF_NUMBER.Text);
                oParamDic.Add("F_RESPTYPE", srcF_RESPTYPE.GetValue());
                oParamDic.Add("F_RESPREMK", srcF_RESPREMK.Text);
                oParamDic.Add("F_RESPDT", srcF_RESPDT.Text);
                oParamDic.Add("F_RESPUSER", srcF_RESPUSER.Text);
                oParamDic.Add("F_STATUS", srcF_STATUS.GetValue());
                oParamDic.Add("F_ATTATCHSEQ", txtIMAGESEQ.Text);
                oParamDic.Add("F_USER", gsUSERID);

                bExecute = biz.QWK_MACH24_UPD(oParamDic, out resultMsg);
            }

            if (!String.IsNullOrEmpty(resultMsg))
            {
                bExecute = false;
            }

            return bExecute;
        }
        #endregion

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

        #endregion

        #region 사용자이벤트

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
            Dictionary<string, string> paramDic = jss.Deserialize<Dictionary<string, string>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            switch (paramDic["ACTION"])
            {
                case "SAVE":
                    if (paramDic["PAGEMODE"] == "U")
                    {
                        if (!QWK_MACH24_UPD(out errMsg))
                        {
                            ISOK = false;
                            result["ISOK"] = ISOK;
                            result["MSG"] = errMsg;
                        }
                        else
                        {
                            ISOK = true;
                            result["ISOK"] = ISOK;
                            result["MSG"] = "수정되었습니다.";
                            PKEY["F_REMEDYIDX"] = srcF_REMEDYIDX.Text;
                            result["PKEY"] = PKEY;
                        }
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "GET":
                    Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                    if (ds != null) ds.Clear(); ;
                    ds = RetrieveDetail(paramDic["F_REMEDYIDX"], out errMsg);
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
                        msg = string.Empty;
                        DataRow dr = ds.Tables[0].Rows[0];
                        // 조회한 데이터를 Dictionary 형태로 변환
                        PAGEDATA = ds.Tables[0].Columns.Cast<DataColumn>()
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

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 조회
            RetrieveList();
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_MACHNM", "F_BANNM", "F_LINENM", "F_INSPNM", "F_NGREMK", "F_RESPREMK" };

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

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            if ((e.Column as DevExpress.Web.GridViewDataColumn).FieldName == "F_USEYN") //|| e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = GlobalFunction.StripHtml(e.Text);
            }
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비이상조치내역", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}