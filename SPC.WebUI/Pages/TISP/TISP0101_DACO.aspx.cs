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
using DevExpress.Web;

namespace SPC.WebUI.Pages.TISP
{
    public partial class TISP0101_DACO : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["TISP0101_DACO"];
            }
            set
            {
                Session["TISP0101_DACO"] = value;
            }
        }
        DataSet dsGrid1
        {
            get
            {
                return (DataSet)Session["TISP0101_DACO_1"];
            }
            set
            {
                Session["TISP0101_DACO_1"] = value;
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
            //if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            //{
            //    QWK08A_TISP0101_LST();
            //}
            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
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
        {
        }
        #endregion

        #endregion

        #region 사용자 정의 함수
        #region Data조회
        void QWK08A_TISP0101_LST()
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
                ds = biz.QWK08A_TISP0101_DACO_LST(oParamDic, out errMsg);
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

        #region Chart 조회
        void QWK08A_TISP0101_CHART(string[] Param)
        {
            string errMsg = String.Empty;

            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_DT", Param[7]);//GetFromDt());
                oParamDic.Add("F_ITEMCD", Param[3]);
                oParamDic.Add("F_WORKCD", Param[4]);
                oParamDic.Add("F_SERIALNO", Param[5]);
                ds = biz.QWK08A_TISP0101_CHART(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                dtChart1 = ds.Tables[0].Copy();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_ITEMNM", "F_WORKNM", "F_INSPDETAIL", "F_ITEMCD" };

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
                }
            }

            if (e.DataColumn.FieldName.Equals("F_OVER"))
            {
                e.Cell.ForeColor = System.Drawing.Color.Red;
            }
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
            QWK08A_TISP0101_LST();
        }
        #endregion

        #region devCallbackPanel1_Callback
        /// <summary>
        /// devCallbackPanel1_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');

            dtChart1 = null;
            QWK08A_TISP0101_CHART(oParams);

            ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
            ASPxImage image = (ASPxImage)panel.FindControl("devImage1");
            image.Width = new Unit(oParams[0]);
            image.Height = new Unit(oParams[1]);
            image.ImageUrl = Page.ResolveUrl(String.Format("~/API/Common/Chart/GetChartImage.ashx?tbl={0}&siryo={1}&calc={2}&type={3}&w={4}&h={5}&memb={6}&dummy={7}&db={8}",
                "TISP0101_DACO",
                oParams[6],
                "0",
                "xbar",
                oParams[0],
                oParams[1],
                "F_TSERIALNO",
                DateTime.Now.ToString("HH:mm:ss.fff"),
                "08"));
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
            devGrid.DataSource = dsGrid1;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비모니터링정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        protected void devGrid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            DataRow dtRow = devGrid.GetDataRow(e.VisibleRowIndex);

            if ((e.Column.FieldName.Equals("F_STANDARD")
                || e.Column.FieldName.Equals("F_MAX")
                || e.Column.FieldName.Equals("F_MIN")
                || e.Column.FieldName.Equals("F_UCLX")
                || e.Column.FieldName.Equals("F_LCLX")
                || e.Column.FieldName.Equals("F_UCLR"))
                && !String.IsNullOrEmpty(e.Value.ToString()))
            {
                string sFormat = "#,##0";
                if (dtRow["F_INSPCD"].ToString() == "AAC501")
                {

                    int nPoint = Convert.ToInt32(dtRow["F_FREEPOINT"]);

                    if (nPoint > 0)
                    {
                        sFormat += ".";
                        for (int i = 0; i < nPoint - 1; i++)
                        {
                            sFormat += "#";
                        }
                        sFormat += "0";
                    }

                    e.DisplayText = Convert.ToDecimal(e.Value).ToString(sFormat);
                }
            }
            else if (e.Column.FieldName.Equals("F_CNT")
                || e.Column.FieldName.Equals("F_NGCNT"))
            {
                e.DisplayText = String.Format("{0:#,##0}", e.Value);
            }
        }

        #endregion
    }
}