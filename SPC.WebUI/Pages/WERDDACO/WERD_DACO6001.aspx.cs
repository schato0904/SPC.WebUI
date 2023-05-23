using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.WERD.Biz;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using DevExpress.XtraReports.UI;

namespace SPC.WebUI.Pages.WERDDACO
{
    public partial class WERD_DACO6001 : WebUIBasePage
    {
        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["WERD_DACO6001"];
            }
            set
            {
                Session["WERD_DACO6001"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["WERD_DACO6001_2"];
            }
            set
            {
                Session["WERD_DACO6001_2"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["WERD_DACO6001_3"];
            }
            set
            {
                Session["WERD_DACO6001_3"] = value;
            }
        }
        #endregion

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

            if (!Page.IsCallback)
            { }
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

                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
            }
        }
        #endregion

        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        { }
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
        { }
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
        { }
        #endregion

        #region 함수

        #region devChart_ResizeTo
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

        #region WERD_DACO6001_LST
        void WERD_DACO6001_LST()
        {
            string errMsg = String.Empty;
            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                ds = biz.WERD_DACO6001_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devChart1.JSProperties["cpResultCode"] = "0";
                    devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";

                    dtChart1 = null;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count < 5)
                    {
                        int empty = 5 - ds.Tables[0].Rows.Count;

                        string t = " ";

                        for (int i = 0; i < empty; i++)
                        {
                            ds.Tables[0].Rows.Add(i, t, 0);
                            t = t + t;
                        }
                    }
                    dtChart1 = ds.Tables[0];
                }
            }
        }
        #endregion

        #region WERD_DACO6001_LST2
        void WERD_DACO6001_LST2(string arg)
        {
            string errMsg = String.Empty;
            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_COMPANYNM", arg);
                ds = biz.WERD_DACO6001_LST2(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devChart2.JSProperties["cpResultCode"] = "0";
                devChart2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devChart2.JSProperties["cpResultCode"] = "0";
                    devChart2.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";

                    dtChart2 = null;
                }
                else
                {
                    dtChart2 = ds.Tables[0];
                }
            }
        }
        #endregion

        #region WERD_DACO6001_LST3
        void WERD_DACO6001_LST3(string arg)
        {
            string errMsg = String.Empty;
            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_COMPANYNM", arg);
                ds = biz.WERD_DACO6001_LST2(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devChart3.JSProperties["cpResultCode"] = "0";
                devChart3.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devChart3.JSProperties["cpResultCode"] = "0";
                    devChart3.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";

                    dtChart3 = null;
                }
                else
                {
                    dtChart3 = ds.Tables[1];
                }
            }
        }
        #endregion

        #region devChart1_Drawing
        protected void devChart1_Drawing()
        {
            if (dtChart1 != null)
            {
                devChart1.Series.Clear();
                DevExpressLib.SetChartBarLineSeries(devChart1, "집계", "F_COMPANYNM", "F_NGCOUNT", System.Drawing.Color.LightBlue, 0.6);
                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                devChart1.SelectionMode = DevExpress.XtraCharts.ElementSelectionMode.Single;
                devChart1.SeriesSelectionMode = DevExpress.XtraCharts.SeriesSelectionMode.Point;

                DevExpressLib.SetChartLegend(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, true, 0, 0, 0, 0, null, null);
            }
        }
        #endregion

        #region devChart2_Drawing
        protected void devChart2_Drawing(string arg)
        {
            if (dtChart2 != null)
            {
                DataTable cdt = dtChart2.Copy();

                devChart2.Series.Clear();
                Series series = new Series("", ViewType.Doughnut);
                ((DoughnutSeriesView)series.View).HoleRadiusPercent = 30;
                ((DoughnutSeriesView)series.View).SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Clockwise;

                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(cdt.Rows[i]["F_ITEMNM"].ToString(), new double[] { Convert.ToDouble(cdt.Rows[i]["F_NGCOUNT"]) }));
                }
                series.Label.TextPattern = "{A}: {VP:p2}";

                devChart2.Titles.Clear();
                devChart2.Series.Add(series);
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                tlt1.Text = arg + " 불량품목 점유율";
                tlt1.Font = font;
                devChart2.Titles.Add(tlt1);
                devChart2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                devChart2.DataSource = cdt;
                devChart2.DataBind();
            }
        }
        #endregion

        #region devChart3_Drawing
        protected void devChart3_Drawing(string arg)
        {
            if (dtChart3 != null)
            {
                DataTable cdt = dtChart3.Copy();

                devChart3.Series.Clear();
                Series series = new Series("", ViewType.Doughnut);
                ((DoughnutSeriesView)series.View).HoleRadiusPercent = 30;
                ((DoughnutSeriesView)series.View).SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Clockwise;

                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(cdt.Rows[i]["F_NGTYPE"].ToString(), new double[] { Convert.ToDouble(cdt.Rows[i]["F_NGCOUNT"]) }));
                }
                series.Label.TextPattern = "{A}: {VP:p2}";

                devChart3.Titles.Clear();
                devChart3.Series.Add(series);
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                tlt1.Text = arg + " 불량유형 점유율";
                tlt1.Font = font;
                devChart3.Titles.Add(tlt1);
                devChart3.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                devChart3.DataSource = cdt;
                devChart3.DataBind();
            }
        }
        #endregion

        #endregion

        #region 이벤트
        #region devChart1_CustomCallback
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            WERD_DACO6001_LST();
            devChart1_Drawing();
        }
        #endregion

        #region devChart2_CustomCallback
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(Convert.ToInt32(oParams[0]) / 2) - 5, Convert.ToInt32(oParams[1]));

            if (oParams.Length == 3)
            {
                string arg = oParams[2];
                WERD_DACO6001_LST2(arg);
                devChart2_Drawing(arg);
            }

        }
        #endregion

        #region devChart3_CustomCallback
        protected void devChart3_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(Convert.ToInt32(oParams[0]) / 2) - 5, Convert.ToInt32(oParams[1]));

            if (oParams.Length == 3)
            {
                string arg = oParams[2];
                WERD_DACO6001_LST3(arg);
                devChart3_Drawing(arg);
            }
        }
        #endregion
        #endregion

    }
}