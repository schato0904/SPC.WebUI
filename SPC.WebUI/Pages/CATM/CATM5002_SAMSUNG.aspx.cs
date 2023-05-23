using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using DevExpress.Web;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraCharts.Native;

using SPC.WebUI.Common;
using SPC.CATM.Biz;
using SPC.Common.Biz;
using SPC.WebUI.Common.Library;

namespace SPC.WebUI.Pages.CATM
{
    public partial class CATM5002_SAMSUNG : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        // 좌측목록
        DataTable CachedData
        {
            get { return Session["CATM5002_Grid_SAMSUNG"] as DataTable; }
            set { Session["CATM5002_Grid_SAMSUNG"] = value; }
        }

        // 상단목록
        DataTable CachedData1
        {
            get { return Session["CATM5002_Grid1_SAMSUNG"] as DataTable; }
            set { Session["CATM5002_Grid1_SAMSUNG"] = value; }
        }

        // 하단목록
        DataTable CachedData2
        {
            get { return Session["CATM5002_Grid2"] as DataTable; }
            set { Session["CATM5002_Grid2"] = value; }
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        {
            // 파라미터 처리
            if (!IsPostBack && !string.IsNullOrEmpty(Request["oSetParam"]))
            {
                var pm = this.DeserializeJSON(Request["oSetParam"]);
                //srcF_PJ10MID.Text = pm["F_PJ10MID"];
                //schF_MNGUSER1.Text = pm["F_MNGUSER"];
            }
        }
        #endregion

        #region Web Init
        /// <summary>
        /// Web_Init
        /// </summary>
        void Web_Init()
        {
            // DefaultButton 세팅
            SetDefaultButton();

            // 객체 초기화
            SetDefaultObject();

            // 클라이언트 스크립트
            SetClientScripts();

            // 서버 컨트럴 객체에 기초값 세팅
            SetDefaultValue();
        }
        #endregion

        #region DefaultButton 세팅
        /// <summary>
        /// SetDefaultButton
        /// </summary>
        void SetDefaultButton()
        { }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        {
            //this.CachedData = null;
            //this.CachedData1 = null;
            //this.CachedData2 = null;
            //this.CachedData3 = null;
            //this.CachedData4 = null;
            //this.CachedData5 = null;
            //this.CachedData6 = null;

            //schF_LINECD.DataBind();
            //schF_DEPTCD.DataBind();
            //srcF_TERM.DataBind();
            //BindCombo(schF_CUSTCD);
            BindCombo(schF_MACHCD);
        }
        #endregion

        #region 클라이언트 스크립트
        /// <summary>
        /// SetClientScripts
        /// </summary>
        void SetClientScripts()
        { }
        #endregion

        #region 서버 컨트럴 객체에 기초값 세팅
        /// <summary>
        /// SetDefaultValue
        /// </summary>
        void SetDefaultValue()
        {
        }
        #endregion

        #endregion

        #region 이벤트

        #region Page Init
        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            // Request
            GetRequest();

            if (!IsPostBack)
            {
                this.CachedData = null;
                this.CachedData1 = null;
                this.CachedData2 = null;
            }
            //BindCombo(srcF_CUSTCD);
            //BindCombo(srcF_MELTCD);
        }
        #endregion

        #region Page Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid1 이벤트 처리
        #region devGrid1_DataBinding
        protected void devGrid1_DataBinding(object sender, EventArgs e)
        {
            this.devGrid1.DataSource = this.CachedData1;
        }
        #endregion

        #region devGrid1 CustomCallback
        /// <summary>
        /// devGrid1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "GET")
            {
                var p1 = this.GetRightParameter();
                string errMsg = string.Empty;
                this.CachedData1 = this.GetDataRightBody(p1, out errMsg);
                this.CachedData = this.CachedData1.Copy();

                devGrid1.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid1.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            }
            devGrid1.DataBind();
        }
        #endregion
        #endregion

        #region devChart1 이벤트 처리

        #region devChart1_DataBinding
        protected void devChart1_DataBinding(object sender, EventArgs e)
        {
            this.devChart1.DataSource = this.CachedData;
        }
        #endregion

        #region devChart1_CustomCallback
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            if (this.CachedData == null) return;
            var c = sender as DevExpress.XtraCharts.Web.WebChartControl;
            //DataTable dt = this.CachedData.Copy();

            //int w = 600, h = 400;
            //if (!string.IsNullOrWhiteSpace(e.Parameter))
            //{
            //    var dic = this.DeserializeJSON(e.Parameter);
            //    w = int.TryParse(dic["WIDTH"], out w) ? w : w;
            //    h = int.TryParse(dic["HEIGHT"], out h) ? h : h;
            //}
            int w = (int)this.hidChartWidth.Number;
            int h = (int)this.hidChartHeight.Number;

            c.Width = Unit.Pixel(w);
            c.Height = Unit.Pixel(h);

            c.Titles.Clear();
            if (!string.IsNullOrWhiteSpace(this.schF_PLANYMD.Text) && !string.IsNullOrWhiteSpace(this.schF_MACHCD.Text))
            {
                var title = new ChartTitle();
                title.Text = string.Format("{0} / {1}", this.schF_PLANYMD.Text, this.schF_MACHCD.Text);
                c.Titles.Add(title);
            }

            c.Series.Clear();
            Series s = new Series("금형온도", ViewType.Line);
            (s.View as LineSeriesView).LineMarkerOptions.Size = 3;
            s.ArgumentDataMember = "F_WORKTIME";
            s.ValueDataMembers.AddRange("F_TEMP01");
            //s.DataSource = dt;
            c.Series.Add(s);
            //s = new Series("금형下온도", ViewType.Line);
            //(s.View as LineSeriesView).LineMarkerOptions.Size = 3;
            //s.ArgumentDataMember = "F_WORKTIME";
            //s.ValueDataMembers.AddRange("F_TEMP02");
            ////s.DataSource = dt;
            //c.Series.Add(s);

            Series s2 = new Series("상한", ViewType.Line);
            //s = new Series("상한", ViewType.Line);
            (s2.View as LineSeriesView).LineMarkerOptions.Size = 3;
            (s2.View as LineSeriesView).Color = System.Drawing.Color.Black;
            (s2.View as LineSeriesView).LineStyle.Thickness = 5;
            s2.ArgumentDataMember = "F_WORKTIME";
            s2.ValueDataMembers.AddRange("F_MAX");
            c.Series.Add(s2);

            Series s3 = new Series("하한", ViewType.Line);
            //s = new Series("하한", ViewType.Line);
            (s3.View as LineSeriesView).LineMarkerOptions.Size = 3;
            (s3.View as LineSeriesView).Color = System.Drawing.Color.Black;
            (s3.View as LineSeriesView).LineStyle.Thickness = 5;
            s3.ArgumentDataMember = "F_WORKTIME";
            s3.ValueDataMembers.AddRange("F_MIN");
            c.Series.Add(s3);

            (c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            (c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
            (c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            (c.Diagram as XYDiagram).AxisX.Tickmarks.MinorVisible = false;

            (c.Diagram as XYDiagram).AxisY.Interlaced = true;

            //c.SeriesDataMember = "F_GBN";
            //c.SeriesTemplate.ArgumentDataMember = "F_WORKTIME";
            //c.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "F_TEMP01" });
            //c.SeriesTemplate.View = new LineSeriesView();
            //c.DataSource = dt;

            c.DataBind();
        }
        #endregion
        #endregion

        #region devGrid2 이벤트 처리
        #region devGrid2_DataBinding
        protected void devGrid2_DataBinding(object sender, EventArgs e)
        {
            this.devGrid2.DataSource = this.CachedData2;
        }
        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "GET")
            {
                var p2 = this.GetRightParameter();
                string errMsg = string.Empty;
                this.CachedData2 = this.GetDataRightBody1(p2, out errMsg);
                //this.CachedData = this.CachedData2.Copy();

                devGrid2.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid2.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            }
            devGrid2.DataBind();
        }
        #endregion
        #endregion

        #region devChart2 이벤트 처리

        #region devChart2_DataBinding
        protected void devChart2_DataBinding(object sender, EventArgs e)
        {
            this.devChart2.DataSource = this.CachedData2;
        }
        #endregion

        #region devChart2_CustomCallback
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            if (this.CachedData2 == null) return;
            var c = sender as DevExpress.XtraCharts.Web.WebChartControl;
            //DataTable dt = this.CachedData.Copy();

            //int w = 600, h = 400;
            //if (!string.IsNullOrWhiteSpace(e.Parameter))
            //{
            //    var dic = this.DeserializeJSON(e.Parameter);
            //    w = int.TryParse(dic["WIDTH"], out w) ? w : w;
            //    h = int.TryParse(dic["HEIGHT"], out h) ? h : h;
            //}
            int w = (int)this.hidChartWidth.Number;
            int h = (int)this.hidChartHeight.Number;

            c.Width = Unit.Pixel(w);
            c.Height = Unit.Pixel(h);

            c.Titles.Clear();
            if (!string.IsNullOrWhiteSpace(this.schF_PLANYMD.Text) && !string.IsNullOrWhiteSpace(this.schF_MACHCD.Text))
            {
                var title = new ChartTitle();
                title.Text = string.Format("{0} / {1}", this.schF_PLANYMD.Text, this.schF_MACHCD.Text);
                c.Titles.Add(title);
            }

            c.Series.Clear();
            //Series s = new Series("틸팅시간", ViewType.Line);
            //(s.View as LineSeriesView).LineMarkerOptions.Size = 3;
            //s.ArgumentDataMember = "F_WORKTIME";
            //s.ValueDataMembers.AddRange("F_SLOPETIME");
            ////s.DataSource = dt;
            //c.Series.Add(s);

            Series s = new Series("응고시간", ViewType.Line);
            (s.View as LineSeriesView).LineMarkerOptions.Size = 3;
            s.ArgumentDataMember = "F_WORKTIME";
            s.ValueDataMembers.AddRange("F_WAITTIME");
            //s.DataSource = dt;
            c.Series.Add(s);

            s = new Series("상한", ViewType.Line);
            (s.View as LineSeriesView).LineMarkerOptions.Size = 3;
            (s.View as LineSeriesView).Color = System.Drawing.Color.Black;
            (s.View as LineSeriesView).LineStyle.Thickness = 5;
            s.ArgumentDataMember = "F_WORKTIME";
            s.ValueDataMembers.AddRange("F_WAITTIME_MAX");
            c.Series.Add(s);

            s = new Series("하한", ViewType.Line);
            (s.View as LineSeriesView).LineMarkerOptions.Size = 3;
            (s.View as LineSeriesView).Color = System.Drawing.Color.Black;
            (s.View as LineSeriesView).LineStyle.Thickness = 5;
            s.ArgumentDataMember = "F_WORKTIME";
            s.ValueDataMembers.AddRange("F_WAITTIME_MIN");
            c.Series.Add(s);

            (c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            (c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
            (c.Diagram as XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            (c.Diagram as XYDiagram).AxisX.Tickmarks.MinorVisible = false;

            (c.Diagram as XYDiagram).AxisY.Interlaced = true;

            //c.SeriesDataMember = "F_GBN";
            //c.SeriesTemplate.ArgumentDataMember = "F_WORKTIME";
            //c.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "F_TEMP01" });
            //c.SeriesTemplate.View = new LineSeriesView();
            //c.DataSource = dt;

            c.DataBind();
        }
        #endregion
        #endregion

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string errMsg = String.Empty;
            //INME2003_EXCEL_LST(schF_COMPGBN.Text);

            //devGridExporter.WriteXlsToResponse(String.Format("공정능력평가[{0} ~ {1}]", GetFromDt(), GetToDt()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
            this.Download_Excelfile();
        }

        void Download_Excelfile()
        {

            //this.grdList1.DataSource = GetGrid1Data();
            //this.grdList1.DataBind();
            //grdList1_Exporter.WriteXlsxToResponse(true);
            PrintingSystemBase ps = new PrintingSystemBase();

            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
            //DataTable dt = this.CachedData;
            //grdList1.DataSource = dt;
            //grdList1.DataBind();
            link1.Component = devGrid1Exporter;

            PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
            int w = (int)this.hidChartWidth.Number;
            int h = (int)this.hidChartHeight.Number;
            devChart1.Width = Unit.Pixel(w);
            devChart1.Height = Unit.Pixel(h);
            link2.Component = ((IChartContainer)devChart1).Chart;
            //devChart1.DataSource = dt;
            //devChart1.DataBind();

            PrintableComponentLinkBase link3 = new PrintableComponentLinkBase(ps);
            link3.Component = devGrid2Exporter;

            PrintableComponentLinkBase link4 = new PrintableComponentLinkBase(ps);
            devChart2.Width = Unit.Pixel(w);
            devChart2.Height = Unit.Pixel(h);
            link4.Component = ((IChartContainer)devChart2).Chart;

            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link2, link1, link4, link3 });

            //compositeLink.CreatePageForEachLink();
            compositeLink.CreateDocument();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                string file_name = Uri.EscapeUriString(string.Format("{0}_{1}.xlsx", "주조기 모니터링", DateTime.Now.ToString("yyyyMMddHHmmss")));
                DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;

                compositeLink.PrintingSystemBase.ExportToXlsx(stream, options);
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", "application/xlsx");
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", file_name));
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
            ps.Dispose();
        }
        #endregion

        #region devGrid1Exporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGrid1Exporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            //if (e.RowType == GridViewRowType.Header)
            //{
            //    e.Text = e.Text.Replace("<br />", String.Empty);
            //}
        }
        #endregion

        #region devGrid2Exporter_RenderBrick
        /// <summary>
        /// devGrid2Exporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGrid2Exporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            //if (e.RowType == GridViewRowType.Header)
            //{
            //    e.Text = e.Text.Replace("<br />", String.Empty);
            //}
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 컨트롤 데이터 조회
        // 조회 시 사용
        #region GetRightParameter
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            result["F_PLANYMD"] = (this.schF_PLANYMD.Text ?? "");
            result["F_MACHCD"] = (this.schF_MACHCD.Value ?? "").ToString();

            return result;
        }
        #endregion

        // 콤보박스 목록 조회
        #region BindCombo
        void BindCombo(ASPxComboBox c)
        {
            var dic = new Dictionary<string, string>() { { "F_MACHTYPECD", "02" } };
            string errMsg = string.Empty;
            DataTable dt = this.GetDataCombo(dic, out errMsg);
            c.DataSource = dt;
            c.TextField = "F_MACHNM";
            c.ValueField = "F_MACHCD";
            c.DataBind();
            if (c.Items.Count > 0) c.SelectedIndex = 0;
        }
        #endregion

        #endregion

        #endregion

        #region DB 처리 함수

        #region 우측 내용 조회
        /// <summary>
        /// 우측 내용 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataRightBody(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_PLANYMD"] = dic.GetString("F_PLANYMD");
                oParamDic["F_MACHCD"] = dic.GetString("F_MACHCD");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM5002_LST1_SAMSUNG(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region 우측 내용 조회
        /// <summary>
        /// 우측 내용 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataRightBody1(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_PLANYMD"] = dic.GetString("F_PLANYMD");
                oParamDic["F_MACHCD"] = dic.GetString("F_MACHCD");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM5002_LST1_SAMSUNG(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 1)
            {
                dt = AutoNumberTable(ds.Tables[1]);
            }

            return dt;
        }
        #endregion

        #region 설비 목록 조회
        /// <summary>
        /// 콤보박스 목록 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataCombo(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHTYPECD"] = dic.GetString("F_MACHTYPECD");
                oParamDic["F_USEYN"] = "1";
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM1101_LST3(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #endregion
    }
}