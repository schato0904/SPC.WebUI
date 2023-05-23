using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.MNTR.Biz;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace SPC.WebUI.Pages.MNTR
{
    public partial class MNTR0102 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataTable dtGrid1
        {
            get
            {
                return (DataTable)Session["MNTR0102_1"];
            }
            set
            {
                Session["MNTR0102_1"] = value;
            }
        }
        DataTable dtGrid2
        {
            get
            {
                return (DataTable)Session["MNTR0102_2"];
            }
            set
            {
                Session["MNTR0102_2"] = value;
            }
        }
        DataTable dtGrid3
        {
            get
            {
                return (DataTable)Session["MNTR0102_3"];
            }
            set
            {
                Session["MNTR0102_3"] = value;
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

        #endregion

        #region 사용자 정의 함수

        #region 모니터링 > 공정능력
        public DataSet MONITORING_PROCESS_CAPABILITY(out string errMsg)
        {
            using (MNTRBiz biz = new MNTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("S_COMPCD", gsCOMPCD);
                oParamDic.Add("S_FACTCD", gsFACTCD);
                oParamDic.Add("T_COMPCD", GetCompCD());
                oParamDic.Add("T_FACTCD", GetFactCD());
                oParamDic.Add("F_DATE", GetFromDt());
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.MONITORING_PROCESS_CAPABILITY(oParamDic, out errMsg);
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
        //void SetProcessCapabilityCnt(string t, Int32[] nCount, Decimal val)
        //{
        //    switch (t)
        //    {
        //        case "CP":
        //            if (val >= Convert.ToDecimal(1.67))
        //                nCount[0]++;
        //            else if (Convert.ToDecimal(1.67) > val && val >= Convert.ToDecimal(1.33))
        //                nCount[1]++;
        //            else if (Convert.ToDecimal(1.33) > val && val >= Convert.ToDecimal(1.00))
        //                nCount[2]++;
        //            else if (Convert.ToDecimal(1.00) > val)
        //                nCount[3]++;
        //            break;
        //        case "CPK":
        //            if (val >= Convert.ToDecimal(1.67))
        //                nCount[4]++;
        //            else if (Convert.ToDecimal(1.67) > val && val >= Convert.ToDecimal(1.33))
        //                nCount[5]++;
        //            else if (Convert.ToDecimal(1.33) > val && val >= Convert.ToDecimal(1.00))
        //                nCount[6]++;
        //            else if (Convert.ToDecimal(1.00) > val)
        //                nCount[7]++;
        //            break;
        //    }
        //}
        #endregion

        #endregion

        #region 사용자이벤트

        #region devCallbackPanel_Callback
        /// <summary>
        /// devCallbackPanel_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string errMsg = String.Empty;

            ds = MONITORING_PROCESS_CAPABILITY(out errMsg);

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devCallbackPanel.JSProperties["cpResultCode"] = "0";
                devCallbackPanel.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    // Grid Callback Init
                    devCallbackPanel.JSProperties["cpResultCode"] = "0";
                    devCallbackPanel.JSProperties["cpResultMsg"] = "데이타 조회 중 장애가 발생하였습니다.\\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                }
                else
                {
                    Int32[] nCount = new Int32[8];
                    for (int i = 0; i < nCount.Length; i++) { nCount[i] = 0; }

                    DataTable dtPrevMonth = GetDataTable(new DataTable(), ds.Tables[0], ds.Tables[1]);
                    DataTable dtPrevWeek = GetDataTable(new DataTable(), ds.Tables[0], ds.Tables[2]);
                    DataTable dtCurrWeek = GetDataTable(new DataTable(), ds.Tables[0], ds.Tables[3]);

                    dtGrid1 = dtPrevMonth;
                    dtGrid2 = dtPrevWeek;
                    dtGrid3 = dtCurrWeek;

                    devGridPrevMonth.DataSource = dtPrevMonth;
                    devGridPrevMonth.DataBind();

                    devGridPrevWeek.DataSource = dtPrevWeek;
                    devGridPrevWeek.DataBind();

                    devGridCurrWeek.DataSource = dtCurrWeek;
                    devGridCurrWeek.DataBind();

                    // 그리드 헤더변경
                    DataRow drHeader = ds.Tables[4].Rows[0];

                    DevExpress.Web.GridViewBandColumn PrevMonthBand = devGridPrevMonth.Columns["F_CAPABILITY"] as DevExpress.Web.GridViewBandColumn;
                    PrevMonthBand.Caption = String.Format("전월({0}) 공정능력", drHeader[0]);
                    DevExpress.Web.GridViewBandColumn PrevWeekBand = devGridPrevWeek.Columns["F_CAPABILITY"] as DevExpress.Web.GridViewBandColumn;
                    PrevWeekBand.Caption = String.Format("전주({0}) 공정능력", drHeader[1]);
                    DevExpress.Web.GridViewBandColumn CurrWeekBand = devGridCurrWeek.Columns["F_CAPABILITY"] as DevExpress.Web.GridViewBandColumn;
                    CurrWeekBand.Caption = String.Format("선택주({0}) 공정능력", drHeader[2]);
                }
            }
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
            devGridPrevMonth.DataSource = dtGrid1; 
            devGridPrevMonth.DataBind();

            devGridPrevWeek.DataSource = dtGrid2;
            devGridPrevWeek.DataBind();

            devGridCurrWeek.DataSource = dtGrid3;
            devGridCurrWeek.DataBind();

            //this.ExcelFileDownLoad();
            PrintingSystemBase ps = new PrintingSystemBase();
            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
            link1.Component = devGridExporter;
            PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
            link2.Component = devGridExporter2;
            PrintableComponentLinkBase link3 = new PrintableComponentLinkBase(ps);
            link3.Component = devGridExporter3;

            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link1, link2,link3});

            compositeLink.CreatePageForEachLink();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                string file_name = String.Format("[{0}]{1} 공정능력조회(cp, cpk)정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM);
                XlsxExportOptions options = new XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                compositeLink.PrintingSystemBase.ExportToXlsx(stream, options);

                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", "application/xls");
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}.xlsx", HttpUtility.UrlEncode(file_name, new System.Text.UTF8Encoding()).Replace("+", "%20")));
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
            ps.Dispose();

            //devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 사용자별라인정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
            //devGridExporter2.WriteXlsToResponse(String.Format("[{0}]{1} 사용자별라인정보상세", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}