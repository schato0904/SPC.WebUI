using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.IPCM.Biz;

namespace SPC.WebUI.Pages.IPCM
{
    public partial class IPCM0104 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
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
            if ((String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false")))
            {
                QWK100_NOPAGING_LST();
            }

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                devGrid.DataBind();
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

        #region 품질이상제기 목록을 구한다
        void QWK100_NOPAGING_LST()
        {
            string errMsg = String.Empty;

            using (IPCMBiz biz = new IPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_STDATE", GetFromDt());
                oParamDic.Add("F_EDDATE", GetToDt());
                if (!gsVENDOR)
                {
                    oParamDic.Add("F_COMPCD", GetCompCD());
                    oParamDic.Add("F_FACTCD", GetFactCD());
                }
                else
                {
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                }
                oParamDic.Add("F_RQCPCD", gsCOMPCD);
                oParamDic.Add("F_RQFTCD", gsFACTCD);
                oParamDic.Add("F_PROGRESS", "");
                oParamDic.Add("F_PROGRESSTP", ddlDATETP.SelectedItem.Value.ToString());
                oParamDic.Add("F_BVENDOR", !gsVENDOR ? "0" : "1");
                oParamDic.Add("F_PROGRESSST", ddlPROGRESS.SelectedItem.Value.ToString());
                if (!String.IsNullOrEmpty(ddlIMPROVE.SelectedItem.Value.ToString()))
                    oParamDic.Add("F_BIMPROVE", ddlIMPROVE.SelectedItem.Value.ToString());
                if (!String.IsNullOrEmpty(ddlCOMPLETE.SelectedItem.Value.ToString()))
                    oParamDic.Add("F_BCOMPLETE", ddlCOMPLETE.SelectedItem.Value.ToString());
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_LANGTP", gsLANGTP);

                ds = biz.QWK100_NOPAGING_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

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

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            QWK100_NOPAGING_LST();
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
            if (e.DataColumn.FieldName.Equals("F_RQRCDT"))
            {
                if (e.CellValue != null)
                {
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
                    string sRSDATE = grid.GetRowValues(e.VisibleIndex, "F_RSDATE").ToString();
                    if (String.IsNullOrEmpty(sRSDATE))
                        sRSDATE = DateTime.Today.ToString("yyyy-MM-dd");

                    //int nDateDiff = UF.Date.DateDiff(e.CellValue.ToString(), sRSDATE);
                    long nDateDiff = DateTime.Parse(e.CellValue.ToString()).Ticks - DateTime.Parse(sRSDATE).Ticks;
                    nDateDiff = nDateDiff / 10000000 / 60 / 60 / 24 + 1;

                    if (nDateDiff <= 0)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                        e.Cell.Font.Bold = true;
                    }
                    else if (nDateDiff <= 7)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Blue;
                        e.Cell.Font.Bold = true;
                    }
                }
            }
            else if (e.DataColumn.FieldName.Equals("F_BIMPROVE"))
            {
                if (e.CellValue != null)
                {
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
                    string sPROGRESS = grid.GetRowValues(e.VisibleIndex, "F_PROGRESS").ToString();

                    if (sPROGRESS.Equals("3"))
                    {
                        if (!Convert.ToBoolean(e.CellValue))
                        {
                            e.Cell.Text = "미개선";
                            e.Cell.ForeColor = System.Drawing.Color.Red;
                            e.Cell.Font.Bold = true;
                        }
                        else
                        {
                            e.Cell.Text = "개선";
                            e.Cell.ForeColor = System.Drawing.Color.Blue;
                            e.Cell.Font.Bold = true;
                        }
                    }
                    else
                        e.Cell.Text = "미처리";
                }
            }
            else if (e.DataColumn.FieldName.Equals("F_PROGRESS"))
            {
                if (e.CellValue != null)
                {
                    if (e.CellValue.ToString().Equals("1"))
                    {
                        e.Cell.Text = "이상제기";
                        e.Cell.ForeColor = System.Drawing.Color.SkyBlue;
                        e.Cell.Font.Bold = true;
                    }
                    else if (e.CellValue.ToString().Equals("2"))
                    {
                        e.Cell.Text = "대책처리";
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                        e.Cell.Font.Bold = true;
                    }
                    else
                    {
                        e.Cell.Text = "완료처리";
                        e.Cell.ForeColor = System.Drawing.Color.Blue;
                        e.Cell.Font.Bold = true;
                    }
                }
            }
            else
                return;
        }
        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_BIMPROVE")) return;
            if (e.VisibleRowIndex < 0) return;

            DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
            string sPROGRESS = grid.GetRowValues(e.VisibleRowIndex, "F_PROGRESS").ToString();

            if (sPROGRESS.Equals("3"))
            {
                if (!Convert.ToBoolean(e.Value))
                {
                    e.DisplayText = "미개선";
                }
                else
                {
                    e.DisplayText = "개선";
                }
            }
            else
                e.DisplayText = "미처리";
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
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_BIMPROVE") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"Unchecked", "미처리");
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 개선대책 진행현황정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}