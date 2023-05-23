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

namespace SPC.WebUI.Pages.WERDDACO
{
    public partial class WERD_DACO4001 : WebUIBasePage
    {
        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        protected string oSetParam = String.Empty;
        string sLINECD = String.Empty;
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["WERD_DACO4001"];
            }
            set
            {
                Session["WERD_DACO4001"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["WERD_DACO4001_2"];
            }
            set
            {
                Session["WERD_DACO4001_2"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["WERD_DACO4001_3"];
            }
            set
            {
                Session["WERD_DACO4001_3"] = value;
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
            {
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

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
                devChart2.JSProperties["cpFunction"] = "resizeTo";
                devChart2.JSProperties["cpChartWidth"] = "0";

                dtChart1 = null;
                dtChart2 = null;
                dtChart3 = null;
            }
        }
        #endregion

        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        {
            oSetParam = Request.QueryString.Get("oSetParam") ?? "";
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

        #region 함수

        #region WERD_DACO4001_LST
        void WERD_DACO4001_LST(bool bCallback)
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());

                ds = biz.WERD_DACO4001_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultMsg"] = errMsg;
                devGrid.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart2.JSProperties["cpResultCode"] = "0";
            }

            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devChart1.JSProperties["cpResultCode"] = "0";
                    devChart2.JSProperties["cpResultCode"] = "0";

                    dtChart1 = null;
                    dtChart2 = null;
                    dtChart3 = null;
                }
                else
                {
                    dtChart1 = ds.Tables[0];
                    dtChart2 = ds.Tables[1];
                    dtChart3 = ds.Tables[2];
                    devGrid.DataSource = dtChart1;
                    devChart1_Drawing();
                    devChart2_Drawing();
                }
            }
        }
        #endregion

        #region devChart1_Drawing
        protected void devChart1_Drawing()
        {
            if (dtChart2 != null)
            {
                DataTable cdt = dtChart2.Copy();

                devChart1.Series.Clear();

                Series series = new Series("", ViewType.Doughnut);
                ((DoughnutSeriesView)series.View).HoleRadiusPercent = 30;
                ((DoughnutSeriesView)series.View).SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Clockwise;
                ((DoughnutSeriesView)series.View).Rotation = 270;

                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(cdt.Rows[i]["F_COMPANYNM"].ToString(), new double[] { Convert.ToDouble(cdt.Rows[i]["F_NGCOUNT"]) }));
                }
                series.Label.TextPattern = "{A}: {VP:p2}";

                devChart1.Titles.Clear();
                devChart1.Series.Add(series);
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 14, System.Drawing.FontStyle.Bold);
                tlt1.Text = "불량수량 점유율";
                tlt1.Font = font;

                devChart1.Titles.Add(tlt1);
                devChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                devChart1.DataSource = cdt;
                devChart1.DataBind();
            }
        }
        #endregion

        #region devChart2_Drawing
        protected void devChart2_Drawing()
        {
            if (dtChart3 != null)
            {
                DataTable cdt = dtChart3.Copy();

                devChart2.Series.Clear();

                Series series = new Series("", ViewType.Doughnut);
                ((DoughnutSeriesView)series.View).HoleRadiusPercent = 30;
                ((DoughnutSeriesView)series.View).SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Clockwise;
                ((DoughnutSeriesView)series.View).Rotation = 270;
                //Series series = new Series("", ViewType.Doughnut3D);                
                //((Doughnut3DSeriesView)series.View).HoleRadiusPercent = 30;
                //((Doughnut3DSeriesView)series.View).SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Clockwise;

                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(cdt.Rows[i]["F_COMPANYNM"].ToString(), new double[] { Convert.ToDouble(cdt.Rows[i]["F_NGTIME"]) }));
                }
                series.Label.TextPattern = "{A}: {VP:p2}";
                devChart2.Titles.Clear();
                devChart2.Series.Add(series);
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 14, System.Drawing.FontStyle.Bold);

                tlt1.Text = "수리시간 점유율";
                tlt1.Font = font;
                devChart2.Titles.Add(tlt1);
                devChart2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                devChart2.DataSource = cdt;
                devChart2.DataBind();
            }
        }
        #endregion

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

        #endregion

        #region 이벤트

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD_DACO4001_LST(true);
            devGrid.DataBind();
        }
        #endregion
        
        #region devChart1_CustomCallback
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(Convert.ToInt32(oParams[0]) / 2) - 5, Convert.ToInt32(oParams[1]));
            devChart1_Drawing();
        }
        #endregion

        #region devChart2_CustomCallback
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(Convert.ToInt32(oParams[0]) / 2) - 5, Convert.ToInt32(oParams[1]));
            devChart2_Drawing();
        }
        #endregion

        #endregion

    }
}