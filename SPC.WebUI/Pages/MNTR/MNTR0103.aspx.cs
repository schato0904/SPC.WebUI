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
    public partial class MNTR0103 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataSet dsGrid1
        {
            get
            {
                return (DataSet)Session["MNTR0103_1"];
            }
            set
            {
                Session["MNTR0103_1"] = value;
            }
        }
        DataSet dsGrid2
        {
            get
            {
                return (DataSet)Session["MNTR0103_2"];
            }
            set
            {
                Session["MNTR0103_2"] = value;
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

            new ASPxGridViewCellMerger(devGrid, "F_COMPNM");
            new ASPxGridViewCellMerger(devGridWorst, "F_COMPNM");

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

        #region 모니터링 > 반별 생산수
        public DataSet MONITORING_PROCESS_ANALISYS(out string errMsg)
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
                ds = biz.MONITORING_PROCESS_ANALISYS(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모니터링 > 반별 생산수 : 업체 품목, 공정별 WORST3
        public DataSet MONITORING_PROCESS_ANALISYS_WORST3(out string errMsg)
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
                ds = biz.MONITORING_PROCESS_ANALISYS_WORST3(oParamDic, out errMsg);
            }

            return ds;
        }
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

            ds = MONITORING_PROCESS_ANALISYS(out errMsg);

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
                    dsGrid1 = ds;
                    devGrid.DataSource = ds;
                    devGrid.DataBind();

                    ds = MONITORING_PROCESS_ANALISYS_WORST3(out errMsg);

                    if (!String.IsNullOrEmpty(errMsg))
                    {
                        // Grid Callback Init
                        devCallbackPanel.JSProperties["cpResultCode"] = "0";
                        devCallbackPanel.JSProperties["cpResultMsg"] = errMsg;
                    }
                    else
                    {
                        if (!bExistsDataSetWhitoutCount(ds))
                        {
                            // Grid Callback Init
                            devCallbackPanel.JSProperties["cpResultCode"] = "0";
                            devCallbackPanel.JSProperties["cpResultMsg"] = "데이타 조회 중 장애가 발생하였습니다.\\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                        }
                        else
                        {
                            dsGrid2 = ds;
                            devGridWorst.DataSource = ds;
                            devGridWorst.DataBind();
                        }
                    }
                }
            }
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            if (!e.Column.FieldName.Equals("F_GOODRATE")) return;

            e.DisplayText = String.Format("{0:n2}%", e.Value);
        }
        #endregion

        #region devGridWorst_CustomColumnDisplayText
        /// <summary>
        /// devGridWorst_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGridWorst_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            if (!e.Column.FieldName.Equals("F_REJRATE")) return;

            e.DisplayText = String.Format("{0:n2}%", e.Value);
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
            devGrid.DataSource = dsGrid1;
            devGrid.DataBind();
            devGridWorst.DataSource = dsGrid2;
            devGridWorst.DataBind();

            //this.ExcelFileDownLoad();
            PrintingSystemBase ps = new PrintingSystemBase();
            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
            link1.Component = devGridExporter;
            PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
            link2.Component = devGridExporter2;

            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link1, link2});

            compositeLink.CreatePageForEachLink();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                string file_name = String.Format("[{0}]{1} 공정능력 Worst 정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM);
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