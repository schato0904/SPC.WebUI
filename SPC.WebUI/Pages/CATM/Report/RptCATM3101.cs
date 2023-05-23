using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SPC.WebUI.Pages.CATM.Report
{
    public partial class RptCATM3101 : DevExpress.XtraReports.UI.XtraReport
    {
        //public RptCATM3101(string f_compcd, string f_factcd, string f_outorderno, string f_langtype)
        //{
        //    InitializeComponent();
        //    this.F_COMPCD.Value = f_compcd;
        //    this.F_FACTCD.Value = f_factcd;
        //    this.F_OUTORDERNO.Value = f_outorderno;
        //    this.F_LANGTYPE.Value = f_langtype;
        //} 
        
        public Dictionary<string, string> dic = new Dictionary<string, string>();

        public RptCATM3101(Dictionary<string, string> dic)
        {
            InitializeComponent();
            this.dic = dic;
        }

        private void RptCATM3101_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {   
            this.lblF_FROMCUSTNM.Text = dic["F_FROMCUSTNM"];
            this.lblF_ORDERNO.Text = dic["F_ORDERNO"];
            this.lblF_ITEMNM.Text = dic["F_ITEMNM"];
            this.lblF_ITEMCD.Text = dic["F_ITEMCD"];
            int oc = int.TryParse(dic["F_OUTCOUNT"], out oc) ? oc : 0;
            this.lblF_OUTCOUNT.Text = oc.ToString("#,0");
            this.lblF_OUTYMD.Text = dic["F_OUTYMD"];
            this.lblF_OUTORDERNO.Text = dic["F_OUTORDERNO"];

            this.barF_ORDERNO.Text = dic["F_ORDERNO"];
            this.barF_OUTORDERNO.Text = dic["F_OUTORDERNO"];
        } 
    }
}
