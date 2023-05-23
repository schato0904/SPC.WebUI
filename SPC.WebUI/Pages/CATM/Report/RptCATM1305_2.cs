using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using DevExpress.XtraReports.UI;

using SPC.CATM.Biz;
using SPC.WebUI.Common.Library;

namespace SPC.WebUI.Pages.CATM.Report
{
    public partial class RptCATM1305_2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Dictionary<string, string> dic = new Dictionary<string, string>();
        float RowHeight = 70F;
        double cwt = 1D; // 불량유형 1칸 너비
        XRTable xrTblData;

        public RptCATM1305_2(Dictionary<string, string> dic)
        {
            InitializeComponent();
            this.dic = dic;
        }

        private void RptCATM1305_2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string errMsg = string.Empty;
            // 0. Header 세팅
            this.lblF_WORKYMD.Text = DateTime.ParseExact(this.dic.GetString("F_WORKYMD"), "yyyy-MM-dd", CultureInfo.CurrentCulture).ToString("yyyy년 MM월 dd일 (dddd)");
            // 1. 데이터 조회
            DataSet ds = this.GetData(this.dic, out errMsg);
            if (!string.IsNullOrWhiteSpace(errMsg) || ds == null || ds.Tables.Count < 3) return;
            DataTable dt0 = ds.Tables[0]; // 메인 데이터
            DataTable dt1 = ds.Tables[1]; // 불량유형 목록
            DataTable dt2 = ds.Tables[2]; // 불량유형별 수량 데이터
            // 2. 테이블 생성 (헤더 포함)
            AddNewTableAndHeader(dt1);
            // 3. Row Loop
            foreach (DataRow dr in dt0.Rows)
            {
                //  2.1. 행 생성
                XRTableRow tr = this.MakeRow(dr, dt1, dt2);
                //  2.2. Add Row and Table Heght reset
                this.xrTblData.Rows.Add(tr);
                this.xrTblData.HeightF += this.RowHeight;
                this.SubBand2.HeightF += this.RowHeight;
            }
        }
        
        #region 데이터표시 테이블 생성
        void AddNewTableAndHeader(DataTable dt)
        {
            XRTableRow HeadRow1 = new XRTableRow();
            XRTableRow HeadRow2 = new XRTableRow();
            List<XRTableCell> cells = new List<XRTableCell>();
            this.xrTblData = new XRTable();

            this.SubBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { this.xrTblData });
            // 
            // xrTblData
            // 
            this.xrTblData.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblData.Dpi = 254F;
            this.xrTblData.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.xrTblData.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTblData.Name = "xrTblData";
            this.xrTblData.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] { HeadRow1, HeadRow2 });
            this.xrTblData.SizeF = new System.Drawing.SizeF(2770F, 140F);
            this.xrTblData.StylePriority.UseBorders = false;
            this.xrTblData.StylePriority.UseFont = false;
            this.xrTblData.StylePriority.UseTextAlignment = false;
            this.xrTblData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTblDataHeadRow
            // 
            HeadRow1.Weight = 1D;
            HeadRow2.Weight = 1D;

            cells.Add(this.GetNewCell("주조기", 2D, 2));
            cells.Add(this.GetNewCell("사용 용해로 No", 2D, 2));
            cells.Add(this.GetNewCell("품번", 2D, 2));
            cells.Add(this.GetNewCell("중자 LOT", 2D, 2));
            cells.Add(this.GetNewCell("작업자", 3D, 2));
            cells.Add(this.GetNewCell("작업시간", 5D, 2));
            cells.Add(this.GetNewCell("작업수량", 1.5D, 2));
            cells.Add(this.GetNewCell("불량수량", 1.5D, 2));
            cells.Add(this.GetNewCell("불량내용", cwt * dt.Rows.Count));
            cells.Add(this.GetNewCell("응고시간", 1.5D, 2));
            cells.Add(this.GetNewCell("경동시간", 1.5D, 2));
            cells.Add(this.GetNewCell("비고", 2D, 2));
            HeadRow1.Cells.AddRange(cells.ToArray());

            cells.Clear();
            cells.Add(this.GetNewCell("", 2D));
            cells.Add(this.GetNewCell("", 2D));
            cells.Add(this.GetNewCell("", 2D));
            cells.Add(this.GetNewCell("", 2D));
            cells.Add(this.GetNewCell("", 3D));
            cells.Add(this.GetNewCell("", 5D));
            cells.Add(this.GetNewCell("", 1.5D));
            cells.Add(this.GetNewCell("", 1.5D));

            foreach (DataRow dr in dt.Rows)
            {
                cells.Add(this.GetNewCell(dr["F_ERRORNM"], cwt));
            }
            cells.Add(this.GetNewCell("", 1.5D));
            cells.Add(this.GetNewCell("", 1.5D));
            cells.Add(this.GetNewCell("", 2D));

            HeadRow2.Cells.AddRange(cells.ToArray());
        }
        #endregion
        
        #region 데이터 행 생성
        XRTableCell GetNewCell(object objText, double weight, int rowspan=1, float dpi=254F)
        {
            XRTableCell c = new XRTableCell();
            c.Dpi = dpi;
            c.Text = (objText??"").ToString();
            c.Weight = weight;
            c.RowSpan = rowspan;
            return c;
        }

        XRTableRow MakeRow(DataRow dr, DataTable dt1, DataTable dt2)
        {
            XRTableRow xrTemplateRow = new XRTableRow();
            xrTemplateRow.Dpi = 254F;
            xrTemplateRow.Weight = 1D;

            List<XRTableCell> cells = new List<XRTableCell>();
            
            cells.Clear();
            cells.Add(this.GetNewCell(dr["F_MACHNM"], 2D));
            cells.Add(this.GetNewCell(dr["F_MELTNM"], 2D));
            cells.Add(this.GetNewCell(dr["F_ITEMCD"], 2D));
            cells.Add(this.GetNewCell(dr["F_LOTNO"], 2D));
            cells.Add(this.GetNewCell(dr["F_WORKER"], 3D));
            cells.Add(this.GetNewCell(dr["F_WORKTIME"], 5D));
            cells.Add(this.GetNewCell(dr["F_PRODCOUNT"], 1.5D));
            cells.Add(this.GetNewCell(dr["F_ERRCOUNT"], 1.5D));

            foreach (DataRow edr in dt1.Rows)
            {
                // 작업지시번호가 존재하고, 해당 작업지시번호의 불량유형별 수량이 존재하면 불량유형별 수량 표기
                if (!string.IsNullOrWhiteSpace((string)dr["F_WORKNO"]) && dt2.AsEnumerable().Any(x => (string)x["F_WORKNO"] == (string)dr["F_WORKNO"] && (string)x["F_ERRORCD"] == (string)edr["F_ERRORCD"]))
                {
                    cells.Add(this.GetNewCell(dt2.AsEnumerable().First(x => (string)x["F_WORKNO"] == (string)dr["F_WORKNO"] && (string)x["F_ERRORCD"] == (string)edr["F_ERRORCD"])["F_ERRORCOUNT"], cwt));
                }
                else
                {
                    cells.Add(this.GetNewCell("", cwt));
                }
            }
            cells.Add(this.GetNewCell(dr["F_WAITTIME_STD"], 1.5D));
            cells.Add(this.GetNewCell(dr["F_SLOPETIME_STD"], 1.5D));
            cells.Add(this.GetNewCell(dr["F_MEMO"], 2D));

            xrTemplateRow.Cells.AddRange(cells.ToArray());
            
            return xrTemplateRow;
        }
        #endregion
        
        #region 사용자 정의
        public DataTable AutoNumberTable(DataTable dt, string FieldName = "No")
        {
            DataTable ndt = new DataTable();

            while (dt.Columns.Contains(FieldName))
            {
                FieldName = FieldName + "_1";
            }

            var col = ndt.Columns.Add(FieldName, typeof(int));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 1;
            col.AutoIncrementStep = 1;

            ndt.Merge(dt);

            return ndt;
        } 
        #endregion

        #region DB처리
        /// <summary>
        /// 좌측 목록 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataSet GetData(Dictionary<string, string> dic, out string errMsg)
        {
            errMsg = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            Dictionary<string, string> oParamDic = new Dictionary<string, string>();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_COMPCD"] = dic.GetString("F_COMPCD");
                oParamDic["F_FACTCD"] = dic.GetString("F_COMPCD");
                oParamDic["F_WORKYMD"] = dic.GetString("F_WORKYMD");
                oParamDic["F_LANGTYPE"] = dic.GetString("F_LANGTYPE");
                ds = biz.USP_CATM1305_LST3(oParamDic, out errMsg);
            }

            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    dt = this.AutoNumberTable(ds.Tables[0]);
            //}
            return ds;
        }
        #endregion

    }
}
