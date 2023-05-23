using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Data;

namespace SPC.WebUI.Pages.MEAS.Report
{
    public partial class MEAS3005RPT : DevExpress.XtraReports.UI.XtraReport
    {
        string[] oParams;
      
        string sessionName = String.Empty;

        DataTable dtTable = null;
        DataTable dtTable1 = null;
        DataTable dtTable2 = null;
        DataTable dtTable3 = null;

        System.Drawing.Image dtimg = null;

        public MEAS3005RPT(string[] oParams, DataTable dtTable, DataTable dtTable1, DataTable dtTable2, DataTable dtTable3, System.Drawing.Image dtimg)
        {
            InitializeComponent();

            this.oParams = oParams;
            this.dtTable = dtTable;
            this.dtTable1 = dtTable1;
            this.dtTable2 = dtTable2;
            this.dtTable3 = dtTable3;
            this.dtimg = dtimg;
            //this.imgChartH = imgChartH;
            int tcnt = dtTable1.Rows.Count;

            if (dtTable1.Rows.Count < 10) {
                for (int j = 0; j < 10 - tcnt; j++)
                {
                    dtTable1.Rows.Add();
                }
            }
            /*
            XRTable xrTable1 = new XRTable();
            xrTable1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular);
            xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable1.BorderWidth = 1F;
            xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrTable1.BeginInit();

            XRTableRow xrRow1 = null;
            XRTableCell xrCell1 = null;

            // 첫줄
            xrRow1 = new XRTableRow();
            xrRow1.HeightF = 30F;
            
            xrCell1 = new XRTableCell();
            xrCell1.WidthF = 370F;
            xrCell1.Text = "점 검 개 소";
            xrCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrRow1.Cells.Add(xrCell1);

            xrCell1 = new XRTableCell();
            xrCell1.WidthF = 380F;
            xrCell1.Text = " 사진 ";
            xrCell1.Font = new Font("맑은 고딕", 8F, FontStyle.Underline);
            xrCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            xrRow1.Cells.Add(xrCell1);
            xrTable1.Rows.Add(xrRow1);

            // 2번째줄
            xrRow1 = new XRTableRow();
            xrRow1.HeightF = 120F;

            xrCell1 = new XRTableCell();
            xrCell1.WidthF = 370F;
            xrCell1.Text = "점검개소내용";
            xrCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            xrCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            xrRow1.Cells.Add(xrCell1);

            xrCell1 = new XRTableCell();

            XRPictureBox pictureBox = new XRPictureBox();
            pictureBox.WidthF = 380F;
            pictureBox.HeightF = 288F;

            int imageW = 0;
            int imageH = 0;
            int pictureBoxW = 380;
            int pictureBoxH = 290;

            if (dtimg != null)
            {
                imageW = dtimg.Width;
                imageH = dtimg.Height;

                Image resizeImg = dtimg;
                if (imageW > pictureBoxW || imageH > pictureBoxH)
                {
                    Decimal oSizeRatio = ((Decimal)pictureBoxW / pictureBoxH);
                    Decimal rSizeRatio = ((Decimal)imageW / imageH);

                    int resizeW, resizeH;

                    if (rSizeRatio > oSizeRatio)
                    {
                        resizeW = pictureBoxW;
                        resizeH = Decimal.ToInt32(pictureBoxW * imageH / imageW);
                    }
                    else
                    {
                        resizeW = Decimal.ToInt32(pictureBoxH * imageW / imageH);
                        resizeH = pictureBoxH;
                    }

                    resizeImg = (Image)(new Bitmap(resizeImg, new Size(resizeW, resizeH)));
                }

                pictureBox.Image = resizeImg;
                pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.CenterImage;
            }

            xrRow1.Cells.Add(pictureBox);
            xrTable1.Rows.Add(xrRow1);



            xrTable1.AdjustSize();
            xrTable1.EndInit();

            this.GroupHeader3.Controls.Add(xrTable1);
            this.GroupHeader3.HeightF = xrTable1.HeightF;
            */

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

                // 점검일자(교정일자)
                xrCell = new XRTableCell();
                xrCell.WidthF = 101F;
                xrCell.Text = this.dtTable1.Rows[idx][6].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 점검내용
                xrCell = new XRTableCell();
                xrCell.WidthF = 149F;
                xrCell.Text = this.dtTable1.Rows[idx][7].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 점검기관
                xrCell = new XRTableCell();
                xrCell.WidthF = 126F;
                xrCell.Text = this.dtTable1.Rows[idx][5].ToString();
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 확인(검인)
                xrCell = new XRTableCell();
                xrCell.WidthF = 99F;
                xrCell.Text = this.dtTable1.Rows[idx][2].ToString();
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 가격
                xrCell = new XRTableCell();
                xrCell.WidthF = 126F;
                xrCell.Text = String.Empty;
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                // 차기점검일(교정일)
                xrCell = new XRTableCell();
                xrCell.WidthF = 149F;
                xrCell.Text = this.dtTable1.Rows[idx][9].ToString();
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                if (idx.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail.Controls.Add(xrTable);
            this.Detail.HeightF = xrTable.HeightF;
            
        }

        // 검색정보
        private void xrTableCell_SetText(XRTableCell cell, int idx)
        {
            cell.Text = oParams[idx];
        }

        //설비명
        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 0);
        }

        //1. 규격
        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 1);
        }

        //2. 관리번호
        private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 2);
        }
        //3. 제조회사
        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 3);
        }
        //4. 측정단위
        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 4);
        }
        //5. 도입일자
        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 5);
        }
        //6. 구입가격
        private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 6);
        }
        //7. 교정구분
        private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 7);
        }
        //8 교정주기
        private void xrTableCell56_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell_SetText(sender as XRTableCell, 8);
        }
        //9 점검개소
        private void xrTableCell25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.HeightF = 30F;
            cell.Text = "";

            if (dtTable.Rows[0]["F_MEMO"].ToString().Length >= 90) {
                cell.Text = dtTable.Rows[0]["F_MEMO"].ToString();
            }
            else if (dtTable.Rows[0]["F_MEMO"].ToString().Length >= 60)
            {
                cell.Text = dtTable.Rows[0]["F_MEMO"].ToString().Substring(0, 30);
                cell.Text += " \n " + dtTable.Rows[0]["F_MEMO"].ToString().Substring(30, 30);
                cell.Text += " \n " + dtTable.Rows[0]["F_MEMO"].ToString().Substring(60, dtTable.Rows[0]["F_MEMO"].ToString().Length - 60);
            }
            else if (dtTable.Rows[0]["F_MEMO"].ToString().Length >= 30)
            {
                cell.Text = dtTable.Rows[0]["F_MEMO"].ToString().Substring(0, 30);
                cell.Text += " \n " + dtTable.Rows[0]["F_MEMO"].ToString().Substring(30, dtTable.Rows[0]["F_MEMO"].ToString().Length - 30);
                cell.Text += " \n ";
            }
            else {
                cell.Text = dtTable.Rows[0]["F_MEMO"].ToString();
                cell.Text += " \n ";
                cell.Text += " \n ";
            }
        }
        //10. 주요부속품
        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.HeightF = 30F;
            cell.Text = "";

            for (int i = 0; i < 3; i++) {
                if (dtTable3.Rows.Count <= i)
                {
                    cell.Text += " \n ";
                }
                else {
                    cell.Text += dtTable3.Rows[i]["F_TITLE"].ToString() + " \n ";
                }
            }

            //if (dtTable3.Rows.Count>0)
            //{
            //    for (int i = 0; i < dtTable3.Rows.Count; i++)
            //    {
            //        cell.Text += dtTable3.Rows[i]["F_TITLE"].ToString() + "\n";
            //    }
            //}
            //else {
            //    cell.Text += "\n\n";
            //}
        }

        private void xrPictureBox3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.WidthF = 380F;
            pictureBox.HeightF = 343F;
            //pictureBox.Image = dtimg;

            int imageW = 0;
            int imageH = 0;
            int pictureBoxW = 380;
            int pictureBoxH = 344;

            if (dtimg != null)
            {
                imageW = dtimg.Width;
                imageH = dtimg.Height;

                Image resizeImg = dtimg;
                if (imageW > pictureBoxW || imageH > pictureBoxH)
                {
                    Decimal oSizeRatio = ((Decimal)pictureBoxW / pictureBoxH);
                    Decimal rSizeRatio = ((Decimal)imageW / imageH);

                    int resizeW, resizeH;

                    resizeW = Decimal.ToInt32(pictureBoxH * imageW / imageH);
                    resizeH = pictureBoxH;

                    //if (rSizeRatio > oSizeRatio)
                    //{
                    //    resizeW = pictureBoxW;
                    //    resizeH = Decimal.ToInt32(pictureBoxW * imageH / imageW);
                    //}
                    //else
                    //{
                    //    resizeW = Decimal.ToInt32(pictureBoxH * imageW / imageH);
                    //    resizeH = pictureBoxH;
                    //}

                    resizeImg = (Image)(new Bitmap(resizeImg, new Size(resizeW, resizeH)));
                }

                pictureBox.Image = resizeImg;
                pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.CenterImage;
            }
        }

        private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.WidthF = 370F;
        }

        private void xrTableCell23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.WidthF = 380;
        }
    }
}
