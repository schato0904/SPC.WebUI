using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.MNTR.Biz;

using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.MNTR
{
    public partial class MNTR0905 : WebUIBasePage
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
                return (DataTable)Session["MNTR090501"];
            }
            set
            {
                Session["MNTR090501"] = value;
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
                devChart1.JSProperties["cpResultCode"] = "";
                devChart1.JSProperties["cpResultMsg"] = "";
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

        #region 모니터링 > 공정능력
        public DataSet MNTR0903_LST(out string errMsg)
        {
            using (MNTRBiz biz = new MNTRBiz())
            {
                var dategbn = this.srcDATEGBNWEEK.Checked ? "WEEK" : (this.srcDATEGBNMONTH.Checked ? "MONTH" : "");
                var gbn = this.srcGBN1.Checked ? "1" : (this.srcGBN2.Checked ? "2" : "");
                var fromdt1 = GetFromDt1();
                fromdt1 = !string.IsNullOrEmpty(fromdt1) && fromdt1.Length >= 10 ? fromdt1.Substring(0, 10) : fromdt1;

                oParamDic.Clear();
                oParamDic.Add("S_COMPCD", gsCOMPCD);
                oParamDic.Add("S_FACTCD", gsFACTCD);
                oParamDic.Add("P_GBN", gbn);
                oParamDic.Add("P_DATEGBN", dategbn);
                oParamDic.Add("F_YY", GetWeekYear());
                oParamDic.Add("F_WEEK", GetWeekFrom());
                oParamDic.Add("F_YEARMONTH", GetFromDt().Substring(0, 7));
                oParamDic.Add("F_DATE", fromdt1 );
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.MNTR0903_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력 DataTable
        DataTable GetDataTable(DataTable dtTable, DataTable dtComp, DataTable dtSource)
        {
            dtTable.Columns.Add("F_COMPCD", typeof(String));
            dtTable.Columns.Add("F_COMPNM", typeof(String));
            dtTable.Columns.Add("F_CP_A", typeof(Int32));
            dtTable.Columns.Add("F_CP_B", typeof(Int32));
            dtTable.Columns.Add("F_CP_C", typeof(Int32));
            dtTable.Columns.Add("F_CP_D", typeof(Int32));
            dtTable.Columns.Add("F_CP_T", typeof(Int32));
            dtTable.Columns.Add("F_CPK_A", typeof(Int32));
            dtTable.Columns.Add("F_CPK_B", typeof(Int32));
            dtTable.Columns.Add("F_CPK_C", typeof(Int32));
            dtTable.Columns.Add("F_CPK_D", typeof(Int32));
            dtTable.Columns.Add("F_CPK_T", typeof(Int32));

            foreach (DataRow dr in dtComp.Rows)
            {
                DataRow dtTableNewRow = dtTable.NewRow();
                dtTableNewRow["F_COMPCD"] = dr["F_COMPCD"];
                dtTableNewRow["F_COMPNM"] = dr["F_COMPNM"];

                DataRow[] drSource = dtSource.Select(String.Format("F_COMPCD='{0}'", dr["F_COMPCD"]));
                if (drSource.Length > 0)
                {
                    foreach (DataRow dr1 in drSource)
                    {
                        dtTableNewRow["F_CP_A"] = dr1["F_CP_A"];
                        dtTableNewRow["F_CP_B"] = dr1["F_CP_B"];
                        dtTableNewRow["F_CP_C"] = dr1["F_CP_C"];
                        dtTableNewRow["F_CP_D"] = dr1["F_CP_D"];
                        dtTableNewRow["F_CP_T"] = Convert.ToInt16(dr1["F_CP_A"]) + Convert.ToInt16(dr1["F_CP_B"]) + Convert.ToInt16(dr1["F_CP_C"]) + Convert.ToInt16(dr1["F_CP_D"]);
                        dtTableNewRow["F_CPK_A"] = dr1["F_CPK_A"];
                        dtTableNewRow["F_CPK_B"] = dr1["F_CPK_B"];
                        dtTableNewRow["F_CPK_C"] = dr1["F_CPK_C"];
                        dtTableNewRow["F_CPK_D"] = dr1["F_CPK_D"];
                        dtTableNewRow["F_CPK_T"] = Convert.ToInt16(dr1["F_CPK_A"]) + Convert.ToInt16(dr1["F_CPK_B"]) + Convert.ToInt16(dr1["F_CPK_C"]) + Convert.ToInt16(dr1["F_CPK_D"]);
                    }
                }
                else
                {
                    dtTableNewRow["F_CP_A"] = 0;
                    dtTableNewRow["F_CP_B"] = 0;
                    dtTableNewRow["F_CP_C"] = 0;
                    dtTableNewRow["F_CP_D"] = 0;
                    dtTableNewRow["F_CP_T"] = 0;
                    dtTableNewRow["F_CPK_A"] = 0;
                    dtTableNewRow["F_CPK_B"] = 0;
                    dtTableNewRow["F_CPK_C"] = 0;
                    dtTableNewRow["F_CPK_D"] = 0;
                    dtTableNewRow["F_CPK_T"] = 0;
                }

                dtTable.Rows.Add(dtTableNewRow);
            }

            return dtTable;
        }
        #endregion

        #region 공정능력
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_Callback
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;

            ds = MNTR0903_LST(out errMsg);

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    // Grid Callback Init
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "데이타 조회 중 장애가 발생하였습니다.\\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                }
                else if (ds.Tables.Count < 2)
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "해당 조건에 맞는 데이터가 없습니다.";
                }
                else
                {
                    Int32[] nCount = new Int32[8];
                    for (int i = 0; i < nCount.Length; i++) { nCount[i] = 0; }

                    DataTable dt = GetDataTable(new DataTable(), ds.Tables[0], ds.Tables[1]);

                    dtChart1 = dt; // GetChartTable(dt);
                    //dtChart2 = ds.Tables[0];

                    devGrid.DataSource = dt;
                    devGrid.DataBind();
                }
            }
        }
        #endregion

        #region 차트 콜백
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            // 색상 팔레트 지정
            var palette = new DevExpress.XtraCharts.Palette("Cp_Cpk"
                , new DevExpress.XtraCharts.PaletteEntry[]{ 
                         new DevExpress.XtraCharts.PaletteEntry(System.Drawing.ColorTranslator.FromHtml("#1aae88"))
                        ,new DevExpress.XtraCharts.PaletteEntry(System.Drawing.ColorTranslator.FromHtml("#1ccacc"))
                        ,new DevExpress.XtraCharts.PaletteEntry(System.Drawing.ColorTranslator.FromHtml("#fcc633"))
                        ,new DevExpress.XtraCharts.PaletteEntry(System.Drawing.ColorTranslator.FromHtml("#e33244"))
                        }
                );
            devChart1.PaletteRepository.Add("Cp_Cpk", palette);
            devChart1.PaletteName = "Cp_Cpk";

            if (!string.IsNullOrEmpty(e.Parameter))
            {
                string[] oParams = e.Parameter.Split('|');
                devChart_ResizeTo(Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            }

            devChart1.Series.Clear();

            if (dtChart1 != null)
            {
                int pane_no = 0;
                int axis_no = 0;
                for (int i = 0; i < dtChart1.Rows.Count; i++)
                {
                    DataRow dr = dtChart1.Rows[i];
                    var name = "Cp_Cpk";
                    var gradeList = new List<string>() { "A", "B", "C", "D" };
                    var gradeNmList = new Dictionary<string, string>() { { "A", "우수" }, { "B", "양호" }, { "C", "보통" }, { "D", "미흡" } };
                    foreach (string grade in gradeList)
                    {
                        // 시리즈 포인트 값이 0일경우, 해당 시리즈는 마우스 오버 툴팁에서 제외하기 위함
                        int pCnt = Convert.ToInt32(dr[string.Format("F_CP_{0}", grade)].ToString()) + Convert.ToInt32(dr[string.Format("F_CPK_{0}", grade)].ToString()) ;
                        // 업체별 등급 단위로 시리즈를 나누어 각 포인트를 찍는다. 추후 pp, ppk추가시 포인트만 더 찍어주면 됨
                        DevExpress.XtraCharts.Series series = new Series(name, ViewType.FullStackedBar);
                        series.Points.Add(new SeriesPoint("Cp", dr[string.Format("F_CP_{0}", grade)]));
                        series.Points.Add(new SeriesPoint("Cpk", dr[string.Format("F_CPK_{0}", grade)]));
                        FullStackedBarSeriesView stackedview = new FullStackedBarSeriesView();
                        stackedview.FillStyle.FillMode = FillMode.Solid;
                        series.Label.TextPattern = "{V}";
                        series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series.View = stackedview;
                        series.CrosshairLabelPattern = string.Format("{0} : {{V}}({{VP:P2}})", gradeNmList[grade]);
                        // 시리즈 포인트 값이 모두 0일 경우, 마우스 오버 툴팁에서 제외
                        if ( pCnt == 0 ) 
                        {
                            series.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
                        }

                        devChart1.Series.Add(series);

                        // 업체단위로 Pane을 나눈다.
                        SecondaryAxisX secaxisx;
                        if (pane_no >= (devChart1.Diagram as XYDiagram).Panes.Count)
                        {
                            pane_no = (devChart1.Diagram as XYDiagram).Panes.Add(new XYDiagramPane(dr["F_COMPNM"].ToString()));
                            secaxisx = new SecondaryAxisX(dr["F_COMPNM"].ToString());
                            secaxisx.Thickness = (devChart1.Diagram as XYDiagram).AxisX.Thickness;

                            secaxisx.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                            secaxisx.Title.Text = dr["F_COMPNM"].ToString();
                            secaxisx.Title.Font = new System.Drawing.Font("dotum", 10f);
                            secaxisx.Alignment = AxisAlignment.Near;
                            axis_no = (devChart1.Diagram as XYDiagram).SecondaryAxesX.Add(secaxisx);
                        }

                        stackedview.Pane = (devChart1.Diagram as XYDiagram).Panes[pane_no];

                        var pane = stackedview.Pane;
                        (devChart1.Diagram as XYDiagram).AxisY.SetVisibilityInPane(false, pane);

                        secaxisx = (devChart1.Diagram as XYDiagram).SecondaryAxesX[axis_no];
                        stackedview.AxisX = secaxisx;
                    }

                    pane_no += 1;
                    axis_no += 1;
                }

                //(devChart1.Diagram as XYDiagram).EnableAxisXScrolling = true;
                (devChart1.Diagram as XYDiagram).PaneLayoutDirection = PaneLayoutDirection.Horizontal;
                // 각 데이터를 추가된 Pane으로 나누었기에, 기본 Pane 및 X축은 숨김 처리
                (devChart1.Diagram as XYDiagram).DefaultPane.Visible = false;
                (devChart1.Diagram as XYDiagram).AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            devChart1.DataSource = dtChart1;
            devChart1.DataBind();

            devChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }
        #endregion

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="Width">Int32</param>
        void devChart_ResizeTo(Int32 Width, Int32 Height)
        {
            devChart1.Width = new Unit(Width);
            devChart1.Height = new Unit(Height);

            this.devChart1.JSProperties["cpFunction"] = "resizeTo";
            this.devChart1.JSProperties["cpChartWidth"] = Width.ToString();
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
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
            //QWK04A_ADTR0103_LST();
            devGrid.DataSource = dtChart1;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 협력사공정능력정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}