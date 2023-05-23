﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Data;

namespace SPC.WebUI.Pages.WERD.Report
{
    public partial class WERD5002_DACO_RPT2 : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds1 = null;
        DataTable dtTable1 = null;
        DataTable dtGrid1 = null;
        string[] oParams;

        public WERD5002_DACO_RPT2(string[] _oParams, DataSet _ds1, DataTable _dt1)
        {
            InitializeComponent();

            this.oParams = _oParams;


            this.xrTableCell8.Text = "제품명: " + oParams[1].ToString(); // 오전품목
            this.xrTableCell11.Text = "제품명: " + oParams[2].ToString(); // 오후품목
            this.xrTableCell4.Text = oParams[3].ToString(); // 라인명

            this.xrTableCell1.Text = oParams[7].ToString() + " " + oParams[4].ToString(); // 작성일자 + 작성자            
            this.xrTableCell2.Text = oParams[8].ToString() + " " + oParams[5].ToString(); // 검토일자 + 검토자
            this.xrTableCell3.Text = oParams[9].ToString() + " " + oParams[6].ToString(); // 승인일자 + 승인자

            this.xrTableCell6.Text = oParams[7].ToString(); // 작성일자
            this.xrTableCell7.Text = oParams[4].ToString(); // 작성자

            this.xrTableCell14.Text = oParams[10].ToString(); // 오전검사시간
            this.xrTableCell16.Text = oParams[11].ToString(); // 오전검사시간

            this.ds1 = _ds1;

            string idtstnd = ""; // 실내온도 규격
            
            string idttime11 = ""; // 실내온도 오전시간1
            string idttime21 = ""; // 실내온도 오후시간1

            string idttime12 = ""; // 실내온도 오전시간2
            string idttime22 = ""; // 실내온도 오후시간2
            string idttime23 = ""; // 실내온도 오후시간3

            string idt11 = ""; // 실내온도 오전1
            string idt21 = ""; // 실내온도 오후1

            string idt12 = ""; // 실내온도 오전2
            string idt22 = ""; // 실내온도 오후2
                        
            string idt23 = ""; // 실내온도 오후3

            string hum1 = ""; // 습도 오전
            string hum2 = ""; // 습도 오후

            string fstandard1 = ""; // 냉각수온도 규격 오전
            string fstandard2 = ""; // 냉각수온도 규격 오후
            string ftemp1 = ""; // 냉각수온도 오전
            string ftemp2 = ""; // 냉각수온도 오후

            string astandard1 = ""; // 에어온도 규격 오전
            string astandard2 = ""; // 에어온도 규격 오후
            string atemp1 = ""; // 에어온도 오전
            string atemp2 = ""; // 에어온도 오후

            // 실내온도 규격
            if (ds1.Tables[3].Rows.Count > 0)
            {
                idtstnd = ds1.Tables[3].Rows[0]["F_STANDARD"].ToString();                
            }
            this.xrTableCell9.Text = "실내온도 " + idtstnd + " ℃ ";

            // 실내온도 오전
            if (ds1.Tables[4].Rows.Count > 0)
            {
                idttime11 = ds1.Tables[4].Rows[0]["F_WORKTIME"].ToString();
                idt11 = ds1.Tables[4].Rows[0]["F_MEASURE"].ToString();
                this.xrTableCell10.Text = idttime11 + ":  " + idt11 + " ℃ ";

                if (ds1.Tables[4].Rows.Count > 1)
                {
                    idttime12 = ds1.Tables[4].Rows[1]["F_WORKTIME"].ToString();
                    idt12 = ds1.Tables[4].Rows[1]["F_MEASURE"].ToString();
                    this.xrTableCell18.Text = idttime12 + ":  " + idt12 + " ℃ ";
                }
            }

            // 실내온도 오후
            if (ds1.Tables[5].Rows.Count > 0)
            {
                idttime21 = ds1.Tables[5].Rows[0]["F_WORKTIME"].ToString();
                idt21 = ds1.Tables[5].Rows[0]["F_MEASURE"].ToString();
                this.xrTableCell12.Text = idttime21 + ":  " + idt21 + " ℃ ";

                if (ds1.Tables[5].Rows.Count > 1)
                {
                    idttime22 = ds1.Tables[5].Rows[1]["F_WORKTIME"].ToString();
                    idt22 = ds1.Tables[5].Rows[1]["F_MEASURE"].ToString();
                    this.xrTableCell13.Text = idttime22 + ":  " + idt22 + " ℃ ";
                }

                if (ds1.Tables[5].Rows.Count > 2)
                {
                    idttime23 = ds1.Tables[5].Rows[2]["F_WORKTIME"].ToString();
                    idt23 = ds1.Tables[5].Rows[2]["F_MEASURE"].ToString();
                    this.xrTableCell19.Text = idttime23 + ":  " + idt23 + " ℃ ";
                }
            }

            // 실내습도 오전
            if (ds1.Tables[6].Rows.Count > 0)
            {
                hum1 = ds1.Tables[6].Rows[0]["F_MEASURE"].ToString();
                this.xrTableCell15.Text = "습도          (  " + hum1 + " % )";
            }

            // 실내습도 오후
            if (ds1.Tables[7].Rows.Count > 0)
            {
                hum2 = ds1.Tables[7].Rows[0]["F_MEASURE"].ToString();
                this.xrTableCell17.Text = "습도          (  " + hum2 + " % )";
            }

            // 냉각수온도 오전
            if (ds1.Tables[8].Rows.Count > 0)
            {
                fstandard1 = ds1.Tables[8].Rows[0]["F_STANDARD"].ToString();
                ftemp1 = ds1.Tables[8].Rows[0]["F_MEASURE"].ToString();
                this.xrTableCell20.Text = "냉각수 온도 " + fstandard1 + " ℃          (  " + ftemp1 + " ℃ )";
            }

            // 냉각수온도 오후
            if (ds1.Tables[9].Rows.Count > 0)
            {
                fstandard2 = ds1.Tables[9].Rows[0]["F_STANDARD"].ToString();
                ftemp2 = ds1.Tables[9].Rows[0]["F_MEASURE"].ToString();
                this.xrTableCell22.Text = "냉각수 온도 " + fstandard2 + " ℃          (  " + ftemp2 + " ℃ )";
            }

            if (ds1.Tables[10].Rows.Count > 0)
            {
                astandard1 = ds1.Tables[10].Rows[0]["F_STANDARD"].ToString();
                atemp1 = ds1.Tables[10].Rows[0]["F_MEASURE"].ToString();
                this.xrTableCell21.Text = "에어 온도 " + astandard1 + " ℃          (  " + atemp1 + " ℃ )";
            }

            if (ds1.Tables[11].Rows.Count > 0)
            {
                astandard2 = ds1.Tables[11].Rows[0]["F_STANDARD"].ToString();
                atemp2 = ds1.Tables[11].Rows[0]["F_MEASURE"].ToString();
                this.xrTableCell23.Text = "에어 온도 " + astandard2 + " ℃          (  " + atemp2 + " ℃ )";
            }

            this.dtTable1 = _dt1;
            this.dtGrid1 = new DataTable();

            this.dtGrid1.Columns.Add("F_MEAINSPNM");
            this.dtGrid1.Columns.Add("F_STANDARD");
            for (int i = 0; i < 10; i++)
            {
                this.dtGrid1.Columns.Add(String.Format("F_DATA{0}", i + 1));
            }

            foreach (DataRow dtRow1 in dtTable1.Rows)
            {
                int i = 0;
                DataRow dtNewRow = dtGrid1.NewRow();
                dtNewRow["F_MEAINSPNM"] = dtRow1["F_MEAINSPNM"].ToString();
                dtNewRow["F_STANDARD"] = dtRow1["F_STANDARD"].ToString();
                for (int j = 0; j < 10; j++)
                {
                    dtNewRow[String.Format("F_DATA{0}", j + 1)] = dtRow1[String.Format("F_DATA{0}", j + 1)].ToString();
                }
                this.dtGrid1.Rows.Add(dtNewRow);

            }

            DataColumnCollection columns = this.dtGrid1.Columns;

            XRTable xrTable = new XRTable();
            xrTable.Font = new Font("맑은 고딕", 8F, FontStyle.Regular);
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable.BorderWidth = 1F;
            xrTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrTable.BeginInit();

            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            for (int i = 0; i < this.dtGrid1.Rows.Count; i++)
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 25F;

                //검사항목
                xrCell = new XRTableCell();
                xrCell.WidthF = 200F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_MEAINSPNM"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                //규격
                xrCell = new XRTableCell();
                xrCell.WidthF = 200F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_STANDARD"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                //데이터                
                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA1"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA2"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA3"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA4"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA5"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA6"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA7"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA8"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA9"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);

                xrCell = new XRTableCell();
                xrCell.WidthF = 55F;
                xrCell.Text = this.dtGrid1.Rows[i]["F_DATA10"].ToString();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (i.Equals(0))
                {
                    xrCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                }
                xrRow.Cells.Add(xrCell);



                xrTable.Rows.Add(xrRow);
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.SubBand3.Controls.Add(xrTable);
            this.SubBand3.HeightF = xrTable.HeightF;

        }
    }
}