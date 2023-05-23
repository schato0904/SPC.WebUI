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
using DevExpress.Web;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0206 : WebUIBasePage
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
                return (DataTable)Session["ANLS0206_1"];
            }
            set
            {
                Session["ANLS0206_1"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["ANLS0206_2"];
            }
            set
            {
                Session["ANLS0206_2"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["ANLS0206_3"];
            }
            set
            {
                Session["ANLS0206_3"] = value;
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
        void SetGrid(DataTable dt1, DataTable dt2, out DataTable dtTemp)
        {
            Int32 nCols = 0, nIndex = 0;

            // 데이타 구성용 DataTable
            dtTemp = new DataTable();
            dtTemp.Columns.Add("구분", typeof(String));
            foreach (DataRow dtRow in dt1.Rows)
            {
                dtTemp.Columns.Add(dtRow["F_FOURNM"].ToString(), typeof(Int32));
            }

            nCols = dtTemp.Columns.Count - 1;
            DataRow dtNewRow = null;

            foreach (DataRow dtRow in dt2.Rows)
            {
                if (nIndex == 0)
                {
                    dtNewRow = dtTemp.NewRow();
                    dtNewRow["구분"] = dtRow["F_WORKYM"].ToString();
                    dtNewRow[dtRow["F_FOURNM"].ToString()] = Convert.ToInt32(dtRow["F_CNT"]);
                }
                else
                {
                    if (nIndex % nCols > 0)
                        dtNewRow[dtRow["F_FOURNM"].ToString()] = Convert.ToInt32(dtRow["F_CNT"]);
                    else
                    {
                        dtTemp.Rows.Add(dtNewRow);
                        dtNewRow = dtTemp.NewRow();
                        dtNewRow["구분"] = dtRow["F_WORKYM"].ToString();
                        dtNewRow[dtRow["F_FOURNM"].ToString()] = Convert.ToInt32(dtRow["F_CNT"]);
                    }
                }

                nIndex++;
            }

            if (nCols % (nIndex - 1) > 0)
                dtTemp.Rows.Add(dtNewRow);

            // Pivot Fill
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "구분");

            dtPivotTable.Columns.Add("색깔", typeof(String));
            dtPivotTable.Columns.Add("합계", typeof(Int32));
            dtPivotTable.Columns.Add("점유율(%)", typeof(String));

            foreach (DataRow dtRow in dtPivotTable.Rows)
            {
                DataRow[] dtRows = dt1.Select(String.Format("F_FOURNM='{0}'", dtRow["구분"]));
                dtRow["색깔"] = dtRows[0][2].ToString();
                dtRow["합계"] = Convert.ToInt32(dtRows[0][3]);
                if (dtRows[0][4].ToString().Length > 0)
                {
                    dtRow["점유율(%)"] = Convert.ToDecimal(dtRows[0][4]).ToString("#,##0.#0");
                }
                else {
                    dtRow["점유율(%)"] = 0;
                }
                
            }

            foreach (DataColumn column in dtPivotTable.Columns)
            {
                GridViewDataColumn col = new GridViewDataColumn(column.ColumnName);
                col.Visible = !column.ColumnName.Equals("색깔");
                col.Width = column.ColumnName.Equals("구분") ? 120 : 100;
                devGrid.Columns.Add(col);
            }

            devGrid.DataSource = dtPivotTable;
            devGrid.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

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

        #region devChart1_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.XtraCharts.Web.CustomCallbackEventArgs</param>
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart1 != null && dtChart2 != null)
            {
                devChart1.Series.Clear();

                foreach (DataRow dtRow in dtChart1.Rows)
                {
                    DevExpressLib.SetChartLineSeries(devChart1, dtRow["F_FOURNM"].ToString(), "구분", dtRow["F_FOURNM"].ToString(), System.Drawing.ColorTranslator.FromHtml(dtRow["F_COLOR2"].ToString()));
                }

                devChart1.DataSource = dtChart2;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, 0, 0, null, "{V}");
            }
        }
        #endregion

        #region devChart2_CustomCallback
        /// <summary>
        /// devChart2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.XtraCharts.Web.CustomCallbackEventArgs</param>
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart1 != null && dtChart3 != null)
            {
                devChart2.Series.Clear();

                DevExpressLib.SetChartPieSeries(devChart2, "점유율", "F_FOURNM", "F_RATIO", "{A} : {V:n2}");
                (devChart2.Series[0].View as PieSeriesView).SweepDirection = PieSweepDirection.Clockwise;
                (devChart2.Series[0].View as PieSeriesView).Rotation = 270;

                devChart2.DataSource = dtChart3;
                devChart2.DataBind();

                DevExpressLib.SetChartLegend(devChart2);
            }
        }
        #endregion

        #region devChart2_CustomDrawSeriesPoint
        /// <summary>
        /// devChart2_CustomDrawSeriesPoint
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e"></param>
        protected void devChart2_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            PieDrawOptions options = (PieDrawOptions)e.SeriesDrawOptions;
            GradientFillOptionsBase gradientOptions = ((GradientFillOptionsBase)options.FillStyle.Options);
            options.FillStyle.FillMode = FillMode.Gradient;
            options.Color = System.Drawing.ColorTranslator.FromHtml(dtChart1.Select(String.Format("F_FOURNM='{0}'", e.SeriesPoint.Argument))[0]["F_COLOR2"].ToString());
            gradientOptions.Color2 = System.Drawing.ColorTranslator.FromHtml(dtChart1.Select(String.Format("F_FOURNM='{0}'", e.SeriesPoint.Argument))[0]["F_COLOR2"].ToString());
        }
        #endregion

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dtOut = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_FROMDT", GetFromDt());
            oParamDic.Add("F_TODT", GetToDt());
            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            oParamDic.Add("F_BANCD", GetBanCD());
            oParamDic.Add("F_LINECD", GetLineCD());

            // 검사규격을 구한다
            using (ANLSBiz biz = new ANLSBiz())
            {
                ds = biz.ANLS0206_LST(oParamDic, out errMsg);
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
                    // 항목테이블
                    dt1 = ds.Tables[1];

                    // 변경이력테이블
                    dt2 = ds.Tables[2];

                    // 파이테이블
                    dt3 = ds.Tables[3];

                    SetGrid(dt1, dt2, out dtOut);

                    if (dt1.Rows.Count > 0)
                    {
                        dtChart1 = dt1.Copy();
                    }
                    else
                        dtChart1 = null;

                    if (dtOut.Rows.Count > 0)
                    {
                        dtChart2 = dtOut.Copy();
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
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (!e.DataColumn.FieldName.Equals("구분")) return;
            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml(devGrid.GetRowValues(e.VisibleIndex, "색깔").ToString());
        }
        #endregion

        #endregion
    }
}