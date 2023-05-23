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

namespace SPC.WebUI.Resources.report.form.megazen
{
    public partial class xtraReport2 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        DataRow dtHeadRow = null;
        DataRow dtGroupRow = null;
        #endregion

        #region 생성자
        public xtraReport2(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
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
            // 제조오더번호
            string tagakno = dtGroupRow["F_TAGAKNO"].ToString();
            string[] tagaknoarr = tagakno.Split('-');

            tagakno = "";
            for (int i = 0; i < tagakno.Length; i++)
            {
                if ((i + 1) == tagakno.Length)
                {
                    tagakno += '.';
                }
                else
                {
                    tagakno += tagaknoarr[i] + '-';
                }
            }

            tagakno.Replace("-.", "");

            tagakno = !String.IsNullOrEmpty(tagakno) ? tagakno.Contains("-") ? tagakno.Split('-')[0] : tagakno : "";
            DevExpressLib.SetText(xrTableCell4, tagakno);

            // 설비번호
            DevExpressLib.SetText(xrTableCell8, dtGroupRow["F_EXTCD"].ToString());

            // 품명
            DevExpressLib.SetText(xrTableCell5, dtHeadRow["F_ITEMNM"].ToString());

            // 품번
            DevExpressLib.SetText(xrTableCell10, dtHeadRow["F_ITEMCD"].ToString());

            // LOT NO
            DevExpressLib.SetText(xrTableCell12, dtHeadRow["F_LOTNO2"].ToString());

            // 검사일자
            DevExpressLib.SetText(xrTableCell14, dtGroupRow["F_WORKDATE"].ToString());

            // 문서번호
            DevExpressLib.SetText(xrTableCell16, dtHeadRow["F_FORMAT"].ToString());
        }
        #endregion

        #region 리포트데이터
        private void RendorReportData()
        {
            XRTable xrTable = new XRTable();
            xrTable.Font = new Font("돋움", 9F, FontStyle.Regular);
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable.BorderWidth = 1F;
            xrTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrTable.BeginInit();

            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            int nSizeIndex = 0;
            int nIndex = 0;
            int i = 0;
            const int nRowCount = 15;
            string sInspection = String.Empty;
            DataRow drItems = null;

            xrRow = new XRTableRow();
            xrRow.HeightF = 30F;
            
            // 구분
            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.RowSpan = 2;
            xrCell.Text = "구분";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 127F;
            xrRow.Cells.Add(xrCell);

            // 1차(초품이므로 사용안함)
            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.RowSpan = 2;
            xrCell.Text = " 1차";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            xrCell.WidthF = 200F;
            xrRow.Cells.Add(xrCell);

            for (i = 0; i < 2; i++)
            {
                if (dtGroup.Rows.Count > i)
                    drItems = dtGroup.Rows[i];
                else
                    drItems = null;

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.RowSpan = 2;
                xrCell.Text = drItems != null ? String.Format(" {0}차 : {1} {2}", i + 2, drItems["F_WORKDATE"], drItems["F_WORKTIME"]) : String.Format(" {0}차 : ", i + 2);
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WidthF = 200F;
                xrRow.Cells.Add(xrCell);
            }

            xrTable.Rows.Add(xrRow);

            xrRow = new XRTableRow();
            xrRow.HeightF = 30F;

            // 구분(Empty)
            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 127F;
            xrRow.Cells.Add(xrCell);

            for (i = 0; i < 3; i++)
            {
                // 검사자, 원소재, 성상, 체결
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "검사자";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 45F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "원소재";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 85F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "성상";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 35F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "체결";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 35F;
                xrRow.Cells.Add(xrCell);
            }

            xrTable.Rows.Add(xrRow);

            xrRow = new XRTableRow();
            xrRow.HeightF = 30F;

            // 구간
            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "구간";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 37F;
            xrRow.Cells.Add(xrCell);

            // 기준값
            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "기준값";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 90F;
            xrRow.Cells.Add(xrCell);

            // 1차(검사자, 원소재, 성상, 체결 초품만 사용하므로 사용안함)
            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 45F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 85F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 35F;
            xrRow.Cells.Add(xrCell);

            xrCell = new XRTableCell();
            xrCell.CanGrow = false;
            xrCell.Text = "";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrCell.WidthF = 35F;
            xrRow.Cells.Add(xrCell);

            for (i = 0; i < 2; i++)
            {
                if (dtGroup.Rows.Count > i)
                    drItems = dtGroup.Rows[i];
                else
                    drItems = null;

                // 검사자, 원소재, 성상, 체결
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = drItems!=null ? drItems["F_WORKMAN"].ToString() : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 45F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtHeadRow["F_MODELNM"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 85F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "Y";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 35F;
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "Y";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 35F;
                xrRow.Cells.Add(xrCell);
            }

            xrTable.Rows.Add(xrRow);

            foreach (DataRow dtRow in dtData.Select("", "F_SAMPLENO ASC"))
            {
                sInspection = dtRow["F_INSPCD"].ToString();

                if (sInspection.Equals("AAC501")) nSizeIndex++;

                xrRow = new XRTableRow();
                xrRow.HeightF = 30F;

                // 구간
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = dtRow["F_RESULTSTAND"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 37F;
                xrRow.Cells.Add(xrCell);

                // 기준값
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = sInspection.Equals("AAC501") ? String.Format("{0}~{1}", dtRow["F_MIN"], dtRow["F_MAX"]) : dtRow["F_STANDARD"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 90F;
                xrRow.Cells.Add(xrCell);

                // 초품측정값(사용안함)
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.RowSpan = 2;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 200F;
                xrRow.Cells.Add(xrCell);

                for (i = 0; i < 2; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 2;
                    xrCell.Text = dtRow[String.Format("F_X{0}", i + 1)].ToString();
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.WidthF = 200F;
                    xrRow.Cells.Add(xrCell);
                }

                xrTable.Rows.Add(xrRow);

                nIndex++;
            }

            // 빈 Row 생성
            for (int j = 0; j < nRowCount - nIndex; j++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 30F;

                // 구간
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 37F;
                xrRow.Cells.Add(xrCell);

                // 기준값
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.Text = "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WidthF = 90F;
                xrRow.Cells.Add(xrCell);

                for (i = 0; i < 3; i++)
                {
                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 2;
                    xrCell.Text = "";
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.WidthF = 200F;
                    xrRow.Cells.Add(xrCell);
                }

                xrTable.Rows.Add(xrRow);
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail1.Controls.Add(xrTable);
            this.Detail1.HeightF = xrTable.HeightF;

            if (xrTable.Rows.Count> nRowCount + 3)
            {
                this.Detail1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
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
            XRPictureBox pictureBox = sender as XRPictureBox;
            pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pictureBox.ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Resources/report/form/megazen/images/megazen.bmp");
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
        #endregion
    }
}
