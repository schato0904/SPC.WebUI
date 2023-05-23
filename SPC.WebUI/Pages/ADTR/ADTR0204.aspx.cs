using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.ADTR.Biz;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0204 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string pact, ban, line;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        #endregion

        #region 프로퍼티
        DataSet dsGrid1
        {
            get
            {
                return (DataSet)Session["ADTR0204"];
            }
            set
            {
                Session["ADTR0204"] = value;
            }
        }
        DataTable dtGrid1
        {
            get
            {
                return (DataTable)Session["ADTR0204_1"];
            }
            set
            {
                Session["ADTR0204_1"] = value;
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

            new ASPxGridViewCellMerger(devGrid, "F_FACTNM|F_BANNM");

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
                devGridDetail.JSProperties["cpResultCode"] = "";
                devGridDetail.JSProperties["cpResultMsg"] = "";

                ucPager.TotalItems = 0;
                ucPager.PagerDataBind();
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

        #region 공정이상미조치현황 목록
        void ADTR0204_LST()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_WORKSTDT", GetFromDt());
                oParamDic.Add("F_WORKEDDT", GetToDt());
                oParamDic.Add("F_WORKTYPE", GetCommonCode());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.ADTR0204_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;
            dsGrid1 = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid.DataBind();
            }
        }
        #endregion

        #region 상세내역 최대 시료수
        Int32 ADTR0204_SIRYO_MAX(string F_FACTCD, string F_BANCD, string F_LINECD, int nPageSize, int nCurrPage)
        {
            Int32 maxCnt = 0;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", F_FACTCD);
                oParamDic.Add("F_BANCD", F_BANCD);
                oParamDic.Add("F_LINECD", F_LINECD);
                oParamDic.Add("F_WORKSTDT", GetFromDt());
                oParamDic.Add("F_WORKEDDT", GetToDt());
                oParamDic.Add("F_WORKTYPE", GetCommonCode());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);
                maxCnt = biz.ADTR0204_SIRYO_MAX(oParamDic);
            }

            return maxCnt;
        }
        #endregion

        #region 상세내역 갯수
        Int32 ADTR0204_DETAIL_CNT(string F_FACTCD, string F_BANCD, string F_LINECD)
        {
            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", F_FACTCD);
                oParamDic.Add("F_BANCD", F_BANCD);
                oParamDic.Add("F_LINECD", F_LINECD);
                oParamDic.Add("F_WORKSTDT", GetFromDt());
                oParamDic.Add("F_WORKEDDT", GetToDt());
                oParamDic.Add("F_WORKTYPE", GetCommonCode());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);
                totalCnt = biz.ADTR0204_DETAIL_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region 상세내역 목록
        void ADTR0204_DETAIL_LST(string F_FACTCD, string F_BANCD, string F_LINECD, int nPageSize, int nCurrPage, bool bCallback)
        {
            totalCnt = ADTR0204_DETAIL_CNT(F_FACTCD, F_BANCD, F_LINECD);

            if (totalCnt > 0)
            {
                string errMsg = String.Empty;

                Int32 nMaxSiryo = ADTR0204_SIRYO_MAX(F_FACTCD, F_BANCD, F_LINECD, nPageSize, nCurrPage);

                using (ADTRBiz biz = new ADTRBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                    oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", F_FACTCD);
                    oParamDic.Add("F_BANCD", F_BANCD);
                    oParamDic.Add("F_LINECD", F_LINECD);
                    oParamDic.Add("F_WORKSTDT", GetFromDt());
                    oParamDic.Add("F_WORKEDDT", GetToDt());
                    oParamDic.Add("F_WORKTYPE", GetCommonCode());
                    oParamDic.Add("F_LANGTYPE", gsLANGTP);
                    ds = biz.ADTR0204_DETAIL_LST(oParamDic, out errMsg);
                }

                DataTable dtTable = new DataTable();
                dtTable.Columns.Add("F_WORKDATE", typeof(String));
                dtTable.Columns.Add("F_ITEMCD", typeof(String));
                dtTable.Columns.Add("F_ITEMNM", typeof(String));
                dtTable.Columns.Add("F_WORKCD", typeof(String));
                dtTable.Columns.Add("F_WORKNM", typeof(String));
                dtTable.Columns.Add("F_QTYCD", typeof(String));
                dtTable.Columns.Add("F_MEASNO", typeof(String));
                dtTable.Columns.Add("F_TSERIALNO", typeof(String));
                dtTable.Columns.Add("F_INSPDETAIL", typeof(String));
                dtTable.Columns.Add("F_STANDARD", typeof(String));
                dtTable.Columns.Add("F_MAX", typeof(String));
                dtTable.Columns.Add("F_MIN", typeof(String));
                dtTable.Columns.Add("F_INSPCD", typeof(String));
                dtTable.Columns.Add("F_SERIALNO", typeof(String));
                for (int i = 0; i < nMaxSiryo; i++)
                {
                    dtTable.Columns.Add(String.Format("F_MEASURE_{0}", i + 1), typeof(String));
                }

                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    bool bExistsPrev = dtTable.Select(String.Format("F_MEASNO='{0}'", dtRow["F_MEASNO"])).Length > 0;

                    if (!bExistsPrev)
                    {
                        DataRow dtNewRow = dtTable.NewRow();
                        dtNewRow["F_WORKDATE"] = dtRow["F_WORKDATE"].ToString();
                        dtNewRow["F_ITEMCD"] = dtRow["F_ITEMCD"].ToString();
                        dtNewRow["F_ITEMNM"] = dtRow["F_ITEMNM"].ToString();
                        dtNewRow["F_WORKCD"] = dtRow["F_WORKCD"].ToString();
                        dtNewRow["F_WORKNM"] = dtRow["F_WORKNM"].ToString();
                        dtNewRow["F_QTYCD"] = dtRow["F_QTYCD"].ToString();
                        dtNewRow["F_MEASNO"] = dtRow["F_MEASNO"].ToString();
                        dtNewRow["F_TSERIALNO"] = dtRow["F_TSERIALNO"].ToString();
                        dtNewRow["F_INSPDETAIL"] = dtRow["F_INSPDETAIL"].ToString();
                        dtNewRow["F_STANDARD"] = dtRow["F_STANDARD"].ToString();
                        dtNewRow["F_MAX"] = dtRow["F_MAX"].ToString();
                        dtNewRow["F_MIN"] = dtRow["F_MIN"].ToString();
                        dtNewRow["F_INSPCD"] = dtRow["F_INSPCD"].ToString();
                        dtNewRow["F_SERIALNO"] = dtRow["F_SERIALNO"].ToString();
                        dtNewRow[String.Format("F_MEASURE_{0}", dtRow["F_NUMBER"])] = dtRow["F_MEASURE"].ToString();

                        dtTable.Rows.Add(dtNewRow);
                    }
                    else
                    {
                        foreach (DataRow dtUserRow in dtTable.Select(String.Format("F_MEASNO='{0}'", dtRow["F_MEASNO"])))
                        {
                            dtUserRow[String.Format("F_MEASURE_{0}", dtRow["F_NUMBER"])] = dtRow["F_MEASURE"].ToString();
                        }
                    }
                }

                // 동적으로 컬럼을 생성한다
                CreateColumns(nMaxSiryo);

                dtGrid1 = dtTable;
                devGridDetail.DataSource = dtTable;
                devGridDetail.DataBind();

                if (!String.IsNullOrEmpty(errMsg))
                {
                    // Grid Callback Init
                    devGridDetail.JSProperties["cpResultCode"] = "0";
                    devGridDetail.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bCallback)
                    {
                        // Pager Setting
                        ucPager.TotalItems = 0;
                        ucPager.PagerDataBind();
                    }
                    else
                    {
                        devGridDetail.JSProperties["cpResultCode"] = "pager";
                        devGridDetail.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, totalCnt);
                    }
                }
            }
            else
            {
                ucPager.TotalItems = 0;
                ucPager.PagerDataBind();

                devGridDetail.DataSource = null;
                devGridDetail.DataBind();
            }
        }
        #endregion

        #region 동적으로 컬럼을 생성한다
        void CreateColumns(Int32 nMaxSiryo)
        {
            for (int i = 0; i < nMaxSiryo; i++)
            {
                DevExpress.Web.GridViewBandColumn band = devGridDetail.Columns["F_MEASURE"] as DevExpress.Web.GridViewBandColumn;
                DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = String.Format("F_MEASURE_{0}", i + 1), Caption = String.Format("측정값{0}", i + 1), Width = Unit.Parse("100") };
                column.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                band.Columns.Add(column);
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 공정이상미조치현황 목록
            ADTR0204_LST();
        }
        #endregion

        #region devGridDetail CustomCallback
        /// <summary>
        /// devGridDetail_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridDetail_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;
            string[] oParams = null;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                oParams = new string[2];
                foreach (string oParam in e.Parameters.Split(';'))
                {
                    oParams = oParam.Split('=');
                    if (oParams[0].Equals("PAGESIZE"))
                        nPageSize = Convert.ToInt32(oParams[1]);
                    else if (oParams[0].Equals("CURRPAGE"))
                        nCurrPage = Convert.ToInt32(oParams[1]);
                }
            }
            else
            {
                nCurrPage = 1;
                nPageSize = ucPager.GetPageSize();
            }

            oParams = hidGridParentKey.Text.Split('|');

            // 상세내역 목록
            ADTR0204_DETAIL_LST(oParams[0], oParams[1], oParams[2], nPageSize, nCurrPage, true);
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
            else if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_UNIT") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.TextValue = e.Text;
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
            //QWKWRONGREPORTGUN_ADTR0201_EXCEL();
            //devGrid2.DataBind();
            //devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 공정이상정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
            devGridDetail.DataSource = dtGrid1;
            devGridDetail.DataBind();
            devGrid.DataSource = dsGrid1;
            devGrid.DataBind();
            

            //this.ExcelFileDownLoad();
            PrintingSystemBase ps = new PrintingSystemBase();
            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
            link1.Component = devGridExporter;
            PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
            link2.Component = devGridExporter2;

            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link1, link2 });

            compositeLink.CreatePageForEachLink();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                string file_name = String.Format("[{0}]{1} 공정이상미조치현황정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM);
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
        }
        #endregion

        #endregion
    }
}