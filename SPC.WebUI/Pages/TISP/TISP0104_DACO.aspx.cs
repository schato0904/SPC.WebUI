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
    public partial class TISP0104_DACO : WebUIBasePage
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
                return (DataTable)Session["TISP0104_DACO"];
            }
            set
            {
                Session["TISP0104_DACO"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["TISP0104_DACO_2"];
            }
            set
            {
                Session["TISP0104_DACO_2"] = value;
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

                // Grid Callback Init
                devGridWork.JSProperties["cpResultCode"] = "";
                devGridWork.JSProperties["cpResultMsg"] = "";

                devGrid1.JSProperties["cpResultCode"] = "";
                devGrid1.JSProperties["cpResultMsg"] = "";

                hidGrid.JSProperties["cpResultCode"] = "";
                hidGrid.JSProperties["cpResultMsg"] = "";

                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
                devChart2.JSProperties["cpFunction"] = "resizeTo";
                devChart2.JSProperties["cpChartWidth"] = "0";
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

            devGridWork.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridWork.JSProperties["cpResultCode"] = "0";
                devGridWork.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGridWork.DataBind();


            }
        }
        #endregion

        #region 공정리스트조회
        void TISP0104_DACO_WORK_LST(string ITEMCD)
        {
            string errMsg = String.Empty;

            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_ITEMCD", ITEMCD);

                ds = biz.TISP0104_DACO_WORK_LST(oParamDic, out errMsg);
            }

            devGrid1.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                {
                    devGrid1.DataBind();
                    //devGrid1.JSProperties["cpResultCode"] = "0";
                    //devGrid1.JSProperties["cpResultMsg"] = errMsg;
                }
            }
        }
        #endregion



        #endregion


        #region 사용자이벤트

        #region PivotDataSet
        DataTable PivotDataSet(DataSet ds)
        {
            DataTable dt = new DataTable();

            return dt;
        }
        #endregion


        public static void SetChartLineSeries(WebChartControl devChart, string name, string ArgumentDataMember, string ValueDataMembers, string Label, object color, int lineThickness = 1)
        {

            //SeriesBase series2 = new SeriesBase();





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

        public static void SetChartDiagram(WebChartControl devChart, bool bGridLinesVisible, object maxAxisX, object minAxisX, object maxAxisY, object minAxisY,
         string textAxisXPattern = null, string textAxisYPattern = null,
         bool axisXVisible = true, bool axisYVisible = true)
        {
            XYDiagram diagram = (XYDiagram)devChart.Diagram;

            diagram.AxisX.GridLines.Visible = bGridLinesVisible;
            diagram.AxisY.GridLines.Visible = bGridLinesVisible;


            //diagram.AxisX.Label.Visible = axisXVisible;
            //diagram.AxisY.Label.Visible = axisYVisible;


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



        #region devChart1_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));



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
                //DevExpressLib.SetChartLineSeries(devChart1, Convert.ToInt32("1") <= 1 ? "X" : "xBar", "F_WORKTIME", "F_WORKCD", System.Drawing.Color.FromArgb(0, 102, 153), 2);



                DevExpressLib.SetChartLineSeries(devChart1, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart1, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart1, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart1.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;





                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                //devChart1.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;





                DevExpressLib.SetCrosshairOptions(devChart1);
                SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart1, Convert.ToInt32("1") <= 1 ? "X 관리도" : "X-Bar 관리도", false);



            }


        }
        #endregion

        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart2 != null)
            {
                devChart2.Series.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                string maxColumn = String.Empty;
                string minColumn = String.Empty;

                foreach (DataRow dtRow in dtChart2.Rows)
                {
                    maxColumn = chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLR" : "C_UCLR";
                    minColumn = chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLR" : "C_LCLR";

                    if (!String.IsNullOrEmpty(dtRow[maxColumn].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow[maxColumn]));

                    if (!String.IsNullOrEmpty(dtRow[minColumn].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow[minColumn]));

                    //if (maxAxisY < Convert.ToDecimal(dtRow["F_XRANGE"]))
                    //    maxAxisY = Convert.ToDecimal(dtRow["F_XRANGE"]);

                    //if (minAxisY > Convert.ToDecimal(dtRow["F_XRANGE"]))
                    //    minAxisY = Convert.ToDecimal(dtRow["F_XRANGE"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                SetChartLineSeries(devChart2, Convert.ToInt32(1) <= 1 ? "Rs" : "R", "F_MEMBER", "F_XRANGE", "F_WORKNM", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                DevExpressLib.SetChartLineSeries(devChart2, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLR" : "C_CLR", System.Drawing.Color.Green);
                DevExpressLib.SetChartLineSeries(devChart2, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLR" : "C_UCLR", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart2, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLR" : "C_LCLR", System.Drawing.Color.Red);

                devChart2.DataSource = dtChart2;
                devChart2.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart2);
                DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart2, Convert.ToInt32("1") <= 1 ? "Rs 관리도" : "R 관리도", false);
            }
        }


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

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
        #endregion

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {

        }
        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
        }
        #endregion



        #region devGridWork CustomCallback
        /// <summary>
        /// devGridWork_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridWork_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            TISP0104_DACO_INSP_LST(e.Parameters);
        }
        #endregion

        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            TISP0104_DACO_WORK_LST(e.Parameters);
        }

        #endregion

        protected void txtITEMCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupUCItemSearch('S')");
        }

        protected void hidGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            string[] oParams = txtMEAINSPCD.Text.Split('|');
            string[] work = e.Parameters.Split(',');
            string work2 = "";

            for (int i = 0; i < work.Count(); i++)
            {
                work2 += (work[i].Split('|'))[0];
            }


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
            oParamDic.Add("F_WORKCD", work2);
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

                    //DataRow dtRow = dt1.Rows[0];

                    //for (int i = 0; i < dt1.Columns.Count; i++)
                    //{
                    //    if (i > 0) sb.Append("|");
                    //    sb.Append(dtRow[i].ToString());
                    //}

                    //hidGrid.JSProperties["cpResult1"] = sb.ToString();
                }
            }



            if (bExecute1)
            {
                //SetGrid(dt1, dt3);

                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart1 = dt1.Copy();
                    dtChart2 = dt1.Copy();
                }
                else
                {
                    dtChart1 = null;
                    dtChart2 = null;
                }

                //if (dt3.Rows.Count > 0)
                //{
                //    dtChart3 = dt3.Copy();
                //}
                //else
                //    dtChart3 = null;
            }
        }









    }
}