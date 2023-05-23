using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Resources.report.form.hkpi
{
    public partial class xtraReport5 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        #endregion
        
        #region 생성자
        public xtraReport5(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
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

            // Prepared Date
            DateTime dtIns = Convert.ToDateTime(dtRow["F_INSDT"]);
            DevExpressLib.SetText(xrTableCell4, String.Format("{0} Year {1} Month {2} Date", dtIns.Year, dtIns.Month, dtIns.Day));

            // 차종
            DevExpressLib.SetText(xrTableCell13, dtRow["F_MODELNM"].ToString());

            // 협력업체
            DevExpressLib.SetText(xrTableCell7, dtRow["F_COMPNM"].ToString());

            // PONO
            DevExpressLib.SetText(xrTableCell9, dtRow["F_PONO"].ToString());

            // 품명
            DevExpressLib.SetText(xrTableCell20, dtRow["F_ITEMNM"].ToString());

            // 품번
            DevExpressLib.SetText(xrTableCell17, dtRow["F_ITEMCD"].ToString());

            // LOT NO
            DevExpressLib.SetText(xrTableCell19, dtRow["F_LOTNO2"].ToString());

            // 수량
            DevExpressLib.SetText(xrTableCell23, dtRow["F_QUANTITY"].ToString());

            // 입고일
            DevExpressLib.SetText(xrTableCell27, dtRow["F_DATE"].ToString());

            // 입고방법
            DevExpressLib.SetText(xrTableCell37, dtRow["F_TYPE"].ToString());

            // 입고장소
            DevExpressLib.SetText(xrTableCell47, dtRow["F_PLACE"].ToString());

            // 서식명
            DevExpressLib.SetText(xrTableCell22, dtRow["F_FORMAT"].ToString());
        }
        #endregion

        #region 리포트데이터
        private void RendorReportData()
        {
            XRTable xrTable = new XRTable();
            xrTable.Font = new Font("돋움", 8F, FontStyle.Regular);
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
                sInspection = dtRow["F_INSPCD"].ToString().Equals("AAC502") ?
                    "Apperance" :
                    dtRow["F_INSPCD"].ToString().Equals("AAC501") ? "Dim's" : dtRow["F_INSPCD"].ToString();

                if (sInspection.Equals("Dim's")) nSizeIndex++;

                xrRow = new XRTableRow();
                xrRow.HeightF = 30F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.WidthF = 30F;
                xrCell.Text = dtRow["F_ROWNUM"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                bSize = sInspection.Equals("Dim's");

                if (true == bSize)
                {
                    // 항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 70F;
                    xrCell.Text = sInspection;
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0);
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 치수순번
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 30F;
                    xrCell.Text = DevExpressLib.GetOrderSimbol(nSizeIndex);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.WidthF = 55F;
                    xrCell.Text = String.Format("{0}{1}", dtRow["F_UNITNM"], dtRow["F_STANDARD"]);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.Multiline = true;
                    xrCell.WidthF = 62F;
                    xrCell.Text = DevExpressLib.RendorTolerance(dtRow["F_STANDARD"].ToString(), dtRow["F_MAX"].ToString(), dtRow["F_MIN"].ToString(), true);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }
                else
                {
                    // 항목
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 8F, FontStyle.Regular);
                    xrCell.WidthF = 100F;
                    xrCell.Text = sInspection;
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0);
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
                    xrCell.WidthF = 117F;
                    xrCell.Text = dtRow["F_STANDARD"].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }

                for (i = 0; i < 5; i++)
                {
                    if (dtRow["F_MEASYESNO"].ToString().Equals("0"))
                    {
                        xrCell = new XRTableCell();
                        xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                        xrCell.WidthF = 40F;
                        xrCell.Text = dtRow[String.Format("F_X{0}", i + 1)].ToString();
                        xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        if (nIndex.Equals(0))
                            xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                        xrRow.Cells.Add(xrCell);
                    }
                    else
                    {
                        if (dtRow["F_INSPCD"].ToString().Equals("AAC501"))
                        {
                            xrCell = new XRTableCell();
                            xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                            xrCell.WidthF = 40F;
                            xrCell.Text = "OK";
                            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            if (nIndex.Equals(0))
                                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                            xrRow.Cells.Add(xrCell);
                        }
                        else
                        {
                            xrCell = new XRTableCell();
                            xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                            xrCell.WidthF = 40F;
                            xrCell.Text = "";
                            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            if (nIndex.Equals(0))
                                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                            xrRow.Cells.Add(xrCell);
                        }
                    }
                }

                for (i = 0; i < 4; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }

                xrCell = new XRTableCell();
                xrCell.WidthF = 50F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 70F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);

                nIndex++;
            }

            // 빈 Row 생성
            for (int j = 0; j < nRowCount - nIndex; j++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 30F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.WidthF = 30F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.WidthF = 100F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 규격
                xrCell = new XRTableCell();
                xrCell.WidthF = 117F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrRow.Cells.Add(xrCell);

                for (i = 0; i < 9; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

                xrCell = new XRTableCell();
                xrCell.WidthF = 50F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 70F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);
            }

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
        private void xrTableCel_LineBreak(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpressLib.LineBreak(sender);
        }

        private void xrTableCell46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpressLib.LineBreak(sender, "|");
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
        #endregion
    }
}
