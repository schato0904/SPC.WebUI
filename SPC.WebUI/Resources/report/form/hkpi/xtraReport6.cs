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
    public partial class xtraReport6 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        #endregion
        
        #region 생성자
        public xtraReport6(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
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

            // 문서개정정보
            // 순
            DevExpressLib.SetText(xrTableCell8, dtRow["F_REVNO"].ToString());
            // 개정일자
            DevExpressLib.SetText(xrTableCell9, dtRow["F_REVDT"].ToString());
            // 개정내용
            DevExpressLib.SetText(xrTableCell10, dtRow["F_REVDESC"].ToString());
            // 담당
            DevExpressLib.SetText(xrTableCell11, dtRow["F_REVMANAGER"].ToString());
            // 승인
            DevExpressLib.SetText(xrTableCell12, dtRow["F_REVAPPROVER"].ToString());

            // 기종
            DevExpressLib.SetText(xrTableCell32, dtRow["F_MODELNM"].ToString());

            // 품명
            DevExpressLib.SetText(xrTableCell34, dtRow["F_ITEMNM"].ToString());

            // 공정명
            DevExpressLib.SetText(xrTableCell36, dtRow["F_USAGE"].ToString());

            // 품번
            DevExpressLib.SetText(xrTableCell38, dtRow["F_ITEMCD"].ToString());

            // EO NO
            DevExpressLib.SetText(xrTableCell40, dtRow["F_EONO"].ToString());

            // 입고수량
            DevExpressLib.SetText(xrTableCell42, dtRow["F_QUANTITY"].ToString());

            // 외주업체명
            DevExpressLib.SetText(xrTableCell44, dtRow["F_COMPNM"].ToString());

            // LOT NO
            DevExpressLib.SetText(xrTableCell46, dtRow["F_LOTNO2"].ToString());

            // 입고일자
            DevExpressLib.SetText(xrTableCell48, dtRow["F_DATE"].ToString());
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
            const int nRowCount = 12;
            string sInspection = String.Empty;
            bool bSize = false;

            foreach (DataRow dtRow in dtData.Rows)
            {
                sInspection = dtRow["F_INSPCD"].ToString().Equals("AAC502") ?
                    "외관" :
                    dtRow["F_INSPCD"].ToString().Equals("AAC501") ? "치수" : dtRow["F_INSPCD"].ToString();

                if (sInspection.Equals("치수")) nSizeIndex++;

                xrRow = new XRTableRow();
                xrRow.HeightF = 20F;

                bSize = sInspection.Equals("치수");

                if (true == bSize)
                {
                    // 순번
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
                    xrCell.Text = DevExpressLib.GetOrderSimbol(nSizeIndex);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 120F;
                    xrCell.Text = sInspection;
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 80F;
                    xrCell.Text = String.Format("{0}{1}", dtRow["F_UNITNM"], dtRow["F_STANDARD"]);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.Multiline = true;
                    xrCell.WidthF = 87F;
                    xrCell.Text = DevExpressLib.RendorTolerance(dtRow["F_STANDARD"].ToString(), dtRow["F_MAX"].ToString(), dtRow["F_MIN"].ToString(), false);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 방법
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
                    xrCell.Text = dtRow["F_AIRCKNM"].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }
                else
                {
                    // 항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 160F;
                    xrCell.Text = sInspection;
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 7F, FontStyle.Regular);
                    xrCell.WidthF = 167F;
                    xrCell.Text = dtRow["F_STANDARD"].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 방법
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
                    xrCell.Text = dtRow["F_MEASYESNO"].ToString().Equals("0") ? "육안" : "성적서";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }

                for (i = 0; i < 3; i++)
                {
                    if (dtRow["F_MEASYESNO"].ToString().Equals("0"))
                    {
                        xrCell = new XRTableCell();
                        xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                        xrCell.WidthF = 45F;
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
                            xrCell.WidthF = 45F;
                            xrCell.Text = "OK";
                            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            if (nIndex.Equals(0))
                                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                            xrRow.Cells.Add(xrCell);
                        }
                        else
                        {
                            if (i.Equals(0))
                            {
                                xrCell = new XRTableCell();
                                xrCell.Font = new Font("돋움", 7F, FontStyle.Regular);
                                xrCell.WidthF = 135F;
                                xrCell.Text = "성적서 참조";
                                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                if (nIndex.Equals(0))
                                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                                xrRow.Cells.Add(xrCell);
                            }
                        }
                    }
                }

                for (i = 0; i < 5; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 45F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }

                xrTable.Rows.Add(xrRow);

                nIndex++;
            }

            // 빈 Row 생성
            for (int j = 0; j < nRowCount - nIndex; j++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 20F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.WidthF = 40F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.WidthF = 120F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 규격
                xrCell = new XRTableCell();
                xrCell.WidthF = 167F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrRow.Cells.Add(xrCell);

                // 방법
                xrCell = new XRTableCell();
                xrCell.WidthF = 40F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                xrRow.Cells.Add(xrCell);

                for (i = 0; i < 8; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 45F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

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

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/hkpi/images/dm.png");
        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRow dtRow = dtHead.Rows[0];

            if (!String.IsNullOrEmpty(dtRow["F_IMAGPT"].ToString()) && !String.IsNullOrEmpty(dtRow["F_IMAG01"].ToString()))
            {
                XRPictureBox pictureBox = sender as XRPictureBox;
                pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
                pictureBox.ImageUrl = String.Format("{0}{1}", dtRow["F_IMAGPT"], dtRow["F_IMAG01"]);
            }
        }

        private void xrPictureBox3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/hkpi/images/dm_workflow.gif");
        }

        #endregion
    }
}
