using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using SPC.WebUI.Common;
using SPC.ANLS.Biz;
using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0103 : WebUIBasePage
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
                return (DataTable)Session["ANLS0103_1"];
            }
            set
            {
                Session["ANLS0103_1"] = value;
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

            // Grid Columns Sum Width
            // hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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
            dtChart1 = null;
            this.chk_reject.Checked = true;
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 그리드
        void SetGrid(DataTable dt1, DataTable dt3)
        {
            Int32   idx = 0,
                    siryo = Convert.ToInt32(txtSIRYO.Text),
                    digit = Convert.ToInt32(txtFREEPOINT.Text);
            
            // 데이타 구성용 DataTable
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("검사일자", typeof(String));
            dtTemp.Columns.Add("Xbar", typeof(String));
            dtTemp.Columns.Add("R", typeof(String));
            //for (idx = 0; idx < siryo; idx++)
            //{
            //    dtTemp.Columns.Add(String.Format("X{0}", idx + 1), typeof(String));
            //}
            
            // 그리드 DataSource용 DataTable(Pivot)
            DataTable dtGrid = new DataTable();
            DataColumnCollection columns = dtTemp.Columns;

            foreach (DataRow dtRow1 in dt1.Rows)
            {
                // 데이타 구성용 DataRow
                idx = 0;
                DataRow dtNewRow = dtTemp.NewRow();
                dtNewRow["검사일자"] = dtRow1["F_WORKDATE"].ToString();
                dtNewRow["Xbar"] = Math.Round(Convert.ToDecimal(dtRow1["F_XBAR"].ToString()), digit + 1).ToString();
                dtNewRow["R"] = Math.Round(Convert.ToDecimal(dtRow1["F_XRANGE"].ToString()), digit + 1).ToString();
                foreach (DataRow dtRow3 in dt3.Select(String.Format("F_WORKDATE='{0}'", dtRow1["F_WORKDATE"])))
                {
                    idx++;
                    if (!columns.Contains(String.Format("X{0}", idx)))
                    {
                        dtTemp.Columns.Add(String.Format("X{0}", idx), typeof(String));
                    }
                    dtNewRow[String.Format("X{0}", idx)] = Math.Round(Convert.ToDecimal(dtRow3["F_MEASURE"].ToString()), digit).ToString();
                }
                dtTemp.Rows.Add(dtNewRow);
            }

            // Pivot Fill
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "검사일자");

            devGrid.DataSource = dtPivotTable;
            devGrid.DataBind();
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

                DevExpressLib.SetChartLineSeries(devChart1, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X" : "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                DevExpressLib.SetChartLineSeries(devChart1, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLX" : "C_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart1, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLX" : "C_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLX" : "C_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart1, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart1, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart1, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X 관리도" : "X-Bar 관리도", false);
            }
        }
        #endregion

        #region devChart2_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart1 != null)
            {
                devChart2.Series.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                string maxColumn = String.Empty;
                string minColumn = String.Empty;

                foreach (DataRow dtRow in dtChart1.Rows)
                {
                    maxColumn = chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLR" : "C_UCLR";
                    minColumn = chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLR" : "C_LCLR";

                    if (!String.IsNullOrEmpty(dtRow[maxColumn].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow[maxColumn]));

                    if (!String.IsNullOrEmpty(dtRow[minColumn].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow[minColumn]));

                    if (maxAxisY < Convert.ToDecimal(dtRow["F_XRANGE"]))
                        maxAxisY = Convert.ToDecimal(dtRow["F_XRANGE"]);

                    if (minAxisY > Convert.ToDecimal(dtRow["F_XRANGE"]))
                        minAxisY = Convert.ToDecimal(dtRow["F_XRANGE"]);
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(devChart2, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "Rs" : "R", "F_MEMBER", "F_XRANGE", System.Drawing.Color.FromArgb(0, 102, 153), 2);
                DevExpressLib.SetChartLineSeries(devChart2, "CL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_CLR" : "C_CLR", System.Drawing.Color.Green);
                DevExpressLib.SetChartLineSeries(devChart2, "UCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_UCLR" : "C_UCLR", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart2, "LCL", "F_MEMBER", chk_calc.SelectedItem.Value.ToString().Equals("0") ? "F_LCLR" : "C_LCLR", System.Drawing.Color.Red);

                devChart2.DataSource = dtChart1;
                devChart2.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart2);
                DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart2, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "Rs 관리도" : "R 관리도", false);
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

        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;            
            StringBuilder sb = null;

            devGrid.JSProperties["cpResult1"] = "";
            devGrid.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", GetCompCD());
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());

            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            // 검사규격을 구한다
            using (ANLSBiz biz = new ANLSBiz())
            {
                ds = biz.ANLS0103_1(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {
                    bExecute1 = true;

                    sb = new StringBuilder();

                    dt1 = ds.Tables[0].Copy();

                    DataRow dtRow = dt1.Rows[0];

                    for (int i = 0; i < dt1.Columns.Count; i++)
                    {
                        if (i > 0) sb.Append("|");
                        sb.Append(dtRow[i].ToString());
                    }

                    devGrid.JSProperties["cpResult1"] = sb.ToString();
                }
            }

            if (true == bExecute1)
            {
                // 검사측정자료를 구한다
                using (ANLSBiz biz = new ANLSBiz())
                {
                    ds = biz.ANLS0103_2(oParamDic, out errMsg);
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bExistsDataSet(ds))
                    {
                        devGrid.JSProperties["cpResultCode"] = "0";
                        devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 측정데이타가 없습니다";
                    }
                    else
                    {
                        sb = new StringBuilder();

                        dt3 = ds.Tables[0].Copy();
                    }
                }
            }

            // 모든 데이타가 구성되면 그리드를 그린다
            if (bExecute1)
            {
                SetGrid(dt1, dt3);

                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart1 = dt1.Copy();
                }
                else
                    dtChart1 = null;
            }
        }

        #endregion
    }
}