using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Data;

namespace SPC.WebUI.Pages.TISP.Report
{
    public partial class TISP0104RPT : DevExpress.XtraReports.UI.XtraReport
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

        Image imgChartX = null;
        Image imgChartR = null;

        public TISP0104RPT(string[] oParams, Image imgChartX, Image imgChartR)
        {
            InitializeComponent();

            this.oParams = oParams;
            this.imgChartX = imgChartX;
            this.imgChartR = imgChartR;
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
            XRLabel xrLabel = sender as XRLabel;
            xrLabel.Text = String.Format("({0})", oParams[2], oParams[3]);
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
