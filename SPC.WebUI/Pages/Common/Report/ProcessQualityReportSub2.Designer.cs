namespace SPC.WebUI.Pages.Common.Report
{
    partial class ProcessQualityReportSub2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter8 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessQualityReportSub2));
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.StartDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.EndDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.Year = new DevExpress.XtraReports.Parameters.Parameter();
            this.WeekOfYear = new DevExpress.XtraReports.Parameters.Parameter();
            this.F_COMPCD = new DevExpress.XtraReports.Parameters.Parameter();
            this.F_FACTCD = new DevExpress.XtraReports.Parameters.Parameter();
            this.F_LANGTP = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
            this.xrChart3 = new DevExpress.XtraReports.UI.XRChart();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "CTF";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "USP_MONITORING_MNTR0907_1_5WORST_CHART";
            queryParameter1.Name = "@F_SDATE";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.StartDate]", typeof(string));
            queryParameter2.Name = "@S_COMPCD";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.F_COMPCD]", typeof(string));
            queryParameter3.Name = "@S_FACTCD";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.F_FACTCD]", typeof(string));
            queryParameter4.Name = "@F_LANGTP";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.F_LANGTP]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.Parameters.Add(queryParameter4);
            storedProcQuery1.StoredProcName = "USP_MONITORING_MNTR0907_1_5WORST_CHART";
            storedProcQuery2.Name = "USP_MONITORING_MNTR0907_1_TWORST_CHART";
            queryParameter5.Name = "@F_SDATE";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("[Parameters.StartDate]", typeof(string));
            queryParameter6.Name = "@S_COMPCD";
            queryParameter6.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter6.Value = new DevExpress.DataAccess.Expression("[Parameters.F_COMPCD]", typeof(string));
            queryParameter7.Name = "@S_FACTCD";
            queryParameter7.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter7.Value = new DevExpress.DataAccess.Expression("[Parameters.F_FACTCD]", typeof(string));
            queryParameter8.Name = "@F_LANGTP";
            queryParameter8.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter8.Value = new DevExpress.DataAccess.Expression("[Parameters.F_LANGTP]", typeof(string));
            storedProcQuery2.Parameters.Add(queryParameter5);
            storedProcQuery2.Parameters.Add(queryParameter6);
            storedProcQuery2.Parameters.Add(queryParameter7);
            storedProcQuery2.Parameters.Add(queryParameter8);
            storedProcQuery2.StoredProcName = "USP_MONITORING_MNTR0907_1_TWORST_CHART";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1,
            storedProcQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart3,
            this.xrChart2,
            this.xrChart1});
            this.Detail.HeightF = 830F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrChart1
            // 
            this.xrChart1.BorderColor = System.Drawing.Color.LightGray;
            this.xrChart1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrChart1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.xrChart1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.xrChart1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.xrChart1.Legend.EquallySpacedItems = false;
            this.xrChart1.Legend.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.xrChart1.Legend.MarkerSize = new System.Drawing.Size(15, 15);
            this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrChart1.Name = "xrChart1";
            this.xrChart1.PivotGridDataSourceOptions.AutoBindingSettingsEnabled = false;
            this.xrChart1.PivotGridDataSourceOptions.AutoLayoutSettingsEnabled = false;
            this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.xrChart1.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.xrChart1.SizeF = new System.Drawing.SizeF(320F, 270F);
            this.xrChart1.StylePriority.UseBorderColor = false;
            this.xrChart1.StylePriority.UseBorders = false;
            this.xrChart1.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart1_CustomDrawSeriesPoint);
            this.xrChart1.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.xrChart1_CustomDrawAxisLabel);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // StartDate
            // 
            this.StartDate.Description = "StartDate";
            this.StartDate.Name = "StartDate";
            this.StartDate.Visible = false;
            // 
            // EndDate
            // 
            this.EndDate.Description = "EndDate";
            this.EndDate.Name = "EndDate";
            this.EndDate.Visible = false;
            // 
            // Year
            // 
            this.Year.Description = "Year";
            this.Year.Name = "Year";
            this.Year.Type = typeof(int);
            this.Year.ValueInfo = "0";
            this.Year.Visible = false;
            // 
            // WeekOfYear
            // 
            this.WeekOfYear.Description = "WeekOfYear";
            this.WeekOfYear.Name = "WeekOfYear";
            this.WeekOfYear.Type = typeof(int);
            this.WeekOfYear.ValueInfo = "0";
            this.WeekOfYear.Visible = false;
            // 
            // F_COMPCD
            // 
            this.F_COMPCD.Description = "F_COMPCD";
            this.F_COMPCD.Name = "F_COMPCD";
            this.F_COMPCD.Visible = false;
            // 
            // F_FACTCD
            // 
            this.F_FACTCD.Description = "F_FACTCD";
            this.F_FACTCD.Name = "F_FACTCD";
            this.F_FACTCD.Visible = false;
            // 
            // F_LANGTP
            // 
            this.F_LANGTP.Description = "F_LANGTP";
            this.F_LANGTP.Name = "F_LANGTP";
            this.F_LANGTP.Visible = false;
            // 
            // xrChart2
            // 
            this.xrChart2.BorderColor = System.Drawing.Color.LightGray;
            this.xrChart2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrChart2.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.xrChart2.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.xrChart2.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.xrChart2.Legend.EquallySpacedItems = false;
            this.xrChart2.Legend.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.xrChart2.Legend.MarkerSize = new System.Drawing.Size(15, 15);
            this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 280F);
            this.xrChart2.Name = "xrChart2";
            this.xrChart2.PivotGridDataSourceOptions.AutoBindingSettingsEnabled = false;
            this.xrChart2.PivotGridDataSourceOptions.AutoLayoutSettingsEnabled = false;
            this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.xrChart2.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.xrChart2.SizeF = new System.Drawing.SizeF(320F, 270F);
            this.xrChart2.StylePriority.UseBorderColor = false;
            this.xrChart2.StylePriority.UseBorders = false;
            chartTitle1.Alignment = System.Drawing.StringAlignment.Near;
            chartTitle1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            chartTitle1.Text = "※미달기준 : Cpk 1.67↓";
            this.xrChart2.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            this.xrChart2.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart2_CustomDrawSeriesPoint);
            this.xrChart2.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.xrChart2_CustomDrawAxisLabel);
            // 
            // xrChart3
            // 
            this.xrChart3.BorderColor = System.Drawing.Color.LightGray;
            this.xrChart3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrChart3.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.xrChart3.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.xrChart3.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.xrChart3.Legend.EquallySpacedItems = false;
            this.xrChart3.Legend.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.xrChart3.Legend.MarkerSize = new System.Drawing.Size(15, 15);
            this.xrChart3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 560F);
            this.xrChart3.Name = "xrChart3";
            this.xrChart3.PivotGridDataSourceOptions.AutoBindingSettingsEnabled = false;
            this.xrChart3.PivotGridDataSourceOptions.AutoLayoutSettingsEnabled = false;
            this.xrChart3.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.xrChart3.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.xrChart3.SizeF = new System.Drawing.SizeF(320F, 270F);
            this.xrChart3.StylePriority.UseBorderColor = false;
            this.xrChart3.StylePriority.UseBorders = false;
            this.xrChart3.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart3_CustomDrawSeriesPoint);
            this.xrChart3.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.xrChart3_CustomDrawAxisLabel);
            // 
            // ProcessQualityReportSub2
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.ComponentStorage.Add(this.sqlDataSource1);
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.StartDate,
            this.EndDate,
            this.Year,
            this.WeekOfYear,
            this.F_COMPCD,
            this.F_FACTCD,
            this.F_LANGTP});
            this.Version = "14.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRChart xrChart1;
        private DevExpress.XtraReports.Parameters.Parameter StartDate;
        private DevExpress.XtraReports.Parameters.Parameter EndDate;
        private DevExpress.XtraReports.Parameters.Parameter Year;
        private DevExpress.XtraReports.Parameters.Parameter WeekOfYear;
        private DevExpress.XtraReports.Parameters.Parameter F_COMPCD;
        private DevExpress.XtraReports.Parameters.Parameter F_FACTCD;
        private DevExpress.XtraReports.Parameters.Parameter F_LANGTP;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRChart xrChart2;
        private DevExpress.XtraReports.UI.XRChart xrChart3;
    }
}
