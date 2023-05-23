using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using DevExpress.XtraReports.UI;

using System.Data;
using System.Text;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.TISP.Biz;

namespace SPC.WebUI.Pages.TISP
{
    public partial class TISP0104_DACO_2 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataSet ds2
        {
            get
            {
                return (DataSet)Session["TISP0104_DACO_2_1"];
            }
            set
            {
                Session["TISP0104_DACO_2_1"] = value;
            }
        }

        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["TISP0104_DACO_2_2"];
            }
            set
            {
                Session["TISP0104_DACO_2_2"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["TISP0104_DACO_2_3"];
            }
            set
            {
                Session["TISP0104_DACO_2_3"] = value;
            }
        }

        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["TISP0104_DACO_2_3"];
            }
            set
            {
                Session["TISP0104_DACO_2_3"] = value;
            }
        }

        DataTable dtChart4
        {
            get
            {
                return (DataTable)Session["TISP0104_DACO_2_4"];
            }
            set
            {
                Session["TISP0104_DACO_2_4"] = value;
            }
        }

        DataTable dtChart5
        {
            get
            {
                return (DataTable)Session["TISP0104_DACO_2_5"];
            }
            set
            {
                Session["TISP0104_DACO_2_5"] = value;
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

            if (!IsCallback)
            {
                chk_calc.SelectedIndex = 0;
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
                InspGrid.JSProperties["cpResultCode"] = "";
                InspGrid.JSProperties["cpResultMsg"] = "";

                hidGrid.JSProperties["cpResultCode"] = "";
                hidGrid.JSProperties["cpResultMsg"] = "";

                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
                devChart2.JSProperties["cpFunction"] = "resizeTo";
                devChart2.JSProperties["cpChartWidth"] = "0";
                devChart3.JSProperties["cpFunction"] = "resizeTo";
                devChart3.JSProperties["cpChartWidth"] = "0";
                devChart4.JSProperties["cpFunction"] = "resizeTo";
                devChart4.JSProperties["cpChartWidth"] = "0";
                devChart5.JSProperties["cpFunction"] = "resizeTo";
                devChart5.JSProperties["cpChartWidth"] = "0";
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
        {
            //this.rdoGBN.SelectedIndex = 0;
            dtChart1 = null;
            dtChart2 = null;
            dtChart3 = null;
            dtChart4 = null;
            dtChart5 = null;

            ds2 = null;
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

        #region 공정리스트조회
        void TISP0104_DACO_INSP_LST(string param)
        {
            string errMsg = String.Empty;

            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                oParamDic.Add("F_WORKCD", param);

                ds = biz.TISP0104_DACO_INSP_LST(oParamDic, out errMsg);
            }

            InspGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                InspGrid.JSProperties["cpResultCode"] = "0";
                InspGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    InspGrid.DataBind();
            }
        }
        #endregion

        #region PivotDataSet
        DataTable PivotDataSet(DataSet ds)
        {
            DataTable dt = new DataTable();
            return dt;
        }
        #endregion

        #region SetChartLineSeries
        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, string Label, object color, int lineThickness = 1)
        {
            Series series = new Series(name, ViewType.Line)
            {
                ArgumentDataMember = ArgumentDataMember
            };
            series.ValueDataMembers.AddRange(new string[] { ValueDataMembers });

            series.ToolTipHintDataMember = Label;
            series.ToolTipPointPattern = "{HINT}";
            series.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;

            LineSeriesView lineSeriesView = new LineSeriesView();
            lineSeriesView.LineStyle.Thickness = lineThickness;
            lineSeriesView.Color = (System.Drawing.Color)color;
            lineSeriesView.LineMarkerOptions.Size = 5;
            lineSeriesView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            series.View = lineSeriesView;
            devChart.Series.Add(series);
        }
        #endregion

        #region SetChartDiagram
        public static void SetChartDiagram(WebChartControl devChart, bool bGridLinesVisible, object maxAxisX, object minAxisX, object maxAxisY, object minAxisY, string textAxisXPattern = null, string textAxisYPattern = null, bool axisXVisible = true, bool axisYVisible = true)
        {
            XYDiagram diagram = (XYDiagram)devChart.Diagram;

            diagram.AxisX.GridLines.Visible = bGridLinesVisible;
            diagram.AxisY.GridLines.Visible = bGridLinesVisible;

            if (textAxisXPattern != null)
                diagram.AxisX.Label.TextPattern = textAxisXPattern;
            if (textAxisYPattern != null)
                diagram.AxisY.Label.TextPattern = textAxisYPattern;

            if (Convert.ToDecimal(maxAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MaxValue = maxAxisX;
            if (Convert.ToDecimal(minAxisX) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisX) < Convert.ToDecimal("0"))
                diagram.AxisX.VisualRange.MinValue = minAxisX;
            if (Convert.ToDecimal(maxAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(maxAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MaxValue = maxAxisY;
            if (Convert.ToDecimal(minAxisY) > Convert.ToDecimal("0") || Convert.ToDecimal(minAxisY) < Convert.ToDecimal("0"))
                diagram.AxisY.VisualRange.MinValue = minAxisY;
        }        
        #endregion

        #region TISP0104_DACO_LST()
        void TISP0104_DACO_LST()
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            string[] oParams = txtMEAINSPCD.Text.Split('|');

            StringBuilder sb = null;

            hidGrid.JSProperties["cpResult1"] = "";
            hidGrid.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());

            oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
            //oParamDic.Add("F_WORKCD", work2);
            oParamDic.Add("F_MEAINSPCD", oParams[0]);
            oParamDic.Add("F_SIRYO", oParams[1]);
            oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");
            //oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            // 검사규격을 구한다
            using (TISPBiz biz = new TISPBiz())
            {
                ds = biz.TISP0104_DACO_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                hidGrid.JSProperties["cpResultCode"] = "0";
                hidGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    hidGrid.JSProperties["cpResultCode"] = "0";
                    hidGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {
                    bExecute1 = true;

                    sb = new StringBuilder();

                    dt1 = ds.Tables[0].Copy();

                }
            }

            if (bExecute1)
            {
                //SetGrid(dt1, dt3);

                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart1 = dt1.Copy();
                }
                else
                {
                    dtChart1 = null;
                }

            }
        }
        #endregion

        #region TISP0104_LST()
        void TISP0104_LST()
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            string[] oParams = txtMEAINSPCD.Text.Split('|');

            StringBuilder sb = null;

            hidGrid.JSProperties["cpResult1"] = "";
            hidGrid.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());

            oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
            oParamDic.Add("F_MEAINSPCD", oParams[0]);
            oParamDic.Add("F_SIRYO", oParams[1]);
            oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            // 검사규격을 구한다
            using (TISPBiz biz = new TISPBiz())
            {
                ds2 = biz.TISP0104_DACO_PIECE_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                hidGrid.JSProperties["cpResultCode"] = "0";
                hidGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    hidGrid.JSProperties["cpResultCode"] = "0";
                    hidGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {
                    bExecute1 = true;

                    sb = new StringBuilder();

                    dt1 = ds2.Tables[0].Copy();
                    dt2 = ds2.Tables[1].Copy();
                    dt3 = ds2.Tables[2].Copy();
                    dt4 = ds2.Tables[3].Copy();

                }
            }

            if (bExecute1)
            {
                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart2 = dt1.Copy();
                }
                else
                {
                    dtChart2 = null;
                }
                if (dt2.Rows.Count > 0)
                {
                    // 2번 차트에 데이타 전달
                    dtChart3 = dt2.Copy();
                }
                else
                {
                    dtChart3 = null;
                }
                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart4 = dt3.Copy();
                }
                else
                {
                    dtChart4 = null;
                }
                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart5 = dt4.Copy();
                }
                else
                {
                    dtChart5 = null;
                }
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devChart1_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]), Convert.ToInt32(oParams[2]));
            //devChart_ResizeTo(sender, Convert.ToInt32(oParams[2]), Convert.ToInt32(oParams[1]));

            if (dtChart1 != null)
            {
                devChart1.Series.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                foreach (DataRow dtRow in dtChart1.Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_XBAR"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_XBAR"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                SetChartLineSeries(devChart1, Convert.ToInt32("1") <= 1 ? "X" : "xBar", "F_MEMBER", "F_XBAR", "F_WORKNM", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                DevExpressLib.SetChartLineSeries(devChart1, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart1, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart1, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart1.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart1, Convert.ToInt32("1") <= 1 ? "X 관리도" : "X-Bar 관리도", false);
            }
        }
        #endregion

        #region devChart2_CustomCallback
        protected void devChart2_CustomCallback(object sender, CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo2(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]), Convert.ToInt32(oParams[2]));

            devChart2 = devChart2 as DevExpress.XtraCharts.Web.WebChartControl;
            devChart2.Series.Clear();
            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            if (ds2 != null)
            {
                foreach (DataRow dtRow in ds2.Tables[0].Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_XBAR"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_XBAR"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(devChart2, "X", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153));
                DevExpressLib.SetChartLineSeries(devChart2, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart2, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart2, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart2, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart2, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart2.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                devChart2.DataSource = ds2.Tables[0];
                devChart2.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart2);
                SetChartDiagram(devChart2, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                XYDiagram diagram = (XYDiagram)devChart2.Diagram;
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
                DevExpressLib.SetChartTitle(devChart2, "과전류시험기 CH 1", false);
            }
        }
        #endregion

        #region devChart3_CustomCallback
        protected void devChart3_CustomCallback(object sender, CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo2(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]), Convert.ToInt32(oParams[2]));

            devChart3 = devChart3 as DevExpress.XtraCharts.Web.WebChartControl;
            devChart3.Series.Clear();
            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            if (ds2 != null)
            {
                foreach (DataRow dtRow in ds2.Tables[1].Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_XBAR"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_XBAR"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(devChart3, "X", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153));
                DevExpressLib.SetChartLineSeries(devChart3, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart3, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart3, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart3, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart3, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart3.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                devChart3.DataSource = ds2.Tables[1];
                devChart3.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart3);
                SetChartDiagram(devChart3, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                XYDiagram diagram = (XYDiagram)devChart3.Diagram;
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
                DevExpressLib.SetChartTitle(devChart3, "과전류시험기 CH 2", false);
            }
        }
        #endregion

        #region devChart4_CustomCallback
        protected void devChart4_CustomCallback(object sender, CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo2(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]), Convert.ToInt32(oParams[2]));

            devChart4 = devChart4 as DevExpress.XtraCharts.Web.WebChartControl;
            devChart4.Series.Clear();
            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            if (ds2 != null)
            {
                foreach (DataRow dtRow in ds2.Tables[2].Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_XBAR"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_XBAR"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(devChart4, "X", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153));
                DevExpressLib.SetChartLineSeries(devChart4, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart4, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart4, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart4, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart4, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart4.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                devChart4.DataSource = ds2.Tables[2];
                devChart4.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart4);
                SetChartDiagram(devChart4, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                XYDiagram diagram = (XYDiagram)devChart4.Diagram;
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
                DevExpressLib.SetChartTitle(devChart4, "과전류시험기 CH 3", false);
            }
        }
        #endregion

        #region devChart5_CustomCallback
        protected void devChart5_CustomCallback(object sender, CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo2(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]), Convert.ToInt32(oParams[2]));

            devChart5 = devChart5 as DevExpress.XtraCharts.Web.WebChartControl;
            devChart5.Series.Clear();
            Decimal maxAxisY = Decimal.MinValue;
            Decimal minAxisY = Decimal.MaxValue;

            if (ds2 != null)
            {
                foreach (DataRow dtRow in ds2.Tables[3].Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_XBAR"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_XBAR"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_XBAR"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(devChart5, "X", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153));
                DevExpressLib.SetChartLineSeries(devChart5, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart5, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart5, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart5, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart5, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart5.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                devChart5.DataSource = ds2.Tables[3];
                devChart5.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart5);
                SetChartDiagram(devChart5, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                XYDiagram diagram = (XYDiagram)devChart5.Diagram;
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;

                DevExpressLib.SetChartTitle(devChart5, "과전류시험기 CH 4", false);
                
            }
        }
        #endregion

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="Width">Int32</param>
        void devChart_ResizeTo(object sender, Int32 Width, Int32 Height, Int32 Width2)
        {
            if (Width >= 0 && Height >= 0 && Width2 >= 0)
            {
                DevExpress.XtraCharts.Web.WebChartControl chart = sender as DevExpress.XtraCharts.Web.WebChartControl;
                chart.Width = new Unit(Width);
                chart.Height = new Unit(Height);

                chart.JSProperties["cpFunction"] = "resizeTo";
                chart.JSProperties["cpChartWidth"] = Width.ToString();
            }
        }

        void devChart_ResizeTo2(object sender, Int32 Width, Int32 Height, Int32 Width2)
        {
            if (Width >= 0 && Height >= 0 && Width2 >= 0)
            {
                DevExpress.XtraCharts.Web.WebChartControl chart = sender as DevExpress.XtraCharts.Web.WebChartControl;
                chart.Width = new Unit(Width2);
                chart.Height = new Unit(Height);

                chart.JSProperties["cpFunction"] = "resizeTo";
                chart.JSProperties["cpChartWidth"] = Width.ToString();
            }
        }
        #endregion

        #region InspGrid CustomCallback
        /// <summary>
        /// InspGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void InspGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            TISP0104_DACO_INSP_LST(e.Parameters);
        }
        #endregion

        #region txtITEMCD_Init
        protected void txtITEMCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupUCItemSearch('S')");
        }
        #endregion

        #region hidGrid_CustomCallback
        protected void hidGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            //string errMsg = String.Empty;
            //bool bExecute1 = false;
            //string[] oParams = txtMEAINSPCD.Text.Split('|');
            //string[] work = e.Parameters.Split(',');
            //string work2 = "";

            //for (int i = 0; i < work.Count(); i++)
            //{
            //    work2 += (work[i].Split('|'))[0];
            //}

            //StringBuilder sb = null;

            //hidGrid.JSProperties["cpResult1"] = "";
            //hidGrid.JSProperties["cpResult2"] = "";

            //DataTable dt1 = new DataTable();
            //DataTable dt2 = new DataTable();
            //DataTable dt3 = new DataTable();

            //oParamDic = new Dictionary<string, string>();
            //oParamDic.Add("F_COMPCD", gsCOMPCD);
            //oParamDic.Add("F_FACTCD", gsFACTCD);
            //oParamDic.Add("F_STDT", GetFromDt());
            //oParamDic.Add("F_EDDT", GetToDt());

            //oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
            //oParamDic.Add("F_WORKCD", work2);
            //oParamDic.Add("F_MEAINSPCD", oParams[0]);
            //oParamDic.Add("F_SIRYO", oParams[1]);
            //oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");
            ////oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            //// 검사규격을 구한다
            //using (TISPBiz biz = new TISPBiz())
            //{
            //    ds = biz.TISP0104_DACO_LST(oParamDic, out errMsg);
            //}

            //if (!String.IsNullOrEmpty(errMsg))
            //{
            //    hidGrid.JSProperties["cpResultCode"] = "0";
            //    hidGrid.JSProperties["cpResultMsg"] = errMsg;
            //}
            //else
            //{
            //    if (!bExistsDataSet(ds))
            //    {
            //        hidGrid.JSProperties["cpResultCode"] = "0";
            //        hidGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
            //    }
            //    else
            //    {
            //        bExecute1 = true;

            //        sb = new StringBuilder();
            //        dt1 = ds.Tables[0].Copy();
            //    }
            //}

            //if (bExecute1)
            //{                
            //    if (dt1.Rows.Count > 0)
            //    {
            //        // 1번 차트에 데이타 전달
            //        dtChart1 = dt1.Copy();

            //    }
            //    else
            //    {
            //        dtChart1 = null;
            //    }
            //}
        }
        #endregion

        #region callbackPanel_Callback
        protected void callbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (txtMEAINSPCD.Text != null && txtMEAINSPCD.Text != "")
            {
                TISP0104_DACO_LST();
                TISP0104_LST();
            }
        }
        #endregion

        #region devCallback_Callback
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
        }
        #endregion

        #region ItemCallback Callback
        /// <summary>
        /// ItemCallback_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void ITEMCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_ITEMCD", txtITEMCD.Text);
                oParamDic.Add("F_STATUS", "1");
                ds = biz.GetQCD01_LST(oParamDic, out errMsg);
            }

            string ITEMCD = String.Empty;
            string ITEMNM = String.Empty;
            string MODELCD = String.Empty;
            string MODELNM = String.Empty;

            if (true == bExistsDataSet(ds))
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        if (txtITEMCD.Text == dtRow["F_ITEMCD"].ToString())
                        {
                            ITEMCD = (string)dtRow["F_ITEMCD"];
                            ITEMNM = (string)dtRow["F_ITEMNM"];
                            MODELCD = dtRow["F_MODELCD"].ToString();
                            MODELNM = dtRow["F_MODELNM"].ToString();
                        }
                    }
                }
            }

            ITEMCallback.JSProperties["cpITEMCD"] = ITEMCD;
            ITEMCallback.JSProperties["cpITEMNM"] = ITEMNM;
            ITEMCallback.JSProperties["cpMODELCD"] = MODELCD;
            ITEMCallback.JSProperties["cpMODELNM"] = MODELNM;
        }
        #endregion

        #endregion


    }
}