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
    public partial class DIOF0402 : WebUIBasePage
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
                return (DataTable)Session["DIOF0402"];
            }
            set
            {
                Session["DIOF0402"] = value;
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
                devGridData.JSProperties["cpResultCode"] = "";
                devGridData.JSProperties["cpResultMsg"] = "";
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
                oParamDic.Add("F_BANCD", srcF_BANCD.Text);
                oParamDic.Add("F_LINECD", srcF_LINECD.Text);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QCD_MACH26_LST4(oParamDic, out errMsg);
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
                //if (IsCallback)
                devGridInsp.DataBind();
            }
        }
        #endregion

        #region 트렌드조회
        void RetrieveTrendList(string sINSPIDX)
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_INSPIDX", sINSPIDX);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QWK_MACH23_CHART(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridData.JSProperties["cpResultCode"] = "0";
                devGridData.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                    dtChart = null;
                else
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("측정일", typeof(string));
                    dtTemp.Columns.Add("측정값", typeof(string));
                    dtTemp.Columns.Add("판정", typeof(string));
                    string[] aMEASURE = new string[2];

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dtNewRow = dtTemp.NewRow();
                        aMEASURE = dr["F_MEASURE"].ToString().IndexOf('.') >= 0 ? dr["F_MEASURE"].ToString().Split('.') : new string[] { dr["F_MEASURE"].ToString(), "" };
                        dtNewRow["측정일"] = dr["F_INDEX"];
                        dtNewRow["측정값"] = int.Parse(dr["F_DIGIT"].ToString()) > 0 ? String.Join(".", aMEASURE) : aMEASURE[0];
                        dtNewRow["판정"] = dr["F_JUDGENM"];
                        dtTemp.Rows.Add(dtNewRow);
                    }

                    // Pivot Fill
                    DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "측정일");

                    devGridData.DataSource = dtPivotTable;
                    devGridData.DataBind();

                    dtChart = ds.Tables[0];
                }
            }
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

        #region devGridData_CustomCallback
        /// <summary>
        /// devGridData_CustomCallback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGridData_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            RetrieveTrendList(e.Parameters);
        }
        #endregion

        #endregion
    }
}