using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;
using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;
using System.Collections.Generic;

namespace SPC.WebUI.Resources.report.form.foseco
{
    public partial class xtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        DataRow dtHeadRow = null;
        DataRow dtGroupRow = null;
        #endregion

        #region 생성자
        public xtraReport1(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
        {
            InitializeComponent();

            dtHead = _dtHead;
            dtGroup = _dtGroup;
            dtData = _dtData;

            dtHeadRow = dtHead.Rows[0];
            dtGroupRow = dtGroup.Rows[0];

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
            // 거래처명
            //DevExpressLib.SetText(xrTableCell5, dtHeadRow["F_PARTNERNM"].ToString());
            //xrTableCell5.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
            bool inspchk = false;
            for (int i = 0; i < dtGroup.Rows.Count; i++)
            {
                if (dtGroup.Rows[i]["F_NUMBER"].ToString() == "99")
                    inspchk = true;
            }

            //발행인
            DevExpressLib.SetText(xrTableCell29, dtHeadRow["F_USERID"].ToString());
            xrTableCell29.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);            

            // 제품명
            DevExpressLib.SetText(xrTableCell7, dtHeadRow["F_ITEMNM"].ToString());
            xrTableCell7.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
            // Lot no
            DevExpressLib.SetText(xrTableCell9, dtHeadRow["F_LOTNO"].ToString());
            xrTableCell9.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
            // 수량
            DevExpressLib.SetText(xrTableCell11, Convert.ToInt32(dtHeadRow["F_QTY"]).ToString("n0") + (inspchk ? " Pcs" : ""));
            xrTableCell11.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
            // 생산일
            DevExpressLib.SetText(xrTableCell15, dtHeadRow["F_WORKDATE"].ToString());
            xrTableCell15.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
            // 검사일
            DevExpressLib.SetText(xrTableCell13, dtHeadRow["F_WORKDATE"].ToString());
            xrTableCell13.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
        }
        #endregion

        #region 리포트데이터
        private void RendorReportData()
        {
            XRTable xrTable = new XRTable();
            //xrTable.WidthF = 727F;
            xrTable.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable.BorderWidth = 1F;
            xrTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrTable.BeginInit();

            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            int nIndex = 0;
            const int nRowCount = 13; // 총 Row수
            string sInspection = String.Empty;

            

            xrRow = new XRTableRow();
            xrRow.HeightF = 30F;
            xrRow.Font = new System.Drawing.Font("맑은 고딕", 10F, FontStyle.Bold);
            
            // Table Header
            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "TEST ITEM";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;            
            xrCell.WidthF = 149F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "SPEC";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 110F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "UNIT";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 40F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Name = "METHOD";
            xrCell.Text = "METHOD";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 77F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "MAX";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 70F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "MIN";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 70F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "NG";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 70F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "AVG";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 70F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "STD";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 70F;
            xrRow.Cells.Add(xrCell);

            xrTable.Rows.Add(xrRow);


            // Data입력
            for (int j = 0; j < dtGroup.Rows.Count; j++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 40F;
                xrRow.Padding = 3;

                xrCell = new XRTableCell();
                xrCell.Multiline = true;
                xrCell.CanGrow = false;
                xrCell.Text = dtGroup.Rows[j]["F_INSPDETAIL"].ToString() + "\r\n" + dtGroup.Rows[j]["F_MANAGESTAN"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;                
                xrCell.WidthF = 149F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtGroup.Rows[j]["F_STANDARD"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WidthF = 110F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtGroup.Rows[j]["F_UNIT"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 40F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.Multiline = true;
                xrCell.CanGrow = false;
                xrCell.Name = "METHOD";
                xrCell.Text = dtGroup.Rows[j]["F_SIRYO"].ToString() + "\r\nC = 0";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 77F;

                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtGroup.Rows[j]["F_MEASUREMAX"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtGroup.Rows[j]["F_INSPCD"].ToString() == "AAC501" ? dtGroup.Rows[j]["F_MEASUREMIN"].ToString() : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtGroup.Rows[j]["F_MEASURENG"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtGroup.Rows[j]["F_MEASUREAVG"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtGroup.Rows[j]["F_MEASURESTD"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);

                nIndex++;
            }
            
           
            // 빈 Row 생성
            for (int j = 0; j < nRowCount - nIndex; j++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 40F;

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WidthF = 149F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WidthF = 110F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 40F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Name = "METHOD";
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 77F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                xrCell.WidthF = 70F;
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);
            }


            // METHOD CELL ROWSPAN 작업
            int rowspanNo = 0;
            int[] rows = new int[dtGroup.Rows.Count];
            int rowscnt = 1;
            for (int i = 0; i < dtGroup.Rows.Count; i++)
            {
                if (i != dtGroup.Rows.Count - 1)
                {
                    if (dtGroup.Rows[i]["F_SIRYO"].ToString() == dtGroup.Rows[i + 1]["F_SIRYO"].ToString())
                    {
                        rowscnt++;
                    }
                    else
                    {
                        rows[rowspanNo] = rowscnt;
                        rowscnt = 1;
                        rowspanNo++;
                    }
                }
                else
                {
                    rows[rowspanNo] = rowscnt;
                }

            }

            rowspanNo = 0;
            int tableCnt = xrTable.Rows.Count;
            for (int i = 0; i < rows.Length; )
            {
                XRTableCell cell = xrTable.Rows[i+1].Cells["METHOD"] as XRTableCell;
                cell.RowSpan = rows[rowspanNo];
                i = i + rows[rowspanNo];
                rowspanNo++;
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail1.Controls.Add(xrTable);
            this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.Detail1.HeightF = xrTable.HeightF;

            //this.Detail1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;

            //if (xrTable.Rows.Count> nRowCount + 3)
            //{
            //    this.Detail1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            //}
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
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/foseco/images/FOSECO.jpg");
        }

        private void xrPictureBox3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/foseco/images/FOSECO_FOOTER.jpg");
        }
        #endregion

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/foseco/images/FOSECO_ACCO.jpg");
        }

        
    }
}
