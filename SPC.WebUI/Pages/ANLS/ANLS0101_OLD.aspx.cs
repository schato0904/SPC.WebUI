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
using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0101_OLD : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string oSetParam = String.Empty;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["ANLS0101_1"];
            }
            set
            {
                Session["ANLS0101_1"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["ANLS0101_2"];
            }
            set
            {
                Session["ANLS0101_2"] = value;
            }
        }

        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["ANLS0101_3"];
            }
            set
            {
                Session["ANLS0101_3"] = value;
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

                //Decimal maxAxisY = Decimal.MinValue;
                //Decimal minAxisY = Decimal.MaxValue;

                //foreach (DataRow dtRow in dtChart1.Rows)
                //{
                //    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                //        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                //    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                //        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));
                //}

                //maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                //minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(devChart1, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X" : "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                DevExpressLib.SetChartLineSeries(devChart1, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart1, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart1, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                //lineSeriesView.AxisY.WholeRange.AlwaysShowZeroLevel


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

                //Decimal maxAxisY = Decimal.MinValue;
                //Decimal minAxisY = Decimal.MaxValue;

                //string maxColumn = String.Empty;
                //string minColumn = String.Empty;

                //foreach (DataRow dtRow in dtChart1.Rows)
                //{
                //    maxColumn = chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLR" : "C_UCLR";
                //    minColumn = chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLR" : "C_LCLR";

                //    if (!String.IsNullOrEmpty(dtRow[maxColumn].ToString()))
                //        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow[maxColumn]));

                //    if (!String.IsNullOrEmpty(dtRow[minColumn].ToString()))
                //        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow[minColumn]));
                //}

                //maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                //minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

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

            if (dtChart1 != null)
            {
                string errMsg = String.Empty;

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

                // 히스토그램을 구한다
                using (ANLSBiz biz = new ANLSBiz())
                {
                    ds = biz.ANLS0101_3(oParamDic, out errMsg);
                }

                // 데이타가 있는 경우
                if (bExistsDataSet(ds))
                {
                    devChart3.Series.Clear();

                    //Decimal maxAxisX = Decimal.MinValue;
                    //Decimal minAxisX = Decimal.MaxValue;
                    //Decimal maxAxisY = Decimal.MinValue;
                    //Decimal minAxisY = Decimal.MaxValue;

                    //foreach (DataRow dtRow in ds.Tables[0].Rows)
                    //{
                    //    if (!String.IsNullOrEmpty(dtRow["F_GBNNM"].ToString()))
                    //    {
                    //        maxAxisX = Math.Max(maxAxisX, Convert.ToDecimal(dtRow["F_GBNNM"]));
                    //        minAxisX = Math.Min(minAxisX, Convert.ToDecimal(dtRow["F_GBNNM"])); 
                    //    }

                    //    if (!String.IsNullOrEmpty(dtRow["F_SIRYO"].ToString()))
                    //    {
                    //        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_SIRYO"]));
                    //        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_SIRYO"]));
                    //    }
                    //}

                    //maxAxisX = maxAxisX == Decimal.MinValue ? 0 : maxAxisX;
                    //minAxisX = minAxisX == Decimal.MaxValue ? 0 : minAxisX;
                    //maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                    //minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                    DevExpressLib.SetChartBarLineSeries(devChart3, "시료수", "F_GBNNM", "F_SIRYO", System.Drawing.Color.LightBlue);
                    DevExpressLib.SetChartBarLineSeries(devChart3, "규격", "F_GBNNM", "F_VALUE", System.Drawing.Color.LightPink, 0.1);

                    devChart3.Series["규격"].Label.TextPattern = "{A}";
                    BarSeriesLabel serieslabel = devChart3.Series["규격"].Label as BarSeriesLabel;
                    serieslabel.Position = BarSeriesLabelPosition.Top;
                    //BarSeriesLabelPosition.Top;
                    
                    devChart3.DataSource = ds.Tables[0];
                    devChart3.DataBind();
                    
                    DevExpressLib.SetChartLegend(devChart3);
                    //DevExpressLib.SetChartDiagram(devChart3, true, 0, 0, 0, 0,null, null);
                    
                    DevExpressLib.SetChartDiagram(devChart3, false, 0, 0, 0, 0, null, "{V:n4}", false, false);
                }
            }



            //DataTable dt2 = new DataTable();

            //if (dtChart1 != null)
            //{
            //    string errMsg = String.Empty;

            //    oParamDic = new Dictionary<string, string>();
            //    oParamDic.Add("F_COMPCD", GetCompCD());
            //    oParamDic.Add("F_FACTCD", GetFactCD());
            //    oParamDic.Add("F_STDT", GetFromDt());
            //    oParamDic.Add("F_EDDT", GetToDt());

            //    oParamDic.Add("F_ITEMCD", GetItemCD());
            //    oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            //    oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);

            //    // 히스토그램을 구한다
            //    using (ANLSBiz biz = new ANLSBiz())
            //    {
            //        ds = biz.ANLS0101_3_New(oParamDic, out errMsg);
            //    }


            //    dt2 = ds.Tables[0].Copy();

            //    if (dt2.Rows.Count == 1)
            //        return;

            //    dt2.Columns.Add("F_STANDARD", typeof(Double));

            //    Decimal maxAxisY = Decimal.MinValue;
            //    Decimal minAxisY = Decimal.MaxValue;

            //    foreach (DataRow dtRow in dt2.Rows)
            //    {
            //        if (maxAxisY < Convert.ToDecimal(dtRow["F_DATA2"]))
            //            maxAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);

            //        if (minAxisY > Convert.ToDecimal(dtRow["F_DATA2"]))
            //            minAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);
            //    }

            //    maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
            //    minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;
                
            //    for (int i = 0; i < dt2.Rows.Count; i++)
            //    {
            //        if (i == 5 || i == 50 || i == 95)
            //        {
            //            dt2.Rows[i]["F_STANDARD"] = maxAxisY;
            //        }
            //    }

            //    // 데이타가 있는 경우
            //    if (bExistsDataSet(ds))
            //    {
            //        devChart3.Series.Clear();

            //        DevExpressLib.SetChartSplineSeries(devChart3, "", "F_DATA1", "F_DATA2", System.Drawing.Color.DarkGreen, null, DevExpress.XtraCharts.ScaleType.Qualitative);
            //        DevExpressLib.SetChartBarLineSeries(devChart3, "", "F_DATA1", "F_STANDARD", System.Drawing.Color.Khaki, 0.2);


            //        devChart3.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

            //        devChart3.DataSource = dt2;
            //        devChart3.DataBind();

            //        //DevExpressLib.SetCrosshairOptions(devChart1);
            //        DevExpressLib.SetChartDiagram(devChart3, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}", false, false);
            //    }
            //}
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

        #endregion
    }
}