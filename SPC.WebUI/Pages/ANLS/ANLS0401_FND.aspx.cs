using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ANLS.Biz;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraPrinting;
using System.Drawing.Imaging;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0401_FND : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티

        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["ADTR0401_1"];
            }
            set
            {
                Session["ADTR0401_1"] = value;
            }
        }

        DataSet DsChart1
        {
            get
            {
                return (DataSet)Session["ADTR0401_2"];
            }
            set
            {
                Session["ADTR0401_2"] = value;
            }
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

                // Grid Callback Init
                pnlChart.JSProperties["cpResultCode"] = "";
                pnlChart.JSProperties["cpResultMsg"] = "";
            }
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

            DsChart1 = new DataSet();
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

        #region 사용자 정의 함수

        #region Data조회
        void ANLS0401_LST(DevExpress.XtraCharts.Web.WebChartControl chart, string arrSerial, int dsNum)
        {
            string errMsg = String.Empty;
            ////F_ITEMCD;F_SERIALNO;F_INSPDETAIL;F_SIRYO;F_WORKCD;F_WORKNM
            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", arrSerial.Split('|')[4]);
                oParamDic.Add("F_SERIALNO", arrSerial.Split('|')[1]);
                ds = biz.ANLS0401_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                pnlChart.JSProperties["cpResultCode"] = "0";
                pnlChart.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                ds.Tables[0].TableName = arrSerial.Split('|')[2];
                chart_Draw(chart, ds, arrSerial.Split('|')[2]);
                if (DsChart1.Tables[ds.Tables[0].TableName] != null) {
                    DsChart1.Tables.Remove(ds.Tables[0].TableName);
                }
                DsChart1.Tables.Add(ds.Tables[0].Copy());
            }
        }
        #endregion

        protected void pnlChart_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');

            DevExpress.XtraCharts.Web.WebChartControl[] arrChart = new DevExpress.XtraCharts.Web.WebChartControl[12];
            for (int i = 0; i < 12; i++)
            {
                arrChart[i] = pnlChart.FindControl("devChart" + (i + 1)) as DevExpress.XtraCharts.Web.WebChartControl;
                arrChart[i].Width = new Unit(oParams[0]);
                arrChart[i].Height = new Unit(oParams[1]);
            }

            string[] INSPCD = hidINSPCD.Text.Split(',');

            if (INSPCD[0] != "")
            {
                for (int i = 0; i < INSPCD.Length; i++)
                {
                    ANLS0401_LST(arrChart[i], INSPCD[i],i);
                }
            }
        }

        #region ChartDraw
        protected void chart_Draw(DevExpress.XtraCharts.Web.WebChartControl chart, DataSet ds, string strINSPDETAIL)
        {
            if (ds.Tables[0] != null)
            {
                string worknm = ds.Tables[0].Rows[1]["WORKNM"].ToString();
                chart.Series.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(chart, "xBar", "F_WORKDATE", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153));
                DevExpressLib.SetChartLineSeries(chart, "상한", "F_WORKDATE", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(chart, "하한", "F_WORKDATE", "F_MIN", System.Drawing.Color.Red);

                //dtChart1 = ds.Tables[0].Copy();
                chart.DataSource = ds.Tables[0];
                chart.DataBind();
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
                
                DevExpress.XtraCharts.ChartTitle ct = new DevExpress.XtraCharts.ChartTitle();
                ct.Text = worknm + " \n [" + strINSPDETAIL + "]";
                ct.Font = new System.Drawing.Font("",10);
                
                chart.Titles.Add(ct);
                DevExpressLib.SetCrosshairOptions(chart);
                DevExpressLib.SetChartDiagram(chart, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}",false);
            }
        }

        protected void chart_Draw2(DevExpress.XtraCharts.Web.WebChartControl chart, DataTable dt, string strINSPDETAIL)
        {
            if (dt != null)
            {
                string worknm = dt.Rows[1]["WORKNM"].ToString();
                chart.Series.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                foreach (DataRow dtRow in dt.Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(chart, "xBar", "F_WORKDATE", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153));
                DevExpressLib.SetChartLineSeries(chart, "상한", "F_WORKDATE", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(chart, "하한", "F_WORKDATE", "F_MIN", System.Drawing.Color.Red);

                //dtChart1 = ds.Tables[0].Copy();
                chart.DataSource = dt;
                chart.DataBind();
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

                DevExpress.XtraCharts.ChartTitle ct = new DevExpress.XtraCharts.ChartTitle();
                ct.Text = worknm + " \n [" + strINSPDETAIL + "]";
                ct.Font = new System.Drawing.Font("", 10);

                chart.Titles.Add(ct);
                DevExpressLib.SetCrosshairOptions(chart);
                DevExpressLib.SetChartDiagram(chart, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}", false);
            }
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (DsChart1.Tables[0].Rows.Count > 0)
            {
                DevExpress.XtraCharts.Web.WebChartControl[] arrChart = new DevExpress.XtraCharts.Web.WebChartControl[12];
                for (int i = 0; i < 12; i++)
                {
                    arrChart[i] = pnlChart.FindControl("devChart" + (i + 1)) as DevExpress.XtraCharts.Web.WebChartControl;
                }

                for (int j = 0; j < DsChart1.Tables.Count; j++)
                {
                    if (DsChart1.Tables[j] != null)
                    {
                        chart_Draw2(arrChart[j], DsChart1.Tables[j], DsChart1.Tables[j].TableName.ToString());
                    }
                }

                PrintingSystemBase ps = new PrintingSystemBase();

                PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
                link1.Component = ((IChartContainer)devChart1).Chart;
                PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
                link2.Component = ((IChartContainer)devChart2).Chart;
                PrintableComponentLinkBase link3 = new PrintableComponentLinkBase(ps);
                link3.Component = ((IChartContainer)devChart3).Chart;
                PrintableComponentLinkBase link4 = new PrintableComponentLinkBase(ps);
                link4.Component = ((IChartContainer)devChart4).Chart;
                PrintableComponentLinkBase link5 = new PrintableComponentLinkBase(ps);
                link5.Component = ((IChartContainer)devChart5).Chart;
                PrintableComponentLinkBase link6 = new PrintableComponentLinkBase(ps);
                link6.Component = ((IChartContainer)devChart6).Chart;
                PrintableComponentLinkBase link7 = new PrintableComponentLinkBase(ps);
                link7.Component = ((IChartContainer)devChart7).Chart;
                PrintableComponentLinkBase link8 = new PrintableComponentLinkBase(ps);
                link8.Component = ((IChartContainer)devChart8).Chart;
                PrintableComponentLinkBase link9 = new PrintableComponentLinkBase(ps);
                link9.Component = ((IChartContainer)devChart9).Chart;
                PrintableComponentLinkBase link10 = new PrintableComponentLinkBase(ps);
                link10.Component = ((IChartContainer)devChart10).Chart;
                PrintableComponentLinkBase link11 = new PrintableComponentLinkBase(ps);
                link11.Component = ((IChartContainer)devChart11).Chart;
                PrintableComponentLinkBase link12 = new PrintableComponentLinkBase(ps);
                link12.Component = ((IChartContainer)devChart12).Chart;

                CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
                compositeLink.Links.AddRange(new object[] { link1, link2, link3, link4, link5, link6, link7, link8, link9, link10, link11, link12 });
                compositeLink.CreateDocument();

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    string file_name = string.Format("{0}_{1}.xls", "종합산포도", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    //XlsxExportOptions options = new XlsxExportOptions();
                    //options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    //compositeLink.PrintingSystemBase.ExportToXlsx(stream, options);
                    compositeLink.PrintingSystemBase.ExportToXls(stream);
                    Response.Clear();
                    Response.Buffer = false;
                    Response.AppendHeader("Content-Type", "application/xlsx");
                    Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", HttpUtility.UrlEncode(file_name, new System.Text.UTF8Encoding()).Replace("+", "%20")));
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
                ps.Dispose();
            }
        }
        #endregion

        #endregion
    }
}