using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using SPC.WebUI.Common;
using SPC.ANLS.Biz;
using SPC.WebUI.Common.Library;
using DevExpress.XtraCharts;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0101 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string oSetParam = String.Empty;
        private DBHelper spcDB;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["ANLS0101_1_NEW"];
            }
            set
            {
                Session["ANLS0101_1_NEW"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["ANLS0101_2_NEW"];
            }
            set
            {
                Session["ANLS0101_2_NEW"] = value;
            }
        }

        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["ANLS0101_3_NEW"];
            }
            set
            {
                Session["ANLS0101_3_NEW"] = value;
            }
        }

        DataTable dtChart4
        {
            get
            {
                return (DataTable)Session["ANLS0101_4_NEW"];
            }
            set
            {
                Session["ANLS0101_4_NEW"] = value;
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

            if (!IsCallback)
            {
                chk_calc.SelectedIndex = 0;
            }

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
                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
                devChart2.JSProperties["cpFunction"] = "resizeTo";
                devChart2.JSProperties["cpChartWidth"] = "0";
                devChart3.JSProperties["cpFunction"] = "resizeTo";
                devChart3.JSProperties["cpChartWidth"] = "0";
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
            oSetParam = Request.QueryString.Get("oSetParam") ?? "";
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
        {
            dtChart1 = null;
            dtChart2 = null;
            dtChart3 = null;

            chk_reject.Checked = String.IsNullOrEmpty(oSetParam);

            if (!String.IsNullOrEmpty(oSetParam) && !gsVENDOR)
            {
                string[] oSetParams = oSetParam.Split('|');
                ucComp.compParam = oSetParams[16];
                ucFact.factParam = oSetParams[17];
            }
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

        #region 그리드
        void SetGrid(DataTable dt1, DataTable dt2, DataTable dt3)
        {
            Int32 idx = 0,
                    siryo = Convert.ToInt32(txtSIRYO.Text),
                    digit = Convert.ToInt32(txtFREEPOINT.Text);

            string xbar = string.Empty;
            string rs = string.Empty;

            if (siryo > 1)
            {
                xbar = "Xbar";
                rs = "R";
            }
            else
            {
                xbar = "X";
                rs = "Rs";
            }
            // 데이타 구성용 DataTable
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("검사일자", typeof(String));
            dtTemp.Columns.Add("검사시간", typeof(String));
            dtTemp.Columns.Add(xbar, typeof(String));
            dtTemp.Columns.Add(rs, typeof(String));

            for (idx = 0; idx < siryo; idx++)
            {
                dtTemp.Columns.Add(String.Format("X{0}", idx + 1), typeof(String));
            }

            // 그리드 DataSource용 DataTable(Pivot)
            DataTable dtGrid = new DataTable();
            DataColumnCollection columns = dtTemp.Columns;

            foreach (DataRow dtRow1 in dt1.Rows)
            {
                // 데이타 구성용 DataRow
                idx = 0;
                DataRow dtNewRow = dtTemp.NewRow();
                dtNewRow["검사일자"] = String.Format("{0}({1})", DateTime.Parse(dtRow1["F_WORKDATE"].ToString()).ToString("yyyy.MM.dd"), dtRow1["F_TSERIALNO"]);
                dtNewRow["검사시간"] = dtRow1["F_WORKTIME"].ToString();
                dtNewRow[xbar] = Math.Round(Convert.ToDecimal(dtRow1["F_XBAR"].ToString()), digit + 1).ToString();
                dtNewRow[rs] = Math.Round(Convert.ToDecimal(dtRow1["F_XRANGE"].ToString()), digit + 1).ToString();
                foreach (DataRow dtRow3 in dt3.Select(String.Format("F_WORKDATE='{0}' AND F_TSERIALNO='{1}'", dtRow1["F_WORKDATE"], dtRow1["F_TSERIALNO"])))
                {
                    idx++;
                    if (!columns.Contains(String.Format("X{0}", idx)))
                    {
                        dtTemp.Columns.Add(String.Format("X{0}", idx), typeof(String));
                    }
                    dtNewRow[String.Format("X{0}", idx)] = Math.Round(Convert.ToDecimal(dtRow3["F_MEASURE"].ToString()), digit).ToString();
                }
                dtTemp.Rows.Add(dtNewRow);
            }

            // Pivot Fill
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "검사일자");

            devGrid.DataSource = dtPivotTable;
            devGrid.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devChart1_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart1 != null)
            {
                devChart1.Series.Clear();

                DevExpressLib.SetChartLineSeries(devChart1, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X" : "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                DevExpressLib.SetChartLineSeries(devChart1, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart1, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart1, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, null, null, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart1, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X 관리도" : "X-Bar 관리도", false);

                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            }
        }
        #endregion

        #region devChart2_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart1 != null)
            {
                devChart2.Series.Clear();

                DevExpressLib.SetChartLineSeries(devChart2, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "Rs" : "R", "F_MEMBER", "F_XRANGE", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                DevExpressLib.SetChartLineSeries(devChart2, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLR" : "C_CLR", System.Drawing.Color.Green);
                DevExpressLib.SetChartLineSeries(devChart2, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLR" : "C_UCLR", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart2, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLR" : "C_LCLR", System.Drawing.Color.Red);

                devChart2.DataSource = dtChart1;
                devChart2.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart2);
                DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, null, null, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart2, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "Rs 관리도" : "R 관리도", false);

                XYDiagram diagram = (XYDiagram)devChart2.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            }
        }
        #endregion

        #region devChart3_CustomCallback
        /// <summary>
        /// devChart3_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart3_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {            
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            bool bExecute1 = false;
            bool bExecute2 = false;
            StringBuilder sb = null;
            DataTable dt3 = new DataTable();
            string errMsg = String.Empty;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());

            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            SPCChart sc = new SPCChart();

            DataSet ds = GetDataHistogram(oParamDic);

            var  c = sc.GetHistogram(this.devChart3, ds);

            if (ds.Tables.Count == 4)
                dtChart4 = ds.Tables[3].Copy();
            if (c == null)
            {
                devChart3.JSProperties["cpResultCode"] = "0";
                devChart3.JSProperties["cpResultMsg"] = "조회 조건에 맞는 데이터가 없습니다.";
            }
            else
            {
                this.devChart3 = c;
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

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            bool bExecute2 = false;
            bool bExecute3 = false;
            StringBuilder sb = null;

            devGrid.JSProperties["cpResult1"] = "";
            devGrid.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", GetCompCD());
            oParamDic.Add("F_FACTCD", GetFactCD());
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());

            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            // 검사규격을 구한다
            using (ANLSBiz biz = new ANLSBiz())
            {
                ds = biz.ANLS0101_1(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {
                    bExecute1 = true;

                    sb = new StringBuilder();

                    dt1 = ds.Tables[0].Copy();

                    DataRow dtRow = dt1.Rows[0];

                    for (int i = 0; i < dt1.Columns.Count; i++)
                    {
                        if (i > 0) sb.Append("|");
                        sb.Append(dtRow[i].ToString());
                    }

                    devGrid.JSProperties["cpResult1"] = sb.ToString();
                }
            }

            if (true == bExecute1)
            {
                // 검사측정자료를 구한다
                using (ANLSBiz biz = new ANLSBiz())
                {
                    ds = biz.ANLS0101_2(oParamDic, out errMsg);
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bExistsDataSet(ds))
                    {
                        devGrid.JSProperties["cpResultCode"] = "0";
                        devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 측정데이타가 없습니다";
                    }
                    else
                    {
                        bExecute2 = true;

                        sb = new StringBuilder();

                        dt3 = ds.Tables[0].Copy();
                    }
                }
            }

            if (true == bExecute2)
            {
                // 검사분석자료를 구한다
                using (ANLSBiz biz = new ANLSBiz())
                {
                    ds = biz.ANLS0101_4(oParamDic, out errMsg);
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bExistsDataSet(ds))
                    {
                        //devGrid.JSProperties["cpResultCode"] = "0";
                        //devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 분석데이타가 없습니다";
                    }
                    else
                    {
                        bExecute3 = true;

                        sb = new StringBuilder();

                        dt2 = ds.Tables[0].Copy();

                        DataRow dtRow = dt2.Rows[0];

                        for (int i = 0; i < dt2.Columns.Count; i++)
                        {
                            if (i > 0) sb.Append("|");
                            sb.Append(dtRow[i].ToString());
                        }

                        devGrid.JSProperties["cpResult2"] = sb.ToString();
                    }
                }
            }

            // 모든 데이타가 구성되면 그리드를 그린다
            if (bExecute1 && bExecute2)
            {
                SetGrid(dt1, dt2, dt3);

                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart1 = dt1.Copy();
                }
                else
                    dtChart1 = null;

                if (dt2.Rows.Count > 0)
                {
                    dtChart2 = dt2.Copy();
                }
                else
                    dtChart2 = null;

                if (dt3.Rows.Count > 0)
                {
                    dtChart3 = dt3.Copy();
                }
                else
                    dtChart3 = null;
            }
        }
        #endregion

        #region GetDataHistogram
        public DataSet GetDataHistogram(Dictionary<string, string> dicParam)
        {
            DataSet ds = new DataSet();
            string errMsg;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(dicParam);
                ds = spcDB.GetDataSet("USP_ANLS0203_1", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}