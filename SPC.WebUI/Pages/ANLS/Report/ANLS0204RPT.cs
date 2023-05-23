using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Data;

namespace SPC.WebUI.Pages.ANLS.Report
{
    public partial class ANLS0204RPT : DevExpress.XtraReports.UI.XtraReport
    {
        string[] oParams;
        /*
         *  0 : 업체코드
         *  1 : 업체명
         *  2 : 조회시작일
         *  3 : 조회종료일
         *  4 : 품목코드
         *  5 : 품목명
         *  6 : 공정코드
         *  7 : 공정명
         *  8 : 항목코드
         *  9 : 항목명
         * 10 : 검사규격
         * 11 : 상한규격
         * 12 : 하한규격
         * 13 : 조회시작일
         * 14 : 조회종료일
         * 15 : 품목코드
         * 16 : 품목명
         * 17 : 공정코드
         * 18 : 공정명
         * 19 : 항목코드
         * 20 : 항목명
         * 21 : 검사규격
         * 22 : 상한규격
         * 23 : 하한규격
         */

        DataTable dtTable1 = null;

        Image imgChart1 = null;
        Image imgChart2 = null;

        public ANLS0204RPT(string[] oParams, DataTable dtTable1, Image imgChart1, Image imgChart2)
        {
            InitializeComponent();

            this.oParams = oParams;
            this.dtTable1 = dtTable1;
            this.imgChart1 = imgChart1;
            this.imgChart2 = imgChart2;

            // 집단1(검색기간)
            xrTableCell8.Text = String.Format("{0} ~ {1}", oParams[2], oParams[3]);
            // 집단1(품목코드)
            xrTable_SetText(xrTableCell4, 4);
            // 집단1(품목명)
            xrTable_SetText(xrTableCell16, 5);
            // 집단1(공정명)
            xrTable_SetText(xrTableCell22, 7);
            // 집단1(검사항목)
            xrTable_SetText(xrTableCell26, 9);
            // 집단1(규격)
            xrTable_SetText(xrTableCell31, 10);
            // 집단1(상한)
            xrTable_SetText(xrTableCell35, 11);
            // 집단1(하한)
            xrTable_SetText(xrTableCell39, 12);
            // 집단2(검색기간)
            xrTableCell10.Text = String.Format("{0} ~ {1}", oParams[13], oParams[14]);
            // 집단2(품목코드)
            xrTable_SetText(xrTableCell14, 15);
            // 집단2(품목명)
            xrTable_SetText(xrTableCell20, 16);
            // 집단2(공정명)
            xrTable_SetText(xrTableCell24, 18);
            // 집단2(검사항목)
            xrTable_SetText(xrTableCell29, 20);
            // 집단2(규격)
            xrTable_SetText(xrTableCell33, 21);
            // 집단2(상한)
            xrTable_SetText(xrTableCell37, 22);
            // 집단2(하한)
            xrTable_SetText(xrTableCell41, 23);

            XRTable xrTable = new XRTable();
            xrTable.Font = new Font("맑은 고딕", 8F, FontStyle.Regular);
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable.BorderWidth = 1F;
            xrTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrTable.BeginInit();

            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            for (int idx = 0; idx < this.dtTable1.Rows.Count; idx++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 25F;

                // No.
                xrCell = new XRTableCell();
                xrCell.WidthF = 30F;
                xrCell.Text = this.dtTable1.Rows[idx][0].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 구분
                xrCell = new XRTableCell();
                xrCell.WidthF = 200F;
                xrCell.Text = this.dtTable1.Rows[idx][1].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 집단1
                xrCell = new XRTableCell();
                xrCell.WidthF = 120F;
                xrCell.Text = this.dtTable1.Rows[idx][2].ToString();
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 집단2
                xrCell = new XRTableCell();
                xrCell.WidthF = 120F;
                xrCell.Text = this.dtTable1.Rows[idx][3].ToString();
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 비고
                xrCell = new XRTableCell();
                xrCell.WidthF = 280F;
                xrCell.Text = String.Empty;
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail1.Controls.Add(xrTable);
            this.Detail1.HeightF = xrTable.HeightF;
        }

        // 검색조건
        private void xrTable_SetText(XRTableCell cell, int idx)
        {
            cell.Text = oParams[idx];
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 인쇄일자
            XRLabel lbl = sender as XRLabel;
            lbl.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
        }

        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 업체명
            XRLabel xrLabel = sender as XRLabel;
            xrLabel.Text = oParams[1];
        }

        private void xrPictureBox3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Image = this.imgChart1;
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Image = this.imgChart2;
        }

    }
}
