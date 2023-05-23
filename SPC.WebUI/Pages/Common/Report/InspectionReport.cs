using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using DevExpress.XtraReports.UI;
using SPC.SPMT.Biz;

namespace SPC.WebUI.Pages.Common.Report
{
    public partial class InspectionReport : XtraReport
    {
        public InspectionReport(string F_GROUPCD, string F_COMPCD, string F_COMPNM, string F_FACTCD, string F_LANGTYPE)
        {
            InitializeComponent();

            this.F_GROUPCD.Value = F_GROUPCD;
            this.F_COMPCD.Value = F_COMPCD;
            this.F_COMPNM.Value = F_COMPNM;
            this.F_FACTCD.Value = F_FACTCD;
            this.F_LANGTYPE.Value = F_LANGTYPE;
            this.DisplayName = String.Format("[{0}]{1}", F_COMPNM, F_GROUPCD);
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            label.Text = String.Format("작성 : {0}", label.Text);
        }

        private void xrTableCell35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            LineBreak(sender);
        }

        private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            LineBreak(sender);
        }

        private void xrTableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            LineBreak(sender);
        }

        private void LineBreak(object sender)
        {
            XRTableCell tableCell = sender as XRTableCell;
            tableCell.Text = tableCell.Text.Replace("|", Environment.NewLine);
        }

        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell tableCell = sender as XRTableCell;
            tableCell.Text = String.Format("{0}      (인)", tableCell.Text);
        }
    }
}