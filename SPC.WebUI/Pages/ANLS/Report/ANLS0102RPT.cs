using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Data;

namespace SPC.WebUI.Pages.ANLS.Report
{
    public partial class ANLS0102RPT : DevExpress.XtraReports.UI.XtraReport
    {
        string[] oParams;
        /*
         *  0 : 업체코드
         *  1 : 업체명
         *  2 : 조회시작일
         *  3 : 조회종료일
         *  4 : 품목코드
         *  5 : 품목명
         *  6 : 모델명
         *  7 : 공정코드
         *  8 : 공정명
         *  9 : 항목코드
         * 10 : 항목명
         * 11 : 관리식 or 계산식
         * 12 : 시료수
         * 13 : 검사규격
         * 14 : 상한규격
         * 15 : 하한규격
         * 16 : UCLR
         * 17 : UCLX
         * 18 : LCLX
         * 19 : 항목순번
         * 20 : 규격이탈제외
         */
        string sessionName = String.Empty;

        DataTable dtTable1 = null;
        DataTable dtTable3 = null;

        DataTable dtGrid = null;

        Image imgChartX = null;
        Image imgChartR = null;

        public ANLS0102RPT(string[] oParams, DataTable dtTable1, DataTable dtTable3, Image imgChartX, Image imgChartR)
        {
            InitializeComponent();

            this.oParams = oParams;
            this.dtTable1 = dtTable1;
            this.dtTable3 = dtTable3;
            this.imgChartX = imgChartX;
            this.imgChartR = imgChartR;

            // 데이타 구성용 DataTable
            Int32 idx = 0, siryo = 0;
            this.dtGrid = new DataTable();
            this.dtGrid.Columns.Add("WorkDT", typeof(String));
            this.dtGrid.Columns.Add("WorkTM", typeof(String));
            this.dtGrid.Columns.Add("Xbar", typeof(String));
            this.dtGrid.Columns.Add("R", typeof(String));
            for (idx = 0; idx < Convert.ToInt32(oParams[12]); idx++)
            {
                this.dtGrid.Columns.Add(String.Format("X{0}", idx + 1), typeof(String));
            }

            foreach (DataRow dtRow1 in dtTable1.Rows)
            {
                // 데이타 구성용 DataRow
                idx = 0;
                DataRow dtNewRow = dtGrid.NewRow();
                dtNewRow["WorkDT"] = String.Format("{0}({1})", DateTime.Parse(dtRow1["F_WORKDATE"].ToString()).ToString("MM/dd"), dtRow1["F_TSERIALNO"]);
                dtNewRow["WorkTM"] = dtRow1["F_WORKTIME"].ToString();
                dtNewRow["Xbar"] = Math.Round(Convert.ToDecimal(dtRow1["F_XBAR"].ToString()), 4).ToString();
                dtNewRow["R"] = Math.Round(Convert.ToDecimal(dtRow1["F_XRANGE"].ToString()), 4).ToString();
                foreach (DataRow dtRow3 in this.dtTable3.Select(String.Format("F_WORKDATE='{0}' AND F_TSERIALNO='{1}'", dtRow1["F_WORKDATE"], dtRow1["F_TSERIALNO"])))
                {
                    dtNewRow[String.Format("X{0}", ++idx)] = Math.Round(Convert.ToDecimal(dtRow3["F_MEASURE"].ToString()), 3).ToString();
                }
                this.dtGrid.Rows.Add(dtNewRow);
            }

            DataColumnCollection columns = this.dtGrid.Columns;

            XRTable xrTable = new XRTable();
            xrTable.Font = new Font("맑은 고딕", 8F, FontStyle.Regular);
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable.BorderWidth = 1F;
            xrTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrTable.BeginInit();

            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            for (idx = 0; idx < this.dtGrid.Rows.Count; idx++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 25F;

                // 작업일자
                xrCell = new XRTableCell();
                xrCell.WidthF = 70F;
                xrCell.Text = this.dtGrid.Rows[idx]["WorkDT"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 작업시간
                xrCell = new XRTableCell();
                xrCell.WidthF = 56F;
                xrCell.Text = this.dtGrid.Rows[idx]["WorkTM"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // Xbar
                xrCell = new XRTableCell();
                xrCell.WidthF = 52F;
                xrCell.Text = this.dtGrid.Rows[idx]["Xbar"].ToString();
                xrCell.Font = new Font("맑은 고딕", 7F, FontStyle.Regular);
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // R
                xrCell = new XRTableCell();
                xrCell.WidthF = 52F;
                xrCell.Text = this.dtGrid.Rows[idx]["R"].ToString();
                xrCell.Font = new Font("맑은 고딕", 7F, FontStyle.Regular);
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 검사결과
                for (siryo = 0; siryo < 10; siryo++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 52F;
                    xrCell.Text = !columns.Contains(String.Format("X{0}", siryo + 1)) ? String.Empty : this.dtGrid.Rows[idx][String.Format("X{0}", siryo + 1)].ToString();
                    xrCell.Font = new Font("맑은 고딕", 7F, FontStyle.Regular);
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                    if (idx.Equals(0))
                    {
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                    }
                    xrRow.Cells.Add(xrCell);
                }

                xrTable.Rows.Add(xrRow);
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail1.Controls.Add(xrTable);
            this.Detail1.HeightF = xrTable.HeightF;
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 인쇄일자
            XRLabel lbl = sender as XRLabel;
            lbl.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
        }

        // 검색정보
        private void xrTableCell_SetText(XRTableCell cell, int idx)
        {
            cell.Text = oParams[idx];
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 업체명
            xrTableCell_SetText(sender as XRTableCell, 5);
        }

        private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 기종명
            xrTableCell_SetText(sender as XRTableCell, 6);
        }

        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 공정명
            xrTableCell_SetText(sender as XRTableCell, 8);
        }

        private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 검사항목명
            xrTableCell_SetText(sender as XRTableCell, 10);
        }

        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 시료수
            xrTableCell_SetText(sender as XRTableCell, 12);
        }

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 검사규격
            xrTableCell_SetText(sender as XRTableCell, 13);
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 상한규격
            xrTableCell_SetText(sender as XRTableCell, 14);
        }

        private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 하한규격
            xrTableCell_SetText(sender as XRTableCell, 15);
        }

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 관리식 OR 계산식
            xrTableCell_SetText(sender as XRTableCell, 11);
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // UCLR
            xrTableCell_SetText(sender as XRTableCell, 16);
        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // UCLX
            xrTableCell_SetText(sender as XRTableCell, 17);
        }

        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // LCLX
            xrTableCell_SetText(sender as XRTableCell, 18);
        }

        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 업체명
            XRLabel xrLabel = sender as XRLabel;
            xrLabel.Text = oParams[1];
        }

        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 조회기간
            XRLabel lbl = sender as XRLabel;
            lbl.Text = String.Format("({0} ~ {1})", oParams[2], oParams[3]);
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Image = this.imgChartX;
        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Image = this.imgChartR;
        }

    }
}
