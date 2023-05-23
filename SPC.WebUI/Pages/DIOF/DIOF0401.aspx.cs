using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;
using System.Text;
using System.Collections;

namespace SPC.WebUI.Pages.DIOF
{
    public partial class DIOF0401 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataTable dtChart
        {
            get
            {
                return (DataTable)Session["DIOF0401"];
            }
            set
            {
                Session["DIOF0401"] = value;
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

            if ((String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false")))
            {
                // 설비조회
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
                devGridInsp.JSProperties["cpResultCode"] = "";
                devGridInsp.JSProperties["cpResultMsg"] = "";
                devChart.JSProperties["cpFunction"] = "resizeTo";
                devChart.JSProperties["cpChartWidth"] = "0";
            }

            // 점검기준조회
            RetrieveInspList();
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
            dtChart = null;
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
        {
            AspxCombox_DataBind(ddlSTATUS, "AAG9");
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 설비조회
        void RetrieveList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_USERID", gsUSERID);
                oParamDic.Add("F_BANCD", schF_BANCD.GetValue());
                oParamDic.Add("F_LINECD", schF_LINECD.GetValue());
                oParamDic.Add("F_MACHNM", schF_MACHNM.Text);
                ds = biz.QCD_MACH21_LST_BY_USR(oParamDic, out errMsg);
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

        #region 점검기준조회
        void RetrieveInspList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_MEASYMD", GetFromDt());
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QCD_MACH23_MACH26_LST(oParamDic, out errMsg);
            }

            devGridInsp.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridInsp.JSProperties["cpResultCode"] = "0";
                devGridInsp.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                //if (isCallback)
                    devGridInsp.DataBind();
            }
        }
        #endregion

        #region 트렌드조회
        void RetrieveTrendList(string sINSPIDX)
        {
            string errMsg = String.Empty;
            string sMEASYMD = GetFromDt();

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_INSPIDX", sINSPIDX);
                oParamDic.Add("F_STDT", DateTime.Parse(sMEASYMD).AddMonths(-1).ToString("yyyy-MM-dd"));
                oParamDic.Add("F_EDDT", sMEASYMD);
                ds = biz.QWK_MACH23_CHART(oParamDic, out errMsg);
            }

            if (!bExistsDataSet(ds))
                dtChart = null;
            else
                dtChart = ds.Tables[0];
        }
        #endregion

        #region 선택일체크정보조회
        DataSet RetrieveConfirmDS(out string errMsg)
        {
            errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_MEASYM", GetFromDt());
                ds = biz.QWK_MACH22_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(object comboBox, string CommonCode)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = comboBox as DevExpress.Web.ASPxComboBox;
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();
            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 설비조회
            RetrieveList();
        }
        #endregion

        #region devGridInsp_CustomCallback
        /// <summary>
        /// devGridInsp_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridInsp_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 점검기준조회
            RetrieveInspList();

            dtChart = null;
        }
        #endregion

        #region devGridInsp_CellEditorInitialize
        /// <summary>
        /// devGridInsp_CellEditorInitialize
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewEditorEventArgs</param>
        protected void devGridInsp_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_JUDGE")) return;

            AspxCombox_DataBind(e.Editor, "AAG7");
        }
        #endregion

        #region devGridInsp_CustomColumnDisplayText
        /// <summary>
        /// devGridInsp_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGridInsp_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_CYCLENM"))
            {
                string sValue = "";
                object obj = devGridInsp.GetRowValues(e.VisibleRowIndex, "F_CYCLECD", "F_NUMBER", "F_CHASU");

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
            else if (e.Column.FieldName.Equals("F_JUDGE"))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
            }
            else
                return;
        }
        #endregion

        #region devGridInsp_BatchUpdate
        /// <summary>
        /// devGridInsp_BatchUpdate
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs</param>
        protected void devGridInsp_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            List<string> oSPs = new List<string>();
            List<Dictionary<string, string>> oParams = new List<Dictionary<string, string>>();
            string resultMsg = String.Empty;
            bool bExecute = true;

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_MEASIDX", (Value.Keys["F_MEASIDX"] ?? "").ToString());
                    oParamDic.Add("F_INSPIDX", (Value.Keys["F_INSPIDX"] ?? "").ToString());
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_MEASYMD", GetFromDt());
                    oParamDic.Add("F_CHASU", (Value.Keys["F_NUMBER"] ?? "").ToString());
                    oParamDic.Add("F_BANCD", srcF_BANCD.Text);
                    oParamDic.Add("F_LINECD", srcF_LINECD.Text);
                    oParamDic.Add("F_MEASURE", (Value.NewValues["F_MEASURE"] ?? "").ToString());
                    oParamDic.Add("F_JUDGE", (Value.NewValues["F_JUDGE"] ?? "").ToString());
                    oParamDic.Add("F_USERID", gsUSERID);
                    oParamDic.Add("F_USERNM", gsUSERNM);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QWK_MACH23_INS_UPD");
                }

                // 이상발생 원인/조치 등록
                var sJsonString = txtRESPVALUES.Text;
                if (!String.IsNullOrEmpty(sJsonString))
                {
                    string uniqueID = UF.Encrypts.GetUniqueKey();
                    int usnqieIDX = 0;

                    List<Dictionary<string, string>> respList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(HttpUtility.UrlDecode(sJsonString));

                    foreach (Dictionary<string, string> respDic in respList)
                    {
                        usnqieIDX++;

                        oParamDic = new Dictionary<string, string>();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_REMEDYSEQ", uniqueID);
                        oParamDic.Add("F_REMEDYIDX", usnqieIDX.ToString());
                        oParamDic.Add("F_INSPIDX", respDic["INSPIDX"]);
                        oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                        oParamDic.Add("F_NUMBER", respDic["NUMBER"]);
                        oParamDic.Add("F_NGTYPE", respDic["NGTYPE"]);
                        oParamDic.Add("F_NGREMK", respDic["NGREMK"]);
                        oParamDic.Add("F_RESPTYPE", respDic["RESPTYPE"]);
                        oParamDic.Add("F_RESPREMK", respDic["RESPREMK"]);
                        oParamDic.Add("F_RESPDT", respDic["RESPDT"]);
                        oParamDic.Add("F_RESPUSER", respDic["RESPUSER"]);
                        oParamDic.Add("F_STATUS", respDic["STATUS"]);
                        oParams.Add(oParamDic);
                        oSPs.Add("USP_QWK_MACH24_TEMP_INS");
                    }

                    // 1. 임시테이블에서 기본테이블로 데이터 이동
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_REMEDYSEQ", uniqueID);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_USER", gsUSERID);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QWK_MACH24_COPY");

                    // 2. 임시테이블에서 정보삭제
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_REMEDYSEQ", uniqueID);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QWK_MACH24_TEMP_DEL");
                }
            }
            #endregion

            #region Database Execute
            if (oSPs.Count > 0 && oParams.Count > 0 && oSPs.Count == oParams.Count)
            {
                using (FDCKBiz biz = new FDCKBiz())
                {
                    bExecute = biz.PROC_QCD_MACH_MULTI(oSPs.ToArray(), oParams.ToArray(), out resultMsg);
                }

                if(!String.IsNullOrEmpty(resultMsg))
                    bExecute = false;
            }

            if (!bExecute)
            {
                devGridInsp.JSProperties["cpResultCode"] = "0";
                devGridInsp.JSProperties["cpResultMsg"] = resultMsg;
            }
            else
            {
                devGridInsp.JSProperties["cpResultCode"] = "9";
                devGridInsp.JSProperties["cpResultMsg"] = "";
            }
            #endregion

            e.Handled = true;

            // 점검기준조회
            RetrieveInspList();
        }
        #endregion

        #region devChart_CustomCallback
        /// <summary>
        /// devChart_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.XtraCharts.Web.CustomCallbackEventArgs</param>
        protected void devChart_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (!String.IsNullOrEmpty(oParams[2]) && !String.IsNullOrEmpty(oParams[3]))
            {
                RetrieveTrendList(oParams[2]);
            }

            if (dtChart != null)
            {
                devChart.Series.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                foreach (DataRow dtRow in dtChart.Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_MEASURE"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_MEASURE"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_MEASURE"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_MEASURE"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(devChart, "상한", "F_INDEX", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart, "규격", "F_INDEX", "F_STAND", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart, "하한", "F_INDEX", "F_MIN", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart, "측정", "F_INDEX", "F_MEASURE", System.Drawing.Color.FromArgb(0, 102, 153), 2);

                devChart.DataSource = dtChart;
                devChart.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart);
                DevExpressLib.SetChartDiagram(devChart, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart, String.Format("{0} 트렌드", HttpUtility.UrlDecode(oParams[3])), false);
            }
            else
            {
                devChart.Series.Clear();

                devChart.DataSource = null;
                devChart.DataBind();

                DevExpressLib.SetChartTitle(devChart, HttpUtility.UrlDecode(oParams[3]), false);
            }
        }
        #endregion

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="Width">Int32</param>
        void devChart_ResizeTo(object sender, Int32 Width, Int32 Height)
        {
            if (Width >= 0 && Height >= 0)
            {
                DevExpress.XtraCharts.Web.WebChartControl chart = sender as DevExpress.XtraCharts.Web.WebChartControl;
                chart.Width = new Unit(Width);
                chart.Height = new Unit(Height);

                chart.JSProperties["cpFunction"] = "resizeTo";
                chart.JSProperties["cpChartWidth"] = Width.ToString();
            }
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
            ds = RetrieveConfirmDS(out errMsg);
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
                DataRow dr = ds.Tables[0].Rows[0];
                if (bool.Parse(dr["F_CNFMYN"].ToString()) == true)
                {
                    PAGEDATA["F_CONFIRM"] = String.Format("[{0}] {1} 확인 처리되었습니다",
                        DateTime.Parse(dr["F_CNFMDT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                        dr["F_USERNM"]);
                }
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