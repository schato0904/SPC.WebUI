using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using DevExpress.XtraReports.UI;

using SPC.CATM.Biz;
using SPC.WebUI.Common.Library;

namespace SPC.WebUI.Pages.CATM.Report
{
    public partial class RptCATM1211_2 : DevExpress.XtraReports.UI.XtraReport
    {

        public Dictionary<string, string> dic = new Dictionary<string, string>();
        float RowHeight = 70F;

        XRTable xrTblData;

        public RptCATM1211_2(Dictionary<string, string> dic)
        {
            InitializeComponent();

            this.dic = dic;


        }

        private void RptCATM1211_2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string errMsg = string.Empty;

            this.xrTableCell13.Text = dic["F_ITEMCD"];
            this.xrTableCell14.Text = dic["F_MELTNM"];
            this.xrLabel4.Text = dic["F_ENDYMD"];
            this.xrLabel3.Text = dic["F_WORKER"];
            this.xrLabel2.Text = dic["F_MACHNM"];

            DataSet dt = this.GetData(this.dic, out errMsg);

            DataTable dt1 = dt.Tables[0];

            string temp = dt1.Rows[0][0].ToString();
            string wait = dt1.Rows[0][1].ToString();
            string slope = dt1.Rows[0][2].ToString();
            

            this.xrTableCell27.Text = temp;
            this.xrTableCell34.Text = wait;
            this.xrTableCell78.Text = slope;

            
        }

      

        protected DataSet GetData(Dictionary<string, string> dic, out string errMsg)
        {




            

            

            errMsg = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            Dictionary<string, string> oParamDic = new Dictionary<string, string>();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_COMPCD"] = "01";
                oParamDic["F_FACTCD"] = "01";
                oParamDic["F_FROMDT"] = dic.GetString("F_FROMDT");
                oParamDic["F_TODT"] = dic.GetString("F_TODT");
                oParamDic["F_MACHCD"] = dic.GetString("F_MACHCD");
                oParamDic["F_ITEMCD"] = dic.GetString("F_ITEMCD");
                oParamDic["F_WORKNO"] = dic.GetString("F_WORKNO");
                oParamDic["F_LOTNO"] = dic.GetString("F_LOTNO");
                oParamDic["F_MOLDNO"] = dic.GetString("F_MOLDNO");
                

                ds = biz.USP_CATM1211_2_RPT(oParamDic, out errMsg);
            }

            return ds;
        }

       

    }
}
