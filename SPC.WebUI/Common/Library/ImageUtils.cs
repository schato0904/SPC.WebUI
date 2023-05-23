using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;

namespace SPC.WebUI.Common.Library
{
    public class ImageUtils
    {
        public Image GetImageFromLocation(String imageLocation)
        {
            HttpRequest request = HttpContext.Current.Request;
            imageLocation = String.Format("{0}://{1}{2}",
                !request.IsSecureConnection ? "http" : "https",
                request.ServerVariables.Get("SERVER_NAME"),
                imageLocation);
            
            Uri uri = new Uri(imageLocation);

            if (uri.IsFile)
            {
                using (StreamReader reader = new StreamReader(uri.LocalPath))
                {
                    using (var image = Image.FromStream(reader.BaseStream))
                    {
                        return new Bitmap(image);
                    }
                }
            }
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(uri.ToString()))
                {
                    using (var image = Image.FromStream(stream))
                    {
                        return new Bitmap(image);
                    }
                }
            }
        }

        public Image GetChartImage(string type, Int32 w, Int32 h, Int32 siryo, string calc, string memb, string db, DataTable dtChart)
        {
            HttpContext context = HttpContext.Current;
            
            if (!String.IsNullOrEmpty(type))
            {
                ChartControl chartControl = new ChartControl() { Width = w, Height = h };

                switch (type)
                {
                    case "x":
                        XBarChart(chartControl, dtChart, siryo, calc, memb, db);
                        break;
                    case "r":
                        RChart(chartControl, dtChart, siryo, calc, memb, db);
                        break;
                    case "h":
                        HistogramChart(chartControl, dtChart, memb);
                        break;
                    case "h_new":
                        HistogramChart_NEW(chartControl, dtChart, memb);
                        break;
                    case "s":
                        SplineChart(chartControl, dtChart, memb);
                        break;
                    case "d":
                        DifferentChart(chartControl, dtChart, memb);
                        break;
                    case "3d":
                        ManhattanChart(chartControl, dtChart, memb);                    
                        break;
                    default:
                        break;
                }

                Image img = null;

                using (MemoryStream memoryImage = new MemoryStream())
                {
                    chartControl.ExportToImage(memoryImage, ImageFormat.Png);
                    memoryImage.Seek(0, SeekOrigin.Begin);

                    img = Image.FromStream(memoryImage);
                }

                return img;
            }
            else
            {
                return Image.FromFile(context.Server.MapPath("~/Resources/images/blank.png"));
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
            chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;

            if (dbType.Equals("03") || dbType.Equals("08"))
            {
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, null, null, null, "{V:n4}");
            }
            else
            {
                chartControl.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
            }

            DevExpressLib.SetImgChartRuler(chartControl, dbType.Equals("03") || dbType.Equals("08"), true);

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
            chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;

            if (dbType.Equals("03") || dbType.Equals("08"))
            {
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, null, null, null, "{V:n4}");
            }
            else
            {
                chartControl.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
                DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
            }

            DevExpressLib.SetImgChartRuler(chartControl, dbType.Equals("03") || dbType.Equals("08"), true);

            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
        }

        void HistogramChart(ChartControl chartControl, DataTable dtChart, string sMemberArg)
        {
            DevExpressLib.SetImgChartBarLineSeries(chartControl, "시료수", sMemberArg, "F_SIRYO", System.Drawing.Color.LightBlue);
            DevExpressLib.SetImgChartBarLineSeriesWithLabel(chartControl, "규격", sMemberArg, "F_VALUE", System.Drawing.Color.LightPink, 0.2);

            chartControl.Series["규격"].Label.TextPattern = "{A}";
            BarSeriesLabel serieslabel = chartControl.Series["규격"].Label as BarSeriesLabel;
            serieslabel.Position = BarSeriesLabelPosition.Top;

            chartControl.DataSource = dtChart;

            chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            DevExpressLib.SetChartLegend(chartControl);
            //DevExpressLib.SetImgChartDiagram(chartControl, true, 0, 0, 0, 0, null, null);
            DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, 0, 0, null, "{V:n4}", false, false);
        }

        void HistogramChart_NEW(ChartControl chartControl, DataTable dtChart, string sMemberArg)
        {
            DevExpressLib.SetImgChartBarLineSeries(chartControl, "시료수", sMemberArg, "ValueCount", System.Drawing.Color.CadetBlue);
            chartControl.DataSource = dtChart;

            chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            DevExpressLib.SetChartLegend(chartControl);
            DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, 0, 0, null, "{V:n4}", false, false);
        }

        void SplineChart(ChartControl chartControl, DataTable dtChart, string sMemberArg)
        {
            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            foreach (DataRow dtRow in dtChart.Rows)
            {
                if (maxAxisY < Convert.ToDecimal(dtRow["F_DATA2"]))
                    maxAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);

                if (minAxisY > Convert.ToDecimal(dtRow["F_DATA2"]))
                    minAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);
            }

            maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
            minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

            DevExpressLib.SetImgChartSplineSeries(chartControl, "", sMemberArg, "F_DATA2", System.Drawing.Color.Green, null, DevExpress.XtraCharts.ScaleType.Qualitative);
            DevExpressLib.SetImgChartBarLineSeries(chartControl, "", sMemberArg, "F_STANDARD", System.Drawing.Color.Khaki, 0.1);

            chartControl.DataSource = dtChart;

            chartControl.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, maxAxisY, minAxisY, null, null, false, false);
            DevExpressLib.SetImgChartRuler(chartControl, false, false);
        }

        void DifferentChart(ChartControl chartControl, DataTable dtChart, string sMemberArg)
        {
            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            maxAxisY = Math.Max(Convert.ToDecimal(dtChart.Compute("MAX(F_VALUE1)", ""))
                , Convert.ToDecimal(dtChart.Compute("MAX(F_VALUE2)", "")));

            minAxisY = Math.Min(Convert.ToDecimal(dtChart.Compute("MIN(F_VALUE1)", ""))
                , Convert.ToDecimal(dtChart.Compute("MIN(F_VALUE2)", "")));

            if (maxAxisY == minAxisY)
            {
                maxAxisY = 0;
                minAxisY = 0;
            }
            else
            {
                maxAxisY += (maxAxisY - minAxisY) / 10m;
                minAxisY -= (maxAxisY - minAxisY) / 10m;
            }

            DevExpressLib.SetImgChartLineSeries(chartControl, "집단1", sMemberArg, "F_VALUE1", System.Drawing.Color.Blue);
            DevExpressLib.SetImgChartLineSeries(chartControl, "집단2", sMemberArg, "F_VALUE2", System.Drawing.Color.Red);

            chartControl.DataSource = dtChart;

            chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            DevExpressLib.SetChartLegend(chartControl);
            DevExpressLib.SetImgChartDiagram(chartControl, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
        }

        void ManhattanChart(ChartControl chartControl, DataTable dtChart, string sMemberArg)
        {
            DevExpressLib.SetImgChartManhattanBarSeries(chartControl, "집단1", sMemberArg, "F_SIRYO1", System.Drawing.Color.Blue);
            DevExpressLib.SetImgChartManhattanBarSeries(chartControl, "집단2", sMemberArg, "F_SIRYO2", System.Drawing.Color.Red);

            chartControl.DataSource = dtChart;

            chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            DevExpressLib.SetChartLegend(chartControl);
            DevExpressLib.SetImgChartDiagram3D(chartControl, false, 0, 0, 0, 0, null, "{V}");
        }
    }
}
