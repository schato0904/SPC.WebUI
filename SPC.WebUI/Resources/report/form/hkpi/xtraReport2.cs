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
    public partial class xtraReport2 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        #endregion
        
        #region 생성자
        public xtraReport2(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
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

            // 협력업체
            DevExpressLib.SetText(xrTableCell30, dtRow["F_COMPNM"].ToString());

            // 측정일자
            DevExpressLib.SetText(xrTableCell6, Convert.ToDateTime(dtRow["F_DATE"]).ToString("yyyy년 MM월 dd일"));

            // 작성자
            DevExpressLib.SetText(xrTableCell8, dtRow["F_USERNM"].ToString());

            // 차종(P/NO)
            DevExpressLib.SetText(xrTableCell14, dtRow["F_MODELCD"].ToString());

            // 차종(P/NO)
            DevExpressLib.SetText(xrTableCell16, dtRow["F_MODELNM"].ToString());

            // 품번
            DevExpressLib.SetText(xrTableCell22, dtRow["F_ITEMCD"].ToString());

            // 품명
            DevExpressLib.SetText(xrTableCell24, dtRow["F_ITEMNM"].ToString());

            // 검사자
            DevExpressLib.SetText(xrTableCell8, dtRow["F_REVMANAGER"].ToString());

            // 용도
            string[] arrUsageText = { "ISIR", "정기검사", "일반검사", "기타" };

            int iUsageIndex = 0;
            bool bExistsUsage = false;
            if (!String.IsNullOrEmpty(dtRow["F_USAGE"].ToString()))
            {
                string[] arrUsage = dtRow["F_USAGE"].ToString().Split(',');
                StringBuilder sb_usage = new StringBuilder();
                foreach (string sUsageText in arrUsageText)
                {
                    foreach (string sUsage in arrUsage[0].Split('|'))
                    {
                        if (iUsageIndex.ToString() == sUsage)
                        {
                            sb_usage.Append(" ■");
                            bExistsUsage = true;
                            break;
                        }
                    }

                    if (!bExistsUsage)
                        sb_usage.Append(" □");

                    sb_usage.Append(arrUsageText[iUsageIndex]);

                    if (iUsageIndex == 0)
                        sb_usage.Append(" ");

                    iUsageIndex++;
                    bExistsUsage = false;
                }

                if (arrUsage.Length == 2)
                    sb_usage.AppendFormat(" ({0})", arrUsage[1]);

                DevExpressLib.SetText(xrTableCell34, sb_usage.ToString());
            }

            // LOT NO
            DevExpressLib.SetText(xrTableCell66, dtRow["F_LOTNO2"].ToString());

            // 입고일
            DevExpressLib.SetText(xrTableCell67, dtRow["F_REVDT"].ToString());

            // 중량
            DevExpressLib.SetText(xrTableCell68, dtRow["F_REVDESC"].ToString());
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
            const int nRowCount = 13;
            string sInspection = String.Empty;
            bool bSize = false;

            foreach (DataRow dtRow in dtData.Rows)
            {
                sInspection = dtRow["F_INSPCD"].ToString().Equals("AAC502") ?
                    "외관" :
                    dtRow["F_INSPCD"].ToString().Equals("AAC501") ? "치수" : dtRow["F_INSPCD"].ToString();

                if (sInspection.Equals("치수")) nSizeIndex++;

                xrRow = new XRTableRow();
                xrRow.HeightF = 25F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.WidthF = 90F;
                xrCell.Text = !bSize ? dtRow["F_ROWNUM"].ToString() : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                //if (nIndex.Equals(0))
                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right));
                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.Font = new Font("돋움", 8F, FontStyle.Bold);
                xrCell.WidthF = 127F;
                xrCell.Text = !bSize ? sInspection : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                bSize = sInspection.Equals("치수");

                // 측정장비
                xrCell = new XRTableCell();
                xrCell.Font = dtRow["F_INSPCD"].ToString().Equals("AAC502") ? new Font("돋움", 8F, FontStyle.Bold) : new Font("돋움", 10F, FontStyle.Bold);
                xrCell.WidthF = 30F;
                xrCell.Text = dtRow["F_INSPCD"].ToString().Equals("AAC502") ? "육안" : dtRow["F_INSPCD"].ToString().Equals("AAC501") ? DevExpressLib.GetOrderSimbol(nSizeIndex) : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nIndex.Equals(0))
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                if (true == bSize)
                {
                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 7F, FontStyle.Bold);
                    xrCell.WidthF = 80F;
                    xrCell.Text = String.Format("{0}{1}", dtRow["F_UNITNM"], dtRow["F_STANDARD"]);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom));
                    else
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.Multiline = true;
                    xrCell.WidthF = 80F;
                    xrCell.Text = DevExpressLib.RendorTolerance(dtRow["F_STANDARD"].ToString(), dtRow["F_MAX"].ToString(), dtRow["F_MIN"].ToString(), true);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom));
                    else
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }
                else
                {
                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.Font = new Font("돋움", 7F, FontStyle.Bold);
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
                    xrCell.WidthF = 160F;
                    xrCell.Text = dtRow["F_STANDARD"].ToString();
                    xrCell.TextAlignment = sInspection.Equals("치수") ? DevExpress.XtraPrinting.TextAlignment.MiddleLeft : DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }

                for (i = 0; i < 3; i++)
                {
                    if (dtRow["F_MEASYESNO"].ToString().Equals("0"))
                    {
                        xrCell = new XRTableCell();
                        xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                        xrCell.WidthF = 40F;
                        xrCell.Text = dtRow[String.Format("F_X{0}", i + 1)].ToString();
                        xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        if (nIndex.Equals(0))
                            xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
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
                                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                            xrRow.Cells.Add(xrCell);
                        }
                        else
                        {
                            if (i.Equals(0))
                            {
                                xrCell = new XRTableCell();
                                xrCell.WidthF = 120F;
                                xrCell.Text = "고객 요구시";
                                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                if (nIndex.Equals(0))
                                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                                xrRow.Cells.Add(xrCell);
                            }
                        }
                    }
                }

                for (i = 0; i < 5; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    if (nIndex.Equals(0))
                        xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
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
                xrCell.WidthF = 90F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (nRowCount - nIndex - j - 1 <= 0)
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                else
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right));

                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.WidthF = 127F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 측정장비
                xrCell = new XRTableCell();
                xrCell.WidthF = 30F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 규격
                xrCell = new XRTableCell();
                xrCell.WidthF = 160F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrRow.Cells.Add(xrCell);

                for (i = 0; i < 8; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
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
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/hkpi/images/jm.png");
        }
        #endregion
    }
}
