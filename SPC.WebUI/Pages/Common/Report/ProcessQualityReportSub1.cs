using System;
using DevExpress.XtraReports.UI;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.Common.Report
{
    public partial class ProcessQualityReportSub1 : DevExpress.XtraReports.UI.XtraReport
    {
        public ProcessQualityReportSub1(Int32 Year, Int32 WeekOfYear, string F_STDT, string F_EDDT, string F_COMPCD, string F_FACTCD, string F_LANGTP)
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

        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            label.Text = String.Format("공정{0}불량{0}현황", Environment.NewLine);
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            label.Text = String.Format("공정{0}능력{0}미달", Environment.NewLine);
        }

        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            label.Text = String.Format("DATA{0}수신{0}현황", Environment.NewLine);
        }

    }
}
