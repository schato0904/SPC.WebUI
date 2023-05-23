using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using DevExpress.XtraReports.UI;
using System.Text;

namespace SPC.WebUI.Common
{
    public static class DevExpressLib
    {
        #region Find Control In DevGrid EditFormTemplate
        public static DevExpress.Web.ASPxTextBox devTextBox(DevExpress.Web.ASPxGridView devGrid, string ControlID)
        {
            return devGrid.FindEditFormTemplateControl(ControlID) as DevExpress.Web.ASPxTextBox;
        }

        public static DevExpress.Web.ASPxDateEdit devDateEdit(DevExpress.Web.ASPxGridView devGrid, string ControlID)
        {
            return devGrid.FindEditFormTemplateControl(ControlID) as DevExpress.Web.ASPxDateEdit;
        }

        public static DevExpress.Web.ASPxSpinEdit devSpinEdit(DevExpress.Web.ASPxGridView devGrid, string ControlID)
        {
            return devGrid.FindEditFormTemplateControl(ControlID) as DevExpress.Web.ASPxSpinEdit;
        }

        public static DevExpress.Web.ASPxComboBox devComboBox(DevExpress.Web.ASPxGridView devGrid, string ControlID)
        {
            return devGrid.FindEditFormTemplateControl(ControlID) as DevExpress.Web.ASPxComboBox;
        }

        public static DevExpress.Web.ASPxCheckBox devCheckBox(DevExpress.Web.ASPxGridView devGrid, string ControlID)
        {
            return devGrid.FindEditFormTemplateControl(ControlID) as DevExpress.Web.ASPxCheckBox;
        }

        public static DevExpress.Web.ASPxCheckBoxList devCheckBoxList(DevExpress.Web.ASPxGridView devGrid, string ControlID)
        {
            return devGrid.FindEditFormTemplateControl(ControlID) as DevExpress.Web.ASPxCheckBoxList;
        }

        public static DevExpress.Web.ASPxRadioButton devRadioButton(DevExpress.Web.ASPxGridView devGrid, string ControlID)
        {
            return devGrid.FindEditFormTemplateControl(ControlID) as DevExpress.Web.ASPxRadioButton;
        }

        public static DevExpress.Web.ASPxRadioButtonList devRadioButtonList(DevExpress.Web.ASPxGridView devGrid, string ControlID)
        {
            return devGrid.FindEditFormTemplateControl(ControlID) as DevExpress.Web.ASPxRadioButtonList;
        }
        #endregion

        #region Find Control In DevGrid EditFormTemplate BatchMode
        public static DevExpress.Web.ASPxTextBox devTextBox(DevExpress.Web.ASPxGridView devGrid, string ColumnID, string ControlID)
        {
            return devGrid.FindEditRowCellTemplateControl(devGrid.Columns[ColumnID] as DevExpress.Web.GridViewDataColumn, ControlID) as DevExpress.Web.ASPxTextBox;
        }

        public static DevExpress.Web.ASPxDateEdit devDateEdit(DevExpress.Web.ASPxGridView devGrid, string ColumnID, string ControlID)
        {
            return devGrid.FindEditRowCellTemplateControl(devGrid.Columns[ColumnID] as DevExpress.Web.GridViewDataColumn, ControlID) as DevExpress.Web.ASPxDateEdit;
        }

        public static DevExpress.Web.ASPxSpinEdit devSpinEdit(DevExpress.Web.ASPxGridView devGrid, string ColumnID, string ControlID)
        {
            return devGrid.FindEditRowCellTemplateControl(devGrid.Columns[ColumnID] as DevExpress.Web.GridViewDataColumn, ControlID) as DevExpress.Web.ASPxSpinEdit;
        }

        public static DevExpress.Web.ASPxComboBox devComboBox(DevExpress.Web.ASPxGridView devGrid, string ColumnID, string ControlID)
        {
            return devGrid.FindEditRowCellTemplateControl(devGrid.Columns[ColumnID] as DevExpress.Web.GridViewDataColumn, ControlID) as DevExpress.Web.ASPxComboBox;
        }

        public static DevExpress.Web.ASPxCheckBox devCheckBox(DevExpress.Web.ASPxGridView devGrid, string ColumnID, string ControlID)
        {
            return devGrid.FindEditRowCellTemplateControl(devGrid.Columns[ColumnID] as DevExpress.Web.GridViewDataColumn, ControlID) as DevExpress.Web.ASPxCheckBox;
        }

        public static DevExpress.Web.ASPxCheckBoxList devCheckBoxList(DevExpress.Web.ASPxGridView devGrid, string ColumnID, string ControlID)
        {
            return devGrid.FindEditRowCellTemplateControl(devGrid.Columns[ColumnID] as DevExpress.Web.GridViewDataColumn, ControlID) as DevExpress.Web.ASPxCheckBoxList;
        }

        public static DevExpress.Web.ASPxRadioButton devRadioButton(DevExpress.Web.ASPxGridView devGrid, string ColumnID, string ControlID)
        {
            return devGrid.FindEditRowCellTemplateControl(devGrid.Columns[ColumnID] as DevExpress.Web.GridViewDataColumn, ControlID) as DevExpress.Web.ASPxRadioButton;
        }

        public static DevExpress.Web.ASPxRadioButtonList devRadioButtonList(DevExpress.Web.ASPxGridView devGrid, string ColumnID, string ControlID)
        {
            return devGrid.FindEditRowCellTemplateControl(devGrid.Columns[ColumnID] as DevExpress.Web.GridViewDataColumn, ControlID) as DevExpress.Web.ASPxRadioButtonList;
        }
        #endregion

        #region DevGrid 내의 컬럼 전체의 Width를 구한다(Visibled Columns Total Width)
        public static Int32 devGridColumnWidth(DevExpress.Web.ASPxGridView devGrid)
        {
            Int32 iColumnsWidth = 0;
            string sWidth = String.Empty;
            for (int iColumn = 0; iColumn < devGrid.VisibleColumns.Count; iColumn++)
            {
                sWidth = Convert.ToString(devGrid.VisibleColumns[iColumn].Width).Replace("px", "");
                iColumnsWidth += !String.IsNullOrEmpty(sWidth) ? Convert.ToInt32(sWidth) : 0;
            }

            return iColumnsWidth;
        }
        #endregion

        #region DevGrid JsProperty(에러메세지가 있는 경우 DevGrid Callback인 경우 에러메세지 리턴한다)
        public static void SetDevGridJSProperties(DevExpress.Web.ASPxGridView devGrid, string errtMsg)
        {
            devGrid.JSProperties["cpResultCode"] = !String.IsNullOrEmpty(errtMsg) ? "0" : "1";
            devGrid.JSProperties["cpResultMsg"] = errtMsg;
        }

        public static void SetDevGridJSProperties(DevExpress.Web.ASPxGridView devGrid, string[] procResult)
        {
            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
        }
        #endregion

        #region Custom Display Text
        public static void GetUsedString(string[] columns, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            bool bColumn = false;

            foreach (string column in columns)
            {
                if (e.Column.FieldName.Equals(column))
                {
                    bColumn = true;
                    break;
                }
            }

            if (!bColumn) return;

            e.EncodeHtml = false;
            e.DisplayText = !(bool)e.Value ? @"<span style='color:red;'>중단</span>" : @"<span style='color:blue;'>사용</span>";
        }
        #endregion

        #region Custom Display Text
        public static void GetBoolString(string[] columns, string bTrueString, string bFalseString, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            bool bColumn = false;

            foreach (string column in columns)
            {
                if (e.Column.FieldName.Equals(column))
                {
                    bColumn = true;
                    break;
                }
            }

            if (!bColumn) return;

            e.EncodeHtml = false;
            e.DisplayText = !(bool)e.Value ? String.Format(@"<span style='color:red;'>{0}</span>", bFalseString) : String.Format(@"<span style='color:blue;'>{0}</span>", bTrueString);
        }
        #endregion

        #region HtmlDataCellPrepared
        public static void SetTextForeColor(string[] columns, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            bool bColumn = false;

            foreach (string column in columns)
            {
                if (e.DataColumn.FieldName.Equals(column))
                {
                    bColumn = true;
                    break;
                }
            }

            if (!bColumn) return;

            e.Cell.ForeColor = !(bool)e.CellValue ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
        }
        #endregion

        #region Grid Dynamic Create

        #region Create Grid Column
        public static void SetGridColumn(DevExpress.Web.ASPxGridView devGrid, string sFieldName, string sCaption, string sWidth, string sHorizontalAlign, object sColor)
        {
            DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = sFieldName, Caption = sCaption, Width = Unit.Parse(sWidth) };
            switch (sHorizontalAlign)
            {
                default:
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    break;
                case "left":
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    break;
                case "right":
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    break;
            }

            if (sColor != null)
                column.CellStyle.BackColor = (System.Drawing.Color)sColor;

            devGrid.Columns.Add(column);
        }
        #endregion

        #endregion

        #region Chart Dynamic Create

        #region Create Chart Series(Line)
        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = 1;
            lineSeriesView.LineMarkerOptions.Size = 1;
            series.View = lineSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Line) With Set Color
        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = 1;
            lineSeriesView.Color = (System.Drawing.Color)color;
            lineSeriesView.LineMarkerOptions.Size = 1;
            series.View = lineSeriesView;
            devChart.Series.Add(series);
        }

        #endregion

        #region Create Chart Series(Line) With Set Color lineThickness
        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers,  int lineThickness = 1)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };

            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = lineThickness;
            lineSeriesView.LineMarkerOptions.Size = 1;
            series.View = lineSeriesView;
            devChart.Series.Add(series);
        }

        #endregion

        #region Create Chart Series(Line) With Set Color lineThickness
        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, int lineThickness = 1)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };

            

            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = lineThickness;
            lineSeriesView.Color = (System.Drawing.Color)color;
            lineSeriesView.LineMarkerOptions.Size = 1;

            series.View = lineSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Line) With Set Color lineThickness
        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, int lineThickness = 1, int pointsize = 1)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };

            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = lineThickness;
            lineSeriesView.Color = (System.Drawing.Color)color;
            lineSeriesView.LineMarkerOptions.Size = 1;

            series.View = lineSeriesView;

            ((PointSeriesView)series.View).PointMarkerOptions.Size = pointsize;

            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Line) With Set Color lineThickness
        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, int lineThickness = 1, bool ShowInLegend = true)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };

            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = lineThickness;
            lineSeriesView.Color = (System.Drawing.Color)color;
            lineSeriesView.LineMarkerOptions.Size = 1;

            series.View = lineSeriesView;
            series.ShowInLegend = ShowInLegend;
            series.ToolTipEnabled = !ShowInLegend ? DevExpress.Utils.DefaultBoolean.False : DevExpress.Utils.DefaultBoolean.True;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Line) With Set Color, LineMarkerSize, ScaleType
        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object lineMarkerSize = null, object scaleType = null)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = 1;
            lineSeriesView.Color = (System.Drawing.Color)color;
            if (lineMarkerSize == null)
                lineSeriesView.LineMarkerOptions.Size = 1;
            else
                lineSeriesView.LineMarkerOptions.Size = (Int32)lineMarkerSize;
            series.View = lineSeriesView;
            devChart.Series.Add(series);

            if (scaleType != null)
                series.ArgumentScaleType = (ScaleType)scaleType;
        }
        #endregion

        #region Create Chart Series(Spline) With Color, LineMarkerSize, ScaleType
        public static void SetChartSplineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object lineMarkerSize = null, object scaleType = null)
        {
            Series series = new Series(name, ViewType.Spline) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            SplineSeriesView splineSeriesView = new SplineSeriesView();
            splineSeriesView.LineStyle.Thickness = 1;
            splineSeriesView.Color = (System.Drawing.Color)color;
            if (lineMarkerSize == null)
                splineSeriesView.LineMarkerOptions.Size = 1;
            else
                splineSeriesView.LineMarkerOptions.Size = (Int32)lineMarkerSize;
            splineSeriesView.LineTensionPercent = 100;
            series.View = splineSeriesView;
            devChart.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;
            if (scaleType != null)
                series.ArgumentScaleType = (ScaleType)scaleType;
        }
        #endregion

        #region Create Chart Series(StepLine)
        public static void SetChartStepLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            StepLineSeriesView stepLineSeriesView = new StepLineSeriesView();
            stepLineSeriesView.LineStyle.Thickness = 1;
            stepLineSeriesView.LineMarkerOptions.Size = 1;
            series.View = stepLineSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(StepLine) With Set Color
        public static void SetChartStepLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };

            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            StepLineSeriesView stepLineSeriesView = new StepLineSeriesView();
            stepLineSeriesView.LineStyle.Thickness = 1;
            stepLineSeriesView.Color = (System.Drawing.Color)color;
            stepLineSeriesView.LineMarkerOptions.Size = 1;
            series.View = stepLineSeriesView;
            devChart.Series.Add(series);
        }

        #endregion

        #region Create Chart Series(StepLine) With Set Color lineThickness
        public static void SetChartStepLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, int lineThickness = 1)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };

            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            StepLineSeriesView stepLineSeriesView = new StepLineSeriesView();
            stepLineSeriesView.LineStyle.Thickness = lineThickness;
            stepLineSeriesView.LineMarkerOptions.Size = 1;
            series.View = stepLineSeriesView;
            devChart.Series.Add(series);
        }

        #endregion

        #region Create Chart Series(StepLine) With Set Color lineThickness
        public static void SetChartStepLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, int lineThickness = 1)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };

            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            StepLineSeriesView stepLineSeriesView = new StepLineSeriesView();
            stepLineSeriesView.LineStyle.Thickness = lineThickness;
            stepLineSeriesView.Color = (System.Drawing.Color)color;
            stepLineSeriesView.LineMarkerOptions.Size = 1;

            series.View = stepLineSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(StepLine) With Set Color lineThickness
        public static void SetChartStepLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, int lineThickness = 1, int pointsize = 1)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };

            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            StepLineSeriesView stepLineSeriesView = new StepLineSeriesView();
            stepLineSeriesView.LineStyle.Thickness = lineThickness;
            stepLineSeriesView.Color = (System.Drawing.Color)color;
            stepLineSeriesView.LineMarkerOptions.Size = 1;

            series.View = stepLineSeriesView;

            ((PointSeriesView)series.View).PointMarkerOptions.Size = pointsize;

            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(StepLine) With Set Color, LineMarkerSize, ScaleType
        public static void SetChartStepLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object lineMarkerSize = null, object scaleType = null)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            StepLineSeriesView stepLineSeriesView = new StepLineSeriesView();
            stepLineSeriesView.LineStyle.Thickness = 1;
            stepLineSeriesView.Color = (System.Drawing.Color)color;
            if (lineMarkerSize == null)
                stepLineSeriesView.LineMarkerOptions.Size = 1;
            else
                stepLineSeriesView.LineMarkerOptions.Size = (Int32)lineMarkerSize;
            series.View = stepLineSeriesView;
            devChart.Series.Add(series);

            if (scaleType != null)
                series.ArgumentScaleType = (ScaleType)scaleType;
        }
        #endregion

        #region Create Chart Series(Point)
        public static void SetChartPointSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object pointSize = null)
        {
            Series series = new Series(name, ViewType.Bar) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            PointSeriesView pointSeriesView = new PointSeriesView();
            if (pointSize == null)
                pointSeriesView.PointMarkerOptions.Size = 2;
            else
                pointSeriesView.PointMarkerOptions.Size = Convert.ToInt32(pointSize);

            pointSeriesView.Color = (System.Drawing.Color)color;
            pointSeriesView.Shadow.Visible = false;
            pointSeriesView.PointMarkerOptions.FillStyle.FillMode = FillMode.Solid;
            series.View = pointSeriesView;
            devChart.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;
        }
        #endregion

        #region Create Chart Series(Bar)
        public static void SetChartBarLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object BarWidth = null)
        {
            Series series = new Series(name, ViewType.Bar) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            SideBySideBarSeriesView barSeriesView = new SideBySideBarSeriesView();
            if (BarWidth == null)
                barSeriesView.BarWidth = 0.8;
            else
                barSeriesView.BarWidth = Convert.ToDouble(BarWidth);

            
            barSeriesView.Color = (System.Drawing.Color)color;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Solid;
            series.View = barSeriesView;
            devChart.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;
        }
        #endregion

        #region Create Chart Series(Bar) With Label
        public static void SetChartBarLineSeriesWithLabel(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object lblColor, object BarWidth = null)
        {
            Series series = new Series(name, ViewType.Bar) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            SideBySideBarSeriesView barSeriesView = new SideBySideBarSeriesView();
            if (BarWidth == null)
                barSeriesView.BarWidth = 0.8;
            else
                barSeriesView.BarWidth = Convert.ToDouble(BarWidth);
            barSeriesView.Color = (System.Drawing.Color)color;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Solid;
            
            series.View = barSeriesView;
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            series.Label.Border.Color = (System.Drawing.Color)lblColor;
            devChart.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;
        }
        #endregion

        #region Create Chart SetChartFullStackedBarSeries
        public static void SetChartFullStackedBarSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, string pattern, DevExpress.Utils.DefaultBoolean labelvisible = DevExpress.Utils.DefaultBoolean.True)
        {
            Series series = new Series(name, ViewType.FullStackedBar) { ArgumentDataMember = ArgumentDataMember };
            FullStackedBarSeriesView stackedview = new FullStackedBarSeriesView();
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });            
            stackedview.FillStyle.FillMode = FillMode.Solid;
            stackedview.Color = (System.Drawing.Color)color;
            series.Label.TextPattern = pattern;
            series.LabelsVisibility = labelvisible;
            series.View = stackedview;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart SetChartSideBySideFullStackedBarSeries
        public static void SetChartSideBySideFullStackedBarSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, string StackedGroup, object color, string pattern, DevExpress.Utils.DefaultBoolean labelvisible = DevExpress.Utils.DefaultBoolean.True)
        {
            Series series = new Series(name, ViewType.SideBySideFullStackedBar) { ArgumentDataMember = ArgumentDataMember };
            SideBySideFullStackedBarSeriesView sidebysidestackedview = new SideBySideFullStackedBarSeriesView();
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            sidebysidestackedview.FillStyle.FillMode = FillMode.Solid;
            sidebysidestackedview.Color = (System.Drawing.Color)color;
            sidebysidestackedview.StackedGroup = StackedGroup;
            series.Label.TextPattern = pattern;
            series.LabelsVisibility = labelvisible;
            series.View = sidebysidestackedview;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Pie)
        public static void SetChartPieSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, string textPatten = "", object color = null)
        {
            Series series = new Series(name, ViewType.Pie) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            PieSeriesView pieSeriesView = new PieSeriesView();
            pieSeriesView.Rotation = 90;
            pieSeriesView.RuntimeExploding = true;
            series.View = pieSeriesView;
            PieSeriesLabel pieSeriesLabel = (PieSeriesLabel)series.Label;
            pieSeriesLabel.Position = PieSeriesLabelPosition.TwoColumns;
            pieSeriesLabel.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            
            if (!String.IsNullOrEmpty(textPatten))
                pieSeriesLabel.TextPattern = textPatten;
            
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(ManhattanBar)
        public static void SetChartManhattanBarSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object BarWidth = null)
        {
            Series series = new Series(name, ViewType.ManhattanBar) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            ManhattanBarSeriesView manhattanbarSeriesView = new ManhattanBarSeriesView();
            if (BarWidth == null)
                manhattanbarSeriesView.BarWidth = 0.8;
            else
                manhattanbarSeriesView.BarWidth = Convert.ToDouble(BarWidth);
            manhattanbarSeriesView.Color = (System.Drawing.Color)color;
            manhattanbarSeriesView.FillStyle.FillMode = FillMode3D.Solid;
            series.View = manhattanbarSeriesView;
            devChart.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;
            ((Bar3DSeriesView)series.View).Transparency = Convert.ToByte("90");
        }
        #endregion

        #region Set Chart Diagram
        public static void SetChartDiagram(WebChartControl devChart, bool bGridLinesVisible, object maxAxisX, object minAxisX, object maxAxisY, object minAxisY,
            string textAxisXPattern = null, string textAxisYPattern = null,
            bool axisXVisible = true, bool axisYVisible = true)
        {
            XYDiagram diagram = (XYDiagram)devChart.Diagram;

            diagram.AxisX.GridLines.Visible = bGridLinesVisible;
            diagram.AxisY.GridLines.Visible = bGridLinesVisible;
            diagram.AxisX.Label.Visible = axisXVisible;
            diagram.AxisY.Label.Visible = axisYVisible;
           
            if (textAxisXPattern != null)
                diagram.AxisX.Label.TextPattern = textAxisXPattern;
            if (textAxisYPattern != null)
                diagram.AxisY.Label.TextPattern = textAxisYPattern;

            if (Convert.ToDecimal(maxAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MaxValue = maxAxisX;
            if (Convert.ToDecimal(minAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MinValue = minAxisX;
            if (Convert.ToDecimal(maxAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MaxValue = maxAxisY;
            if (Convert.ToDecimal(minAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MinValue = minAxisY;

            
        }
        #endregion

        #region Set Chart Diagram3D
        public static void SetChartDiagram3D(WebChartControl devChart, bool bGridLinesVisible, object maxAxisX, object minAxisX, object maxAxisY, object minAxisY,
            string textAxisXPattern = null, string textAxisYPattern = null,
            bool axisXVisible = true, bool axisYVisible = true)
        {
            XYDiagram3D diagram = (XYDiagram3D)devChart.Diagram;
            diagram.ZoomPercent = 130;

            diagram.AxisX.GridLines.Visible = bGridLinesVisible;
            diagram.AxisY.GridLines.Visible = bGridLinesVisible;
            diagram.AxisX.Label.Visible = axisXVisible;
            diagram.AxisY.Label.Visible = axisYVisible;

            if (textAxisXPattern != null)
                diagram.AxisX.Label.TextPattern = textAxisXPattern;
            if (textAxisYPattern != null)
                diagram.AxisY.Label.TextPattern = textAxisYPattern;

            if (Convert.ToDecimal(maxAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MaxValue = maxAxisX;
            if (Convert.ToDecimal(minAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MinValue = minAxisX;
            if (Convert.ToDecimal(maxAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MaxValue = maxAxisY;
            if (Convert.ToDecimal(minAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MinValue = minAxisY;


        }
        #endregion

        #region Set Chart Diagram Ruler
        public static void SetChartRuler(ChartControl devChart, bool minorXVisible = true, bool minorYVisible = true)
        {
            XYDiagram diagram = (XYDiagram)devChart.Diagram;

            diagram.AxisX.GridLines.MinorVisible = minorXVisible;
            diagram.AxisY.GridLines.MinorVisible = minorYVisible;
        }
        #endregion

        #region Set Chart Legend
        public static void SetChartLegend(WebChartControl devChart)
        {
            devChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //devChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
            //devChart.Legend.Direction = LegendDirection.LeftToRight;
            //devChart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
        }
        #endregion

        #region Set Chart  CrosshairOptions
        public static void SetCrosshairOptions(WebChartControl devChart)
        {
            devChart.CrosshairOptions.ShowArgumentLabels = true;
            devChart.CrosshairOptions.ShowArgumentLine = true;
            devChart.CrosshairOptions.ArgumentLineColor = System.Drawing.Color.DeepSkyBlue;
            devChart.CrosshairOptions.ArgumentLineStyle.Thickness = 2;
            devChart.CrosshairOptions.ShowOnlyInFocusedPane = false;
        }
        #endregion

        #region Create Chart ConstantLine
        public static void SetChartConstantLine(WebChartControl devChart, string name, object axisValue, object color)
        {
            XYDiagram diagram = (XYDiagram)devChart.Diagram;
            ConstantLine constantLine = new ConstantLine(name);
            diagram.AxisY.ConstantLines.Add(constantLine);
            constantLine.AxisValue = axisValue;
            constantLine.Color = (System.Drawing.Color)color;
        }
        #endregion

        #region Clear Chart ConstantLine
        public static void ClearChartConstantLine(WebChartControl devChart)
        {
            XYDiagram diagram = (XYDiagram)devChart.Diagram;
            diagram.AxisY.ConstantLines.Clear();
        }
        #endregion

        #region Set Chart Title
        public static void SetChartTitle(WebChartControl devChart, string sTitle, bool bCenter = true)
        {
            devChart.Titles.Clear();

            ChartTitle chartTitle = new ChartTitle() { Text = sTitle, Font = new System.Drawing.Font("Malgun Gothic", 10, System.Drawing.FontStyle.Underline) };
            if (!bCenter)
                chartTitle.Alignment = System.Drawing.StringAlignment.Near;
            devChart.Titles.Add(chartTitle);
        }
        #endregion

        #endregion

        #region Chart Image Export

        #region Create Chart Series(Point)
        public static void SetImgChartPointSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers)
        {
            Series series = new Series(name, ViewType.Point) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            PointSeriesView pointSeriesView = new PointSeriesView();
            pointSeriesView.PointMarkerOptions.Size = 1;
            series.View = pointSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Point) With Set Color
        public static void SetImgChartPointSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color)
        {
            Series series = new Series(name, ViewType.Point) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            PointSeriesView pointSeriesView = new PointSeriesView();
            pointSeriesView.PointMarkerOptions.Size = 1;
            pointSeriesView.Color = (System.Drawing.Color)color;
            series.View = pointSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Point) With Set Color, LineMarkerSize, ScaleType
        public static void SetImgChartPointSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object pointMarkerSize = null, object scaleType = null)
        {
            Series series = new Series(name, ViewType.Point) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            PointSeriesView pointSeriesView = new PointSeriesView();
            if (pointMarkerSize == null)
                pointSeriesView.PointMarkerOptions.Size = 1;
            else
                pointSeriesView.PointMarkerOptions.Size = (Int32)pointMarkerSize;
            
            pointSeriesView.Color = (System.Drawing.Color)color;
            series.View = pointSeriesView;
            devChart.Series.Add(series);

            if (scaleType != null)
                series.ArgumentScaleType = (ScaleType)scaleType;
        }
        #endregion

        #region Create Chart Series(Line)
        public static void SetImgChartLineSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = 1;
            lineSeriesView.LineMarkerOptions.Size = 1;
            series.View = lineSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Line) With Set Color
        public static void SetImgChartLineSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = 1;
            lineSeriesView.Color = (System.Drawing.Color)color;
            lineSeriesView.LineMarkerOptions.Size = 1;
            series.View = lineSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region Create Chart Series(Line) With Set Color, LineMarkerSize, ScaleType
        public static void SetImgChartLineSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object lineMarkerSize = null, object scaleType = null)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = 1;
            lineSeriesView.Color = (System.Drawing.Color)color;
            if (lineMarkerSize == null)
                lineSeriesView.LineMarkerOptions.Size = 1;
            else
                lineSeriesView.LineMarkerOptions.Size = (Int32)lineMarkerSize;
            series.View = lineSeriesView;
            devChart.Series.Add(series);

            if (scaleType != null)
                series.ArgumentScaleType = (ScaleType)scaleType;
        }
        #endregion

        #region Create Chart Series(Bar)
        public static void SetImgChartBarLineSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object BarWidth = null)
        {
            Series series = new Series(name, ViewType.Bar) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            SideBySideBarSeriesView barSeriesView = new SideBySideBarSeriesView();
            if (BarWidth == null)
                barSeriesView.BarWidth = 0.8;
            else
                barSeriesView.BarWidth = Convert.ToDouble(BarWidth);
            barSeriesView.Color = (System.Drawing.Color)color;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Solid;
            series.View = barSeriesView;
            devChart.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;
        }

        public static void SetImgChartBarLineSeriesWithLabel(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object BarWidth = null)
        {
            Series series = new Series(name, ViewType.Bar) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            SideBySideBarSeriesView barSeriesView = new SideBySideBarSeriesView();
            if (BarWidth == null)
                barSeriesView.BarWidth = 0.8;
            else
                barSeriesView.BarWidth = Convert.ToDouble(BarWidth);

            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            barSeriesView.Color = (System.Drawing.Color)color;
            barSeriesView.Shadow.Visible = false;
            barSeriesView.FillStyle.FillMode = FillMode.Solid;
            series.View = barSeriesView;
            devChart.Series.Add(series);

            series.ArgumentScaleType = ScaleType.Qualitative;
        }
        #endregion

        #region Create Chart Series(Line) With Color, LineMarkerSize, ScaleType
        public static void SetImgChartSplineSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object lineMarkerSize = null, object scaleType = null)
        {
            Series series = new Series(name, ViewType.Line) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            SplineSeriesView splineSeriesView = new SplineSeriesView();
            splineSeriesView.LineStyle.Thickness = 1;
            splineSeriesView.Color = (System.Drawing.Color)color;
            if (lineMarkerSize == null)
                splineSeriesView.LineMarkerOptions.Size = 1;
            else
                splineSeriesView.LineMarkerOptions.Size = (Int32)lineMarkerSize;
            splineSeriesView.LineTensionPercent = 100;
            series.View = splineSeriesView;
            devChart.Series.Add(series);

            if (scaleType != null)
                series.ArgumentScaleType = (ScaleType)scaleType;
        }
        #endregion

        #region Create Chart Series(ManhattanBar)
        public static void SetImgChartManhattanBarSeries(ChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, object color, object BarWidth = null)
        {
            Series series = new Series(name, ViewType.ManhattanBar) { ArgumentDataMember = ArgumentDataMember };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });
            ManhattanBarSeriesView manhattanbarSeriesView = new ManhattanBarSeriesView();
            if (BarWidth == null)
                manhattanbarSeriesView.BarWidth = 0.8;
            else
                manhattanbarSeriesView.BarWidth = Convert.ToDouble(BarWidth);
            manhattanbarSeriesView.Color = (System.Drawing.Color)color;
            manhattanbarSeriesView.FillStyle.FillMode = FillMode3D.Solid;
            series.View = manhattanbarSeriesView;
            devChart.Series.Add(series);
            series.ArgumentScaleType = ScaleType.Qualitative;
            ((Bar3DSeriesView)series.View).Transparency = Convert.ToByte("90");
        }
        #endregion

        #region Set Chart  CrosshairOptions
        public static void SetImgCrosshairOptions(ChartControl devChart)
        {
            devChart.CrosshairOptions.ShowArgumentLabels = true;
            devChart.CrosshairOptions.ShowArgumentLine = true;
            devChart.CrosshairOptions.ArgumentLineColor = System.Drawing.Color.DeepSkyBlue;
            devChart.CrosshairOptions.ArgumentLineStyle.Thickness = 2;
            devChart.CrosshairOptions.ShowOnlyInFocusedPane = false;
        }
        #endregion

        #region Set Chart Diagram
        public static void SetImgChartDiagram(ChartControl devChart, bool bGridLinesVisible, object maxAxisX, object minAxisX, object maxAxisY, object minAxisY,
            string textAxisXPattern = null, string textAxisYPattern = null,
            bool axisXVisible = true, bool axisYVisible = true)
        {
            XYDiagram diagram = (XYDiagram)devChart.Diagram;

            diagram.AxisX.GridLines.Visible = bGridLinesVisible;
            diagram.AxisY.GridLines.Visible = bGridLinesVisible;
            diagram.AxisX.Label.Visible = axisXVisible;
            diagram.AxisY.Label.Visible = axisYVisible;

            if (textAxisXPattern != null)
                diagram.AxisX.Label.TextPattern = textAxisXPattern;
            if (textAxisYPattern != null)
                diagram.AxisY.Label.TextPattern = textAxisYPattern;

            if (Convert.ToDecimal(maxAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MaxValue = maxAxisX;
            if (Convert.ToDecimal(minAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MinValue = minAxisX;
            if (Convert.ToDecimal(maxAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MaxValue = maxAxisY;
            if (Convert.ToDecimal(minAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MinValue = minAxisY;
        }
        #endregion

        #region Set Chart Diagram3D
        public static void SetImgChartDiagram3D(ChartControl devChart, bool bGridLinesVisible, object maxAxisX, object minAxisX, object maxAxisY, object minAxisY,
            string textAxisXPattern = null, string textAxisYPattern = null,
            bool axisXVisible = true, bool axisYVisible = true)
        {
            XYDiagram3D diagram = (XYDiagram3D)devChart.Diagram;
            diagram.ZoomPercent = 130;

            diagram.AxisX.GridLines.Visible = bGridLinesVisible;
            diagram.AxisY.GridLines.Visible = bGridLinesVisible;
            diagram.AxisX.Label.Visible = axisXVisible;
            diagram.AxisY.Label.Visible = axisYVisible;

            if (textAxisXPattern != null)
                diagram.AxisX.Label.TextPattern = textAxisXPattern;
            if (textAxisYPattern != null)
                diagram.AxisY.Label.TextPattern = textAxisYPattern;

            if (Convert.ToDecimal(maxAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MaxValue = maxAxisX;
            if (Convert.ToDecimal(minAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MinValue = minAxisX;
            if (Convert.ToDecimal(maxAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MaxValue = maxAxisY;
            if (Convert.ToDecimal(minAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MinValue = minAxisY;


        }
        #endregion

        #region Set Chart Diagram Ruler
        public static void SetImgChartRuler(ChartControl devChart, bool minorXVisible = true, bool minorYVisible = true)
        {
            XYDiagram diagram = (XYDiagram)devChart.Diagram;

            diagram.AxisX.Tickmarks.Visible = minorXVisible;
            diagram.AxisX.Tickmarks.MinorVisible = minorXVisible;
            diagram.AxisY.Tickmarks.Visible = minorYVisible;
            diagram.AxisY.Tickmarks.MinorVisible = minorYVisible;
        }
        #endregion

        #region Set Chart Legend
        public static void SetChartLegend(ChartControl devChart)
        {
            devChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //devChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
            //devChart.Legend.Direction = LegendDirection.LeftToRight;
            //devChart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
        }
        #endregion

        #region Set Chart Title
        public static void SetChartTitle(ChartControl devChart, string sTitle, bool bCenter = true)
        {
            devChart.Titles.Clear();

            ChartTitle chartTitle = new ChartTitle() { Text = sTitle, Font = new System.Drawing.Font("Malgun Gothic", 10, System.Drawing.FontStyle.Underline) };
            if (!bCenter)
                chartTitle.Alignment = System.Drawing.StringAlignment.Near;
            devChart.Titles.Add(chartTitle);
        }
        #endregion

        #endregion

        #region XtraReport 함수

        #region SetText
        /// <summary>
        /// SetText In Table
        /// </summary>
        /// <param name="xrTableCell">XRTableCell</param>
        /// <param name="value">string</param>
        public static void SetText(object sender, string value)
        {
            XRTableCell xrTableCell = sender as XRTableCell;
            xrTableCell.Text = value;
        }

        /// <summary>
        /// SetText In Table
        /// </summary>
        /// <param name="xrTableCell">XRTableCell</param>
        /// <param name="value">string</param>
        public static void SetText(XRTableCell xrTableCell, string value)
        {
            xrTableCell.Text = value;            
        }

        /// <summary>
        /// SetText In Table
        /// </summary>
        /// <param name="xrTableCell">XRTableCell</param>
        /// <param name="value">string</param>
        public static void SetText(XRLabel xrLable, string value)
        {
            xrLable.Text = value;
        }
        #endregion

        #region LineBreak
        public static void LineBreak(object sender, int step = 1)
        {
            XRTableCell xrTableCell = sender as XRTableCell;
            string value = xrTableCell.Text;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < value.Length; i += step)
            {
                sb.Append(value.Substring(i, step));
                if (value.Length - step > i)
                    sb.Append(Environment.NewLine);
            }

            xrTableCell.Multiline = true;
            xrTableCell.Text = sb.ToString();
        }


        public static void LineBreak(object sender, string separator)
        {
            XRTableCell xrTableCell = sender as XRTableCell;
            xrTableCell.Multiline = true;
            xrTableCell.Text = xrTableCell.Text.Replace(separator, Environment.NewLine);
        }
        #endregion

        #region 공차만들기
        public static string RendorTolerance(string Standard, string Max, string Min, bool bNewline)
        {
            int dLen = Standard.IndexOf('.') > 0 ? Standard.Length - (Standard.IndexOf('.') + 1) : 0;

            decimal dStandard, dMax, dMin, dUpper, dLower, drUpper, drLower;
            string sUpper, sLower, sTolerance;

            if (!String.IsNullOrEmpty(Standard))
            {
                if (!String.IsNullOrEmpty(Max) && !String.IsNullOrEmpty(Min))
                {
                    dStandard = Convert.ToDecimal(Standard);
                    dMax = Convert.ToDecimal(Max);
                    dMin = Convert.ToDecimal(Min);

                    dUpper = dMax - dStandard;
                    dLower = dMin - dStandard;

                    drUpper = Math.Round(dUpper, dLen + 2);
                    drLower = Math.Round(dLower, dLen + 2);

                    sUpper = drUpper.ToString();
                    sUpper = dLen > 0 ? sUpper.Substring(0, sUpper.IndexOf('.') + dLen + 1) : sUpper;
                    sUpper = String.Format("+{0}", sUpper);

                    sLower = drLower.ToString();
                    sLower = dLen > 0 ? sLower.Substring(0, sLower.IndexOf('.') + dLen + 1) : sLower;

                    if (Math.Abs(drUpper) == Math.Abs(drLower))
                        return String.Format("±{0}", sUpper.Replace("+", "").Replace("-", ""));
                    else
                        return !bNewline ? String.Format("({0}/{1})", sUpper, sLower) : String.Format("{0}{1}{2}", sUpper, Environment.NewLine, sLower);
                }
                else if (!String.IsNullOrEmpty(Max))
                {
                    dStandard = Convert.ToDecimal(Standard);
                    dMax = Convert.ToDecimal(Max);

                    dUpper = dMax - dStandard;

                    drUpper = Math.Round(dUpper, dLen + 2);

                    sUpper = drUpper.ToString();
                    sUpper = dLen > 0 ? sUpper.Substring(0, sUpper.IndexOf('.') + dLen + 1) : sUpper;
                    sUpper = drUpper > 0 ? String.Format("+{0}", sUpper) : sUpper;

                    return String.Format("{0}{1}{2}", sUpper, Environment.NewLine, "     ");
                }
                else if (!String.IsNullOrEmpty(Min))
                {
                    dStandard = Convert.ToDecimal(Standard);
                    dMin = Convert.ToDecimal(Min);

                    dLower = dMin - dStandard;

                    drLower = Math.Round(dLower, dLen + 2);

                    sLower = drLower.ToString();
                    sLower = dLen > 0 ? sLower.Substring(0, sLower.IndexOf('.') + dLen + 1) : sLower;

                    return String.Format("{0}{1}{2}", "     ", Environment.NewLine, sLower);
                }
                else
                    return "";
            }
            else
                return "";
        }
        #endregion

        #region 순번기호
        public static string GetOrderSimbol(int idx)
        {
            switch (idx)
            {
                default: return "";
                case 1: return "①";
                case 2: return "②";
                case 3: return "③";
                case 4: return "④";
                case 5: return "⑤";
                case 6: return "⑥";
                case 7: return "⑦";
                case 8: return "⑧";
                case 9: return "⑨";
                case 10: return "⑩";
                case 11: return "⑪";
                case 12: return "⑫";
                case 13: return "⑬";
                case 14: return "⑭";
                case 15: return "⑮";
            }
        }
        #endregion

        #endregion
    }
}
