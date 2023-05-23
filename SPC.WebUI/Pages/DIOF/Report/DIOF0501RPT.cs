using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SPC.WebUI.Common;
using System.Data;
using System.Linq;

namespace SPC.WebUI.Pages.DIOF.Report
{
    public partial class DIOF0501RPT : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수
        string m_sMonth = String.Empty;
        string m_sMachIDX = String.Empty;
        string m_sMachNM = String.Empty;

        DataTable m_dtCheckSheet = null;
        DataTable m_dtResponse = null;
        DataTable m_dtConfirm = null;

        XRTable xrCheckSheetTable = new XRTable();
        XRTable xrResponseTable = new XRTable();

        int nSizeIndex = 0;
        #endregion
        
        #region 생성자
        public DIOF0501RPT(string sMonth, string sMachIDX, string sMachNM, DataTable dtCheckSheet, DataTable dtResponse, DataTable dtConfirm)
        {
            InitializeComponent();

            this.m_sMonth = sMonth;
            this.m_sMachIDX = sMachIDX;
            this.m_sMachNM = sMachNM;
            this.m_dtCheckSheet = dtCheckSheet;
            this.m_dtResponse = dtResponse;
            this.m_dtConfirm = dtConfirm;

            // 리포트 정보
            SetReportInfomation();

            // 리포트 체크시트
            RendorReportCheckSheet();

            // 리포트 불량조치이력
            RendorReportResponse();

            this.Detail1.Controls.Add(xrCheckSheetTable);
            this.Detail1.HeightF = xrCheckSheetTable.HeightF;

            if (nSizeIndex > 13)
                this.Detail1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;

            this.Detail2.Controls.Add(xrResponseTable);
            this.Detail2.HeightF = xrResponseTable.HeightF;
        }
        #endregion

        #region 사용자 함수

        #region 리포트 정보
        private void SetReportInfomation()
        {
            // 설비명
            DevExpressLib.SetText(xrTableCell24, this.m_sMachNM);

            // 점검년월
            DevExpressLib.SetText(xrTableCell18, DateTime.Parse(String.Format("{0}-01", this.m_sMonth)).ToString("yyyy년 MM월"));
        }
        #endregion

        #region 리포트 체크시트
        private void RendorReportCheckSheet()
        {
            xrCheckSheetTable.Font = new Font("돋움", 8F, FontStyle.Regular);
            xrCheckSheetTable.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom);
            xrCheckSheetTable.BorderWidth = 1F;
            xrCheckSheetTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrCheckSheetTable.BeginInit();

            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            int iDay = 0;

            foreach (DataRow dr in this.m_dtCheckSheet.Rows)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 40F;

                // 점검부위
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 30F;
                xrCell.Text = dr["F_INSPNO"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 점검항목
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 80F;
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0);
                xrCell.Text = dr["F_INSPNM"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WordWrap = true;
                xrRow.Cells.Add(xrCell);

                // 점검내용
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 139F;
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0);
                xrCell.Text = dr["F_INSPREMARK"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WordWrap = true;
                xrRow.Cells.Add(xrCell);

                // 점검방법
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 140F;
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0);
                xrCell.Text = dr["F_INSPWAY"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WordWrap = true;
                xrRow.Cells.Add(xrCell);

                // 점검주기
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 60F;
                xrCell.Text = GetCycleNM(dr["F_CYCLECD"].ToString(), dr["F_CYCLENM"].ToString(), dr["F_NUMBER"].ToString(), dr["F_CHASU"].ToString());
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WordWrap = true;
                xrRow.Cells.Add(xrCell);

                string sINSPKINDCD = dr["F_INSPKINDCD"].ToString();
                string sMEASURE = String.Empty;

                // 일자별
                for (iDay = 0; iDay < 31; iDay++)
                {
                    sMEASURE = dr[String.Format("F_DAY{0}", iDay + 1)].ToString();

                    xrCell = new XRTableCell();
                    xrCell.CanGrow = false;
                    xrCell.WidthF = 20F;
                    xrCell.Angle = sINSPKINDCD.Equals("AAG601") ? 270F : 0F;
                    xrCell.Font = new Font("돋움", 7F, FontStyle.Regular);
                    xrCell.Text = GetMeasure(sMEASURE);
                    xrCell.BackColor = GetJudge(sMEASURE);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    xrCell.WordWrap = true;
                    xrRow.Cells.Add(xrCell);
                }

                xrCheckSheetTable.Rows.Add(xrRow);

                nSizeIndex++;
            }

            // 관리자점검
            xrRow = new XRTableRow();
            xrRow.HeightF = 20F;

            // 점검부위
            xrCell = new XRTableCell();
            xrCell.WidthF = 449F;
            xrCell.Text = "관리자 점검 확인";
            xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrRow.Cells.Add(xrCell);

            bool bManagerCheck = false;
            
            // 일자별
            for (iDay = 0; iDay < 31; iDay++)
            {
                bManagerCheck = this.m_dtConfirm == null ? false : this.m_dtConfirm.AsEnumerable().Any(row => (iDay + 1) == row.Field<Int32>("F_DAY"));

                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 20F;
                xrCell.Text = !bManagerCheck ? "" : "V";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.WordWrap = true;
                xrRow.Cells.Add(xrCell);
            }

            xrCheckSheetTable.Rows.Add(xrRow);

            xrCheckSheetTable.AdjustSize();
            xrCheckSheetTable.EndInit();
        }
        #endregion

        #region 리포트 불량조치이력
        private void RendorReportResponse()
        {
            xrResponseTable.Font = new Font("돋움", 8F, FontStyle.Regular);
            xrResponseTable.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom);
            xrResponseTable.BorderWidth = 1F;
            xrResponseTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrResponseTable.BeginInit();

            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            string sStatus = "";

            foreach (DataRow dr in this.m_dtResponse.Rows)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 40F;

                // 점검일
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 110F;
                xrCell.Text = dr["F_OCCURDT"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 점검항목
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 100F;
                xrCell.Text = dr["F_INSPNM"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 이상내역
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 239F;
                xrCell.Text = dr["F_NGREMK"].ToString();
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0);
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WordWrap = true;
                xrRow.Cells.Add(xrCell);

                // 등록자
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 80F;
                xrCell.Text = dr["F_USERNM"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 조치여부
                sStatus = dr["F_STATUS"].ToString();

                // 조치유형
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 100F;
                xrCell.Text = sStatus.Equals("AAG902") ? dr["F_RESPTYPENM"].ToString() : "미조치";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 조치내역
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 280F;
                xrCell.Text = sStatus.Equals("AAG902") ? dr["F_RESPREMK"].ToString() : "";
                xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0);
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrCell.WordWrap = true;
                xrRow.Cells.Add(xrCell);

                // 조치자
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 80F;
                xrCell.Text = sStatus.Equals("AAG902") ? dr["F_RESPUSER"].ToString() : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                // 조치일
                xrCell = new XRTableCell();
                xrCell.CanGrow = false;
                xrCell.WidthF = 80F;
                xrCell.Text = sStatus.Equals("AAG902") ? dr["F_RESPDT"].ToString() : "";
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrRow.Cells.Add(xrCell);

                xrResponseTable.Rows.Add(xrRow);

                nSizeIndex++;
            }

            xrResponseTable.AdjustSize();
            xrResponseTable.EndInit();
        }
        #endregion

        #region 점검주기
        private string GetCycleNM(string sCYCLECD, string sCYCLENM, string  sNUMBER, string sCHASU)
        {
            string sValue = String.Empty;

            switch (sCYCLECD)
            {
                case "AAG401":
                    if (sCHASU.Equals("1")) sValue = "매일"; else sValue = String.Format("{0}차/일", sNUMBER);
                    break;
                case "AAG407":
                    if (sNUMBER.Equals("1")) sValue = "주간"; else sValue = "야간";
                    break;
                default:
                    sValue = sCYCLENM;
                    break;
            }

            return sValue;
        }
        #endregion

        #region 점검결과
        private string GetMeasure(string sMEASURE)
        {
            string sValue = String.Empty;

            if (sMEASURE.Equals("X") || sMEASURE.Equals("--"))
                sValue = "";
            else
                sValue = sMEASURE.Split('|')[0];

            return sValue;
        }

        private Color GetJudge(string sMEASURE)
        {
            Color color = Color.Transparent;

            if (!sMEASURE.Equals("X") && !sMEASURE.Equals("--"))
            {
                string sJudge = sMEASURE.Split('|')[1];

                if (sJudge.Equals("AAG702"))
                    color = Color.LightGray;
            }

            return color;
        }
        #endregion

        #endregion

        #region 사용자 이벤트

        #region xrTableCel_LineBreak
        /// <summary>
        /// xrTableCel_LineBreak
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">System.Drawing.Printing.PrintEventArgs</param>
        private void xrTableCel_LineBreak(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpressLib.LineBreak(sender);
        }
        #endregion

        #region xrTableCel_LineBreak2
        /// <summary>
        /// xrTableCel_LineBreak2
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">System.Drawing.Printing.PrintEventArgs</param>
        private void xrTableCel_LineBreak2(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpressLib.LineBreak(sender, "|");
        }
        #endregion

        #endregion
    }
}
