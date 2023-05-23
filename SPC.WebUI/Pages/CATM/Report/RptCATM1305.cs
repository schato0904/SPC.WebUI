using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using SPC.WebUI.Common.Library;
using DevExpress.XtraReports.UI;

namespace SPC.WebUI.Pages.CATM.Report
{
    public partial class RptCATM1305 : DevExpress.XtraReports.UI.XtraReport
    {
        public Dictionary<string, string> dic = new Dictionary<string, string>();

        public RptCATM1305(Dictionary<string, string> dic)
        {
            InitializeComponent();
            this.dic = dic;
        }

        private void RptCATM1305_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 용해 작업일보
            this.xrSubreport1.ReportSource = new RptCATM1305_1(this.dic);
            // 주조 작업일보
            this.xrSubreport2.ReportSource = new RptCATM1305_2(this.dic);
        } 
    }
}
