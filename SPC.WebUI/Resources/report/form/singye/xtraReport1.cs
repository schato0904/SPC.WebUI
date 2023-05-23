using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;
using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Resources.report.form.singye
{
    public partial class xtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        #endregion
        
        #region 생성자
        public xtraReport1(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
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

            // 구분
            int iUsageIndex = int.Parse((dtRow["F_INSPECTION"] ?? "0").ToString());
            switch (iUsageIndex)
            {
                case 0:
                    xrTableCell8.Text = "○";
                    break;
                case 1:
                    xrTableCell9.Text = "○";
                    break;
                case 2:
                    xrTableCell10.Text = "○";
                    break;
                case 3:
                    xrTableCell11.Text = "○";
                    break;
                case 4:
                    xrTableCell12.Text = "○";
                    break;
            }

            // 작성일자
            xrLabel2.Text = String.Format("작성 {0}", DateTime.Parse(dtRow["F_DATE"].ToString()).ToString("yyyy년 MM월 dd일"));

            // LOT NO
            DevExpressLib.SetText(xrTableCell18, dtRow["F_LOTNO2"].ToString());

            // LOT SIZE
            DevExpressLib.SetText(xrTableCell24, dtRow["F_QUANTITY"].ToString());

            // 품질보증책임자
            DevExpressLib.SetText(xrTableCell82, dtRow["F_REVMANAGER"].ToString());

            // 검사자
            DevExpressLib.SetText(xrTableCell93, dtRow["F_REVAPPROVER"].ToString());

            // 품명
            DevExpressLib.SetText(xrTableCell104, dtRow["F_ITEMNM"].ToString());

            // 품번
            DevExpressLib.SetText(xrTableCell126, dtRow["F_ITEMCD"].ToString());

            // 업체명
            DevExpressLib.SetText(xrTableCell98, dtRow["F_CUSTOMNM"].ToString());
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
            const int nRowCount = 15;
            string sInspection = String.Empty;
            bool bSize = false;

            string sStandard;
            int nDecimalCnt;
            List<Decimal> oMeas;
            decimal dAverage, dAbs;

            foreach (DataRow dtRow in dtData.Rows)
            {
                sInspection = dtRow["F_INSPCD"].ToString().Equals("AAC502") ?
                    "외관" :
                    dtRow["F_INSPCD"].ToString().Equals("AAC501") ? "치수" : dtRow["F_INSPCD"].ToString();

                if (sInspection.Equals("치수")) nSizeIndex++;

                xrRow = new XRTableRow();
                xrRow.HeightF = 18F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.RowSpan = 2;
                xrCell.CanGrow = false;
                xrCell.WidthF = 40F;
                xrCell.Text = !bSize ? dtRow["F_ROWNUM"].ToString() : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.RowSpan = 2;
                xrCell.CanGrow = false;
                xrCell.WidthF = 80F;
                xrCell.Text = !bSize ? sInspection : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                bSize = sInspection.Equals("치수");

                sStandard = dtRow["F_STANDARD"].ToString();

                if (true == bSize)
                {
                    nDecimalCnt = sStandard.IndexOf('.')>=0 ? sStandard.Substring(sStandard.IndexOf('.')+1).Length : 0;

                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.RowSpan = 2;
                    xrCell.CanGrow = false;
                    xrCell.Font = new Font("돋움", 7F, FontStyle.Bold);
                    xrCell.WidthF = 65F;
                    xrCell.Text = String.Format("{0}{1}", dtRow["F_UNITNM"], sStandard);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    xrCell = new XRTableCell();
                    xrCell.RowSpan = 2;
                    xrCell.CanGrow = false;
                    xrCell.Font = new Font("돋움", 7F, FontStyle.Regular);
                    xrCell.Multiline = true;
                    xrCell.WidthF = 80F;
                    xrCell.Text = DevExpressLib.RendorTolerance(sStandard, dtRow["F_MAX"].ToString(), dtRow["F_MIN"].ToString(), true);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }
                else
                {
                    nDecimalCnt = 0;

                    // 규격
                    xrCell = new XRTableCell();
                    xrCell.RowSpan = 2;
                    xrCell.CanGrow = false;
                    xrCell.Font = new Font("돋움", 7F, FontStyle.Bold);
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
                    xrCell.WidthF = 145F;
                    xrCell.Text = sStandard;
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }

                oMeas = new List<decimal>();

                for (i = 0; i < 5; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.WidthF = 65F;
                    xrCell.Text = dtRow[String.Format("F_X{0}", i + 1)].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    if (bSize)
                    {
                        if(dtRow[String.Format("F_X{0}", i + 1)].ToString() != ""){
                            oMeas.Add(decimal.Parse(dtRow[String.Format("F_X{0}", i + 1)].ToString()));
                        }
                    }
                }

                if (bSize)
                {
                    // X
                    dAverage = oMeas.Average();
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.WidthF = 45F;
                    xrCell.Text = nDecimalCnt > 0 ? dAverage.ToString().Substring(0, dAverage.ToString().IndexOf('.') + 1 + nDecimalCnt) : dAverage.ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // R
                    dAbs = Math.Abs(oMeas.Max() - oMeas.Min());
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.Font = new Font("돋움", 6F, FontStyle.Regular);
                    xrCell.WidthF = 45F;
                    xrCell.Text = nDecimalCnt > 0 ? dAbs.ToString().Substring(0, dAbs.ToString().IndexOf('.') + 1 + nDecimalCnt) : dAbs.ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }
                else
                {
                    // X
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 45F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);

                    // R
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 45F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                    xrRow.Cells.Add(xrCell);
                }

                // 판정
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.RowSpan = 2;
                xrCell.WidthF = 47F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);

                // 발주사측
                xrRow = new XRTableRow();
                xrRow.HeightF = 18F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 40F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 80F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);
                
                if (true == bSize)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 65F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);

                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 80F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }
                else
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 145F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

                for (i = 0; i < 5; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 65F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

                for (i = 0; i < 2; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 45F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 47F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);

                nIndex++;
            }

            // 빈 Row 생성
            for (int j = 0; j < nRowCount - nIndex; j++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 18F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.RowSpan = 2;
                xrCell.CanGrow = false;
                xrCell.WidthF = 40F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.RowSpan = 2;
                xrCell.CanGrow = false;
                xrCell.WidthF = 80F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 규격
                xrCell = new XRTableCell();
                xrCell.RowSpan = 2;
                xrCell.CanGrow = false;
                xrCell.WidthF = 145F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrRow.Cells.Add(xrCell);

                for (i = 0; i < 5; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 65F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

                for (i = 0; i < 2; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 45F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

                xrCell = new XRTableCell();
                xrCell.RowSpan = 2;
                xrCell.CanGrow = false;
                xrCell.WidthF = 47F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);

                xrRow = new XRTableRow();
                xrRow.HeightF = 18F;

                // 순번
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 40F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 항목
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 80F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 규격
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 145F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrRow.Cells.Add(xrCell);

                for (i = 0; i < 5; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 65F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

                for (i = 0; i < 2; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 45F;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xrCell);
                }

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 47F;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail1.Controls.Add(xrTable);
            this.Detail1.HeightF = xrTable.HeightF;

            if (xrTable.Rows.Count / 2 > nRowCount)
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

        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/hkpi/images/hkpi.png");
        }
        #endregion
    }
}
