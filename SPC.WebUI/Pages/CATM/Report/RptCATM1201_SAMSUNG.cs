using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SPC.WebUI.Pages.CATM.Report
{
    public partial class RptCATM1201_SAMSUNG : DevExpress.XtraReports.UI.XtraReport
    {
        //public RptCATM1201(string f_compcd, string f_factcd, string f_workno, string f_langtype)
        //{
        //    InitializeComponent();
        //    this.F_COMPCD.Value = f_compcd;
        //    this.F_FACTCD.Value = f_factcd;
        //    this.F_WORKNO.Value = f_workno;
        //    this.F_LANGTYPE.Value = f_langtype;
        //}
        public Dictionary<string, string> dic = new Dictionary<string, string>();

        public RptCATM1201_SAMSUNG(Dictionary<string, string> dic)
        {
            InitializeComponent();
            this.dic = dic;
        }

        private void RptCATM1201_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.xrLabel2.Text = dic["F_WORKNO"];
            this.xrLabel3.Text = dic["F_PLANYMD"];
            this.xrLabel4.Text = dic["F_MACHNM"];
            this.xrLabel5.Text = dic["F_MELTNM"];
            this.xrLabel6.Text = dic["F_ITEMCD"];
            this.xrLabel7.Text = dic["F_CAVITY"];
            this.xrLabel8.Text = dic["F_MOLDNO"];
            this.xrLabel9.Text = dic["F_MOLDNTH"];
            int pc = int.TryParse(dic["F_PLANCOUNT"], out pc) ? pc : 0;
            this.xrLabel10.Text = pc.ToString("#,0");
        } 

    }
}
