using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraReports.UI;
using CTF.Web.Framework.Helper;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.Common.Report
{
    public partial class ProcessQualityReport : DevExpress.XtraReports.UI.XtraReport
    {
        Functions.Dates fnDates = new Functions.Dates();

        public ProcessQualityReport(Int32 Year, Int32 WeekOfYear, string F_COMPCD, string F_FACTCD, string F_LANGTP)
        {
            InitializeComponent();

            this.Year.Value = Year;
            this.WeekOfYear.Value = WeekOfYear;

            Dictionary<string, string> oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_YEAR", Year.ToString());
            oParamDic.Add("F_WEEK", WeekOfYear.ToString().PadLeft(2, '0'));

            DataSet ds = null;
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                ds = biz.GetQCD107_GET(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dtRow = ds.Tables[0].Rows[0];
                this.StartDate.Value = dtRow["F_STDT"];
                this.EndDate.Value = dtRow["F_EDDT"];
            }
            else
            {
                DateTime startingDate = fnDates.GetFirstDayOfWeek(Convert.ToInt32(this.Year.Value), Convert.ToInt32(this.WeekOfYear.Value));
                this.StartDate.Value = startingDate.ToString("yyyy-MM-dd");
                this.EndDate.Value = startingDate.AddDays(7).ToString("yyyy-MM-dd");
            }

            this.F_COMPCD.Value = F_COMPCD;
            this.F_FACTCD.Value = F_FACTCD;
            this.F_LANGTP.Value = F_LANGTP;

            xrSubreport1.ReportSource = new ProcessQualityReportSub1(Convert.ToInt32(this.Year.Value), Convert.ToInt32(this.WeekOfYear.Value), this.StartDate.Value.ToString(), this.EndDate.Value.ToString(), this.F_COMPCD.Value.ToString(), this.F_FACTCD.Value.ToString(), this.F_LANGTP.Value.ToString());
            xrSubreport2.ReportSource = new ProcessQualityReportSub2(Convert.ToInt32(this.Year.Value), Convert.ToInt32(this.WeekOfYear.Value), this.StartDate.Value.ToString(), this.EndDate.Value.ToString(), this.F_COMPCD.Value.ToString(), this.F_FACTCD.Value.ToString(), this.F_LANGTP.Value.ToString());
            xrSubreport3.ReportSource = new ProcessQualityReportSub3(Convert.ToInt32(this.Year.Value), Convert.ToInt32(this.WeekOfYear.Value), this.StartDate.Value.ToString(), this.EndDate.Value.ToString(), this.F_COMPCD.Value.ToString(), this.F_FACTCD.Value.ToString(), this.F_LANGTP.Value.ToString());
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            label.Text = String.Format("산출기간 : {0} ~ {1}", this.StartDate.Value, this.EndDate.Value);
        }
    }
}
