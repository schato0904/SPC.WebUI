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
    public partial class RptCATM1302 : DevExpress.XtraReports.UI.XtraReport
    {
        public Dictionary<string, string> dic = new Dictionary<string, string>();
        float RowHeight = 70F;

        XRTable xrTblData;


        public RptCATM1302(string[] oParams)
        {
            InitializeComponent();

            dic = new Dictionary<string, string>();

            dic["F_FROMDT"] = oParams[0];
            dic["F_TODT"] = oParams[1];
            dic["F_MACHCD"] = oParams[2];
            dic["F_ITEMCD"] = oParams[3];
            dic["F_WORKNO"] = oParams[4];
            dic["F_LOTNO"] = oParams[5];
            dic["F_MOLDNO"] = oParams[6];

         

        }

        private void RptCATM1302_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


            XRControl xrcontrol = new XRControl();
            //xrSubreport1
            



            string errMsg = string.Empty;

            DataSet dt = this.GetData(this.dic, out errMsg);


            DataTable dt1 = dt.Tables[0];



            int drc = dt1.Rows.Count;


            AddNewTableAndHeader(dt1);

            

            foreach (DataRow dr in dt1.Rows)
            {
                XRTableRow tr = this.MakeRow(dr);

                this.xrTblData.Rows.Add(tr);
                this.xrTblData.HeightF += this.RowHeight;
                this.SubBand2.HeightF += this.RowHeight;

            }
        


        }


        void AddNewTableAndHeader(DataTable dt)
        {

            XRTableRow HeadRow1 = new XRTableRow();
            XRTableRow HeadRow2 = new XRTableRow();
            List<XRTableCell> cells = new List<XRTableCell>();


            this.xrTblData = new XRTable();

            this.SubBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { this.xrTblData });

            

            this.xrTblData.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
           | DevExpress.XtraPrinting.BorderSide.Right)
           | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblData.Dpi = 254F;
            this.xrTblData.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.xrTblData.LocationFloat = new DevExpress.Utils.PointFloat(400F, 5F);
            this.xrTblData.Name = "xrTblData";
            this.xrTblData.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] { HeadRow1, HeadRow2 });
            this.xrTblData.SizeF = new System.Drawing.SizeF(1700F, 140F);
            
            this.xrTblData.StylePriority.UseBorders = false;
            this.xrTblData.StylePriority.UseFont = false;
            this.xrTblData.StylePriority.UseTextAlignment = false;
            this.xrTblData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            

            HeadRow1.Weight = 1D;
            HeadRow2.Weight = 1D;

            cells.Add(this.GetNewCell("작업일자", 0.5D, 2));
            cells.Add(this.GetNewCell("설비명", 0.5D, 2));
            cells.Add(this.GetNewCell("작업자", 0.5D, 2));
            cells.Add(this.GetNewCell("품번", 0.5D, 2));
            cells.Add(this.GetNewCell("지시수량", 0.5D, 2));
            cells.Add(this.GetNewCell("생산수량", 0.5D, 2));

            cells.Add(this.GetNewCell("불량유형", 1D, 2));

            cells.Add(this.GetNewCell("비고", 0.5D, 2));

            HeadRow1.Cells.AddRange(cells.ToArray());



            cells.Clear();


            cells.Add(this.GetNewCell("", 0.5D, 1));
            cells.Add(this.GetNewCell("", 0.5D, 1));
            cells.Add(this.GetNewCell("", 0.5D, 1));
            cells.Add(this.GetNewCell("", 0.5D, 1));
            cells.Add(this.GetNewCell("", 0.5D, 1));
            cells.Add(this.GetNewCell("", 0.5D, 1));

            cells.Add(this.GetNewCell("미성형", 0.5D, 1));
            cells.Add(this.GetNewCell("중자파손", 0.5D, 1));

            cells.Add(this.GetNewCell("", 0.5D, 1));


            HeadRow2.Cells.AddRange(cells.ToArray());



        }



       




        XRTableCell GetNewCell(object objText, double weight, int rowspan = 1, float dpi = 254F)
        {
            XRTableCell c = new XRTableCell();
            c.Dpi = dpi;
            c.Text = (objText ?? "").ToString();
            c.Weight = weight;
            c.RowSpan = rowspan;
            return c;
        }
        


        XRTableRow MakeRow(DataRow dr)
        {
            XRTableRow xrTemplateRow = new XRTableRow();
            xrTemplateRow.Dpi = 254F;
            xrTemplateRow.Weight = 1D;

            List<XRTableCell> cells = new List<XRTableCell>();
            cells.Add(this.GetNewCell(dr["F_ENDYMD"], 0.5D));
            cells.Add(this.GetNewCell(dr["F_MACHNM"], 0.5D));
            cells.Add(this.GetNewCell(dr["F_WORKER1"], 0.5D));
            cells.Add(this.GetNewCell(dr["F_ITEMCD"], 0.5D));
            cells.Add(this.GetNewCell(dr["F_PLANCOUNT"], 0.5D));
            cells.Add(this.GetNewCell(dr["F_PRODCOUNT"], 0.5D));
            cells.Add(this.GetNewCell(dr["F_ERR1"], 0.5D));
            cells.Add(this.GetNewCell(dr["F_ERR2"], 0.5D));
            cells.Add(this.GetNewCell(dr["F_MEMO"], 0.5D));


            xrTemplateRow.Cells.AddRange(cells.ToArray());



            return xrTemplateRow;
        }

        public DataTable AutoNumberTable(DataTable dt, string FieldName = "No")
        {
            DataTable ndt = new DataTable();

            while (dt.Columns.Contains(FieldName))
            {
                FieldName = FieldName + "_1";
            }

            var col = ndt.Columns.Add(FieldName, typeof(int));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 1;
            col.AutoIncrementStep = 1;

            ndt.Merge(dt);

            return ndt;
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
                oParamDic["F_FROMDT"] = dic.GetString("F_FROMDT");
                oParamDic["F_TODT"] = dic.GetString("F_TODT");
                oParamDic["F_MACHCD"] = dic.GetString("F_MACHCD");
                oParamDic["F_ITEMCD"] = dic.GetString("F_ITEMCD");
                oParamDic["F_WORKNO"] = dic.GetString("F_WORKNO");
                oParamDic["F_LOTNO"] = dic.GetString("F_LOTNO");
                oParamDic["F_MOLDNO"] = dic.GetString("F_MOLDNO");
                oParamDic["F_COMPCD"] = "01";
                oParamDic["F_FACTCD"] = "01";
                ds = biz.USP_CATM1302_RPT(oParamDic, out errMsg);

            }

            //dt = this.AutoNumberTable(ds.Tables[0]);




            return ds;

        }


    }
}
