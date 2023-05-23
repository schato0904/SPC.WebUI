using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Resources.report.form.chunileng
{
    public partial class xtraReport : XtraReport
    {
        #region 변수
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;

        #endregion

        #region 생성자
        public xtraReport(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
        {
            InitializeComponent();

            dtHead = _dtHead;
            dtGroup = _dtGroup;
            dtData = _dtData;


            // 리포트 헤더
            SetReportHeader();

            // 리포트데이터
            RendorReportData();
        }
        #endregion

        #region 사용자 함수

        #region 리포트 헤더
        private void SetReportHeader()
        {
            DataRow dtRow = dtHead.Rows[0];


            if (dtRow["F_IMAGPT"].ToString() == "" || dtRow["F_IMAGPT"].ToString() == null)
            {
                ReportHeader.Visible = false;
                GroupHeader2.Visible = false;
            }

            if (dtRow["F_IMAG01"].ToString() == "" || dtRow["F_IMAG01"].ToString() == null)
            {
                GroupHeader3.Visible = false;
            }

            if (dtRow["F_IMAG02"].ToString() == "" || dtRow["F_IMAG02"].ToString() == null)
            {
                GroupHeader4.Visible = false;
            }

            if (dtRow["F_IMAG03"].ToString() == "" || dtRow["F_IMAG03"].ToString() == null)
            {
                GroupHeader5.Visible = false;
            }

            if (dtRow["F_IMAG04"].ToString() == "" || dtRow["F_IMAG04"].ToString() == null)
            {
                GroupHeader6.Visible = false;
            }


            // 품명
            DevExpressLib.SetText(xrTableCell6, dtRow["F_ITEMNM"].ToString());
            DevExpressLib.SetText(xrTableCell30, dtRow["F_ITEMNM"].ToString());
            DevExpressLib.SetText(xrTableCell105, dtRow["F_ITEMNM"].ToString());
            DevExpressLib.SetText(xrTableCell132, dtRow["F_ITEMNM"].ToString());
            DevExpressLib.SetText(xrTableCell159, dtRow["F_ITEMNM"].ToString());
            DevExpressLib.SetText(xrTableCell12, dtRow["F_ITEMNM"].ToString());

            // 품번
            DevExpressLib.SetText(xrTableCell20, dtRow["F_ITEMCD"].ToString());
            DevExpressLib.SetText(xrTableCell34, dtRow["F_ITEMCD"].ToString());
            DevExpressLib.SetText(xrTableCell107, dtRow["F_ITEMCD"].ToString());
            DevExpressLib.SetText(xrTableCell134, dtRow["F_ITEMCD"].ToString());
            DevExpressLib.SetText(xrTableCell161, dtRow["F_ITEMCD"].ToString());
            DevExpressLib.SetText(xrTableCell84, dtRow["F_ITEMCD"].ToString());

            // 기종
            DevExpressLib.SetText(xrTableCell22, dtRow["F_MODELNM"].ToString());
            DevExpressLib.SetText(xrTableCell48, dtRow["F_MODELNM"].ToString());
            DevExpressLib.SetText(xrTableCell109, dtRow["F_MODELNM"].ToString());
            DevExpressLib.SetText(xrTableCell136, dtRow["F_MODELNM"].ToString());
            DevExpressLib.SetText(xrTableCell163, dtRow["F_MODELNM"].ToString());

            // 일자
            DevExpressLib.SetText(xrTableCell31, dtRow["F_WORKDATE"].ToString());
            DevExpressLib.SetText(xrTableCell51, dtRow["F_WORKDATE"].ToString());
            DevExpressLib.SetText(xrTableCell111, dtRow["F_WORKDATE"].ToString());
            DevExpressLib.SetText(xrTableCell138, dtRow["F_WORKDATE"].ToString());
            DevExpressLib.SetText(xrTableCell165, dtRow["F_WORKDATE"].ToString());
            DevExpressLib.SetText(xrTableCell71, dtRow["F_WORKDATE"].ToString());

            //Lot
            DevExpressLib.SetText(xrTableCell26, dtRow["F_LOTNO1"].ToString());
            DevExpressLib.SetText(xrTableCell55, dtRow["F_LOTNO1"].ToString());
            DevExpressLib.SetText(xrTableCell115, dtRow["F_LOTNO1"].ToString());
            DevExpressLib.SetText(xrTableCell142, dtRow["F_LOTNO1"].ToString());
            DevExpressLib.SetText(xrTableCell169, dtRow["F_LOTNO1"].ToString());

            


        }
        #endregion

        #region 리포트데이터
        private void RendorReportData()
        {
            XRTable xrTable = new XRTable();
            xrTable.Font = new Font("돋움", 6F, FontStyle.Regular);
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable.BorderWidth = 1F;
            xrTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrTable.BeginInit();

            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            int nSizeIndex = 0;
            int nIndex = 0;
            int i = 0;
            const int nRowCount = 14;
            string sInspection = String.Empty;
            bool bSize = false;

            foreach (DataRow dtRow in dtData.Rows)
            {
                //sInspection = dtRow["F_INSPCD"].ToString().Equals("AAC502") ?
                //    "외관" :
                //    dtRow["F_INSPCD"].ToString().Equals("AAC501") ? "치수" : dtRow["F_INSPCD"].ToString();

                //if (sInspection.Equals("치수")) nSizeIndex++;

                xrRow = new XRTableRow();
                xrRow.HeightF = 25F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.WidthF = 25F;
                xrCell.Text = !bSize ? dtRow["F_ROWNUM"].ToString() : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                //// 항목
                //xrCell = new XRTableCell();
                //xrCell.WidthF = 82F;
                //xrCell.Text = !bSize ? sInspection : "";
                //xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                //if (nIndex.Equals(0))
                //    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                //xrRow.Cells.Add(xrCell);

                //bSize = sInspection.Equals("치수");

                //// 측정장비
                //xrCell = new XRTableCell();
                //xrCell.WidthF = 30F;
                //xrCell.Text = dtRow["F_INSPCD"].ToString().Equals("AAC502") ? "육안" : dtRow["F_AIRCKNM"].ToString();
                //xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                //if (nIndex.Equals(0))
                //    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                //xrRow.Cells.Add(xrCell);

                //if (true == bSize)
                {
                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0);
                    xrCell.WidthF = 50F;
                    xrCell.Text = dtRow["F_STANDARD"].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    xrCell = new XRTableCell();
                    //xrCell.Multiline = true;
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0);
                    xrCell.WidthF = 30F;
                    xrCell.Text = dtRow["F_MIN"].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    xrCell = new XRTableCell();
                    //xrCell.Multiline = true;
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0);
                    xrCell.WidthF = 30F;
                    xrCell.Text = dtRow["F_MAX"].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }
                //else
                //{
                //    // 규격
                //    xrCell = new XRTableCell();
                //    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
                //    xrCell.WidthF = 107F;
                //    xrCell.Text = dtRow["F_STANDARD"].ToString();
                //    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                //    if (nIndex.Equals(0))
                //        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                //    xrRow.Cells.Add(xrCell);
                //}

                for (i = 0; i < 6; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 70F;
                    xrCell.Text = dtRow[String.Format("F_X{0}", i + 1)].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }


                xrCell = new XRTableCell();
                //xrCell.Multiline = true;
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
                xrCell.WidthF = 46F;

                float a = 0;
                float avr = 0;

                //a == Convert. dtRow["SUM"]

                xrCell.Text = dtRow["F_AVG"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                //xrCell.Multiline = true;
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
                xrCell.WidthF = 50F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                //xrCell.Multiline = true;
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
                xrCell.WidthF = 76F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);
                //for (i = 0; i < 7; i++)
                //{
                //    xrCell = new XRTableCell();
                //    xrCell.WidthF = 40F;
                //    xrCell.Text = "";
                //    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                //    if (nIndex.Equals(0))
                //        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                //    xrRow.Cells.Add(xrCell);
                //}

                xrTable.Rows.Add(xrRow);

                nIndex++;
            }



            // 빈 Row 생성
            //for (int j = 0; j < nRowCount - nIndex; j++)
            //{
            //    xrRow = new XRTableRow();
            //    xrRow.HeightF = 25F;

            //    // 순번
            //    xrCell = new XRTableCell();
            //    xrCell.WidthF = 28F;
            //    xrCell.Text = "";
            //    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            //    xrRow.Cells.Add(xrCell);

            //    //// 항목
            //    //xrCell = new XRTableCell();
            //    //xrCell.WidthF = 82F;
            //    //xrCell.Text = "";
            //    //xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            //    //xrRow.Cells.Add(xrCell);

            //    //// 측정장비
            //    //xrCell = new XRTableCell();
            //    //xrCell.WidthF = 30F;
            //    //xrCell.Text = "";
            //    //xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            //    //xrRow.Cells.Add(xrCell);

            //    // 규격
            //    xrCell = new XRTableCell();
            //    xrCell.WidthF = 107F;
            //    xrCell.Text = "";
            //    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            //    xrRow.Cells.Add(xrCell);

            //    for (i = 0; i < 6; i++)
            //    {
            //        xrCell = new XRTableCell();
            //        xrCell.WidthF = 40F;
            //        xrCell.Text = "";
            //        xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            //        xrRow.Cells.Add(xrCell);
            //    }

            //    xrTable.Rows.Add(xrRow);
            //}

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail1.Controls.Add(xrTable);
            this.Detail1.HeightF = xrTable.HeightF;

            if (xrTable.Rows.Count > nRowCount)
            {
                this.Detail1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            }
        }
        #endregion

        #endregion

        #region 사용자 이벤트
        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpressLib.LineBreak(sender);
        }

        private void xrTableCell39_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpressLib.LineBreak(sender);
        }

        private void xrTableCell55_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpressLib.LineBreak(sender, 2);
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRow dtRow = dtHead.Rows[0];

            if (!String.IsNullOrEmpty(dtRow["F_IMAGPT"].ToString()) && !String.IsNullOrEmpty(dtRow["F_IMAG01"].ToString()))
            {
                XRPictureBox pictureBox = sender as XRPictureBox;
                pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
                pictureBox.ImageUrl = String.Format("{0}{1}", dtRow["F_IMAGPT"], dtRow["F_IMAG01"]);
            }
        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRow dtRow = dtHead.Rows[0];

            if (!String.IsNullOrEmpty(dtRow["F_IMAGPT"].ToString()) && !String.IsNullOrEmpty(dtRow["F_IMAG02"].ToString()))
            {
                XRPictureBox pictureBox = sender as XRPictureBox;
                pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
                pictureBox.ImageUrl = String.Format("{0}{1}", dtRow["F_IMAGPT"], dtRow["F_IMAG02"]);
            }
        }

        private void xrPictureBox3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRow dtRow = dtHead.Rows[0];

            if (!String.IsNullOrEmpty(dtRow["F_IMAGPT"].ToString()) && !String.IsNullOrEmpty(dtRow["F_IMAG03"].ToString()))
            {
                XRPictureBox pictureBox = sender as XRPictureBox;
                pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
                pictureBox.ImageUrl = String.Format("{0}{1}", dtRow["F_IMAGPT"], dtRow["F_IMAG03"]);
            }
        }

        private void xrPictureBox4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRow dtRow = dtHead.Rows[0];

            if (!String.IsNullOrEmpty(dtRow["F_IMAGPT"].ToString()) && !String.IsNullOrEmpty(dtRow["F_IMAG04"].ToString()))
            {
                XRPictureBox pictureBox = sender as XRPictureBox;
                pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
                pictureBox.ImageUrl = String.Format("{0}{1}", dtRow["F_IMAGPT"], dtRow["F_IMAG04"]);
            }
        }

        private void xrPictureBox5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRow dtRow = dtHead.Rows[0];

            if (!String.IsNullOrEmpty(dtRow["F_IMAGPT"].ToString()) && !String.IsNullOrEmpty(dtRow["F_IMAG05"].ToString()))
            {
                XRPictureBox pictureBox = sender as XRPictureBox;
                pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
                pictureBox.ImageUrl = String.Format("{0}{1}", dtRow["F_IMAGPT"], dtRow["F_IMAG05"]);
            }
        }
        #endregion
    }
}
