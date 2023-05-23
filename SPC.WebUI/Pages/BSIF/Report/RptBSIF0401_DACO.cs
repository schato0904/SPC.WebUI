using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SPC.WebUI.Pages.BSIF.Report
{
    public partial class RptBSIF0401_DACO : DevExpress.XtraReports.UI.XtraReport
    {

        public string dic;

        public RptBSIF0401_DACO(string _dic)
        {
            InitializeComponent();

            
            
            this.dic = _dic;
        }

        private void RptBSIF0401_DACO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.barF_QRCODE.Text = dic;
        }

    }
}