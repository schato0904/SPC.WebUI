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
using SPC.WebUI.Common;
using SPC.PLCM.Biz;
using SPC.Common.Biz;
using SPC.WebUI.Common.Library;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraCharts.Native;
using System.Text;

namespace SPC.WebUI.Pages.PLCM
{
    public partial class PLCM2006 : WebUIBasePage
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
            get { return Session["PLCM2006_Grid"] as DataTable; }
            set { Session["PLCM2006_Grid"] = value; }
        }

        // 우측목록
        DataTable CachedData1
        {
            get { return Session["PLCM2006_Grid1"] as DataTable; }
            set { Session["PLCM2006_Grid1"] = value; }
        }
        DataTable CachedData2
        {
            get { return Session["PLCM2006_Grid2"] as DataTable; }
            set { Session["PLCM2006_Grid2"] = value; }
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
            //BindCombo(schF_MACHCD);
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
            GetMaster();
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

            if (!this.IsPostBack)
            {
                this.CachedData = null;
                this.CachedData1 = null;
                this.CachedData2 = null;
            }
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
            this.devGrid2.DataSource = this.CachedData1;
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
            devGrid1.JSProperties["cpResult1"] = "";
            devGrid1.JSProperties["cpResult2"] = "";
            devGrid2.JSProperties["cpResult1"] = "";
            devGrid2.JSProperties["cpResult2"] = "";

            if (e.Parameters.Equals("clear"))
            {
                CachedData1 = null;
                this.devGrid1.DataSourceID = null;
                this.devGrid1.DataSource = null;
                this.devGrid1.DataBind();
                return;
            }
            else
            {
                string errMsg = string.Empty;
                this.CachedData1 = this.GetDataRightBody(e.Parameters, out errMsg);
                devGrid1.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid1.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;

                devGrid2.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid2.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            }
            devGrid1.DataBind();
            devGrid2.DataBind();
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
            //string errMsg = String.Empty;
            this.Download_Excelfile();
            //devGrid1.DataSource = CachedData1;
            //devGrid1.DataBind();
            //devGrid1Exporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비Data조회", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }

        void Download_Excelfile()
        {
            PrintingSystemBase ps = new PrintingSystemBase();
            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
            PrintableComponentLinkBase link3 = new PrintableComponentLinkBase(ps);
            link1.Component = devGrid1Exporter;
            link3.Component = devGrid1Exporter2;

            PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
            int w = (int)this.hidChartWidth.Number;
            int h = (int)this.hidChartHeight.Number;
            devChart1.Width = Unit.Pixel(w);
            devChart1.Height = Unit.Pixel(h);
            link2.Component = ((IChartContainer)devChart1).Chart;

            devChart1.DataSource = CachedData1;
            devChart1.DataBind();

            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link2, link1, link3 });
            compositeLink.CreateDocument();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                string file_name = Uri.EscapeUriString(string.Format("{0}_{1}.xlsx", "PR코팅 데이터조회", DateTime.Now.ToString("yyyyMMddHHmmss")));
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
        }
        #endregion

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GET_INSP();
        }
        #endregion

        #region devGrid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            this.devGrid.DataSource = this.CachedData;
        }
        #endregion

        #region schF_MASTERID_Callback
        protected void schF_MASTERID_Callback(object sender, CallbackEventArgsBase e)
        {
            GetMaster();
        }
        #endregion

        #region devChart1_CustomCallback
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            devChart1.DataSource = null;

            if (CachedData1 != null)
            {
                devChart1.Series.Clear();

                DevExpressLib.SetChartLineSeries(devChart1, "변위 측정값", "F_NO", "data1");
                DevExpressLib.SetChartLineSeries(devChart1, "변위 상한", "F_NO", "F_MAX1", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart1, "변위 하한", "F_NO", "F_MIN1", System.Drawing.Color.Red);

                DevExpressLib.SetChartLineSeries(devChart1, "두께 측정값", "F_NO", "data2");
                DevExpressLib.SetChartLineSeries(devChart1, "두께 상한", "F_NO", "F_MAX2", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart1, "두께 하한", "F_NO", "F_MIN2", System.Drawing.Color.Red);

                devChart1.DataSource = CachedData1;
                devChart1.DataBind();

                DevExpressLib.SetChartLegend(devChart1);
                XYDiagram diagram = (XYDiagram)devChart1.Diagram;

                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            }
        }
        #endregion

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="Width">Int32</param>
        void devChart_ResizeTo(object sender, Int32 Width, Int32 Height)
        {
            if (Width >= 0 && Height >= 0)
            {
                DevExpress.XtraCharts.Web.WebChartControl chart = sender as DevExpress.XtraCharts.Web.WebChartControl;
                chart.Width = new Unit(Width);
                chart.Height = new Unit(Height);

                chart.JSProperties["cpFunction"] = "resizeTo";
                chart.JSProperties["cpChartWidth"] = Width.ToString();
            }
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 컨트롤 데이터 조회
        // 조회 시 사용
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            result["F_FROMYMD"] = GetFromDt();
            result["F_TOYMD"] = GetToDt();
            result["F_MACHCD"] = "11";
            result["F_RECIPID"] = "PR코팅";
            result["F_MASTERID"] = (this.schF_MASTERID.Value ?? "").ToString();

            return result;
        }

        #endregion

        #region 항목조회
        void GET_INSP()
        {
            var p1 = this.GetRightParameter();
            string errMsg = string.Empty;
            this.CachedData = this.GetDataLeftBody(p1, out errMsg);
            devGrid.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
            devGrid.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            devGrid.DataBind();
        }

        #endregion

        #region 마스터ID구하기
        void GetMaster()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = "11";
                oParamDic["F_FROMDT"] = GetFromDt();
                oParamDic["F_TODT"] = GetToDt();
                oParamDic["F_RECIPID"] = "PR코팅";
                ds = biz.PLC02_LST(oParamDic, out errMsg);
            }
            schF_MASTERID.DataSource = ds;
            schF_MASTERID.TextField = "tMasterID";
            schF_MASTERID.ValueField = "tMasterID";
            schF_MASTERID.DataBind();
            schF_MASTERID.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
        }
        #endregion

        #region 좌측 내용 조회 마스터리스트
        /// <summary>
        /// 우측 내용 조회(추진일정 목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataLeftBody(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = "11";
                oParamDic["F_FROMDT"] = dic.GetString("F_FROMYMD");
                oParamDic["F_TODT"] = dic.GetString("F_TOYMD");
                oParamDic["F_RECIPID"] = dic.GetString("F_RECIPID");
                oParamDic["F_MASTERID"] = dic.GetString("F_MASTERID");
                ds = biz.PLC05_LST(oParamDic, out errMsg);
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
        /// 우측 내용 조회(추진일정 목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataRightBody(string tid, out string errMsg)
        {
            DataTable dt = new DataTable();
            DataTable dtTemp = new DataTable();

            StringBuilder sb = null;
            StringBuilder sb2 = null;
            devGrid1.JSProperties["cpResult1"] = "";
            devGrid2.JSProperties["cpResult1"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();

                oParamDic["F_MACHCD"] = "11";
                oParamDic["F_MEASCD1"] = "";
                oParamDic["F_RECIPID"] = "PR코팅";
                oParamDic["F_TID"] = tid;
                oParamDic["F_NO"] = (this.srcF_NO.Text ?? "").ToString();
                oParamDic["F_X"] = "";
                oParamDic["F_Y"] = "";

                ds = biz.USP_PLCM2006_LST1(oParamDic, out errMsg);
            }

            sb = new StringBuilder();
            sb2 = new StringBuilder();
            dt1 = ds.Tables[1].Copy();
            dt2 = ds.Tables[2].Copy();

            DataRow dtRow = dt1.Rows[0];
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                if (i > 0) sb.Append("|");
                sb.Append(dtRow[i].ToString());
            }

            DataRow dtRow2 = dt2.Rows[0];
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                if (i > 0) sb2.Append("|");
                sb2.Append(dtRow2[i].ToString());
            }

            devGrid1.JSProperties["cpResult1"] = sb.ToString();
            devGrid1.JSProperties["cpResult2"] = sb2.ToString();

            devGrid2.JSProperties["cpResult1"] = sb.ToString();
            devGrid2.JSProperties["cpResult2"] = sb2.ToString();
            return ds.Tables[0];
        }
        #endregion

        #region 설비 목록 조회
        /// <summary>
        /// 우측 내용 조회(추진일정 목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataCombo(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHTYPECD"] = "2";
                oParamDic["F_USEYN"] = "1";
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.PLC01_LST(oParamDic, out errMsg);
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