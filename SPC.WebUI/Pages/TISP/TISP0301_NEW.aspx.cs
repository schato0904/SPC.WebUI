using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.TISP.Biz;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace SPC.WebUI.Pages.TISP
{
    public partial class TISP0301_NEW : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        #endregion

        #region 프로퍼티
        DataSet dsGrid1
        {
            get
            {
                return (DataSet)Session["TISP0301_1"];
            }
            set
            {
                Session["TISP0301_1"] = value;
            }
        }
        DataSet dsGrid2
        {
            get
            {
                return (DataSet)Session["TISP0301_2"];
            }
            set
            {
                Session["TISP0301_2"] = value;
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

            new ASPxGridViewCellMerger(devGrid, "F_WORKNM|F_ITEMCD|F_ITEMNM");
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

                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";

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
        {
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

        #region Data조회
        void QWK08A_TISP0301_LST()
        {
            string errMsg = String.Empty;

            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_BANCD", GetBanCD());
                ds = biz.QWK08A_TISP0301_LST_NSUMDATE(oParamDic, out errMsg);
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

        #region Data 상세 조회
        Int32 QWK08A_TISP0301_DETAIL_CNT(string[] param)
        {
            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", param[0]);
                oParamDic.Add("F_WORKCD", param[1]);
                oParamDic.Add("F_SERIALNO", param[2]);

                totalCnt = biz.QWK08A_TISP0301_DETAIL_CNT(oParamDic);
            }

            return totalCnt;
        }

        void QWK08A_TISP0301_DETAIL_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            if (!String.IsNullOrEmpty(hidKeys.Text))
            {
                string[] param = hidKeys.Text.Split('|');

                Int32 nTotalCnt = QWK08A_TISP0301_DETAIL_CNT(param);

                using (TISPBiz biz = new TISPBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", GetCompCD());
                    oParamDic.Add("F_FACTCD", GetFactCD());
                    oParamDic.Add("F_FROMDT", GetFromDt());
                    oParamDic.Add("F_TODT", GetToDt());
                    oParamDic.Add("F_ITEMCD", param[0]);
                    oParamDic.Add("F_WORKCD", param[1]);
                    oParamDic.Add("F_SERIALNO", param[2]);
                    oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                    oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                    ds = biz.QWK08A_TISP0301_DETAIL_LST(oParamDic, out errMsg);
                }

                devGrid2.DataSource = ds;
                dsGrid2 = ds;

                if (!String.IsNullOrEmpty(errMsg))
                {
                    // Grid Callback Init
                    devGrid2.JSProperties["cpResultCode"] = "0";
                    devGrid2.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bCallback)
                    {
                        // Pager Setting
                        ucPager.TotalItems = nTotalCnt;
                        ucPager.PagerDataBind();
                    }
                    else
                    {
                        devGrid2.JSProperties["cpResultCode"] = "pager";
                        devGrid2.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, nTotalCnt);
                    }
                }
            }
            else
            {
                ds = null;
                devGrid2.DataSource = ds;
                dsGrid2 = ds;
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //e.NewValues["F_STATUS"] = true;
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.ClientSideEvents.Init = "fn_OnControlDisableBox";
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
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
            // Tooltip 출력
            string[] sTooltipFields = { "F_ITEMNM", "F_WORKNM", "F_ITEMCD" };

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
                }
            }

            if (e.DataColumn.FieldName.Equals("F_NGCNT"))
            {
                e.Cell.ForeColor = System.Drawing.Color.Red;
            }

            string colIdx = e.DataColumn.Index.ToString();
            string rowIdx = e.VisibleIndex.ToString();
            string rowcnt = devGrid.VisibleRowCount.ToString();

            e.Cell.Attributes.Add("onclick", String.Format("OnCellOver(this, '{0}', '{1}','{2}', devGrid);", colIdx, rowIdx, rowcnt));
            //e.Cell.Attributes.Add("onmouseout", "OnCellOut(this, '" + colIdx + "', '" + rowIdx + "','" + rowcnt + "');");  
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            QWK08A_TISP0301_LST();
        }
        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                string[] oParams = new string[2];
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

            QWK08A_TISP0301_DETAIL_LST(nPageSize, nCurrPage, true);
            devGrid2.DataBind();
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

            devGrid2.DataSource = dsGrid2;
            devGrid2.DataBind();

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
                string file_name = String.Format("[{0}]{1} 설비별 불량집계", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM);
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

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            //string strJudge = e.GetValue("F_NGOKCHK").ToString();

            //if (strJudge == "1")
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Red;
            //}
            //else if (strJudge == "2")
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Blue;
            //}
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("F_CNT")
                || e.Column.FieldName.Equals("F_NGCNT"))
            {
                e.DisplayText = String.Format("{0:#,##0}", e.Value);
            }
        }
        #endregion

        #region devGrid2_CustomColumnDisplayText
        protected void devGrid2_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("F_STANDARD")
               || e.Column.FieldName.Equals("F_MAX")
               || e.Column.FieldName.Equals("F_MIN")
               || e.Column.FieldName.Equals("F_MEASURE"))
            {
                e.DisplayText = String.Format("{0:#,##0}", Convert.ToDecimal(e.Value));
            }
        }
        #endregion

        #endregion

        
    }
}