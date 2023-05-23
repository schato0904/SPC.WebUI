using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Common.Library
{
    public class SPCChart
    {

        #region 생성자
        public SPCChart()
        {
        }
        #endregion

        #region 변수선언
        private DBHelper spcDB;
        #endregion

        #region 주어진 데이터로 히스토그램 생성 후 차트 컨트롤 반환
        public WebChartControl GetHistogram(WebChartControl c, DataSet ds)
        {
            if (ds == null || ds.Tables.Count < 3)
            {
                return null;
            }
            if (ds.Tables[2].Rows.Count < 1)
            {
                return null;
            }
            // 표준편차 = sqrt(편차제곱의 합 / 시료수)
            //ChartControl c = null;
            if (ds == null || ds.Tables.Count < 3) return null;
            // 2. min max 계산
            double MIN = 0d, MAX = 0d;
            DataTable dt = ds.Tables[2];
            MIN = dt.AsEnumerable().Min(x => double.Parse(x["YValue"].ToString()));
            MAX = dt.AsEnumerable().Max(x => double.Parse(x["YValue"].ToString()));
            // 3. 구간 수 계산 k = round(sqrt(n))
            int n = dt.Rows.Count;
            int k = Convert.ToInt32(Math.Round(Math.Sqrt(n)));
            // 4. 구간 폭 계산(측정단위의 정수배로 수치맺음) h = (max - min) / k 
            int pointLen = 3;//dt.AsEnumerable().Where(x => (double)x["YValue"] % 1 > 0).Max(x => (new Regex(@"\.(?<dp>.+$)").Match(x["YValue"].ToString()).Groups["dp"].Length));
            double h = Math.Round((MAX - MIN) / k, pointLen);
            // 5. 각 계급의 경계치 계산   1급하한 = min - (측정단위/2) 이후 급 경계는 (전단계 + h)
            List<double> xGrp = new List<double>();
            double t = MIN - (Math.Pow(0.1, pointLen) / 2);
            xGrp.Add(t);
            for (int i = 0; i < k; i++)
            {
                xGrp.Add((t += h));
            }
            // 5.5. 평균 및 표준편차 계산
            double mean = dt.AsEnumerable().Average(x => (double)x["YValue"]);
            double stdev = Math.Sqrt(dt.AsEnumerable().Select(x => Math.Pow((mean - (double)x["YValue"]), 2)).Sum() / dt.Rows.Count);

            // 6. 도수표 작성
            DataTable dtm = new DataTable("MainData");
            dtm.Columns.AddRange(new DataColumn[] { 
                new DataColumn("No", typeof(int))
                , new DataColumn("FromValue", typeof(double))
                , new DataColumn("ToValue", typeof(double))
                , new DataColumn("MidValue", typeof(double))
                , new DataColumn("ValueCount", typeof(int))
                , new DataColumn("NormalDist", typeof(double))
            });
            int imax = 1;
            for (int i = 1; i < k + 1; i++)
            {
                double fv = 0d, tv = 0d, mv = 0d, nd = 0d;
                int vcnt = 0;
                fv = xGrp[i - 1];
                tv = xGrp[i];
                mv = (fv + tv) / 2;
                vcnt = dt.AsEnumerable().Count(x => (double)x["YValue"] > fv && (double)x["YValue"] <= tv);
                nd = CTFMath.NORMDIST(mv, mean, stdev, false);
                dtm.Rows.Add(i, fv, tv, mv, vcnt, nd);
                imax = i;
            }
            dtm.Rows.Add(0, xGrp[0] - h, xGrp[0], (xGrp[0] - h + xGrp[0]) / 2, 0, 0);
            //dtm.Rows.Add(imax + 1, xGrp[imax], xGrp[imax] + h, (xGrp[imax] + h + xGrp[imax]) / 2, 0, 0);
            dtm.Rows.Add(imax + 1, xGrp[imax], xGrp[imax] + h, (xGrp[imax] + h + xGrp[imax]) / 2, dt.AsEnumerable().Count(x => (double)x["YValue"] > xGrp[imax]), 0);

            ds.Tables.Add(dtm);
            // 7. 히스토그램 작성
            //    USL, LSL, 센터라인 작성
            c = this.DrawHistogram(c, ds, h, pointLen);

            return c;
            /////////////////////////////////////////////////////////////
            //// 표준편차 = sqrt(편차제곱의 합 / 시료수)
            ////ChartControl c = null;
            //if (ds == null || ds.Tables.Count < 3)
            //{
            //    return null;
            //}
            //if (ds.Tables[2].Rows.Count < 1)
            //{
            //    return null;
            //}

            //// 2. min max 계산
            //double MIN = 0d, MAX = 0d;
            //DataTable dt = ds.Tables[2];
            //MIN = dt.AsEnumerable().Min(x => double.Parse(x["YValue"].ToString()));
            //MAX = dt.AsEnumerable().Max(x => double.Parse(x["YValue"].ToString()));
            //// 3. 구간 수 계산 k = round(sqrt(n))
            //int n = dt.Rows.Count;
            //int k = Convert.ToInt32(Math.Round(Math.Sqrt(n)));
            //// 4. 구간 폭 계산(측정단위의 정수배로 수치맺음) h = (max - min) / k 


            //int pointLen = 1;

            //try
            //{
            //    pointLen = dt.AsEnumerable().Where(x => (double)x["YValue"] % 1 > 0).Max(x => (new Regex(@"\.(?<dp>.+$)").Match(x["YValue"].ToString()).Groups["dp"].Length));
            //}
            //catch
            //{
            //    pointLen = 1;
            //}
            ////int pointLen = 3;

            //double h = Math.Round(Math.Round(Convert.ToDouble((MAX - MIN) / k), pointLen), pointLen);

            //if (h == 0.0)
            //{
            //    h = 3;
            //    //while (h == 0.0)
            //    //{
            //    //    pointLen += pointLen;
            //    //    h = Math.Round(Math.Round(Convert.ToDouble((MAX - MIN) / k), pointLen), pointLen);
            //    //}
            //}

            //// 5. 각 계급의 경계치 계산   1급하한 = min - (측정단위/2) 이후 급 경계는 (전단계 + h)
            //List<double> xGrp = new List<double>();
            //double t = MIN - (Math.Pow(0.1, pointLen) / 2);
            //xGrp.Add(t);
            //for (int i = 0; i < k; i++)
            //{
            //    xGrp.Add((t += h));
            //}

            //// 5.5. 평균 및 표준편차 계산
            //double mean = dt.AsEnumerable().Average(x => (double)x["YValue"]);
            //double stdev = Math.Sqrt(dt.AsEnumerable().Select(x => Math.Pow((mean - (double)x["YValue"]), 2)).Sum() / dt.Rows.Count);

            //// 6. 도수표 작성
            //DataTable dtm = new DataTable("MainData");
            //dtm.Columns.AddRange(new DataColumn[] { 
            //    new DataColumn("No", typeof(int))
            //    , new DataColumn("FromValue", typeof(double))
            //    , new DataColumn("ToValue", typeof(double))
            //    , new DataColumn("MidValue", typeof(double))
            //    , new DataColumn("ValueCount", typeof(int))
            //    , new DataColumn("NormalDist", typeof(double))
            //});
            //int imax = 1;
            //for (int i = 1; i < k + 1; i++)
            //{
            //    double fv = 0d, tv = 0d, mv = 0d, nd = 0d;
            //    int vcnt = 0;
            //    fv = xGrp[i - 1];
            //    tv = xGrp[i];
            //    mv = (fv + tv) / 2;
            //    vcnt = dt.AsEnumerable().Count(x => (double)x["YValue"] > fv && (double)x["YValue"] <= tv);
            //    nd = CTFMath.NORMDIST(mv, mean, stdev, false);
            //    dtm.Rows.Add(i, fv, tv, mv, vcnt, nd);
            //    imax = i;
            //}
            //dtm.Rows.Add(0, xGrp[0] - h, xGrp[0], (xGrp[0] - h + xGrp[0]) / 2, 0, 0);
            //dtm.Rows.Add(imax + 1, xGrp[imax], xGrp[imax] + h, (xGrp[imax] + h + xGrp[imax]) / 2, dt.AsEnumerable().Count(x => (double)x["YValue"] > xGrp[imax]), 0);

            //ds.Tables.Add(dtm);
            //// 7. 히스토그램 작성
            ////    USL, LSL, 센터라인 작성
            //c = this.DrawHistogram(c, ds, h, pointLen);

            //return c;
        }
        #endregion

        #region 히스토그램 그리기
        private WebChartControl DrawHistogram(WebChartControl c2, DataSet ds, double barWidth, int pointLength)
        {
            

            double CV = 0d, LOW = 0d, HIGH = 0d , MAX = 0d, MIN = 0d, STANDARD = 0d;
            if (ds != null && ds.Tables.Count > 2 && ds.Tables[1].AsEnumerable().Any())
            {
                DataRow dr = ds.Tables[1].Rows[0];
                LOW = double.TryParse(dr["LOW"].ToString(), out LOW) ? LOW : 0d;
                HIGH = double.TryParse(dr["HIGH"].ToString(), out HIGH) ? HIGH : 0d;
                MIN = double.TryParse(dr["F_MIN"].ToString(), out MIN) ? MIN : 0d;
                MAX = double.TryParse(dr["F_MAX"].ToString(), out MAX) ? MAX : 0d;
                STANDARD = double.TryParse(dr["F_STANDARD"].ToString(), out STANDARD) ? STANDARD : 0d;
                CV = (LOW + HIGH) / 2;
            }

            DataTable dt = ds.Tables["MainData"];
            double FROMVALUE = 0d, TOVALUE = 0d;
            FROMVALUE = dt.AsEnumerable().Min(x => double.Parse(x["FromValue"].ToString()));
            TOVALUE = dt.AsEnumerable().Max(x => double.Parse(x["ToValue"].ToString()));

            #region Spec 표시
            ConstantLine cl = new ConstantLine(MIN.ToString(), MIN);
            ConstantLine ch = new ConstantLine(MAX.ToString(), MAX);
            ConstantLine cc = new ConstantLine(STANDARD.ToString(), STANDARD);
            cl.Color = Color.Red;
            ch.Color = Color.Red;
            cc.Color = Color.Red;
            cl.LineStyle.Thickness = 1;
            ch.LineStyle.Thickness = 1;
            cc.LineStyle.Thickness = 1;
            cl.Title.Alignment = ConstantLineTitleAlignment.Far;
            ch.Title.Alignment = ConstantLineTitleAlignment.Far;
            cc.Title.Alignment = ConstantLineTitleAlignment.Far;
             
            #endregion

            c2.Series.Clear();
            c2.DataSource = dt;
            c2.DataBind();

            Series s = new Series("Count", ViewType.Bar);
            if (barWidth != 0.0)
            {
                (s.View as BarSeriesView).BarWidth = barWidth;
            }
            s.ArgumentDataMember = "MidValue";
            s.ValueDataMembers.AddRange("ValueCount");
            s.ArgumentScaleType = ScaleType.Numerical;
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = false;
            c2.Series.Add(s);
            (s.View as BarSeriesView).AxisY.Label.TextPattern = string.Format("{{A:F{0}}}", 0);
            (s.View as BarSeriesView).AxisX.Label.TextPattern = string.Format("{{A:F{0}}}", pointLength);

            s = new Series("NormalDist", ViewType.Spline);
            (s.View as SplineSeriesView).LineMarkerOptions.Size = 1;
            (s.View as SplineSeriesView).Color = Color.Red;
            s.ArgumentDataMember = "MidValue";
            s.ValueDataMembers.AddRange("NormalDist");
            s.ArgumentScaleType = ScaleType.Numerical;
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = false;
            s.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
            c2.Series.Add(s);
            SecondaryAxisY sy = new SecondaryAxisY();
            if ((c2.Diagram as XYDiagram).SecondaryAxesY.Count == 0)
                (c2.Diagram as XYDiagram).SecondaryAxesY.Add(sy);
            (s.View as SplineSeriesView).AxisY = (c2.Diagram as XYDiagram).SecondaryAxesY[0];

            (c2.Diagram as XYDiagram).AxisX.Label.Visible = true;
            (c2.Diagram as XYDiagram).AxisX.Tickmarks.Visible = true;
            (c2.Diagram as XYDiagram).AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            (c2.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            (c2.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
            (c2.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            (c2.Diagram as XYDiagram).AxisX.Tickmarks.MinorVisible = false;

            (c2.Diagram as XYDiagram).AxisY.Interlaced = true;
            (c2.Diagram as XYDiagram).SecondaryAxesY[0].Visibility = DevExpress.Utils.DefaultBoolean.False;
            (c2.Diagram as XYDiagram).AxisX.WholeRange.Auto = false;
            (c2.Diagram as XYDiagram).AxisX.WholeRange.AutoSideMargins = false;
            if (barWidth != 0.0)
            {
                (c2.Diagram as XYDiagram).AxisX.WholeRange.SideMarginsValue = barWidth * 2;
            }
            (c2.Diagram as XYDiagram).AxisX.WholeRange.SetMinMaxValues(Math.Min(Math.Min(FROMVALUE, LOW),MIN),Math.Max(Math.Max(TOVALUE, HIGH),MAX));
            (c2.Diagram as XYDiagram).AxisX.VisualRange.Auto = false;
            (c2.Diagram as XYDiagram).AxisX.VisualRange.AutoSideMargins = false;
            (c2.Diagram as XYDiagram).AxisX.VisualRange.SetMinMaxValues(Math.Min(Math.Min(FROMVALUE, LOW), MIN), Math.Max(Math.Max(TOVALUE, HIGH), MAX));
            if (barWidth != 0.0)
            {
                (c2.Diagram as XYDiagram).AxisX.VisualRange.SideMarginsValue = barWidth * 2;
            }

            (c2.Diagram as XYDiagram).AxisX.ConstantLines.Clear();
            (c2.Diagram as XYDiagram).AxisX.ConstantLines.AddRange(new ConstantLine[] { cl, ch, cc });
            cl.ShowInLegend = false;
            ch.ShowInLegend = false;
            cc.ShowInLegend = false;
            return c2;
            ////////////////////////////////////////////////////////
            //double CV = 0d, LOW = 0d, HIGH = 0d, MAX = 0d, MIN = 0d, STANDARD = 0d;
            //if (ds != null && ds.Tables.Count > 2 && ds.Tables[1].AsEnumerable().Any())
            //{
            //    DataRow dr = ds.Tables[1].Rows[0];
            //    LOW = double.TryParse(dr["LOW"].ToString(), out LOW) ? LOW : 0d;
            //    HIGH = double.TryParse(dr["HIGH"].ToString(), out HIGH) ? HIGH : 0d;
            //    MIN = double.TryParse(dr["F_MIN"].ToString(), out MIN) ? MIN : 0d;
            //    MAX = double.TryParse(dr["F_MAX"].ToString(), out MAX) ? MAX : 0d;
            //    STANDARD = double.TryParse(dr["F_STANDARD"].ToString(), out STANDARD) ? STANDARD : 0d;
            //    CV = (LOW + HIGH) / 2;
            //}

            //DataTable dt = ds.Tables["MainData"];
            //DataTable dt2 = ds.Tables["Table1"];
            //DataTable dt3 = new DataTable();
            //for (int i = 0; i < 3; i++)
            //{
            //    dt3.Rows.Add();
            //}

            //dt3.Columns.AddRange(new DataColumn[] {
            //    new DataColumn("STNX", typeof(double))
            //    ,new DataColumn("STNY", typeof(int))
            //});
            
            //dt3.Rows[0][0] = dt2.Rows[0]["F_MAX"];
            //dt3.Rows[1][0] = dt2.Rows[0]["F_MIN"];
            //dt3.Rows[2][0] = dt2.Rows[0]["F_STANDARD"];

            //double STNY = 0d;
            //STNY = dt.AsEnumerable().Max(x => double.Parse(x["ValueCount"].ToString()));
            //dt3.Rows[0][1] = STNY;
            //dt3.Rows[1][1] = STNY;
            //dt3.Rows[2][1] = STNY;
            
            //double FROMVALUE = 0d, TOVALUE = 0d;
            //FROMVALUE = dt.AsEnumerable().Min(x => double.Parse(x["FromValue"].ToString()));
            //TOVALUE = dt.AsEnumerable().Max(x => double.Parse(x["ToValue"].ToString()));

            //ds.Tables.Add(dt3);

            //c.Series.Clear();
            ////c.DataSource = ds;
            ////c.DataBind();
            
            //Series s = new Series("ValueCount", ViewType.Bar);
            //(s.View as BarSeriesView).BarWidth = barWidth;

            //s.DataSource = ds.Tables["MainData"];
            //s.ArgumentDataMember = "MidValue";
            //s.ValueDataMembers.AddRange("ValueCount");
            //s.ArgumentScaleType = ScaleType.Numerical;
            //s.ValueScaleType = ScaleType.Numerical;
            //s.ShowInLegend = false;
            //c.Series.Add(s);
            
            //(s.View as BarSeriesView).AxisY.Label.TextPattern = string.Format("{{A:F{0}}}", 0);
            //(s.View as BarSeriesView).AxisX.Label.TextPattern = string.Format("{{A:F{0}}}", pointLength);
            
            //s = new Series("Standard", ViewType.Bar);
            //(s.View as BarSeriesView).BarWidth = barWidth * 0.3;

            //s.DataSource = dt3;
            //s.ArgumentDataMember = "STNX";
            //s.ValueDataMembers.AddRange("STNY");
            //s.ArgumentScaleType = ScaleType.Numerical;
            //s.ValueScaleType = ScaleType.Numerical;
            //s.ShowInLegend = false;
            //c.Series.Add(s);
            
            //s = new Series("NormalDistribution", ViewType.Spline);
            //(s.View as SplineSeriesView).Color = Color.Red;

            //s.DataSource = ds.Tables["MainData"];

            //(s.View as SplineSeriesView).LineMarkerOptions.Size = 1;
            //s.ArgumentDataMember = "MidValue";
            //s.ValueDataMembers.AddRange("NormalDist");
            //s.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
            //s.ArgumentScaleType = ScaleType.Numerical;
            //s.ValueScaleType = ScaleType.Numerical;
            //s.ShowInLegend = false;
            //c.Series.Add(s);

            //SecondaryAxisY sy = new SecondaryAxisY();
            //if ((c.Diagram as XYDiagram).SecondaryAxesY.Count == 0)
            //    (c.Diagram as XYDiagram).SecondaryAxesY.Add(sy);
            //(s.View as SplineSeriesView).AxisY = (c.Diagram as XYDiagram).SecondaryAxesY[0];

            //c.DataBind();

            //(c.Diagram as XYDiagram).AxisX.Label.Visible = true;
            //(c.Diagram as XYDiagram).AxisX.Tickmarks.Visible = true;
            //(c.Diagram as XYDiagram).AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            //(c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            //(c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
            //(c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            //(c.Diagram as XYDiagram).AxisX.Tickmarks.MinorVisible = false;

            //(c.Diagram as XYDiagram).AxisY.Interlaced = true;
            //(c.Diagram as XYDiagram).SecondaryAxesY[0].Visibility = DevExpress.Utils.DefaultBoolean.False;
            //(c.Diagram as XYDiagram).AxisX.WholeRange.Auto = false;
            //(c.Diagram as XYDiagram).AxisX.WholeRange.AutoSideMargins = false;
            //(c.Diagram as XYDiagram).AxisX.WholeRange.SideMarginsValue = barWidth * 2;
            //(c.Diagram as XYDiagram).AxisX.WholeRange.SetMinMaxValues(Math.Min(Math.Min(FROMVALUE, LOW), MIN), Math.Max(Math.Max(TOVALUE, HIGH), MAX));
            //(c.Diagram as XYDiagram).AxisX.VisualRange.Auto = false;
            //(c.Diagram as XYDiagram).AxisX.VisualRange.AutoSideMargins = false;
            //(c.Diagram as XYDiagram).AxisX.VisualRange.SetMinMaxValues(Math.Min(Math.Min(FROMVALUE, LOW), MIN), Math.Max(Math.Max(TOVALUE, HIGH), MAX));
            //(c.Diagram as XYDiagram).AxisX.VisualRange.SideMarginsValue = barWidth * 2;



            //return c;
        }
        #endregion





    }
}
