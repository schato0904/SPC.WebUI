using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Data;

namespace SPC.WebUI.Pages.FITM.Report
{
    public partial class FITM0205RPT : DevExpress.XtraReports.UI.XtraReport
    {
        string[] oParams;
        /*
         *  0 : 작업일자
         *  1 : 품목코드
         *  2 : 공정코드
        */

        public FITM0205RPT(string[] oParams, DataSet ds)
        {
            InitializeComponent();

            this.oParams = oParams;

            //xrTable_SetText(xrTableCell4, 4);
            xrTable_SetText(xrTableCell8, ds.Tables[0].Rows[0]["F_WORKDATE"].ToString());
            xrTable_SetText(xrTableCell10, ds.Tables[0].Rows[0]["F_ITEMNM"].ToString());
            xrTable_SetText(xrTableCell24, ds.Tables[0].Rows[0]["F_TAGAKNO"].ToString());

            // 외관 초물1
            DataView dv;
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC502' AND F_FIRSTITEM = '0' AND F_NUMBER = 1 ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell32, drv["F_MEASURE"].ToString());
            }
            // 외관 초물2
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC502' AND F_FIRSTITEM = '0' AND F_NUMBER = 2 ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell31, drv["F_MEASURE"].ToString());
            }
            // 외관 종물1
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC502' AND F_FIRSTITEM = '2' AND F_NUMBER = 1 ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell33, drv["F_MEASURE"].ToString());
            }
            // 외관 종물2
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC502' AND F_FIRSTITEM = '2' AND F_NUMBER = 2 ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell28, drv["F_MEASURE"].ToString());
            }
            //하단 초물1
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '0' AND F_NUMBER = 1 AND F_INSPDETAIL = '도막두께 (하도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell61, drv["F_MEASURE"].ToString());
                xrTable_SetText(xrTableCell85, drv["F_STANDARD"].ToString());
            }
            //하단 초물2
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '0' AND F_NUMBER = 2 AND F_INSPDETAIL = '도막두께 (하도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell66, drv["F_MEASURE"].ToString());
            }
            //하단 종물1
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '2' AND F_NUMBER = 1 AND F_INSPDETAIL = '도막두께 (하도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell67, drv["F_MEASURE"].ToString());
            }
            //하단 종물2
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '2' AND F_NUMBER = 2 AND F_INSPDETAIL = '도막두께 (하도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell68, drv["F_MEASURE"].ToString());
            }
            //중단 초물1
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '0' AND F_NUMBER = 1 AND F_INSPDETAIL = '도막두께 (중도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell77, drv["F_MEASURE"].ToString());
                xrTable_SetText(xrTableCell84, drv["F_STANDARD"].ToString());
            }
            //중단 초물2
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '0' AND F_NUMBER = 2 AND F_INSPDETAIL = '도막두께 (중도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell78, drv["F_MEASURE"].ToString());
            }
            //중단 종물1
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '2' AND F_NUMBER = 1 AND F_INSPDETAIL = '도막두께 (중도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell80, drv["F_MEASURE"].ToString());
            }
            //중단 종물2
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '2' AND F_NUMBER = 2 AND F_INSPDETAIL = '도막두께 (중도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell81, drv["F_MEASURE"].ToString());
            }
            //상단 초물1
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '0' AND F_NUMBER = 1 AND F_INSPDETAIL = '도막두께 (상도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell91, drv["F_MEASURE"].ToString());
                xrTable_SetText(xrTableCell83, drv["F_STANDARD"].ToString());
            }
            //상단 초물2
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '0' AND F_NUMBER = 2 AND F_INSPDETAIL = '도막두께 (상도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell92, drv["F_MEASURE"].ToString());
            }
            //상단 종물1
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '2' AND F_NUMBER = 1 AND F_INSPDETAIL = '도막두께 (상도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell93, drv["F_MEASURE"].ToString());
            }
            //상단 종물2
            dv = new DataView(ds.Tables[0], "F_INSPCD = 'AAC501' AND F_FIRSTITEM = '2' AND F_NUMBER = 2 AND F_INSPDETAIL = '도막두께 (상도)' ", "F_DISPLAYNO, F_FIRSTITEM, F_NUMBER", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                xrTable_SetText(xrTableCell94, drv["F_MEASURE"].ToString());
            }
        }

        // 검색조건
        private void xrTable_SetText(XRTableCell cell, string strText)
        {
            cell.Text = strText;
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 인쇄일자
            XRLabel lbl = sender as XRLabel;
            lbl.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
        }



    }
}
