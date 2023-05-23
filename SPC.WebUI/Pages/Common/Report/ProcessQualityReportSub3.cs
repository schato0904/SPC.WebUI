using System;
using DevExpress.XtraReports.UI;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.Common.Report
{
    public partial class ProcessQualityReportSub3 : DevExpress.XtraReports.UI.XtraReport
    {
        Functions.Dates fnDates = new Functions.Dates();

        public ProcessQualityReportSub3(Int32 Year, Int32 WeekOfYear, string F_STDT, string F_EDDT, string F_COMPCD, string F_FACTCD, string F_LANGTP)
        {
            InitializeComponent();

            this.Year.Value = Year;
            this.WeekOfYear.Value = WeekOfYear;
            this.StartDate.Value = F_STDT;
            this.EndDate.Value = F_EDDT;

            this.F_COMPCD.Value = F_COMPCD;
            this.F_FACTCD.Value = F_FACTCD;
            this.F_LANGTP.Value = F_LANGTP;
        }

        private void xrTableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).AddDays(-14).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(-14))
                );
        }

        private void xrTableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).AddDays(-7).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(-7))
                );
        }

        private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value))
                );
        }

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).AddDays(-14).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(-14))
                );
        }

        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).AddDays(-7).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(-7))
                );
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value))
                );
        }

        private void xrTableCell23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).AddDays(-14).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(-14))
                );
        }

        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).AddDays(-7).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value).AddDays(-7))
                );
        }

        private void xrTableCell25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = String.Format("{0}월 {1}주",
                Convert.ToDateTime(this.StartDate.Value).Month,
                fnDates.GetWeekOfMonth(Convert.ToDateTime(this.StartDate.Value))
                );
        }

        private void xrTableCell_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (!String.IsNullOrEmpty(cell.Text))
                cell.Text = String.Format("{0}%", cell.Text);
        }
    }
}
