using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using DevExpress.Web;
using SPC.WERD.Biz;
using DevExpress.XtraCharts;



namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD4002 : WebUIBasePage
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
                return (DataTable)Session["WERD4002"];
            }
            set
            {
                Session["WERD4002"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["WERD4002_1"];
            }
            set
            {
                Session["WERD4002_1"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["WERD4002_TYPE"];
            }
            set
            {
                Session["WERD4002_TYPE"] = value;
            }
        }
        DataTable dtChart4
        {
            get
            {
                return (DataTable)Session["WERD4002_2"];
            }
            set
            {
                Session["WERD4002_2"] = value;
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
            dtChart2 = null;
            dtChart3 = null;
            dtChart4 = null;
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
            //this.AspxCombox_DataBind(this.srcF_PROCCD, "PP", "PPA4");
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 그리드 목록 조회(품목)
        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void WERD4002_LST()
        {
            string errMsg = String.Empty;

            bool bExecute1 = false; //추가



            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt() + "-01");
                oParamDic.Add("F_TODT", this.ucDate.GetToDt() + "-31");
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                ds = biz.WERD4002_LST(oParamDic, out errMsg);
            }




            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultMsg"] = errMsg;
                devChart2.JSProperties["cpResultCode"] = "0";
                devChart2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";

                    dtChart1 = null;
                    dtChart2 = null;
                    dtChart3 = null;
                    dtChart4 = null;
                }
                else
                {
                    SetGrid(ds.Tables[0], ds.Tables[1], ds.Tables[2]);
                }


            }



        }
        #endregion

        #region SetGrid
        void SetGrid(DataTable dt, DataTable dt2, DataTable dt3)
        {
            dtChart1 = dt;
            dtChart2 = dt2;
            dtChart3 = dt3;


            if (dt == null)
                return;

            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("검사일자", typeof(String));
            dtTemp.Columns.Add("생산수량", typeof(String));
            dtTemp.Columns.Add("검사수량", typeof(String));
            dtTemp.Columns.Add("부적합수량", typeof(String));
            dtTemp.Columns.Add("부적합률(%)", typeof(String));
            dtTemp.Columns.Add("PPM", typeof(String));

            if (dt2.Rows.Count > 0)
            {
                if (dt2.Rows[0]["F_TYPENM"].ToString() != null && dt2.Rows[0]["F_TYPENM"].ToString() != "")
                {
                    dtTemp.Columns.Add("부적합유형", typeof(String));
                    foreach (DataRow dtRow3 in dt2.Rows)
                    {
                        if (dtTemp.Columns.IndexOf(dtRow3["F_TYPENM"].ToString()) < 0)
                        {
                            dtTemp.Columns.Add(dtRow3["F_TYPENM"].ToString(), typeof(String));
                        }
                        else
                        {
                            dtTemp.Columns.Add(dtRow3["F_TYPENM"].ToString() + "(유형)", typeof(String));
                        }

                    }
                }
            }

            if (dt3.Rows.Count > 0)
            {
                if (dt3.Rows[0]["F_CAUSENM"].ToString() != null && dt3.Rows[0]["F_CAUSENM"].ToString() != "")
                {
                    dtTemp.Columns.Add("부적합원인", typeof(String));

                    foreach (DataRow dtRow4 in dt3.Rows)
                    {
                        if (dtTemp.Columns.IndexOf(dtRow4["F_CAUSENM"].ToString()) < 0)
                        {
                            dtTemp.Columns.Add(dtRow4["F_CAUSENM"].ToString(), typeof(String));
                        }
                        else
                        {
                            dtTemp.Columns.Add(dtRow4["F_CAUSENM"].ToString() + "(원인)", typeof(String));
                        }

                    }
                }
            }

            // 그리드 DataSource용 DataTable(Pivot)
            foreach (DataRow dtRow1 in dt.Rows)
            {
                // 데이타 구성용 DataRow
                //idx = 0;
                DataRow dtNewRow = dtTemp.NewRow();
                dtNewRow["검사일자"] = (dtRow1["F_WORKDATE"] ?? "").ToString();
                dtNewRow["생산수량"] = Convert.ToInt32(dtRow1["F_PRODUCTQTY"] ?? "").ToString("#,##0");
                dtNewRow["검사수량"] = Convert.ToInt32(dtRow1["F_INSPQTY"] ?? "").ToString("#,##0");
                dtNewRow["부적합수량"] = Convert.ToInt32(dtRow1["F_NGQTY"] ?? "").ToString("#,##0");
                dtNewRow["부적합률(%)"] = (dtRow1["F_NGRATE"] ?? "").ToString();
                dtNewRow["PPM"] = Convert.ToInt32(dtRow1["F_PPM"] ?? "").ToString("#,##0");

                if (dtRow1["F_TYPECNT"] != null && dtRow1["F_TYPECNT"].ToString() != "")
                {
                    string[] arrstrTypecnt = dtRow1["F_TYPECNT"].ToString().Split('|');
                    string[] arrstrTypenm = dtRow1["F_TYPENM"].ToString().Split('|');
                    int idx = 0;
                    foreach (string strTyepcnt in arrstrTypecnt)
                    {

                        if (dtTemp.Columns.IndexOf(arrstrTypenm[idx] + "(유형)") < 0)
                            dtNewRow[arrstrTypenm[idx]] = string.Format("{0:n0}", strTyepcnt); 
                        else
                            dtNewRow[arrstrTypenm[idx] + "(유형)"] = string.Format("{0:n0}", strTyepcnt);
                        idx++;
                    }
                }

                if (dtRow1["F_CAUSECNT"] != null && dtRow1["F_CAUSECNT"].ToString() != "")
                {
                    string[] arrstrTypecnt = dtRow1["F_CAUSECNT"].ToString().Split('|');
                    string[] arrstrTypenm = dtRow1["F_CAUSENM"].ToString().Split('|');
                    int idx = 0;
                    foreach (string strTyepcnt in arrstrTypecnt)
                    {

                        if (dtTemp.Columns.IndexOf(arrstrTypenm[idx] + "(원인)") < 0)
                            dtNewRow[arrstrTypenm[idx]] = string.Format("{0:n0}", strTyepcnt);
                        else
                            dtNewRow[arrstrTypenm[idx] + "(원인)"] = string.Format("{0:n0}", strTyepcnt);
                        idx++;
                    }
                }
                dtTemp.Rows.Add(dtNewRow);
            }
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "검사일자");

            devGrid.DataSource = dtPivotTable;
            devGrid.DataBind();

        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlComboBox_DataBound
        protected void DataBound(object sender, EventArgs e)
        {
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {

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

        #region devChart1_Drawing
        protected void devChart1_Drawing()
        {

            if (dtChart1 != null)
            {
                DataTable cdt = dtChart1.Copy();
                cdt.Rows.Remove(cdt.Rows[cdt.Rows.Count - 1]);

                devChart1.Series.Clear();

                if (rdoBAN.Value.ToString() == "RATE")
                    DevExpressLib.SetChartLineSeries(devChart1, "부적합률(%)", "F_WORKDATE", "F_NGRATE", System.Drawing.Color.Blue, 2);
                else
                    DevExpressLib.SetChartLineSeries(devChart1, "PPM", "F_WORKDATE", "F_PPM", System.Drawing.Color.Blue, 2);

                DevExpressLib.SetChartLineSeries(devChart1, "목표", "F_WORKDATE", "F_VALUE", System.Drawing.Color.Red, 2);

                devChart1.DataSource = cdt;
                devChart1.DataBind();
                
                DevExpressLib.SetCrosshairOptions(devChart1);

                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, null, null, null, "{V}");

                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                devChart1.Titles.Clear();
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                tlt1.Text = "월별 부적합률";
                tlt1.Font = font;
                devChart1.Titles.Add(tlt1);

            }


        }
        #endregion

        #region devChart2_Drawing
        protected void devChart2_Drawing()
        {
            if (dtChart1 != null)
            {
                DataTable cdt = dtChart1.Copy();
                cdt.Rows.Remove(cdt.Rows[cdt.Rows.Count - 1]);

                devChart2.Series.Clear();

                DevExpressLib.SetChartLineSeries(devChart2, "손실금액", "F_WORKDATE", "F_LOSSAMT", System.Drawing.Color.Green, 2);

                devChart2.DataSource = cdt;
                devChart2.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart2);
                DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, null, null, null);

                XYDiagram diagram = (XYDiagram)devChart2.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;


                devChart2.Titles.Clear();
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                tlt1.Text = "월별 손실금액";
                tlt1.Font = font;
                devChart2.Titles.Add(tlt1);
            }
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            string strJudge = e.GetValue("검사일자").ToString();

            if (strJudge == "부적합유형")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(0xDD, 0xDD, 0xDD);
            }
            else if (strJudge == "부적합원인")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(0xDD, 0xDD, 0xDD);
            }

        }
        #endregion

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD4002_LST();
            devGrid.DataBind();
        }
        #endregion

        #region devGrid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {

        }
        #endregion

        #region devChart1_CustomCallback
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart1_Drawing();
        }
        #endregion

        #region devChart2_CustomCallback
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart2_Drawing();
        }
        #endregion

        #endregion

        #region devGrid_HtmlDataCellPrepared
        protected void devGrid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName != "검사일자")
                e.Cell.HorizontalAlign = HorizontalAlign.Right;
            else if (e.DataColumn.FieldName == "검사일자")
                e.Cell.HorizontalAlign = HorizontalAlign.Left;
        }
        #endregion


    }
}