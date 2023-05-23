using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.QCAN.Biz;
using SPC.ANLS.Biz;
using System.Text;
using DevExpress.XtraCharts;
using System.Drawing;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0102_WORK : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataSet dsGrid
        {
            get
            {
                return (DataSet)Session["ANLS0102_WORK"];
            }
            set
            {
                Session["ANLS0102_WORK"] = value;
            }
        }
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["ANLS0102_1"];
            }
            set
            {
                Session["ANLS0102_1"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["ANLS0102_3"];
            }
            set
            {
                Session["ANLS0102_3"] = value;
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

                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
                
                devGridWork.JSProperties["cpResultCode"] = "";
                devGridWork.JSProperties["cpResultMsg"] = "";
                
                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
                devChart2.JSProperties["cpFunction"] = "resizeTo";
                devChart2.JSProperties["cpChartWidth"] = "0";
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
            dtChart1 = null;
            dtChart3 = null;
            this.chk_reject.Checked = true;
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


        #region 공정리스트조회
        void QCD74_QCAN0104_LST()
        {
            string errMsg = String.Empty;

            using (QCANBiz biz = new QCANBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_MEAINSPCD", GetInspItemCD());
                ds = biz.QCD74_QCAN0104_LST(oParamDic, out errMsg);
            }

            devGridWork.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridWork.JSProperties["cpResultCode"] = "0";
                devGridWork.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGridWork.DataBind();
            }
        }
        #endregion


        #region 그리드
        void SetGrid(DataTable dt1, DataTable dt3)
        {
            Int32 idx = 0,
                    siryo = Convert.ToInt32(txtSIRYO.Text),
                    digit = Convert.ToInt32(txtFREEPOINT.Text);

            // 데이타 구성용 DataTable
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("공정", typeof(String));
            dtTemp.Columns.Add("검사일자", typeof(String));
            dtTemp.Columns.Add("검사시간", typeof(String));
            dtTemp.Columns.Add("Xbar", typeof(String));
            dtTemp.Columns.Add("R", typeof(String));
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
                dtNewRow["공정"] = String.Format("{0}({1})", dtRow1["F_WORKNM"].ToString(), dtRow1["F_SEQ"]);
                dtNewRow["검사일자"] = String.Format("{0}({1})", DateTime.Parse(dtRow1["F_WORKDATE"].ToString()).ToString("MM/dd"), dtRow1["F_TSERIALNO"]);
                dtNewRow["검사시간"] = dtRow1["F_WORKTIME"].ToString();
                dtNewRow["Xbar"] = Math.Round(Convert.ToDecimal(dtRow1["F_XBAR"].ToString()), digit + 1).ToString();
                dtNewRow["R"] = Math.Round(Convert.ToDecimal(dtRow1["F_XRANGE"].ToString()), digit + 1).ToString();
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
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "공정");

            devGrid.DataSource = dtPivotTable;
            devGrid.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        void setConstLine(DevExpress.XtraCharts.Web.WebChartControl devchart, string strNm, string strAgument)
        {
            ConstantLine cl = new ConstantLine(strNm, strAgument);
            cl.Color = Color.Black;
            cl.LineStyle.Thickness = 1;
            cl.Title.Alignment = ConstantLineTitleAlignment.Far;
            cl.ShowInLegend = false;
            //(devchart.Diagram as XYDiagram).AxisX.ConstantLines.Clear();
            (devchart.Diagram as XYDiagram).AxisX.ConstantLines.AddRange(new ConstantLine[] { cl });
        }

        Series setSeries(Series sr, int tickness, Color color)
        {
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = tickness;
            lineSeriesView.Color = color;
            lineSeriesView.LineMarkerOptions.Size = 1;

            sr.View = lineSeriesView;

            return sr;
        }

        #region devGridWork CustomCallback
        /// <summary>
        /// devGridWork_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridWork_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            QCD74_QCAN0104_LST();
        }
        #endregion   

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
                //(devChart1.Diagram as XYDiagram).AxisX.ConstantLines.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                foreach (DataRow dtRow in dtChart1.Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_XBAR"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_XBAR"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                //DevExpressLib.SetChartLineSeries(devChart1, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X" : "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                //DevExpressLib.SetChartLineSeries(devChart1, "CL", "F_IDX", "F_CLR", System.Drawing.Color.Green);
                //DevExpressLib.SetChartLineSeries(devChart1, "UCL", "F_IDX", "F_UCLR", System.Drawing.Color.Red);
                //DevExpressLib.SetChartLineSeries(devChart1, "LCL", "F_IDX", "F_LCLR", System.Drawing.Color.Red);
                //DevExpressLib.SetChartLineSeries(devChart1, "상한", "F_IDX", "F_MAX", System.Drawing.Color.Red);
                //DevExpressLib.SetChartLineSeries(devChart1, "하한", "F_IDX", "F_MIN", System.Drawing.Color.Red);


                Series series = new Series(Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X" : "xBar", ViewType.Line);
                Series series1 = new Series("CL", ViewType.Line);
                Series series2 = new Series("UCL", ViewType.Line);
                Series series3 = new Series("LCL", ViewType.Line);
                Series series4 = new Series("상한", ViewType.Line);
                Series series5 = new Series("하한", ViewType.Line);

                DataTable dt = dsGrid.Tables[1];

                foreach (DataRow dtRow1 in dt.Rows)
                {
                    string strAgument = "";
                    foreach (DataRow dtRow in dtChart1.Select(String.Format("F_WORKCD = '{0}'", dtRow1["F_WORKCD"])))
                    {
                        SeriesPoint sp = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_XBAR"]));
                        series.Points.Add(sp);

                        if (dtRow["F_CLR"].ToString() != "")
                        {
                            SeriesPoint sp1 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_CLR"]));
                            series1.Points.Add(sp1);
                        }

                        if (dtRow["F_UCLR"].ToString() != "")
                        {
                            SeriesPoint sp2 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_UCLR"]));
                            series2.Points.Add(sp2);
                        }

                        if (dtRow["F_LCLR"].ToString() != "")
                        {
                            SeriesPoint sp3 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_LCLR"]));
                            series3.Points.Add(sp3);
                        }

                        if (dtRow["F_MAX"].ToString() != "")
                        {
                            SeriesPoint sp4 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_MAX"]));
                            series4.Points.Add(sp4);
                        }

                        if (dtRow["F_MIN"].ToString() != "")
                        {
                            SeriesPoint sp5 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_MIN"]));
                            series5.Points.Add(sp5);
                        }


                        strAgument = dtRow["F_IDX"].ToString();
                    }


                    //setConstLine(devChart1, dtRow1["F_WORKNM"].ToString(), strAgument);                    
                }

                devChart1.Series.Add(setSeries(series1, 1, System.Drawing.Color.Green));
                //devChart1.Series.Add(setSeries(series2, 1, System.Drawing.Color.Red));
                //devChart1.Series.Add(setSeries(series3, 1, System.Drawing.Color.Red));
                devChart1.Series.Add(setSeries(series4, 1, System.Drawing.Color.Red));
                devChart1.Series.Add(setSeries(series5, 1, System.Drawing.Color.Red));
                devChart1.Series.Add(setSeries(series, 2, System.Drawing.Color.FromArgb(0, 102, 153)));



                DevExpressLib.SetCrosshairOptions(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart1, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X 관리도" : "X-Bar 관리도", false);


                foreach (DataRow dtRow1 in dt.Rows)
                {
                    string strAgument = "";
                    foreach (DataRow dtRow in dtChart1.Select(String.Format("F_WORKCD = '{0}'", dtRow1["F_WORKCD"])))
                    {
                        strAgument = dtRow["F_IDX"].ToString();
                    }
                    setConstLine(devChart1, dtRow1["F_WORKNM"].ToString(), strAgument);
                }

            }
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            string strWorknm = e.GetValue("F_WORKNM").ToString();

            if (strWorknm == "전체")
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
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

            //if (dtChart1 != null)
            //{
            //    devChart2.Series.Clear();

            //    Decimal maxAxisY = Decimal.MinValue;
            //    Decimal minAxisY = Decimal.MaxValue;

            //    string maxColumn = String.Empty;
            //    string minColumn = String.Empty;

            //    foreach (DataRow dtRow in dtChart1.Rows)
            //    {
            //        maxColumn = "F_UCLR";
            //        minColumn = "F_LCLR";

            //        if (!String.IsNullOrEmpty(dtRow[maxColumn].ToString()))
            //            maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow[maxColumn]));

            //        if (!String.IsNullOrEmpty(dtRow[minColumn].ToString()))
            //            minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow[minColumn]));

            //        if (maxAxisY < Convert.ToDecimal(dtRow["F_XRANGE"]))
            //            maxAxisY = Convert.ToDecimal(dtRow["F_XRANGE"]);

            //        if (minAxisY > Convert.ToDecimal(dtRow["F_XRANGE"]))
            //            minAxisY = Convert.ToDecimal(dtRow["F_XRANGE"]);
            //    }

            //    maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
            //    minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

            //    DevExpressLib.SetChartLineSeries(devChart2, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "Rs" : "R", "F_MEMBER", "F_XRANGE", System.Drawing.Color.FromArgb(0, 102, 153), 2);
            //    DevExpressLib.SetChartLineSeries(devChart2, "CL", "F_MEMBER", "F_CLR" , System.Drawing.Color.Green);
            //    DevExpressLib.SetChartLineSeries(devChart2, "UCL", "F_MEMBER", "F_UCLR" , System.Drawing.Color.Red);
            //    DevExpressLib.SetChartLineSeries(devChart2, "LCL", "F_MEMBER", "F_LCLR" , System.Drawing.Color.Red);

            //    devChart2.DataSource = dtChart1;
            //    devChart2.DataBind();

            //    DevExpressLib.SetCrosshairOptions(devChart2);
            //    DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
            //    DevExpressLib.SetChartTitle(devChart2, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "Rs 관리도" : "R 관리도", false);
            //}

            if (dtChart1 != null)
            {
                devChart2.Series.Clear();
                //(devChart2.Diagram as XYDiagram).AxisX.ConstantLines.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                string maxColumn = String.Empty;
                string minColumn = String.Empty;

                foreach (DataRow dtRow in dtChart1.Rows)
                {
                    maxColumn = "F_UCLR";
                    minColumn = "F_LCLR";

                    if (!String.IsNullOrEmpty(dtRow[maxColumn].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow[maxColumn]));

                    if (!String.IsNullOrEmpty(dtRow[minColumn].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow[minColumn]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_XRANGE"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_XRANGE"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_XRANGE"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_XRANGE"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                //DevExpressLib.SetChartLineSeries(devChart2, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X" : "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                //DevExpressLib.SetChartLineSeries(devChart2, "CL", "F_IDX", "F_CLR", System.Drawing.Color.Green);
                //DevExpressLib.SetChartLineSeries(devChart2, "UCL", "F_IDX", "F_UCLR", System.Drawing.Color.Red);
                //DevExpressLib.SetChartLineSeries(devChart2, "LCL", "F_IDX", "F_LCLR", System.Drawing.Color.Red);
                //DevExpressLib.SetChartLineSeries(devChart2, "상한", "F_IDX", "F_MAX", System.Drawing.Color.Red);
                //DevExpressLib.SetChartLineSeries(devChart2, "하한", "F_IDX", "F_MIN", System.Drawing.Color.Red);


                Series series = new Series(Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X" : "xBar", ViewType.Line);
                Series series1 = new Series("CL", ViewType.Line);
                Series series2 = new Series("UCL", ViewType.Line);
                Series series3 = new Series("LCL", ViewType.Line);
                Series series4 = new Series("상한", ViewType.Line);
                Series series5 = new Series("하한", ViewType.Line);

                DataTable dt = dsGrid.Tables[1];

                foreach (DataRow dtRow1 in dt.Rows)
                {
                    string strAgument = "";
                    foreach (DataRow dtRow in dtChart1.Select(String.Format("F_WORKCD = '{0}'", dtRow1["F_WORKCD"])))
                    {
                        SeriesPoint sp = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_XRANGE"]));
                        series.Points.Add(sp);

                        if (dtRow["F_CLR"].ToString() != "")
                        {
                            SeriesPoint sp1 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_CLR"]));
                            series1.Points.Add(sp1);
                        }

                        if (dtRow["F_UCLR"].ToString() != "")
                        {
                            SeriesPoint sp2 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_UCLR"]));
                            series2.Points.Add(sp2);
                        }

                        if (dtRow["F_LCLR"].ToString() != "")
                        {
                            SeriesPoint sp3 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_LCLR"]));
                            series3.Points.Add(sp3);
                        }

                        if (dtRow["F_MAX"].ToString() != "")
                        {
                            SeriesPoint sp4 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_MAX"]));
                            series4.Points.Add(sp4);
                        }

                        if (dtRow["F_MIN"].ToString() != "")
                        {
                            SeriesPoint sp5 = new SeriesPoint(dtRow["F_IDX"], Convert.ToDecimal(dtRow["F_MIN"]));
                            series5.Points.Add(sp5);
                        }

                        strAgument = dtRow["F_IDX"].ToString();
                    }


                    //setConstLine(devChart2, dtRow1["F_WORKNM"].ToString(), strAgument);                    
                }

                devChart2.Series.Add(setSeries(series1, 1, System.Drawing.Color.Green));
                devChart2.Series.Add(setSeries(series2, 1, System.Drawing.Color.Red));
                devChart2.Series.Add(setSeries(series3, 1, System.Drawing.Color.Red));
                //devChart2.Series.Add(setSeries(series4, 1, System.Drawing.Color.Red));
                //devChart2.Series.Add(setSeries(series5, 1, System.Drawing.Color.Red));
                devChart2.Series.Add(setSeries(series, 2, System.Drawing.Color.FromArgb(0, 102, 153)));



                DevExpressLib.SetCrosshairOptions(devChart2);
                DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart2, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "Rs 관리도" : "R 관리도", false);


                foreach (DataRow dtRow1 in dt.Rows)
                {
                    string strAgument = "";
                    foreach (DataRow dtRow in dtChart1.Select(String.Format("F_WORKCD = '{0}'", dtRow1["F_WORKCD"])))
                    {
                        strAgument = dtRow["F_IDX"].ToString();
                    }
                    setConstLine(devChart2, dtRow1["F_WORKNM"].ToString(), strAgument);
                }

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

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_MEAINSPCD", GetInspItemCD());
                oParamDic.Add("F_WORKCD", this.txtWORKCD.Value.ToString());
                oParamDic.Add("F_CNT", this.txtCNT.Value.ToString());
                ds = biz.ANLS0101_WORK_4(oParamDic, out errMsg);
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
                devGrid2.DataBind();
            }
        }
        #endregion     

        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;

            StringBuilder sb = null;

            devGrid.JSProperties["cpResultCode"] = "";
            devGrid.JSProperties["cpResultMsg"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_MEAINSPCD", GetInspItemCD());
                oParamDic.Add("F_WORKCD", this.txtWORKCD.Value.ToString());
                oParamDic.Add("F_SIRYO", txtSIRYO.Text);
                oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");
                oParamDic.Add("F_CNT", this.txtCNT.Value.ToString());
                ds = biz.ANLS0102_WORK_1(oParamDic, out errMsg);
            }

            dsGrid = ds;

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
                    ds = biz.ANLS0101_WORK_2(oParamDic, out errMsg);
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

                        sb = new StringBuilder();

                        dt3 = ds.Tables[0].Copy();
                    }
                }
            }

            // 모든 데이타가 구성되면 그리드를 그린다
            if (bExecute1)
            {
                SetGrid(dt1, dt3);

                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart1 = dt1.Copy();
                }
                else
                    dtChart1 = null;

                if (dt3.Rows.Count > 0)
                {
                    dtChart3 = dt3.Copy();
                }
                else
                    dtChart3 = null;
            }
        }

        #endregion
    }
}