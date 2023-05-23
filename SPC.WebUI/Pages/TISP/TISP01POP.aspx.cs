using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using SPC.WebUI.Common;
using SPC.TISP.Biz;
using DevExpress.XtraCharts;
using DevExpress.Web;

namespace SPC.WebUI.Pages.TISP
{
    public partial class TISP01POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string oSetParam = String.Empty;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["TISP01POP_1"];
            }
            set
            {
                Session["TISP01POP_1"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["TISP01POP_2"];
            }
            set
            {
                Session["TISP01POP_2"] = value;
            }
        }

        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["TISP01POP_3"];
            }
            set
            {
                Session["TISP01POP_3"] = value;
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
                 //devChart1.JSProperties["cpFunction"] = "resizeTo";
                 //devChart1.JSProperties["cpChartWidth"] = "0";
                 //devChart2.JSProperties["cpFunction"] = "resizeTo";
                 //devChart2.JSProperties["cpChartWidth"] = "0";
                 devChart3.JSProperties["cpFunction"] = "resizeTo";
                 devChart3.JSProperties["cpChartWidth"] = "0";
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
            dtChart1 = null;

            chk_reject.Checked = String.IsNullOrEmpty(oSetParam);

            if (!String.IsNullOrEmpty(oSetParam) && !gsVENDOR)
            {
                string[] oSetParams = oSetParam.Split('|');
                ucComp.compParam = oSetParams[16];
                ucFact.factParam = oSetParams[17];
            }
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
        void SetGrid(DataTable dt1, DataTable dt2, DataTable dt3)
        {
            Int32   idx = 0,
                    siryo = Convert.ToInt32(txtSIRYO.Text),
                    digit = Convert.ToInt32(txtFREEPOINT.Text);
            
            // 데이타 구성용 DataTable
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("검사일자", typeof(String));
            dtTemp.Columns.Add("검사시간", typeof(String));
            dtTemp.Columns.Add("Xbar", typeof(String));
            dtTemp.Columns.Add("R", typeof(String));
            for (idx = 0; idx < siryo; idx++)
            {
                dtTemp.Columns.Add(String.Format("X{0}", idx + 1), typeof(String));
            }

            Int32 rowCount = dt1.Rows.Count;
            Int32 modCount = (rowCount > 100) ? rowCount / 100 : 0;  // 100개만 출력한다.
            Int32 rowIndex = 0;

            foreach (DataRow dtRow1 in dt1.Rows)
            {
                if (modCount > 0 && rowIndex % modCount > 0) { rowIndex++; continue; }
                // 데이타 구성용 DataRow
                idx = 0;
                DataRow dtNewRow = dtTemp.NewRow();
                dtNewRow["검사일자"] = String.Format("{0}({1})", DateTime.Parse(dtRow1["F_WORKDATE"].ToString()).ToString("MM/dd"), dtRow1["F_TSERIALNO"]);
                dtNewRow["검사시간"] = dtRow1["F_WORKTIME"].ToString();
                dtNewRow["Xbar"] = Math.Round(Convert.ToDecimal(dtRow1["F_XBAR"].ToString()), digit + 1).ToString();
                dtNewRow["R"] = Math.Round(Convert.ToDecimal(dtRow1["F_XRANGE"].ToString()), digit + 1).ToString();
                foreach (DataRow dtRow3 in dt3.Select(String.Format("F_WORKDATE='{0}' AND F_TSERIALNO='{1}'", dtRow1["F_WORKDATE"], dtRow1["F_TSERIALNO"])))
                {
                    dtNewRow[String.Format("X{0}", ++idx)] = Math.Round(Convert.ToDecimal(dtRow3["F_MEASURE"].ToString()), digit).ToString();
                }
                dtTemp.Rows.Add(dtNewRow);

                rowIndex++;
            }

            // Pivot Fill
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "검사일자");

            devGrid.DataSource = dtPivotTable;
            devGrid.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devCallbackPanel1_Callback
        /// <summary>
        /// devCallbackPanel1_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');

            ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
            ASPxImage image = (ASPxImage)panel.FindControl("devImage1");
            image.Width = new Unit(oParams[0]);
            image.Height = new Unit(oParams[1]);
            image.ImageUrl = String.Format("~/API/Common/Chart/GetChartImage.ashx?tbl={0}&siryo={1}&calc={2}&type={3}&w={4}&h={5}&memb={6}&dummy={7}&db={8}",
                "TISP01POP_1",
                txtSIRYO.Text,
                chk_calc.SelectedItem.Value.ToString(),
                "xbar",
                oParams[0],
                oParams[1],
                "F_MEMBER",
                DateTime.Now.ToString("HH:mm:ss.fff"),
                "08");
        }
        #endregion

        #region devCallbackPanel2_Callback
        /// <summary>
        /// devCallbackPanel2_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel2_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');

            ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
            ASPxImage image = (ASPxImage)panel.FindControl("devImage2");
            image.Width = new Unit(oParams[0]);
            image.Height = new Unit(oParams[1]);
            image.ImageUrl = String.Format("~/API/Common/Chart/GetChartImage.ashx?tbl={0}&siryo={1}&calc={2}&type={3}&w={4}&h={5}&memb={6}&dummy={7}&db={8}",
                "TISP01POP_1",
                txtSIRYO.Text,
                chk_calc.SelectedItem.Value.ToString(),
                "r",
                oParams[0],
                oParams[1],
                "F_MEMBER",
                DateTime.Now.ToString("HH:mm:ss.fff"),
                "08");
        }
        #endregion

        #region devChart3_CustomCallback
        /// <summary>
        /// devChart3_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart3_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));         

            if (dtChart1 != null)
            {
                string errMsg = String.Empty;

                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetFromDt());

                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
                oParamDic.Add("F_SIRYO", txtSIRYO.Text);
                oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

                // 히스토그램을 구한다
                using (TISPBiz biz = new TISPBiz())
                {
                    ds = biz.TISP01POP_3(oParamDic, out errMsg);
                }

                // 데이타가 있는 경우
                if (bExistsDataSet(ds))
                {
                    devChart3.Series.Clear();

                    Decimal maxAxisX = Decimal.MinValue;
                    Decimal minAxisX = Decimal.MaxValue;
                    Decimal maxAxisY = Decimal.MinValue;
                    Decimal minAxisY = Decimal.MaxValue;

                    foreach (DataRow dtRow in ds.Tables[0].Rows)
                    {
                        if (!String.IsNullOrEmpty(dtRow["F_GBNNM"].ToString()))
                        {
                            maxAxisX = Math.Max(maxAxisX, Convert.ToDecimal(dtRow["F_GBNNM"]));
                            minAxisX = Math.Min(minAxisX, Convert.ToDecimal(dtRow["F_GBNNM"]));
                        }

                        if (!String.IsNullOrEmpty(dtRow["F_SIRYO"].ToString()))
                        {
                            maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_SIRYO"]));
                            minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_SIRYO"]));
                        }
                    }

                    maxAxisX = maxAxisX == Decimal.MinValue ? 0 : maxAxisX;
                    minAxisX = minAxisX == Decimal.MaxValue ? 0 : minAxisX;
                    maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                    minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                    DevExpressLib.SetChartBarLineSeries(devChart3, "시료수", "F_GBNNM", "F_SIRYO", System.Drawing.Color.LightBlue);
                    DevExpressLib.SetChartBarLineSeries(devChart3, "", "F_GBNNM", "F_VALUE", System.Drawing.Color.LightPink, 0.2);

                    devChart3.DataSource = ds;
                    devChart3.DataBind();

                    DevExpressLib.SetChartLegend(devChart3);
                    DevExpressLib.SetChartDiagram(devChart3, true, 0, 0, 0, 0, null, null);
                }
            }
        }
        #endregion

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="Width">Int32</param>
        /// <param name="Height">Int32</param>
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

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            bool bExecute2 = false;
            bool bExecute3 = false;
            StringBuilder sb = null;

            devGrid.JSProperties["cpResult1"] = "";
            devGrid.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", GetCompCD());
            oParamDic.Add("F_FACTCD", GetFactCD());
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetFromDt());

            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            // 검사규격을 구한다
            using (TISPBiz biz = new TISPBiz())
            {
                ds = biz.TISP01POP_1(oParamDic, out errMsg);
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
                // 검사분석자료를 구한다
                using (TISPBiz biz = new TISPBiz())
                {
                    ds = biz.TISP01POP_4(oParamDic, out errMsg);
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
                        devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 분석데이타가 없습니다";
                    }
                    else
                    {
                        bExecute2 = true;

                        sb = new StringBuilder();

                        dt2 = ds.Tables[0].Copy();

                        DataRow dtRow = dt2.Rows[0];

                        for (int i = 0; i < dt2.Columns.Count; i++)
                        {
                            if (i > 0) sb.Append("|");
                            sb.Append(dtRow[i].ToString());
                        }

                        devGrid.JSProperties["cpResult2"] = sb.ToString();
                    }
                }

                if (true == bExecute2)
                {
                    // 검사측정자료를 구한다
                    using (TISPBiz biz = new TISPBiz())
                    {
                        ds = biz.TISP01POP_2(oParamDic, out errMsg);
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
                            bExecute3 = true;

                            sb = new StringBuilder();

                            dt3 = ds.Tables[0].Copy();
                        }
                    }
                }
            }

            // 모든 데이타가 구성되면 그리드를 그린다
            if (bExecute1 && bExecute2 && bExecute3)
            {
                SetGrid(dt1, dt2, dt3);

                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart1 = dt1.Copy();
                }
                else
                    dtChart1 = null;

                if (dt2.Rows.Count > 0)
                {
                    dtChart2 = dt2.Copy();
                }
                else
                    dtChart2 = null;

                if (dt3.Rows.Count > 0)
                {
                    dtChart3 = dt3.Copy();
                }
                else
                    dtChart3 = null;
            }
        }
        #endregion

        #endregion
    }
}