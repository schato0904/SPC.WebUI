using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Data;
using System.Web.Services;
using DevExpress.XtraCharts;
using SPC.WebUI.Common;

namespace SPC.WebUI.API.Common.Chart
{
    /// <summary>
    /// GetChartImage의 요약 설명입니다.
    /// </summary>
    public class GetChartImage : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string tbl = context.Request.QueryString.Get("tbl");
            string type = context.Request.QueryString.Get("type");
            Int32 w = Convert.ToInt32(context.Request.QueryString.Get("w"));
            Int32 h = Convert.ToInt32(context.Request.QueryString.Get("h"));
            string db = context.Request.QueryString.Get("db");

            if (!String.IsNullOrEmpty(type) && context.Session[tbl] != null)
            {
                Int32 siryo = Convert.ToInt32(context.Request.QueryString.Get("siryo"));
                string calc = context.Request.QueryString.Get("calc");
                string sMemberArg = context.Request.QueryString.Get("memb");

                ChartControl chartControl = new ChartControl() { Width = w, Height = h };
                DataTable dtChart = (DataTable)context.Session[tbl];

                switch (type)
                {
                    case "xbar":
                        XBarChart(chartControl, dtChart, siryo, calc, sMemberArg, db);
                        break;
                    case "allxbar":
                        AllXBarChart(chartControl, dtChart, siryo, calc, sMemberArg, db);
                        break;
                    case "r":
                        RChart(chartControl, dtChart, siryo, calc, sMemberArg, db);
                        break;
                    default:
                        break;
                }

                using (MemoryStream memoryImage = new MemoryStream())
                {
                    chartControl.ExportToImage(memoryImage, ImageFormat.Png);
                    memoryImage.Seek(0, SeekOrigin.Begin);

                    context.Response.ContentType = "image/png";
                    context.Response.BinaryWrite(memoryImage.ToArray());
                }
            }
            else
            {
                System.Drawing.Image blankImg = System.Drawing.Image.FromFile(context.Server.MapPath("~/Resources/images/blank.png"));

                using (MemoryStream memoryImage = new MemoryStream())
                {
                    blankImg.Save(memoryImage, ImageFormat.Png);
                    memoryImage.Seek(0, SeekOrigin.Begin);

                    context.Response.ContentType = "image/png";
                    context.Response.BinaryWrite(memoryImage.ToArray());
                }
            }
        }

        void XBarChart(ChartControl chartControl, DataTable dtChart, Int32 siryo, string calc, string sMemberArg, string dbType)
        {
            chartControl.Series.Clear();
            
            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            foreach (DataRow dtRow in dtChart.Rows)
            {
                if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                    maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                    minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));
            }

            maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
            minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

            DevExpressLib.SetImgChartLineSeries(chartControl, siryo <= 1 ? "X" : "xBar", sMemberArg, "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2);
            DevExpressLib.SetImgChartLineSeries(chartControl, "CL", sMemberArg, calc.Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
            DevExpressLib.SetImgChartLineSeries(chartControl, "UCL", sMemberArg, calc.Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
            DevExpressLib.SetImgChartLineSeries(chartControl, "LCL", sMemberArg, calc.Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
            DevExpressLib.SetImgChartLineSeries(chartControl, "상한", sMemberArg, "F_MAX", System.Drawing.Color.Red);
            DevExpressLib.SetImgChartLineSeries(chartControl, "하한", sMemberArg, "F_MIN", System.Drawing.Color.Red);

            chartControl.DataSource = dtChart;

            DevExpressLib.SetImgCrosshairOptions(chartControl);

            if (dbType.Equals("03"))
            {
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, null, null, null, "{V:n4}");
            }
            else
            {
                chartControl.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
            }

            DevExpressLib.SetChartTitle(chartControl, siryo <= 1 ? "X 관리도" : "X-Bar 관리도", false);

            DevExpressLib.SetImgChartRuler(chartControl, dbType.Equals("03"), true);

            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            
        }

        void AllXBarChart(ChartControl chartControl, DataTable dtChart, Int32 siryo, string calc, string sMemberArg, string dbType)
        {
            chartControl.Series.Clear();

            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            foreach (DataRow dtRow in dtChart.Rows)
            {
                if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                    maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                    minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));
            }

            maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
            minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

            DevExpressLib.SetImgChartPointSeries(chartControl, siryo <= 1 ? "X" : "xBar", sMemberArg, "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2);
            //DevExpressLib.SetImgChartLineSeries(chartControl, siryo <= 1 ? "X" : "xBar", sMemberArg, "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 1);
            DevExpressLib.SetImgChartLineSeries(chartControl, "CL", sMemberArg, calc.Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
            DevExpressLib.SetImgChartLineSeries(chartControl, "UCL", sMemberArg, calc.Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
            DevExpressLib.SetImgChartLineSeries(chartControl, "LCL", sMemberArg, calc.Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
            DevExpressLib.SetImgChartLineSeries(chartControl, "상한", sMemberArg, "F_MAX", System.Drawing.Color.Red);
            DevExpressLib.SetImgChartLineSeries(chartControl, "하한", sMemberArg, "F_MIN", System.Drawing.Color.Red);

            chartControl.DataSource = dtChart;

            DevExpressLib.SetImgCrosshairOptions(chartControl);

            if (dbType.Equals("03"))
            {
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, null, null, null, "{V:n4}");
            }
            else
            {
                chartControl.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
            }

            DevExpressLib.SetChartTitle(chartControl, siryo <= 1 ? "X 관리도" : "X-Bar 관리도", false);

            DevExpressLib.SetImgChartRuler(chartControl, dbType.Equals("03"), true);

            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;

        }

        void RChart(ChartControl chartControl, DataTable dtChart, Int32 siryo, string calc, string sMemberArg, string dbType)
        {
            chartControl.Series.Clear();

            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            string maxColumn = String.Empty;
            string minColumn = String.Empty;

            foreach (DataRow dtRow in dtChart.Rows)
            {
                maxColumn = calc.Equals("0") ? "F_UCLR" : "C_UCLR";
                minColumn = calc.Equals("0") ? "F_LCLR" : "C_LCLR";

                if (!String.IsNullOrEmpty(dtRow[maxColumn].ToString()))
                    maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow[maxColumn]));

                if (!String.IsNullOrEmpty(dtRow[minColumn].ToString()))
                    minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow[minColumn]));
            }

            maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
            minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

            DevExpressLib.SetImgChartLineSeries(chartControl, Convert.ToInt32(siryo) <= 1 ? "Rs" : "R", sMemberArg, "F_XRANGE", System.Drawing.Color.FromArgb(0, 102, 153), 2);
            DevExpressLib.SetImgChartLineSeries(chartControl, "CL", sMemberArg, calc.Equals("0") ? "F_CLR" : "C_CLR", System.Drawing.Color.Green);
            DevExpressLib.SetImgChartLineSeries(chartControl, "UCL", sMemberArg, calc.Equals("0") ? "F_UCLR" : "C_UCLR", System.Drawing.Color.Red);
            DevExpressLib.SetImgChartLineSeries(chartControl, "LCL", sMemberArg, calc.Equals("0") ? "F_LCLR" : "C_LCLR", System.Drawing.Color.Red);

            chartControl.DataSource = dtChart;

            DevExpressLib.SetImgCrosshairOptions(chartControl);

            if (dbType.Equals("03"))
            {
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, null, null, null, "{V:n4}");
            }
            else
            {
                chartControl.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
            }

            DevExpressLib.SetChartTitle(chartControl, siryo <= 1 ? "Rs 관리도" : "R 관리도", false);

            DevExpressLib.SetImgChartRuler(chartControl, dbType.Equals("03"), true);

            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}