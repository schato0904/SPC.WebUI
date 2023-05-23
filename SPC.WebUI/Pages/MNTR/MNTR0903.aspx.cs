using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.MNTR.Biz;

namespace SPC.WebUI.Pages.MNTR
{
    public partial class MNTR0903 : WebUIBasePage
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
                return (DataTable)Session["MNTR090301"];
            }
            set
            {
                Session["MNTR090301"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["MNTR090302"];
            }
            set
            {
                Session["MNTR090302"] = value;
            }
        }
        DataTable dtGrid
        {
            get
            {
                return (DataTable)Session["MNTR090303"];
            }
            set
            {
                Session["MNTR090303"] = value;
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
                devCallbackPanel.JSProperties["cpResultCode"] = "";
                devCallbackPanel.JSProperties["cpResultMsg"] = "";
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

        #region devCallbackPanel_Callback
        /// <summary>
        /// devCallbackPanel_Callback : 차트 그리기
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (oParams.Length > 2)
            {
                // F_COMPNM;F_CP_A;F_CP_B;F_CP_C;F_CP_D;F_CPK_A;F_CPK_B;F_CPK_C;F_CPK_D
                var dt = new DataTable();
                dt.Columns.Add("F_COMPNM", typeof(string));
                dt.Columns.Add("GRADE", typeof(string));
                dt.Columns.Add("COUNT", typeof(int));
                dt.Rows.Add(oParams[2], "우수", Convert.ToInt32(oParams[3]));
                dt.Rows.Add(oParams[2], "양호", Convert.ToInt32(oParams[4]));
                dt.Rows.Add(oParams[2], "보통", Convert.ToInt32(oParams[5]));
                dt.Rows.Add(oParams[2], "미흡", Convert.ToInt32(oParams[6]));

                devChart1.Series.Clear();

                DevExpressLib.SetChartPieSeries(devChart1, "Cp", "GRADE", "COUNT", "{A}: {VP:P2}");
                (devChart1.Series[0].View as DevExpress.XtraCharts.PieSeriesView).SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Clockwise;
                (devChart1.Series[0].View as DevExpress.XtraCharts.PieSeriesView).Rotation = 270;
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

                devChart1.DataSource = dt;
                
                // F_COMPNM;F_CP_A;F_CP_B;F_CP_C;F_CP_D;F_CPK_A;F_CPK_B;F_CPK_C;F_CPK_D
                var dt2 = new DataTable();
                dt2.Columns.Add("F_COMPNM", typeof(string));
                dt2.Columns.Add("GRADE", typeof(string));
                dt2.Columns.Add("COUNT", typeof(int));
                dt2.Rows.Add(oParams[2], "우수", Convert.ToInt32(oParams[7]));
                dt2.Rows.Add(oParams[2], "양호", Convert.ToInt32(oParams[8]));
                dt2.Rows.Add(oParams[2], "보통", Convert.ToInt32(oParams[9]));
                dt2.Rows.Add(oParams[2], "미흡", Convert.ToInt32(oParams[10]));

                devChart2.Series.Clear();

                DevExpressLib.SetChartPieSeries(devChart2, "Cpk", "GRADE", "COUNT", "{A}: {VP:P2}");
                (devChart2.Series[0].View as DevExpress.XtraCharts.PieSeriesView).SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Clockwise;
                (devChart2.Series[0].View as DevExpress.XtraCharts.PieSeriesView).Rotation = 270;
                devChart2.PaletteRepository.Add("Cp_Cpk", palette);
                devChart2.PaletteName = "Cp_Cpk";

                devChart2.DataSource = dt2;
            }

            devChart1.DataBind();
            devChart2.DataBind();

            DevExpressLib.SetChartLegend(devChart1);
            DevExpressLib.SetChartTitle(devChart1, "Cp");
            
            DevExpressLib.SetChartLegend(devChart2);
            DevExpressLib.SetChartTitle(devChart2, "Cpk");
        }
        #endregion

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

                    //dtChart1 = dt;
                    //dtChart2 = dt;

                    dtGrid = dt;
                    devGrid.DataSource = dt;
                    devGrid.DataBind();
                }
            }
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
            devChart2.Width = new Unit(Width);
            devChart2.Height = new Unit(Height);

            this.devCallbackPanel.JSProperties["cpFunction"] = "resizeTo";
            this.devCallbackPanel.JSProperties["cpChartHeight"] = Height.ToString();
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
            devGrid.DataSource = dtGrid;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 협력사별 공정능력정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion
        #endregion
    }
}