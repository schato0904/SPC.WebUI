using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.LTRK.Biz;

namespace SPC.WebUI.Pages.LTRK.Popup
{
    public partial class LTRKINOUTPOP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sITEMCD = String.Empty;
        string sITEMNM = String.Empty;
        string sGROUP = String.Empty;
        decimal nCNT = 0;
        string sUNIT = String.Empty;
        #endregion

        #region 프로퍼티

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

            // 조회
            QPM13_LST();

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
            sITEMCD = Request.Params.Get("pITEMCD");
            srcF_ITEMCD.Text = sITEMCD;
            sITEMNM = Request.Params.Get("pITEMNM");
            srcF_ITEM.Text = String.Format("[{0}] {1}", sITEMCD, sITEMNM);
            sGROUP = Request.Params.Get("pGROUP");
            nCNT = Convert.ToDecimal(Request.Params.Get("pCNT"));
            srcF_REMAINCNT.Value = nCNT;
            sUNIT = Request.Params.Get("pUNIT");
            srcF_UNITNM.Text = sUNIT;
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

        #region 조회
        void QPM13_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", srcF_ITEMCD.Text);
                oParamDic.Add("F_INOUTTP", GetCommonCode());
                oParamDic.Add("F_ISMATERIAL", sGROUP);
                ds = biz.QPM13_LST(oParamDic, out errMsg);
            }

            DataTable dtRetrieve = new DataTable();

            if (bExistsDataSetWhitoutCount(ds))
            {
                dtRetrieve = ds.Tables[0].Copy();
                dtRetrieve.Columns.Add("F_RCNT", typeof(decimal));

                decimal dRemainCnt = Convert.ToDecimal(0);

                foreach (DataRow dr in dtRetrieve.Rows)
                {
                    switch (dr["F_INOUTTP"].ToString())
                    {
                        case "AAE701":
                            dRemainCnt += Convert.ToDecimal(dr["F_ICNT"]);
                            break;
                        case "AAE702":
                        case "AAE704":
                        case "AAE705":
                            dRemainCnt -= Convert.ToDecimal(dr["F_OCNT"]);
                            break;
                        case "AAE703":
                            dRemainCnt += Convert.ToDecimal(dr["F_BCNT"]);
                            break;
                    }

                    dr["F_RCNT"] = dRemainCnt;
                }

                devGrid.DataSource = dtRetrieve;
            }

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

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 조회
            QPM13_LST();
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
            if (e.Column.FieldName.Equals("F_INOUTTP"))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
            }
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName.Equals("F_ICNT"))
            {
                if (e.CellValue != DBNull.Value)
                {
                    decimal F_CNT = Convert.ToDecimal(e.CellValue);
                    decimal F_DANGA = Convert.ToDecimal(0);

                    if (F_CNT >= F_DANGA)
                        e.Cell.ForeColor = System.Drawing.Color.Blue;
                    else
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (e.DataColumn.FieldName.Equals("F_OCNT"))
            {
                if (e.CellValue != DBNull.Value)
                {
                    decimal F_CNT = Convert.ToDecimal(e.CellValue);
                    decimal F_DANGA = Convert.ToDecimal(0);

                    if (F_CNT >= F_DANGA)
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                    else
                        e.Cell.ForeColor = System.Drawing.Color.Blue;
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
            if (devGridDataColumn != null
                && devGridDataColumn.FieldName.Equals("F_INOUTTP")
                && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = GetCommonCodeText(e.TextValue.ToString());
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0} ~ {1}]{2} 자재수불현황", GetFromDt(), GetToDt(), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}