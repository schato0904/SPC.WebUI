using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraCharts;
using SPC.MNTR.Biz;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.Common.Report
{
    public partial class ProcessQualityReportSub2 : DevExpress.XtraReports.UI.XtraReport
    {
        Functions.Dates fnDates = new Functions.Dates();
        DataTable dtTable1 = new DataTable();
        DataTable dtTable2 = new DataTable();
        DataTable dtTable3 = new DataTable();

        Series series = null;
        SideBySideBarSeriesView barSeriesView = null;
        BarSeriesLabel barSeriesLabel = null;

        public ProcessQualityReportSub2(Int32 Year, Int32 WeekOfYear, string F_STDT, string F_EDDT, string F_COMPCD, string F_FACTCD, string F_LANGTP)
        {
            InitializeComponent();

            this.Year.Value = Year;
            this.WeekOfYear.Value = WeekOfYear;
            this.StartDate.Value = F_STDT;
            this.EndDate.Value = F_EDDT;

            this.F_COMPCD.Value = F_COMPCD;
            this.F_FACTCD.Value = F_FACTCD;
            this.F_LANGTP.Value = F_LANGTP;

            // 공정불량현황 차트 생성
            Chart1_Rendoring(F_COMPCD, F_FACTCD, F_LANGTP);

            // 공정능력미달 차트 생성
            Chart2_Rendoring(F_COMPCD, F_FACTCD, F_LANGTP);

            // DATA수신현황 차트 생성
            Chart3_Rendoring(F_COMPCD, F_FACTCD, F_LANGTP);
        }

        #region 공정불량현황 차트 생성
        void Chart1_Rendoring(string F_COMPCD, string F_FACTCD, string F_LANGTP)
        {
            dtTable1.Columns.Add("F_WEEKDAYS", typeof(string));
            dtTable1.Columns.Add("F_5NGCNT", typeof(Int32));
            dtTable1.Columns.Add("F_5PPM", typeof(Int32));
            dtTable1.Columns.Add("F_5LABEL", typeof(string));
            dtTable1.Columns.Add("F_TNGCNT", typeof(Int32));
            dtTable1.Columns.Add("F_TPPM", typeof(Int32));
            dtTable1.Columns.Add("F_TLABEL", typeof(string));
            dtTable1.Columns.Add("F_AXISXLABEL", typeof(string));

            Dictionary<string, string> oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_SDATE", this.StartDate.Value.ToString());
            oParamDic.Add("S_COMPCD", F_COMPCD);
            oParamDic.Add("S_FACTCD", F_FACTCD);
            oParamDic.Add("F_LANGTP", F_LANGTP);

            DataSet ds = null;
            string errMsg = String.Empty;

            using (MNTRBiz biz = new MNTRBiz())
            {
                ds = biz.MONITORING_MNTR0907_1_5WORST_CHART(oParamDic, out errMsg);
            }

            int nDecrease = -14;

            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                dtTable1.Rows.Add(
                    dtRow["F_WEEKDAYS"],
                    dtRow["F_NGCNT"],
                    dtRow["F_PPM"],
                    String.Format("{1}건{0}{2}PPM",
                        Environment.NewLine,
                        String.Format("{0:#,##0}", dtRow["F_NGCNT"]),
                        String.Format("{0:#,##0}", dtRow["F_PPM"])),
                    0,
                    0,
                    String.Empty,
                    String.Format("{0}월 {1}주",
                        Convert.ToDateTime(this.StartDate.Value).AddDays(nDecrease).Month,
                        fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(nDecrease))
                        )
                    );

                nDecrease = nDecrease + 7;
            }

            using (MNTRBiz biz = new MNTRBiz())
            {
                ds = biz.MONITORING_MNTR0907_1_TWORST_CHART(oParamDic, out errMsg);
            }

            foreach (DataRow dr in dtTable1.Rows)
            {
                foreach (DataRow dtRow in ds.Tables[0].Select(String.Format("F_WEEKDAYS='{0}'", dr["F_WEEKDAYS"])))
                {
                    dr["F_TNGCNT"] = dtRow["F_NGCNT"];
                    dr["F_TPPM"] = dtRow["F_PPM"];
                    dr["F_TLABEL"] = String.Format("{1}건{0}{2}PPM",
                                        Environment.NewLine,
                                        String.Format("{0:#,##0}", dtRow["F_NGCNT"]),
                                        String.Format("{0:#,##0}", dtRow["F_PPM"]));
                }
            }

            xrChart1.DataSource = dtTable1;

            series = new Series("Worst 5사", ViewType.Bar) { ArgumentDataMember = "F_WEEKDAYS" };
            series.ValueDataMembers.AddRange(new string[] { "F_5NGCNT" });
            barSeriesView = new SideBySideBarSeriesView();
            barSeriesView.BarWidth = 0.5;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Hatch;
            series.View = barSeriesView;
            barSeriesLabel = (BarSeriesLabel)series.Label;
            barSeriesLabel.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            barSeriesLabel.Position = BarSeriesLabelPosition.Auto;
            xrChart1.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;

            series = new Series("전 협력사", ViewType.Bar) { ArgumentDataMember = "F_WEEKDAYS" };
            series.ValueDataMembers.AddRange(new string[] { "F_TNGCNT" });
            barSeriesView = new SideBySideBarSeriesView();
            barSeriesView.BarWidth = 0.5;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Solid;
            series.View = barSeriesView;
            barSeriesLabel = (BarSeriesLabel)series.Label;
            barSeriesLabel.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            barSeriesLabel.Position = BarSeriesLabelPosition.Auto;
            xrChart1.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;

            XYDiagram diagram = (XYDiagram)xrChart1.Diagram;

            DataTable dtScaleBreak = new DataTable();
            dtScaleBreak.Columns.Add("AxisY", typeof(Int32));

            foreach (DataRow dtRow in dtTable1.Rows)
            {
                dtScaleBreak.Rows.Add(dtRow["F_TNGCNT"]);
            }

            Int32 prevAxisY = 0;
            Int32 currAxisY = 0;
            Int32 cMaxAxisY = 0;
            Int32 cMinAxisY = 0;
            Int32 scleAxisY = 0;

            foreach (DataRow dtRow in dtScaleBreak.Select("", "AxisY ASC"))
            {
                currAxisY = Convert.ToInt32(dtRow["AxisY"]);

                if (prevAxisY > 0 && (currAxisY / 3) > prevAxisY)
                {
                    cMaxAxisY = Math.Max(prevAxisY, currAxisY);
                    cMinAxisY = Math.Min(prevAxisY, currAxisY);

                    scleAxisY = Convert.ToInt32("1".PadRight((cMaxAxisY - cMinAxisY).ToString().Length - 1, '0'));

                    diagram.AxisY.ScaleBreaks.Add(new ScaleBreak(prevAxisY.ToString(), cMinAxisY + scleAxisY, cMaxAxisY - scleAxisY));
                }

                prevAxisY = currAxisY;
            }
        }
        #endregion

        #region 공정능력미달 차트 생성
        void Chart2_Rendoring(string F_COMPCD, string F_FACTCD, string F_LANGTP)
        {
            dtTable2.Columns.Add("F_WEEKDAYS", typeof(string));
            dtTable2.Columns.Add("F_5NCNT", typeof(Int32));
            dtTable2.Columns.Add("F_5TCNT", typeof(Int32));
            dtTable2.Columns.Add("F_5RATE", typeof(Int32));
            dtTable2.Columns.Add("F_5LABEL", typeof(string));
            dtTable2.Columns.Add("F_TNCNT", typeof(Int32));
            dtTable2.Columns.Add("F_TTCNT", typeof(Int32));
            dtTable2.Columns.Add("F_TRATE", typeof(Int32));
            dtTable2.Columns.Add("F_TLABEL", typeof(string));
            dtTable2.Columns.Add("F_AXISXLABEL", typeof(string));

            Dictionary<string, string> oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_SDATE", this.StartDate.Value.ToString());
            oParamDic.Add("S_COMPCD", F_COMPCD);
            oParamDic.Add("S_FACTCD", F_FACTCD);
            oParamDic.Add("F_LANGTP", F_LANGTP);

            DataSet ds = null;
            string errMsg = String.Empty;

            using (MNTRBiz biz = new MNTRBiz())
            {
                ds = biz.MONITORING_MNTR0907_2_5WORST_CHART(oParamDic, out errMsg);
            }

            int nDecrease = -14;

            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                dtTable2.Rows.Add(
                    dtRow["F_WEEKDAYS"],
                    dtRow["F_NCNT"],
                    dtRow["F_TCNT"],
                    dtRow["F_RATE"],
                    String.Format("{1}건{0}{2}건({3}%)",
                        Environment.NewLine,
                        String.Format("{0:#,##0}", dtRow["F_TCNT"]),
                        String.Format("{0:#,##0}", dtRow["F_NCNT"]),
                        dtRow["F_RATE"]),
                    0,
                    0,
                    0.0,
                    String.Empty,
                    String.Format("{0}월 {1}주",
                        Convert.ToDateTime(this.StartDate.Value).AddDays(nDecrease).Month,
                        fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(nDecrease))
                        )
                    );

                nDecrease = nDecrease + 7;
            }

            using (MNTRBiz biz = new MNTRBiz())
            {
                ds = biz.MONITORING_MNTR0907_2_TWORST_CHART(oParamDic, out errMsg);
            }

            foreach (DataRow dr in dtTable2.Rows)
            {
                foreach (DataRow dtRow in ds.Tables[0].Select(String.Format("F_WEEKDAYS='{0}'", dr["F_WEEKDAYS"])))
                {
                    dr["F_TNCNT"] = dtRow["F_NCNT"];
                    dr["F_TTCNT"] = dtRow["F_TCNT"];
                    dr["F_TRATE"] = dtRow["F_RATE"];
                    dr["F_TLABEL"] = String.Format("{1}건{0}{2}건({3}%)",
                                        Environment.NewLine,
                                        String.Format("{0:#,##0}", dtRow["F_TCNT"]),
                                        String.Format("{0:#,##0}", dtRow["F_NCNT"]),
                                        dtRow["F_RATE"]);
                }
            }

            xrChart2.DataSource = dtTable2;

            series = new Series("Worst 5사", ViewType.Bar) { ArgumentDataMember = "F_WEEKDAYS" };
            series.ValueDataMembers.AddRange(new string[] { "F_5NCNT" });
            barSeriesView = new SideBySideBarSeriesView();
            barSeriesView.BarWidth = 0.5;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Hatch;
            series.View = barSeriesView;
            barSeriesLabel = (BarSeriesLabel)series.Label;
            barSeriesLabel.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            barSeriesLabel.Position = BarSeriesLabelPosition.Auto;
            xrChart2.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;

            series = new Series("전 협력사", ViewType.Bar) { ArgumentDataMember = "F_WEEKDAYS" };
            series.ValueDataMembers.AddRange(new string[] { "F_TNCNT" });
            barSeriesView = new SideBySideBarSeriesView();
            barSeriesView.BarWidth = 0.5;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Solid;
            series.View = barSeriesView;
            barSeriesLabel = (BarSeriesLabel)series.Label;
            barSeriesLabel.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            barSeriesLabel.Position = BarSeriesLabelPosition.Auto;
            xrChart2.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;

            XYDiagram diagram = (XYDiagram)xrChart2.Diagram;

            DataTable dtScaleBreak = new DataTable();
            dtScaleBreak.Columns.Add("AxisY", typeof(Int32));

            foreach (DataRow dtRow in dtTable2.Rows)
            {
                dtScaleBreak.Rows.Add(dtRow["F_TNCNT"]);
            }

            Int32 prevAxisY = 0;
            Int32 currAxisY = 0;
            Int32 cMaxAxisY = 0;
            Int32 cMinAxisY = 0;
            Int32 scleAxisY = 0;

            foreach (DataRow dtRow in dtScaleBreak.Select("", "AxisY ASC"))
            {
                currAxisY = Convert.ToInt32(dtRow["AxisY"]);

                if (prevAxisY > 0 && (currAxisY / 3) > prevAxisY)
                {
                    cMaxAxisY = Math.Max(prevAxisY, currAxisY);
                    cMinAxisY = Math.Min(prevAxisY, currAxisY);

                    scleAxisY = Convert.ToInt32("1".PadRight((cMaxAxisY - cMinAxisY).ToString().Length, '0'));

                    diagram.AxisY.ScaleBreaks.Add(new ScaleBreak("scale break", cMinAxisY + scleAxisY, cMaxAxisY - scleAxisY));
                }

                prevAxisY = currAxisY;
            }
        }
        #endregion

        #region DATA수신현황 차트 생성
        void Chart3_Rendoring(string F_COMPCD, string F_FACTCD, string F_LANGTP)
        {
            dtTable3.Columns.Add("F_WEEKDAYS", typeof(string));
            dtTable3.Columns.Add("F_5NCNT", typeof(Int32));
            dtTable3.Columns.Add("F_5TCNT", typeof(Int32));
            dtTable3.Columns.Add("F_5RATE", typeof(Int32));
            dtTable3.Columns.Add("F_5LABEL", typeof(string));
            dtTable3.Columns.Add("F_TNCNT", typeof(Int32));
            dtTable3.Columns.Add("F_TTCNT", typeof(Int32));
            dtTable3.Columns.Add("F_TRATE", typeof(Int32));
            dtTable3.Columns.Add("F_TLABEL", typeof(string));
            dtTable3.Columns.Add("F_AXISXLABEL", typeof(string));

            Dictionary<string, string> oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_SDATE", this.StartDate.Value.ToString());
            oParamDic.Add("S_COMPCD", F_COMPCD);
            oParamDic.Add("S_FACTCD", F_FACTCD);
            oParamDic.Add("F_LANGTP", F_LANGTP);

            DataSet ds = null;
            string errMsg = String.Empty;

            using (MNTRBiz biz = new MNTRBiz())
            {
                ds = biz.MONITORING_MNTR0907_3_5WORST_CHART(oParamDic, out errMsg);
            }

            int nDecrease = -14;

            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                dtTable3.Rows.Add(
                    dtRow["F_WEEKDAYS"],
                    dtRow["F_CHECKCNT"],
                    dtRow["F_TOTALCNT"],
                    dtRow["F_RATE"],
                    String.Format("{1}건{0}{2}건({3}%)",
                        Environment.NewLine,
                        String.Format("{0:#,##0}", dtRow["F_TOTALCNT"]),
                        String.Format("{0:#,##0}", dtRow["F_CHECKCNT"]),
                        dtRow["F_RATE"]),
                    0,
                    0,
                    0.0,
                    String.Empty,
                    String.Format("{0}월 {1}주",
                        Convert.ToDateTime(this.StartDate.Value).AddDays(nDecrease).Month,
                        fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(nDecrease))
                        )
                    );

                nDecrease = nDecrease + 7;
            }

            using (MNTRBiz biz = new MNTRBiz())
            {
                ds = biz.MONITORING_MNTR0907_3_TWORST_CHART(oParamDic, out errMsg);
            }

            foreach (DataRow dr in dtTable3.Rows)
            {
                foreach (DataRow dtRow in ds.Tables[0].Select(String.Format("F_WEEKDAYS='{0}'", dr["F_WEEKDAYS"])))
                {
                    dr["F_TNCNT"] = dtRow["F_CHECKCNT"];
                    dr["F_TTCNT"] = dtRow["F_TOTALCNT"];
                    dr["F_TRATE"] = dtRow["F_RATE"];
                    dr["F_TLABEL"] = String.Format("{1}건{0}{2}건({3}%)",
                                        Environment.NewLine,
                                        String.Format("{0:#,##0}", dtRow["F_TOTALCNT"]),
                                        String.Format("{0:#,##0}", dtRow["F_CHECKCNT"]),
                                        dtRow["F_RATE"]);
                }
            }

            xrChart3.DataSource = dtTable3;

            series = new Series("Worst 5사", ViewType.Bar) { ArgumentDataMember = "F_WEEKDAYS" };
            series.ValueDataMembers.AddRange(new string[] { "F_5NCNT" });
            barSeriesView = new SideBySideBarSeriesView();
            barSeriesView.BarWidth = 0.5;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Hatch;
            series.View = barSeriesView;
            barSeriesLabel = (BarSeriesLabel)series.Label;
            barSeriesLabel.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            barSeriesLabel.Position = BarSeriesLabelPosition.Auto;
            xrChart3.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;

            series = new Series("전 협력사", ViewType.Bar) { ArgumentDataMember = "F_WEEKDAYS" };
            series.ValueDataMembers.AddRange(new string[] { "F_TNCNT" });
            barSeriesView = new SideBySideBarSeriesView();
            barSeriesView.BarWidth = 0.5;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Solid;
            series.View = barSeriesView;
            barSeriesLabel = (BarSeriesLabel)series.Label;
            barSeriesLabel.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            barSeriesLabel.Position = BarSeriesLabelPosition.Auto;
            xrChart3.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;

            XYDiagram diagram = (XYDiagram)xrChart2.Diagram;

            DataTable dtScaleBreak = new DataTable();
            dtScaleBreak.Columns.Add("AxisY", typeof(Int32));

            foreach (DataRow dtRow in dtTable3.Rows)
            {
                dtScaleBreak.Rows.Add(dtRow["F_TNCNT"]);
            }

            Int32 prevAxisY = 0;
            Int32 currAxisY = 0;
            Int32 cMaxAxisY = 0;
            Int32 cMinAxisY = 0;
            Int32 scleAxisY = 0;

            foreach (DataRow dtRow in dtScaleBreak.Select("", "AxisY ASC"))
            {
                currAxisY = Convert.ToInt32(dtRow["AxisY"]);

                if (prevAxisY > 0 && (currAxisY / 3) > prevAxisY)
                {
                    cMaxAxisY = Math.Max(prevAxisY, currAxisY);
                    cMinAxisY = Math.Min(prevAxisY, currAxisY);

                    scleAxisY = Convert.ToInt32("1".PadRight((cMaxAxisY - cMinAxisY).ToString().Length, '0'));

                    diagram.AxisY.ScaleBreaks.Add(new ScaleBreak("scale break", cMinAxisY + scleAxisY, cMaxAxisY - scleAxisY));
                }

                prevAxisY = currAxisY;
            }
        }
        #endregion

        #region xrChart1_CustomDrawAxisLabel
        /// <summary>
        /// xrChart1_CustomDrawAxisLabel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomDrawAxisLabelEventArgs</param>
        private void xrChart1_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            AxisBase axis = e.Item.Axis;
            if (axis is AxisX)
            {
                foreach (DataRow dtRow in dtTable1.Select(String.Format("F_WEEKDAYS='{0}'", e.Item.AxisValue)))
                {
                    e.Item.Text = dtRow["F_AXISXLABEL"].ToString();
                }
            }
        }
        #endregion

        #region xrChart1_CustomDrawSeriesPoint
        /// <summary>
        /// xrChart1_CustomDrawSeriesPoint
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomDrawSeriesPointEventArgs</param>
        private void xrChart1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            foreach (DataRow dtRow in dtTable1.Select(String.Format("F_WEEKDAYS='{0}'", e.SeriesPoint.Argument)))
            {
                if (e.Series.ValueDataMembers[0].Equals("F_5NGCNT"))
                    e.LabelText = dtRow["F_5LABEL"].ToString();
                else if (e.Series.ValueDataMembers[0].Equals("F_TNGCNT"))
                    e.LabelText = dtRow["F_TLABEL"].ToString();
            }
        }
        #endregion

        #region xrChart2_CustomDrawAxisLabel
        /// <summary>
        /// xrChart2_CustomDrawAxisLabel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomDrawAxisLabelEventArgs</param>
        private void xrChart2_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            AxisBase axis = e.Item.Axis;
            if (axis is AxisX)
            {
                foreach (DataRow dtRow in dtTable2.Select(String.Format("F_WEEKDAYS='{0}'", e.Item.AxisValue)))
                {
                    e.Item.Text = dtRow["F_AXISXLABEL"].ToString();
                }
            }
        }
        #endregion

        #region xrChart2_CustomDrawSeriesPoint
        /// <summary>
        /// xrChart2_CustomDrawSeriesPoint
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomDrawSeriesPointEventArgs</param>
        private void xrChart2_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            foreach (DataRow dtRow in dtTable2.Select(String.Format("F_WEEKDAYS='{0}'", e.SeriesPoint.Argument)))
            {
                if (e.Series.ValueDataMembers[0].Equals("F_5NCNT"))
                    e.LabelText = dtRow["F_5LABEL"].ToString();
                else if (e.Series.ValueDataMembers[0].Equals("F_TNCNT"))
                    e.LabelText = dtRow["F_TLABEL"].ToString();
            }
        }
        #endregion

        #region xrChart3_CustomDrawAxisLabel
        /// <summary>
        /// xrChart3_CustomDrawAxisLabel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomDrawAxisLabelEventArgs</param>
        private void xrChart3_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            AxisBase axis = e.Item.Axis;
            if (axis is AxisX)
            {
                foreach (DataRow dtRow in dtTable3.Select(String.Format("F_WEEKDAYS='{0}'", e.Item.AxisValue)))
                {
                    e.Item.Text = dtRow["F_AXISXLABEL"].ToString();
                }
            }
        }
        #endregion

        #region xrChart3_CustomDrawSeriesPoint
        /// <summary>
        /// xrChart3_CustomDrawSeriesPoint
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomDrawSeriesPointEventArgs</param>
        private void xrChart3_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            foreach (DataRow dtRow in dtTable3.Select(String.Format("F_WEEKDAYS='{0}'", e.SeriesPoint.Argument)))
            {
                if (e.Series.ValueDataMembers[0].Equals("F_5NCNT"))
                    e.LabelText = dtRow["F_5LABEL"].ToString();
                else if (e.Series.ValueDataMembers[0].Equals("F_TNCNT"))
                    e.LabelText = dtRow["F_TLABEL"].ToString();
            }
        }
        #endregion
    }
}
