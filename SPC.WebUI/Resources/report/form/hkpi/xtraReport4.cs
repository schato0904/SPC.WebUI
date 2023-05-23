using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;
using System.Text;

namespace SPC.WebUI.Resources.report.form.hkpi
{
    public partial class xtraReport4 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        #endregion
        
        #region 생성자
        public xtraReport4(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
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

            // 차수
            DevExpressLib.SetText(xrTableCell4, dtRow["F_ORDER"].ToString());

            // 수량
            DevExpressLib.SetText(xrTableCell6, dtRow["F_QUANTITY"].ToString());

            // ISNPECTION
            string[] arrUsageText1 = { "APPERANCE  ", "DIM'S  ", "METERIAL  ", "PERPORMANCE" };

            int iUsageIndex1 = 0;
            bool bExistsUsage1 = false;
            if (!String.IsNullOrEmpty(dtRow["F_INSPECTION"].ToString()))
            {
                string[] arrUsage = dtRow["F_INSPECTION"].ToString().Split(',');
                StringBuilder sb_usage1 = new StringBuilder();
                foreach (string sUsageText in arrUsageText1)
                {
                    foreach (string sUsage in arrUsage[0].Split('|'))
                    {
                        if (iUsageIndex1.ToString() == sUsage)
                        {
                            sb_usage1.Append("■");
                            bExistsUsage1 = true;
                            break;
                        }
                    }

                    if (!bExistsUsage1)
                        sb_usage1.Append("□");

                    sb_usage1.Append(arrUsageText1[iUsageIndex1]);

                    if (iUsageIndex1 == 0)
                        sb_usage1.Append(" ");

                    iUsageIndex1++;
                    bExistsUsage1 = false;
                }

                if (arrUsage.Length == 2)
                    sb_usage1.AppendFormat(" ({0})", arrUsage[1]);

                DevExpressLib.SetText(xrTableCell12, sb_usage1.ToString());
            }

            // 차종
            DevExpressLib.SetText(xrTableCell26, dtRow["F_MODELNM"].ToString());

            // 협력업체
            DevExpressLib.SetText(xrTableCell27, dtRow["F_COMPNM"].ToString());

            // 품번
            DevExpressLib.SetText(xrTableCell21, dtRow["F_ITEMCD"].ToString());

            // EO-NO
            DevExpressLib.SetText(xrTableCell22, dtRow["F_EONO"].ToString());

            // 품명
            DevExpressLib.SetText(xrTableCell17, dtRow["F_ITEMNM"].ToString());

            // 측정일자
            DevExpressLib.SetText(xrTableCell31, Convert.ToDateTime(dtRow["F_DATE"]).ToString("yyyy.MM.dd"));

            // 용도
            string[] arrUsageText = { "ISIR  ", "regular Inspection  ", "Ordinary  ", "Others" };

            int iUsageIndex = 0;
            bool bExistsUsage = false;
            StringBuilder sb_usage = new StringBuilder();
            foreach (string sUsageText in arrUsageText)
            {
                foreach (string sUsage in dtRow["F_USAGE"].ToString().Split('|'))
                {
                    if (iUsageIndex.ToString() == sUsage)
                    {
                        sb_usage.Append("■");
                        bExistsUsage = true;
                        break;
                    }
                }

                if (!bExistsUsage)
                    sb_usage.Append("□");

                sb_usage.Append(arrUsageText[iUsageIndex]);

                if (iUsageIndex == 0)
                    sb_usage.Append(" ");

                iUsageIndex++;
                bExistsUsage = false;
            }
            DevExpressLib.SetText(xrTableCell35, sb_usage.ToString());

            // 서식
            DevExpressLib.SetText(xrTableCell33, String.Format("서식 {0} A4", dtRow["F_FORMAT"]));
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
                    "외관|(Apperance)" :
                    dtRow["F_INSPCD"].ToString().Equals("AAC501") ? "DIM'S" : dtRow["F_INSPCD"].ToString();

                if (sInspection.Equals("DIM'S")) nSizeIndex++;

                xrRow = new XRTableRow();
                xrRow.HeightF = 25F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.WidthF = 25F;
                xrCell.Text = !bSize ? dtRow["F_ROWNUM"].ToString() : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                bSize = sInspection.Equals("DIM'S");

                if (true == bSize)
                {
                    // 항목
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.WidthF = 55F;
                    xrCell.Text = sInspection;
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 계측기 및 순번
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.WidthF = 45F;
                    xrCell.Text = String.Format("({0})-{1}", dtRow["F_AIRCKNM"], DevExpressLib.GetOrderSimbol(nSizeIndex));
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0);
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.WidthF = 40F;
                    xrCell.Text = String.Format("{0}{1}", dtRow["F_UNITNM"], dtRow["F_STANDARD"]);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.Multiline = true;
                    xrCell.WidthF = 37F;
                    xrCell.Text = DevExpressLib.RendorTolerance(dtRow["F_STANDARD"].ToString(), dtRow["F_MAX"].ToString(), dtRow["F_MIN"].ToString(), true);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }
                else
                {
                    // 항목
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.WidthF = 100F;
                    xrCell.Text = sInspection;
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrCell.BeforePrint += xrTableCel_LineBreak2;
                    xrRow.Cells.Add(xrCell);

                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 5F, FontStyle.Regular);
                    xrCell.WidthF = 77F;
                    xrCell.Text = dtRow["F_STANDARD"].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrCell.BeforePrint += xrTableCel_LineBreak2;
                    xrRow.Cells.Add(xrCell);
                }

                for (i = 0; i < 3; i++)
                {
                    if (dtRow["F_MEASYESNO"].ToString().Equals("0"))
                    {
                        xrCell = new XRTableCell();
                        xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                        xrCell.WidthF = 35F;
                        xrCell.Text = dtRow[String.Format("F_X{0}", i + 1)].ToString();
                        xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                        xrRow.Cells.Add(xrCell);
                    }
                    else
                    {
                        if (dtRow["F_INSPCD"].ToString().Equals("AAC501"))
                        {
                            xrCell = new XRTableCell();
                            xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                            xrCell.WidthF = 35F;
                            xrCell.Text = "OK";
                            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                            xrRow.Cells.Add(xrCell);
                        }
                        else
                        {
                            if (i.Equals(0))
                            {
                                xrCell = new XRTableCell();
                                xrCell.Font = new Font("돋움", 7F, FontStyle.Regular);
                                xrCell.WidthF = 525F;
                                xrCell.Text = "수요자요구시|(Consumer Demand)";
                                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                                xrCell.BeforePrint += xrTableCel_LineBreak2;
                                xrRow.Cells.Add(xrCell);
                            }
                        }
                    }
                }

                for (i = 0; i < 12; i++)
                {
                    if (dtRow["F_MEASYESNO"].ToString().Equals("0") || dtRow["F_INSPCD"].ToString().Equals("AAC501"))
                    {
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 35F;
                        xrCell.Text = "";
                        xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                        xrRow.Cells.Add(xrCell);
                    }
                }

                xrTable.Rows.Add(xrRow);

                nIndex++;
            }

            // 빈 Row 생성
            for (int j = 0; j < nRowCount - nIndex; j++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 25F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.WidthF = 25F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nRowCount - nIndex - j - 1 <= 0)
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                else
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right));

                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.WidthF = 100F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 규격
                xrCell = new XRTableCell();
                xrCell.WidthF = 77F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrRow.Cells.Add(xrCell);

                for (i = 0; i < 15; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 35F;
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

        private void xrTableCel_LineBreak2(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/hkpi/images/das.png");
        }

        #endregion
    }
}
