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
    public partial class RptCATM1305_1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Dictionary<string, string> dic = new Dictionary<string, string>();
        float RowHeight = 70F;

        public RptCATM1305_1(Dictionary<string, string> dic)
        {
            InitializeComponent();
            this.dic = dic;
        }

        private void RptCATM1305_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string errMsg = string.Empty;
            // 0. Header 세팅
            this.lblF_WORKYMD.Text = DateTime.ParseExact(this.dic.GetString("F_WORKYMD"), "yyyy-MM-dd", CultureInfo.CurrentCulture).ToString("yyyy년 MM월 dd일 (dddd)");
            // 1. 데이터 조회
            DataTable dt = this.GetData(this.dic, out errMsg);
            // 2. Row Loop
            foreach (DataRow dr in dt.Rows)
            {
                //  2.1. 행 생성
                XRTableRow tr = this.MakeRow(dr);
                //  2.2. Add Row and Table Heght reset
                this.xrTable2.Rows.Add(tr);
                this.xrTable2.HeightF += this.RowHeight;
                this.SubBand2.HeightF += this.RowHeight;
            }
        }
        
        #region 데이터 행 생성
        XRTableCell GetNewCell(object objText, double weight, float dpi=254F)
        {
            XRTableCell c = new XRTableCell();
            c.Dpi = dpi;
            c.Text = (objText??"").ToString();
            c.Weight = weight;
            return c;
        }

        XRTableRow MakeRow(DataRow dr)
        {
            XRTableRow xrTemplateRow = new XRTableRow();
            xrTemplateRow.Dpi = 254F;
            xrTemplateRow.Weight = 1D;

            List<XRTableCell> cells = new List<XRTableCell>();
            cells.Add(this.GetNewCell(dr["F_MACHNM"], 4D));
            cells.Add(this.GetNewCell(dr["F_ITEMCD"], 4D));
            cells.Add(this.GetNewCell(dr["F_OUTCNT"], 3D));
            cells.Add(this.GetNewCell(dr["F_SCRAP"], 3D));
            cells.Add(this.GetNewCell("", 3D));
            cells.Add(this.GetNewCell(dr["F_LOTNO"], 6D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));

            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));
            cells.Add(this.GetNewCell("", 1D));

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
        protected DataTable GetData(Dictionary<string, string> dic, out string errMsg)
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
                ds = biz.USP_CATM1305_LST2(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = this.AutoNumberTable(ds.Tables[0]);
            }
            return dt;
        }
        #endregion
    }
}
