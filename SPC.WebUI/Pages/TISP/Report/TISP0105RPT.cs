using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Data;

namespace SPC.WebUI.Pages.TISP.Report
{
    public partial class TISP0105RPT : DevExpress.XtraReports.UI.XtraReport
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
         * 11 : 시료수
         * 12 : 검사규격
         * 13 : 상한규격
         * 14 : 하한규격
         * 15 : 항목순번
         * 16 : 평가방법
         */
        string sessionName = String.Empty;

        DataTable dtTable1 = null;
        DataTable dtTable2 = null;
        DataTable dtTable3 = null;

        Image imgChartH = null;

        public TISP0105RPT(string[] oParams, DataTable dtTable1, DataTable dtTable2, DataTable dtTable3, Image imgChartH)
        {
            InitializeComponent();

            this.oParams = oParams;
            this.dtTable1 = dtTable1;
            this.dtTable2 = dtTable2;
            this.dtTable3 = dtTable3;
            this.imgChartH = imgChartH;
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
            // 품목명
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
            xrTableCell_SetText(sender as XRTableCell, 11);
        }

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 검사규격
            xrTableCell_SetText(sender as XRTableCell, 12);
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 상한규격
            xrTableCell_SetText(sender as XRTableCell, 13);
        }

        private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 하한규격
            xrTableCell_SetText(sender as XRTableCell, 14);
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 평가방법
            XRTableCell cell = sender as XRTableCell;
            cell.Text = oParams[16].Equals("1") ? "Cp" : "Cpk";
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
            lbl.Text = String.Format("({0})", oParams[2]);
        }

        private void xrPictureBox3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Image = this.imgChartH;
        }


        // 분석결과
        private void xrTableCell_SetResultText(XRTableCell cell, int idx, int digit)
        {
            string format = String.Empty;

            if (digit == 0)
                format = "#,##0";
            else if (digit == 2)
                format = "#,##0.#0";
            else if (digit == 3)
                format = "#,##0.##0";
            else if (digit == 4)
                format = "#,##0.###0";

            object value = dtTable2.Rows[0][idx];
            if (idx == 12 || idx == 13)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                    value = "0";
            }

            cell.Text = Convert.ToDecimal(value).ToString(format);
        }

        private void xrTableCell33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 최대치
            xrTableCell_SetResultText(sender as XRTableCell, 0, 3);
        }

        private void xrTableCell35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 최소치
            xrTableCell_SetResultText(sender as XRTableCell, 1, 3);
        }

        private void xrTableCell37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // X평균
            xrTableCell_SetResultText(sender as XRTableCell, 2, 4);
        }

        private void xrTableCell39_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 범위
            xrTableCell_SetResultText(sender as XRTableCell, 3, 3);
        }

        private void xrTableCell41_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // Cp
            xrTableCell_SetResultText(sender as XRTableCell, 4, 2);
        }

        private void xrTableCell43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // Cpk
            xrTableCell_SetResultText(sender as XRTableCell, 5, 2);
        }

        private void xrTableCell45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 6시그마(장)
            xrTableCell_SetResultText(sender as XRTableCell, 6, 3);
        }

        private void xrTableCell47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 6시그마(단)
            xrTableCell_SetResultText(sender as XRTableCell, 7, 3);
        }

        private void xrTableCell49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 표준편차
            xrTableCell_SetResultText(sender as XRTableCell, 8, 4);
        }

        private void xrTableCell51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 예상부적합율
            xrTableCell_SetResultText(sender as XRTableCell, 10, 3);
        }

        private void xrTableCell53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 예상수율
            xrTableCell_SetResultText(sender as XRTableCell, 9, 3);
        }

        private void xrTableCell55_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 예상PPM
            xrTableCell_SetResultText(sender as XRTableCell, 11, 0);
        }

        private void xrTableCell57_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 상한추정불량
            xrTableCell_SetResultText(sender as XRTableCell, 12, 2);
        }

        private void xrTableCell59_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 하한추정불량
            xrTableCell_SetResultText(sender as XRTableCell, 13, 2);
        }

        private void xrTableCell60_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 검사횟수
            xrTableCell_SetResultText(sender as XRTableCell, 15, 0);
        }

        private void xrTableCell62_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 규격이탈수
            xrTableCell_SetResultText(sender as XRTableCell, 16, 0);
        }

        private void xrTableCell_SetJudgeText(XRTableCell cell, int idx)
        {
            cell.Text = dtTable3.Rows[0][idx].ToString();
        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 평가
            xrTableCell_SetJudgeText(sender as XRTableCell, 5);
        }

        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 조치
            xrTableCell_SetJudgeText(sender as XRTableCell, 6);
        }

    }
}
